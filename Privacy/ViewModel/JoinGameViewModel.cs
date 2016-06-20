using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class JoinGameViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        public int MenuSize { get{ return ShowMenu ? 200 : 0; }}
        public bool ShowMenu { get; set; }
        private string gameId="120";
        public string GameId { get { return gameId; }
            set {
                ulong result;
                if (ulong.TryParse(value, out result))
                    gameId = value;
                else
                    RaisePropertyChanged(nameof(GameId));                }
        }
        public JoinGameViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
        public void HambugerInteraction()
        {
            ShowMenu = !ShowMenu;
        }
        public void NavigateToQuestionView()
        {
            navigationService.NavigateTo(Common.Navigation.Question);
        }
    }
}
