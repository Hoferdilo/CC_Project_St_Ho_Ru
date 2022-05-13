using CloudComputingProject.Model.Dto;

namespace CloudComputingProject.Service
{
    public interface ITrainService
    {
        Task<TrainDto> AddTrain(TrainDto train);
        Task<TrainDto> UpdateTrain(TrainDto train);
        Task<TrainDto> DeleteTrain(TrainDto train);
        Task<ICollection<TrainDto>> GetTrains();
    }
}
