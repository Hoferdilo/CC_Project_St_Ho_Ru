namespace CloudComputingProject.Model
{
    public class Train
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[]? Image { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
