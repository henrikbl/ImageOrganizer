using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using ImageOrganizer.Model;
using System.Windows.Input;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage.Search;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;

namespace ImageOrganizer.GUI.ViewModels
{

    public class MainPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _CurrentPictureTitle;
        public string CurrentPictureTitle
        {
            get => _CurrentPictureTitle;
            set
            {
                if (value != _CurrentPictureTitle)
                {
                    _CurrentPictureTitle = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _PathToFolder;
        public string PathToFolder
        {
            get => _PathToFolder;
            set
            {
                if (value != _PathToFolder)
                {
                    _PathToFolder = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // TODO image not showing. probably incorrect method.
        private BitmapImage _CurrentImage;
        public BitmapImage CurrentImage
        {
            get => _CurrentImage;
            set
            {
                if (value != _CurrentImage)
                {
                    _CurrentImage = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private StorageFile _SelectedPicture;
        public StorageFile SelectedPicture
        {
            get => _SelectedPicture;
            set
            {
                if(value != _SelectedPicture)
                {
                    _SelectedPicture = value;
                    CurrentPictureTitle = _SelectedPicture.DisplayName;
                    NotifyPropertyChanged();
                }
            }
        }

        private Group _SelectedGroup;
        public Group SelectedGroup
        {
            get => _SelectedGroup;
            set
            {
                if(value != _SelectedGroup)
                {
                    _SelectedGroup = value;
                    NotifyPropertyChanged();
                }
            }
        }

        ObservableCollection<Group> groupList;
        public ObservableCollection<Group> GroupList { get { return groupList; } set { Set(ref groupList, value); } }

        /*ObservableCollection<Picture> pictureList;
        public ObservableCollection<Picture> PictureList { get { return pictureList; } set { Set( ref pictureList, value); } } */

        ObservableCollection<StorageFile> pictureList;
        public ObservableCollection<StorageFile> PictureList { get { return pictureList; } set { Set(ref pictureList, value); } }


        //Mapping images with bitmap created from storagefiles.
        ObservableCollection<BitmapImage> images;
        public ObservableCollection<BitmapImage> Images { get { return images; } set { if (images != value) { images = value; NotifyPropertyChanged(); } } }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
            _PathToFolder = "Directory";

            Images = new ObservableCollection<BitmapImage>();

            PictureList = new ObservableCollection<StorageFile>();

          /*  PictureList = new ObservableCollection<Picture>()
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
            }; */
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if(GroupList == null)
            {
                GroupList = new ObservableCollection<Group>(await DataSource.Groups.Instance.GetGroups());
            }
            
            if(state.Any())
            {

            }
            await Task.CompletedTask;
        } 

        public ICommand FindFolderCommand { get { return new FindFolderCommand(async ()=> await FindFolderAsync()); }}

        // Folder picker method with image list creation.
        public async Task FindFolderAsync()
        {
            var folderPicker = new FolderPicker();
            folderPicker.ViewMode = PickerViewMode.List;
            folderPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            folderPicker.FileTypeFilter.Add("*");

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                QueryOptions options;

                options = new QueryOptions(CommonFileQuery.DefaultQuery, new[] { ".png", ".jpg", ".jpeg" });
                StorageFileQueryResult sfqr = folder.CreateFileQueryWithOptions(options);

                IReadOnlyList<StorageFile> picture_files = await sfqr.GetFilesAsync();
                await CreatePictureListFromFolderAsync(picture_files);

                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                PathToFolder = folder.Path;
            }
        }

        // creates a list of pictures from computer for viewing.
        public async Task CreatePictureListFromFolderAsync(IReadOnlyList<StorageFile> files)
        {
            PictureList.Clear();

            foreach(var f in files)
            {
                PictureList.Add(f);
              /*  Picture p = new Picture();
                p.Title = f.DisplayName.ToString();
                p.FilePath = f.Path;
                PictureList.Add(p);

                BitmapImage img = await RetrieveImageAsync(f);
                Images.Add(img);  */
            }
        }

        // Converts bitmapimage from a storagefile.
        public async Task<BitmapImage> RetrieveImageAsync(StorageFile file)
        {
            var bitmapImage = new BitmapImage();
            var stream = (FileRandomAccessStream)await file.OpenAsync(FileAccessMode.Read);

            bitmapImage.SetSource(stream);

            return (BitmapImage)bitmapImage;
        }

        // method for updating UI elements using INotifyPropertyChanged
        private void NotifyPropertyChanged([CallerMemberName] String propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /*
     * 
     * Commands 
     * 
     */

    // Command for finding a folder with images.
    public class FindFolderCommand : ICommand
    {
        private Action _action;

        public FindFolderCommand(Action action)
        {
            _action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this._action();
        }
    }
}
