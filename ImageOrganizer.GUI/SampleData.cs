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
        public string CurrentPictureTitle { get; set; }

        public ObservableCollection<Picture> PictureList { get; set; }

        public ObservableCollection<Group> GroupList { get; set; }

        public SampleData()
        {
            PathToFolder = "Directory";

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
