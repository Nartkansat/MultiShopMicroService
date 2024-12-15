namespace MultiShop.SignalRApi.Services.SignalRCommentServices
{
    public interface ISignalRCommentService
    {
        Task<int> GetTotalCommentCount();
    }
}
