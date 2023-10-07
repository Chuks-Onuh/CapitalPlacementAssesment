namespace ConsoleProject.Dtos.ServiceRequestModels.Question
{
    public class DropdownQuestionModel
    {
        public List<string> Choices { get; set; } = new List<string>();
        public bool EnableOtherOption { get; set; }
    }
}
