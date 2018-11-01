
namespace BurgerApi.Services
{
    public interface IUserServices
    {
        bool ValidateUser(string username, string password);
    }
}
