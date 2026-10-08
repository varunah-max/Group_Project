using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClassDefinition.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool IsOnline { get; set; }
        public DateTime Created_At { get; set; }
        public Role Role { get; set; }
        public List<Message> Messages { get; set; }

    }
}
