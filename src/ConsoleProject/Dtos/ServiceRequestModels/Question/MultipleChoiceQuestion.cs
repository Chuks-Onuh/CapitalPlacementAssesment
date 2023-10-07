namespace ConsoleProject.Dtos.ServiceRequestModels.Question
{
    public class MultipleChoiceQuestionModel
    {
        public List<string> Options { get; set; } = new List<string>();
        public int MaximumNumberOfChoicesAllowed { get; set; }
        public bool EnableOtherOption { get; set; }
    }
}
