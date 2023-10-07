namespace ConsoleProject.Models.Questions
{
    public class YesOrNoQuestion
    {
        public bool Choice { get; set; }
        public bool DisqualifyForNoChoice { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
