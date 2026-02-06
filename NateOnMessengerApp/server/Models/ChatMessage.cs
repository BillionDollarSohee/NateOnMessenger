using System;
namespace server.Models
{
    public class ChatMessage
    {
        public long Id { get; set; }
        public string MessageText { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public long SenderId { get; set; }
        public User Sender { get; set; } = null!;

        public long RoomId { get; set; }
        public ChatRoom? Room { get; set; }

        public bool IsRead { get; set; } = false;

    }
}
