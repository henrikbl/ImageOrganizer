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
        public string PathToFolder { get; set; }

        public ObservableCollection<Picture> PictureList { get; set; }

        public ObservableCollection<Group> GroupList { get; set; }

        public SampleData()
        {
            PathToFolder = "Directory";

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

            GroupList = new ObservableCollection<Group>()
            {
                new Group()
                {
                    GroupId = 1,
                    Name = "Places"
                },

                new Group()
                {
                    GroupId = 1,
                    Name = "People"
                }
            };
        }
        
    }
}
