using ConsoleProject.Enums;
using ConsoleProject.Models;
using ConsoleProject.Models.Stages;

namespace ConsoleProject.Dtos.RetrievalModels.Stages
{
    public class StageModel
    {
        public string StageName { get; set; }
        public StageType StageType { get; set; }
        public VideoInterviewQuestion VideoInterviewQuestion { get; set; }
    }
}
