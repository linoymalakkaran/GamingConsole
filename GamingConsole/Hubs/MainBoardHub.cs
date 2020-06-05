using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;


namespace GamingConsole.Hubs
{
    [HubName("mainBoardHub")]
    public class MainBoardHub : Hub
    {
        public static void PushDataToMainBoard()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MainBoardHub>();
            context.Clients.All.PushDataToMainBoard();
        }

        public static void PullDataFromClient(int consoleNumber)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MainBoardHub>();
            context.Clients.All.PullDataFromClient(consoleNumber);
        }
    }
}