using ImageOrganizer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageOrganizer.GUI
{
    class SampleData
    {

        public string testString1 { get; set; }
        public string testString2 { get; set; }

        public string testImage { get; set; }

        public ObservableCollection<Picture> PictureList { get; set; }

        public SampleData()
        {
            testString1 = "Test string 1";
            testString2 = "Test string 2";

            PictureList = new ObservableCollection<Picture>()
            {
                new Picture()
                {
                    PictureId = 1,
                    Title = "Image 1",
                    FilePath = "/Assets/ExampleImage.png"

                },

                new Picture()
                {
                    PictureId = 2,
                    Title = "Image 2",
                    FilePath = "/Assets/StoreLogo.png"
                }
            };

            //testImage = "/Assets/StoreLogo.png";
        }
        
    }
}
