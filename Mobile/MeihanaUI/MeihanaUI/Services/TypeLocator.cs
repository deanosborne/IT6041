using Autofac;
using MeihanaUI.ViewModels.About;
using MeihanaUI.ViewModels.Ecommerce;
using MeihanaUI.ViewModels.Login;
using MeihanaUI.ViewModels.Onboarding;
using MeihanaUI.Core;
using MeihanaUI.Core.Services;
using System;

namespace MeihanaUI.Services
{
    public class TypeLocator
    {
        private IContainer container;

        private readonly ContainerBuilder containerBuilder;

        private static readonly TypeLocator TypeLocatorInstance = new TypeLocator();

        public static TypeLocator Instance
        {
            get
            {
                return TypeLocatorInstance;
            }
        }

        public TypeLocator()
        {
            containerBuilder = new ContainerBuilder();            
            containerBuilder.RegisterType<NavigationService>().As<INavigationService>();
            containerBuilder.RegisterType<DialogService>().As<IDialogService>();
            containerBuilder.RegisterType<PreferenceService>().As<IPreferenceService>();
            containerBuilder.RegisterType<Startup>();

            containerBuilder.RegisterType<LoginPageViewModel>();
            containerBuilder.RegisterType<SignUpPageViewModel>();
            containerBuilder.RegisterType<ForgotPasswordViewModel>();
            containerBuilder.RegisterType<ResetPasswordViewModel>();
            containerBuilder.RegisterType<OnBoardingGradientViewModel>();
            containerBuilder.RegisterType<CategoryPageViewModel>();
            containerBuilder.RegisterType<CatalogPageViewModel>();
            containerBuilder.RegisterType<DetailPageViewModel>();
            containerBuilder.RegisterType<CartPageViewModel>();
            containerBuilder.RegisterType<CheckoutPageViewModel>();
            containerBuilder.RegisterType<AboutUsViewModel>();
        }

        public object Resolve(Type type, NamedParameter namedParameter = null)
        {
            if (namedParameter == null)
            {
                return container.Resolve(type);
            }
            return container.Resolve(type, namedParameter);
        }

        public void Build()
        {
            container = containerBuilder?.Build();
        }
    }
}
