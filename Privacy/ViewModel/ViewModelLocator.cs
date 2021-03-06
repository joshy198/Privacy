﻿using GalaSoft.MvvmLight.Ioc;
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
        /// <summary>
        /// Static class ViewModellocator, registers all ViewModels und Services to the GalaSoft SimpleIoc
        /// </summary>
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<CategoryViewModel>();
            SimpleIoc.Default.Register<CentralMenuViewModel>();
            SimpleIoc.Default.Register<GuessViewModel>();
            SimpleIoc.Default.Register<QuestionViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<JoinGameViewModel>();
            SimpleIoc.Default.Register<LobbyViewModel>();
            SimpleIoc.Default.Register<AboutViewModel>();
            SimpleIoc.Default.Register(RegisterNavigationService);
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<IStorageService, LocalStorageService>();
            SimpleIoc.Default.Register<IDataService, ConcreteDataService>();
        }
        #region ViewModels
        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();
        public CategoryViewModel CategoryViewModel => ServiceLocator.Current.GetInstance<CategoryViewModel>();
        public GuessViewModel GuessViewModel => ServiceLocator.Current.GetInstance<GuessViewModel>();
        public QuestionViewModel QuestionViewModel => ServiceLocator.Current.GetInstance<QuestionViewModel>();
        public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();
        public CentralMenuViewModel CentralMenuViewModel => ServiceLocator.Current.GetInstance<CentralMenuViewModel>();
        public JoinGameViewModel JoinGameViewModel => ServiceLocator.Current.GetInstance<JoinGameViewModel>();
        public LobbyViewModel LobbyViewModel => ServiceLocator.Current.GetInstance<LobbyViewModel>();
        public AboutViewModel AboutViewModel => ServiceLocator.Current.GetInstance<AboutViewModel>();
        #endregion

        /// <summary>
        /// Configuration of the NavigationService
        /// </summary>
        /// <returns>Returns an Implementation of GalaSoft'S INavigationService, which is configured to be able to Navigate to all Views</returns>
        private static INavigationService RegisterNavigationService()
        {
            var service = new NavigationService();
            service.Configure(Navigation.Category, typeof(CategoryView));
            service.Configure(Navigation.Guess, typeof(GuessView));
            service.Configure(Navigation.Main, typeof(MainPage));
            service.Configure(Navigation.Question, typeof(QuestionView));
            service.Configure(Navigation.Settings, typeof(SettingsView));
            service.Configure(Navigation.CentralMenu, typeof(CentralMenuView));
            service.Configure(Navigation.Join, typeof(JoinGameView));
            service.Configure(Navigation.Lobby, typeof(LobbyView));
            service.Configure(Navigation.About, typeof(AboutView));
            return service;
        }
    }
}
