using Dropbox.Api.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoApp.Models
{
    public class DropBoxAccount
    {       
            public string AccountId { get; protected set; }
            
            public Name Name { get; protected set; }
        
            public string Email { get; protected set; }
            
            public bool EmailVerified { get; protected set; }
            
            public bool Disabled { get; protected set; }
          
            public string ProfilePhotoUrl { get; protected set; }
    }
}