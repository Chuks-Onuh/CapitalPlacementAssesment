using ConsoleProject.Enums; 
using ConsoleProject.Models;
using ConsoleProject.Models.Stages;

namespace ConsoleProject.Dtos.ServiceRequestModels.Stage
{
    public class CreateVideoInterviewStage
    {
        public List<VideoInterviewQuestion> VideoInterviewQuestions { get; set; } = new List<VideoInterviewQuestion>();
    }

    public class CreateStage
    {
        public StageType StageType { get; set; }
        public string StageName { get; set; }
        public CreateVideoInterviewStage CreateVideoInterviewStage { get; set; }
    }
}
