using Azure;
using CloudComputingProject.Model.Dto;
using Microsoft.JSInterop.Infrastructure;

namespace CloudComputingProject.Model.Profile
{
    public class TrainProfile : AutoMapper.Profile
    {
        public TrainProfile()
        {
            CreateMap<TrainDto, Train>()
                .ForMember(x => x.Timestamp, opt => opt.Ignore())
                .ForMember(x => x.RowKey, opt => opt.Ignore())
                .ForMember(x => x.PartitionKey, opt => opt.MapFrom(dto => dto.Type))
                .ForMember(x => x.ETag, opt => opt.MapFrom(dto => new ETag(dto.ETag)));

            CreateMap<Train, TrainDto>();
        }
    }
}
