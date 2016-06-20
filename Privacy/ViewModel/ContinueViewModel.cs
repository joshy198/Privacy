using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class ContinueViewModel:ViewModelBase
    {
        private readonly INavigationService navigationService;
        public ContinueViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
