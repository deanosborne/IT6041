using System;
using MeihanaUI.ViewModels.Ecommerce;
using MeihanaUI.ViewModels.Login;
using MeihanaUI.ViewModels.Onboarding;
using MeihanaUI.Core.Services;

namespace MeihanaUI.Core
{
    public class Startup
    {
        IPreferenceService preferenceService;

        public Startup(IPreferenceService preferenceService)
        {
            this.preferenceService = preferenceService;
        }

        public Type GetMainPage()
        {
            var isNew = preferenceService.Get("isnew");
            //if (isNew == "false")
            if (true)
            {
                preferenceService.Set("isnew", "false");
                return typeof(OnBoardingGradientViewModel);
            }
            else
            {
                var email = preferenceService.Get("email");

                if (string.IsNullOrEmpty(email))
                {
                    return typeof(LoginPageViewModel);
                }
                else
                {
                    return typeof(CategoryPageViewModel);
                }
            }
        }
    }
}