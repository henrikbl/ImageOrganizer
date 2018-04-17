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

        ObservableCollection<Group> groupList;
        public ObservableCollection<Group> GroupList { get { return groupList; } set { Set(ref groupList, value); } }

        ObservableCollection<Picture> pictureList;
        public ObservableCollection<Picture> PictureList { get { return pictureList; } set { Set( ref pictureList, value); } }


        //Mapping images with bitmap created from storagefiles.
        ObservableCollection<BitmapImage> images;
        public ObservableCollection<BitmapImage> Images { get { return images; } set { if (images != value) { images = value; NotifyPropertyChanged(); } } }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {

            _PathToFolder = "Directory";

            Images = new ObservableCollection<BitmapImage>();
            //AddGroupsToGroupListAsync();

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
        }


       /* public async Task AddGroupsToGroupListAsync()
        {
            GroupList = new ObservableCollection<Group>(await DataSource.Groups.Instance.getGroups());
        } */

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if(GroupList == null)
            {
                GroupList = new ObservableCollection<Group>(await DataSource.Groups.Instance.getGroups());
            }
            
            if(state.Any())
            {

            }
            await Task.CompletedTask;
        }

        public ICommand FindFolderCommand { get { return new FindFolderCommand(async ()=> await FindFolderAsync()); }}

        // Folder picker including image list creation.
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
                await createPictureListFromFolderAsync(picture_files);

                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                PathToFolder = folder.Path;
            }

        }

        // creates a list of pictures for viewing.
        public async Task createPictureListFromFolderAsync(IReadOnlyList<StorageFile> files)
        {
            PictureList.Clear();
            //Images.Clear();

            foreach(var f in files)
            {
               // StorageFiles = new ObservableCollection<StorageFile>();
               // StorageFiles.Add(f);
                Picture p = new Picture();
                p.Title = f.DisplayName.ToString();
                p.FilePath = f.Path;
                PictureList.Add(p);

                BitmapImage img = await RetrieveImageAsync(f);
                Images.Add(img);
            }
        }

        // Converts bitmapimage from a storagefile.
        public async Task<BitmapImage> RetrieveImageAsync(StorageFile file)
        {
            BitmapImage bitmapImage = new BitmapImage();
            FileRandomAccessStream stream = (FileRandomAccessStream)await file.OpenAsync(FileAccessMode.Read);

            bitmapImage.SetSource(stream);

            return bitmapImage;
        }

        // method for updating UI elements using INotifyPropertyChanged
        private void NotifyPropertyChanged([CallerMemberName] String propertyName="")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
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
