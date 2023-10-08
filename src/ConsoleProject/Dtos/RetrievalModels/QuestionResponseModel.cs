using ConsoleProject.Models;
using ConsoleProject.Models.Questions;

namespace ConsoleProject.Dtos.RetrievalModels
{
    public class QuestionResponseModel
    {
        public string Id { get; set; }
        public YesOrNoQuestion YesOrNoQuestion { get; set; }
        public ShortAnswerQuestion ShortAnswerQuestion { get; set; }
        public ParagraphQuestion ParagraphQuestion { get; set; }
        public string QuestionContent { get; set; }
        public MultipleChoiceQuestion MultipleChoiceQuestion { get; set; }
        public DropdownQuestion DropdownQuestion { get; set; }
        public DateQuestion DateQuestion { get; set; }
        public FileUploadQuestion FileUploadQuestion { get; set; }
        public NumberQuestion NumberQuestion { get; set; }
        public VideoBasedQuestion VideoBasedQuestion { get; set; }
        public string Response { get; set; }
    }
}
