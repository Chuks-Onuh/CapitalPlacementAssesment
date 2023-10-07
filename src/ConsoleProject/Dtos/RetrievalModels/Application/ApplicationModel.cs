using ConsoleProject.Enums; 
using ConsoleProject.Models;
using ConsoleProject.Models.Profile;

namespace ConsoleProject.Dtos.RetrievalModels.Application
{
    public class ApplicationModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string ApplicationCoverImage { get; set; }
        public string Nationality { get; set; }
        public string CurrentResidence { get; set; }
        public string IdNumber { get; set; }
        public Gender Gender { get; set; }
        public UserProfile Profile { get; set; }
    }
}
