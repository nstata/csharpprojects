using System.Collections.Generic;
using UserFxCurrencyConverter.Interfaces;
using UserFxCurrencyConverter.UserCurrencyConverter;

namespace UserFxCurrencyConverterIntegrationTests
{
    public class TestUserSettingsProvider : IUserSettings
    {
        private Dictionary<long, UserSettings> _userSettingsDetails;
        public Dictionary<long, bool> UserSettingsCalled;

        public TestUserSettingsProvider()
        {
            _userSettingsDetails = new Dictionary<long, UserSettings>();
            UserSettingsCalled = new Dictionary<long, bool>();
        }

        internal void SetUserSettings(long userId)
        {
            _userSettingsDetails[userId] = null;
        }

        public UserSettings GetUserSettings(long userId)
        {
            UserSettingsCalled[userId] = true;
            if (_userSettingsDetails.ContainsKey(userId))
            {
                return _userSettingsDetails[userId];
            }

            return null;
        }
    }
}
