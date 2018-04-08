using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace ImageOrganizer.GUI.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public string testString1 = "Changed text";
        public string testString2 = "Hello world";

        public string testImage = "/Assets/ExampleImage.png";
    }
}
