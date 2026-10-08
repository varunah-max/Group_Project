using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClassDefinition.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsDelivered { get; set; }
        public User Sender { get; set; }
        public User Reciever { get; set; }
    }
}
