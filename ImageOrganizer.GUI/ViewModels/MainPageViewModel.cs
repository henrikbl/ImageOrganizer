using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using ImageOrganizer.Model;

namespace ImageOrganizer.GUI.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public string testString1 { get; set; }
        public string testString2 { get; set; }

        public string testImage { get; set; }

        public ObservableCollection<Picture> PictureList { get; set; }

        public MainPageViewModel()
        {
            testString1 = "Changed text";
            testString2 = "Hello world";

            PictureList = new ObservableCollection<Picture>()
            {
                new Picture()
                {
                    PictureId = 1,
                    Title = "Image 1",
                    FileDirectory = "/Assets/ExampleImage.png"
                },

                new Picture()
                {
                    PictureId = 2,
                    Title = "Image 2",
                    FileDirectory = "/Assets/StoreLogo.png"
                }
            };
           // testImage = "/Assets/ExampleImage.png";
        }
    }
}
