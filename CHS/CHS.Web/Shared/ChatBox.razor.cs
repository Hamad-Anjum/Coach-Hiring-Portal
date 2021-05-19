using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace CHS.Web.Shared
{
    public partial class ChatBox : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private HubConnection _hubConnection;

        protected override async Task OnInitializedAsync()
        {
            //_hubConnection = new HubConnectionBuilder().WithUrl(NavigationManager.ToAbsoluteUri("/chatHub")).Build();


            _hubConnection = new HubConnectionBuilder().WithUrl(NavigationManager.ToAbsoluteUri("/chatHub"), option =>
            {
                //option.AccessTokenProvider = () => Task.FromResult(_myAccessToken);
            }).Build();

            _ = _hubConnection.On<object>("ReceiveCourseMessage", (course) =>
            {
                if (course != null)
                {

                }
            });
            await _hubConnection.StartAsync();
            await Send(null);
        }

        Task Send(object course)
        {
            return _hubConnection.SendAsync("SendCourseMessage", course);
        }

        public bool IsConnected =>
        _hubConnection.State == HubConnectionState.Connected;

        public async ValueTask DisposeAsync()
        {
            await _hubConnection.DisposeAsync();
        }
    }
}
