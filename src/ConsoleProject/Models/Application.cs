using ConsoleProject.Enums;
using ConsoleProject.Models.Profile;
using ConsoleProject.Models.Stages;
using System.ComponentModel.DataAnnotations;

namespace ConsoleProject.Models
{
    public class Application : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string ApplicationCoverImage { get; set; }

        public string Nationality { get; set; }

        public string CurrentResidence { get; set; }

        public string IdNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string StageId { get; set; }

        public ApplicationProgram Program { get; set; }

        public string ProgramId { get; set; }

        public Gender Gender { get; set; }

        public UserProfile Profile { get; set; }

        public Stage Stage { get; set; }
    }
}
