﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace NZWalks.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public List<string> Roles { get; set; }

        //Navigation Property

        public List<User_Role> UserRoles { get; set; }
    }
}
