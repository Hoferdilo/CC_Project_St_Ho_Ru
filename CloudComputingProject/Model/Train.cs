using Azure;
using Azure.Data.Tables;

namespace CloudComputingProject.Model
{
    public class Train : ITableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[]? Image { get; set; }
        public string RowKey { get; set; }
        public string PartitionKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
