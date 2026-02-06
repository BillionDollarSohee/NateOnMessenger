using System;
namespace server.Models
{
    public class ChatRoom
    {
        public long Id { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public bool IsGroupChat { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<User> participants { get; set; } = new List<User>();
        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

    }
}
