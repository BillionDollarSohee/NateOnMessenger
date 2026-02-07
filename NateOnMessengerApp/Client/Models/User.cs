using System;
using System.Collections.Generic;
using System.Text;

namespace client.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string StatusMessage { get; set; } = string.Empty;
        public bool IsOnline { get; set; }
    }
}
