using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Privacy.Common;
using Privacy.Services;
using Privacy.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<CategoryViewModel>();
            SimpleIoc.Default.Register<CentralMenuViewModel>();
            SimpleIoc.Default.Register<ContinueViewModel>();
            SimpleIoc.Default.Register<GuessViewModel>();
            SimpleIoc.Default.Register<QuestionViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register(RegisterNavigationService);
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<IStorageService, LocalStorageService>();
        }
        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();
        public CategoryViewModel CategoryViewModel => ServiceLocator.Current.GetInstance<CategoryViewModel>();
        public ContinueViewModel ContinueViewModel => ServiceLocator.Current.GetInstance<ContinueViewModel>();
        public GuessViewModel GuessViewModel => ServiceLocator.Current.GetInstance<GuessViewModel>();
        public QuestionViewModel QuestionViewModel => ServiceLocator.Current.GetInstance<QuestionViewModel>();
        public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();
        public CentralMenuViewModel MenuViewModel => ServiceLocator.Current.GetInstance<CentralMenuViewModel>();
        private static INavigationService RegisterNavigationService()
        {
            var service = new NavigationService();
            service.Configure(Navigation.Category, typeof(CategoryView));
            service.Configure(Navigation.Continue, typeof(ContinueView));
            service.Configure(Navigation.Guess, typeof(GuessView));
            service.Configure(Navigation.Main, typeof(MainPage));
            service.Configure(Navigation.Question, typeof(QuestionView));
            service.Configure(Navigation.Settings, typeof(SettingsView));
            service.Configure(Navigation.CentralMenu, typeof(CentralMenuView));
            return service;
        }
    }
}
