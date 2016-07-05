using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privacy.ViewModel
{
    public class AboutViewModel:ViewModelBase
    {
        #region variables

        #region public variables
        public string Data { get; set; }
        #endregion

        #region private readonly variables
        private readonly INavigationService navigationService;
        #endregion

        #endregion

        /// <summary>
        /// Constructor of the AboutViewModel
        /// Sets the given Parameter to a private readonly field
        /// </summary>
        /// <param name="navigationService">Instance of an implementation Galasoft's INavigationService Interface</param>
        public AboutViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        /// <summary>
        /// Loads the Data needed at this page
        /// </summary>
        public void LoadData() {
            Data = "GalaSoft MVVM Light Toolkit\n" +
                "Fody\n" +
                "Newtonsoft.Json\n" +
                "The aforementioned products are used in this application and are under the MIT Licence" +
            "\nThe MIT License(MIT)" +
            "\n\nCopyright(c) 2009 - 2014 Laurent Bugnion (Galasoft MVVM Light Toolkit)" +
            "\nCopyright(c) Simon Cropp and contributors (Fody)" +
            "\nCopyright (c) 2007 James Newton-King (Newtonsoft.Json)" +
            "\nPermission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files(the \"Software\"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/ or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:" +
            "\n" +
            "\nThe above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software." +
            "\n" +
            "\nTHE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.";
        }

        /// <summary>
        /// Navigates back to the previous page
        /// </summary>
        public void GoBackRequest()
        {
            navigationService.GoBack();
        }
    }
}
