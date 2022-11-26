﻿using System.ComponentModel.DataAnnotations;

namespace Cresk.Models
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public User()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
