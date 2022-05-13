using Azure;

namespace CloudComputingProject.Model.Dto
{
    public class TrainDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? ETag { get; set; }
    }
}
