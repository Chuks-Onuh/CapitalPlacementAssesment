namespace ConsoleProject.Models.Profile
{
    public class EducationProfile : ProfileBase
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
    }
}
