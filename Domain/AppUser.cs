using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        // below are additional properties for AppUser, the other properties were already defined in IdentityUser object
        public string DisplayName{get;set;}
        public string Bio {get;set;}
        public ICollection<ActivityAttendee> Activities{get;set;}
        public ICollection<Photo> Photos {get;set;} 
    }
}