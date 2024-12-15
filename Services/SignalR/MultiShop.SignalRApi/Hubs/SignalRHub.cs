using Microsoft.AspNetCore.SignalR;
using MultiShop.SignalRApi.Services.SignalRCommentServices;
using MultiShop.SignalRApi.Services.SignalRMessageServices;

namespace MultiShop.SignalRApi.Hubs
{
    public class SignalRHub:Hub
    {
        private readonly ISignalRCommentService _signalRCommentService;
        private readonly ISignalRMessageService _signalRMessageService;
        public SignalRHub(ISignalRCommentService signalRCommentService, ISignalRMessageService signalRMessageService)
        {
            _signalRCommentService = signalRCommentService;
            _signalRMessageService = signalRMessageService;
        }

        public async Task SendStatisticCount()
        {
            var getTotalCommentCount = await _signalRCommentService.GetTotalCommentCount();
            await Clients.All.SendAsync("ReceiveCommentCount", getTotalCommentCount);

            //var getTotalMessageCount = await _signalRMessageService.GetTotalMessageCountByReceiverId(id);
            //await Clients.All.SendAsync("ReceiveMessageCount", getTotalMessageCount);
        }
    }
}
