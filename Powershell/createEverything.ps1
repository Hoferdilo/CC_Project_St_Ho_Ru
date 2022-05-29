#Functions
Function New-RandomString{Param ([Int]$Length = 10) return $(-join ((97..122) + (48..57) | Get-Random -Count $Length | ForEach-Object {[char]$_}))}
#Variables
#RG & Location
$rgName = "RG01"
$loc = "West Europe"
#Webservice
$nameOfWebService = 'IchMagZuegeWeb'
#Storage
$nameOfStorageAccount = 'ichmagzuegestorage1'
$nameOfTable = 'IchMagZuegeTable'
$nameOfQueue = 'ichmagzuegequeue'
$nameOfBlob = 'ichmagzuegeblob'
#Cosmos
$uniqueId = New-RandomString -Length 7
$cosmosDBAccount = "ichmagzuegecosmos-$uniqueId"
$apiKind = "SQL"
$locations = @("East US", "West US")
$databaseName = 'zuege'
$containerName1 = 'zuegecontainer1'
$containerName2 = 'zuegecontainer2'
$partitionKeyPath = "/myPartitionKey"
$throughput = 400
$consistencyLevel = "Session"
#Azure Function
$nameOfFunction1 = 'ichmagzuegefunction1'
$nameOfFunction2 = 'ichmagzuegefunction2'
#VM
$SubnetName = "ZuegeSubnet"
$SubnetRange = "192.168.1.0/24"
$VNetName = "ZuegerVMNetwork"
$VNetRange = "192.168.0.0/16"
$PublicIPName = "ZuegePublicIP"
$NSGName = "ZuegeNSG"
$NICName = "ZuegeNIC"
$ComputerName = "ZuegeSRV01"
$VMName = "ZuegeSRV01"
$VMSize = "Standard_DS2_v2"
$VMImage = "MicrosoftWindowsServer:WindowsServer:2022-datacenter-azure-edition:latest"
#Git
$gitRepo = "https://github.com/Hoferdilo/CC_Project_St_Ho.git"
#Connect
Connect-AzAccount

#Create RG
$rg = New-AzResourceGroup -Name $rgName -Location $loc
#Create Webservice + Plan
$plan = New-AzAppServicePlan -Name "$nameOfWebService-plan" -Location $loc -ResourceGroupName $rg.ResourceGroupName -Tier Free
New-AzWebApp -Name $nameOfWebService -Location $loc -AppServicePlan $plan.Name -ResourceGroupName $rg.ResourceGroupName
#Don´t Work because of GitHub Token
<#$PropertiesObject = @{
    repoUrl = "$gitrepo";
    branch = "master";
    isManualIntegration = "true";
}
#>
#Set-AzResource -Properties $PropertiesObject -ResourceGroupName $rg.ResourceGroupName -ResourceType Microsoft.Web/sites/sourcecontrols -ResourceName $nameOfWebService/Web -ApiVersion 2015-08-01 -Force

#Create Azure Storage
$storageAccount = New-AzStorageAccount -ResourceGroupName $rgName -Name $nameOfStorageAccount -Location $loc -SkuName Standard_LRS -Kind Storage
$ctx = $storageAccount.Context
New-AzStorageTable -Name $nameOfTable -Context $ctx
New-AzStorageQueue –Name $nameOfQueue -Context $ctx
New-AzStorageContainer -Name $nameOfBlob -Context $ctx -Permission Blob

#Create CosmosDB with 2 Containers
$cosmosDB = New-AzCosmosDBAccount -ResourceGroupName $rgName -Location $locations -Name $cosmosDBAccount -ApiKind $apiKind -DefaultConsistencyLevel $consistencyLevel -EnableAutomaticFailover:$true
$cosmosDatabase = New-AzCosmosDBSqlDatabase -ResourceGroupName $rgName -AccountName $cosmosDB.Name -Name $databaseName
New-AzCosmosDBSqlContainer -ResourceGroupName $rgName -AccountName $cosmosDB.Name -DatabaseName $cosmosDatabase.Name -Name $containerName1 -PartitionKeyKind Hash -PartitionKeyPath $partitionKeyPath -Throughput $throughput
New-AzCosmosDBSqlContainer -ResourceGroupName $rgName -AccountName $cosmosDB.Name -DatabaseName $cosmosDatabase.Name -Name $containerName2 -PartitionKeyKind Hash -PartitionKeyPath $partitionKeyPath -Throughput $throughput

#Create Azure Functions
New-AzFunctionApp -Name $nameOfFunction1 -ResourceGroupName $rgName -StorageAccount $storageAccount.StorageAccountName -Runtime dotnet -FunctionsVersion 3 -Location $loc
New-AzFunctionApp -Name $nameOfFunction2 -ResourceGroupName $rgName -StorageAccount $storageAccount.StorageAccountName -Runtime dotnet -FunctionsVersion 3 -Location $loc

#Create VM
$SubnetConfig = New-AzVirtualNetworkSubnetConfig -Name $SubnetName -AddressPrefix $SubnetRange
$VirtualNetwork = New-AzVirtualNetwork -ResourceGroupName $rgName -Location $loc -Name $VNetName -AddressPrefix $VNetRange -Subnet $SubnetConfig
$PublicIP = New-AzPublicIpAddress -ResourceGroupName $rgName -Location $loc -AllocationMethod "Dynamic" -Name $PublicIPName
$SecurityGroupRule = New-AzNetworkSecurityRuleConfig -Name "RDP-Rule" -Description "Allow RDP" -Access "Allow" -Protocol "TCP" -Direction "Inbound" -Priority 100 -DestinationPortRange 3389 -SourceAddressPrefix "*" -SourcePortRange "*" -DestinationAddressPrefix "*"
$NetworkSG = New-AzNetworkSecurityGroup -ResourceGroupName $rgName -Location $loc -Name $NSGName -SecurityRules $SecurityGroupRule
$NetworkInterface = New-AzNetworkInterface -Name $NICName -ResourceGroupName $rgName -Location $loc -SubnetId $VirtualNetwork.Subnets[0].Id -PublicIpAddressId $PublicIP.Id -NetworkSecurityGroupId $NetworkSG.Id
$Username = "myAdminUser"
$Password = 'ichmagZuege1!' | ConvertTo-SecureString -Force -AsPlainText
$Credential = New-Object -TypeName PSCredential -ArgumentList ($Username, $Password)
New-AzVm -ResourceGroupName $rgName -Name $VMName -Location $loc -VirtualNetworkName $VNetName -SubnetName $SubnetName -SecurityGroupName $SecurityGroupName -PublicIpAddressName $PublicIpAddressName -ImageName $VMImage  -Credential $Credential


