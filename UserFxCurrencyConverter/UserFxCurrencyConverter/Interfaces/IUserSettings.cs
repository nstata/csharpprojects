using UserFxCurrencyConverter.UserCurrencyConverter;

namespace UserFxCurrencyConverter.Interfaces
{
    public interface IUserSettings
    {
        UserSettings GetUserSettings(long userId);
    }
}
