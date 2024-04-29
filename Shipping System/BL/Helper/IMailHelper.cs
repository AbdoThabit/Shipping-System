namespace Shipping_System.BL.Helper
{
    public interface IMailHelper
    {
        Task SendMail(string Reciver, string Title, string body);
        Task WelcomeEmail(string Name, string Username, string email, string password ,string title);

    }
}
