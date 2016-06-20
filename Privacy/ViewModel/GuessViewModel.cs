using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class GuessViewModel:ViewModelBase
    {
        private readonly INavigationService navigationService;
        public GuessViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
