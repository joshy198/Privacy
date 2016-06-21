using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class LobbyViewModel:ViewModelBase
    {
        public string Mode { get; set; }
        private readonly INavigationService navigationService;
        public LobbyViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        public void NavigateToQuestionView()
        {
            if (Common.Mode.HostStart == Mode||Common.Mode.HostStatistic==Mode)
                navigationService.NavigateTo(Common.Navigation.Question, Common.Mode.IsHost);
            else if(Common.Mode.ClientStatistic==Mode)
                navigationService.NavigateTo(Common.Navigation.Question, Common.Mode.IsHost);
            else if (Common.Mode.HostWait == Mode)
                navigationService.NavigateTo(Common.Navigation.Guess, Common.Mode.IsHost);
            else if (Common.Mode.ClientWait == Mode)
                navigationService.NavigateTo(Common.Navigation.Guess,Common.Mode.IsClient);
        }
    }
}
