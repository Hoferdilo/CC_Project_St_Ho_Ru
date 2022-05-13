using System.Runtime.CompilerServices;
using AutoMapper;
using Azure;
using Azure.Data.Tables;
using CloudComputingProject.Model;
using CloudComputingProject.Model.Dto;

namespace CloudComputingProject.Service
{
    public class TrainService : ITrainService
    {
        private readonly TableClient _tableClient;
        private readonly IMapper _mapper;

        public TrainService(IMapper mapper)
        {
            _mapper = mapper;
            _tableClient = new TableClient("UseDevelopmentStorage=true", "train");
            _tableClient.CreateIfNotExists();
        }

        public async Task<TrainDto> AddTrain(TrainDto train)
        {
            var trainData = _mapper.Map<Train>(train);
            trainData.Id = Guid.NewGuid();
            trainData.PartitionKey = trainData.Type;
            trainData.RowKey = trainData.Id.ToString();
            trainData.Timestamp = DateTimeOffset.Now;
            trainData.ETag = new ETag(Guid.NewGuid().ToString());
            await _tableClient.AddEntityAsync(trainData);

            return _mapper.Map<TrainDto>(trainData);
        }

        public async Task<TrainDto> UpdateTrain(Guid id, TrainDto train)
        {
            var storedTrain = (await _tableClient.GetEntityAsync<Train>(train.Type, train.Id.ToString())).Value;
            if (!storedTrain.ETag.Equals(train.ETag))
            {
                throw new Exception($"Invalid ETag, please reload your data!");
            }

            storedTrain = _mapper.Map(train, storedTrain);
            storedTrain.ETag = new ETag(Guid.NewGuid().ToString());
            await _tableClient.UpdateEntityAsync(storedTrain, new ETag(train.ETag));
            return _mapper.Map<TrainDto>(storedTrain);
        }

        public async Task<TrainDto> DeleteTrain(Guid id, TrainDto train)
        {
            await _tableClient.DeleteEntityAsync(id.ToString(), train.Type);
            return train;
        }

        public async Task<ICollection<TrainDto>> GetTrains()
        {
            var enumerator = _tableClient.QueryAsync<Train>();
            var result = new List<TrainDto>();
            await foreach (var item in enumerator)
            {
                if (item != null)
                {
                    result.Add(_mapper.Map<TrainDto>(item));
                }
            }
            return result;
        }
    }
}
