using ConsoleProject.Models;

namespace ConsoleProject.Dtos.ServiceRequestModels.Program
{
    public class UpdateProgram
    {
        public List<string> Benefits { get; set; }
        public List<string> ApplicationCriteria { get; set; }
        public List<ProgramLocation> ProgramLocations { get; set; }
    }
}
