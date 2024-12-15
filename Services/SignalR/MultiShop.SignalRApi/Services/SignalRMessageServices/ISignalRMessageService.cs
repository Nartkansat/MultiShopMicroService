namespace MultiShop.SignalRApi.Services.SignalRMessageServices
{
    public interface ISignalRMessageService
    {
        Task<int> GetTotalMessageCountByReceiverId(string id);
    }
}
