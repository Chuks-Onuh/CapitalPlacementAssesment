using ConsoleProject.Models;
using ConsoleProject.Models.Stages;

namespace ConsoleProject.Dtos.ServiceRequestModels.Stage
{
    public class UpdateVideoInterviewStage
    {
        public string StageName { get; set; }
        public List<VideoInterviewQuestion> VideoInterviewQuestions { get; set; } = new List<VideoInterviewQuestion>();
    }

    public class UpdateUsualStage
    {
        public string StageName { get; set; }
    }
}
