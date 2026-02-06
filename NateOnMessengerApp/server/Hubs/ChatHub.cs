using Microsoft.AspNetCore.SignalR;
namespace server.Hubs
{
    public class ChatHub : Hub
    {
		// 클라이언트가 연결되었을 때
		public override async Task OnConnectedAsync()
		{
			await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
			await base.OnConnectedAsync();
        }

        // 클라이언트가 연결이 헤제되었을 때
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        // 메세지 전송
        public async Task SendMessage(string user, string messageText)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, messageText, DateTime.Now);

        }

        // 개인 메세지 전송
        public async Task SendPrivateMessage(string connectionId, string user, string messageText)
        {
            await Clients.Client(connectionId).SendAsync("ReceivePrivateMessage", user, messageText, DateTime.Now);
        }

        // 그룹 메세지 전송
        public async Task SendPublicMessage(string roomId, string user, string messageText)
        {
            await Clients.Group(roomId).SendAsync("ReceiveMessage", user, messageText, DateTime.Now);
        }

        // 채팅방 입장
        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("UserJoinedRoom", Context.ConnectionId, roomId);
        }

        // 채팅방 퇴장
        public async Task LeaveRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("UserLeftRoom", Context.ConnectionId, roomId);
        }

    }
}
