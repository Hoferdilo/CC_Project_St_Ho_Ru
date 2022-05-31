namespace CloudComputingProject.Model
{
    public class Station
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double xCord { get; set; }
        public double yCord { get; set; }
        public string AdministrativeArea { get; set; }
        public DateTime Created { get; set; }
    }
}
