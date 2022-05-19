using CloudComputingProject.Model.Dto;

namespace CloudComputingProject.Service
{
    public interface ITrainService
    {
        Task<TrainDto> AddTrain(TrainDto train);
        Task<TrainDto> UpdateTrain(Guid id, TrainDto train);
        Task<TrainDto> DeleteTrain(Guid id, TrainDto train);
        Task<TrainPictureDto> AddTrainPicture(TrainPictureDto trainpicture);
        Task<ICollection<TrainDto>> GetTrains();
    }
}
