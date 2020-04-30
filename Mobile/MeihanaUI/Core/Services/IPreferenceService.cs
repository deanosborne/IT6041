using System;
namespace MeihanaUI.Core.Services
{
    public interface IPreferenceService
    {
        void Set(string key, string value);

        string Get(string key);
    }
}