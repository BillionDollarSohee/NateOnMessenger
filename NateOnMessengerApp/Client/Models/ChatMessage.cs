using System;
using System.Collections.Generic;
using System.Text;

namespace client.Models
{
    public class CharMessage
    {
        public string Username { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public bool IsMyMessage { get; set; }
    }
}
