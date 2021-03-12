using Dropbox.Api.Common;
using Dropbox.Api.Users;
using Dropbox.Api.UsersCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoApp.Models
{
    public class FullAccount : DropBoxAccount
    {
      
        public string Locale { get; protected set; }
        
        public string ReferralLink { get; protected set; }
       
        public bool IsPaired { get; protected set; }
        
        public AccountType AccountType { get; protected set; }
      
        public RootInfo RootInfo { get; protected set; }
        
        public string Country { get; protected set; }
        
        public FullTeam Team { get; protected set; }
        
        public string TeamMemberId { get; protected set; }
    }
}