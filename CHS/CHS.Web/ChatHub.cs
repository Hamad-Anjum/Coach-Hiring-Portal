
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

namespace CHS.Web
{
    public class ChatHub : Hub
    {
        public Task SendCourseMessage(object user)
        {
            return Clients.All.SendAsync("ReceiveCourseMessage", user);
        }
    }
}
