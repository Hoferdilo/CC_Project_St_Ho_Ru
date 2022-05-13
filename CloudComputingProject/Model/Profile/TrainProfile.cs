using CloudComputingProject.Model.Dto;

namespace CloudComputingProject.Model.Profile
{
    public class TrainProfile : AutoMapper.Profile
    {
        public TrainProfile()
        {
            CreateMap<TrainDto, Train>()
                .ForMember(x => x.CreatedDateTime, opt => opt.Ignore())
                .ForMember(x => x.ModifiedDateTime, opt => opt.Ignore());

            CreateMap<Train, TrainDto>();
        }
    }
}
