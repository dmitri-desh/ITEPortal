namespace MessengerService
{
    public interface IEmailManager
    {
        Task SendMessage(NetworkSettings networkSettings, EmailMessage emailMessage, string username = "", string password = "");
    }
}
