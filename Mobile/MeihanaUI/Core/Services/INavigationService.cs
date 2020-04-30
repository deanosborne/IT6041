using System;
using System.Collections.Generic;
using System.Text;

namespace MeihanaUI.Core.Services
{
    public interface INavigationService
    {
        void NavigateTo(Type type, string parameterName, string parameterValue, bool replaceView = false);

        void NavigateBack();
    }
}
