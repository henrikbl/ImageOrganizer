using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using ImageOrganizer.Model;
using System.Windows.Input;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage.Search;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace ImageOrganizer.GUI.ViewModels
{

    public class MainPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public string testString1 { get; set; }
        public string testString2 { get; set; }

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

        ObservableCollection<Picture> pictureList;
        public ObservableCollection<Picture> PictureList { get { return pictureList; } set { Set( ref pictureList, value); } }


        //Test with storagefiles
        ObservableCollection<StorageFile> storeFiles;
        public ObservableCollection<StorageFile> StorageFiles { get { return storeFiles; } set { if (storeFiles != value) { storeFiles = value; NotifyPropertyChanged(); } } }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
            testString1 = "Changed text";
            testString2 = "Hello world";

            _PathToFolder = "Path to folder";

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
                createPictureListFromFolder(picture_files);

                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                PathToFolder = folder.Path;
            }

        }

        // creates a list of pictures for viewing.
        public void createPictureListFromFolder(IReadOnlyList<StorageFile> files)
        {
            PictureList.Clear();

            foreach(var f in files)
            {
                StorageFiles = new ObservableCollection<StorageFile>();
                StorageFiles.Add(f);
               /* Picture p = new Picture();
                p.Title = f.DisplayName.ToString();
                p.FilePath = f.Path;
                PictureList.Add(p); */
            }
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

    /*
     * 
     * Converters
     * 
     */

    // Converter for changing uri to bitmap.
    public class UriToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value != null)
            {
                string picturePath = value as string;
                return new BitmapImage(new Uri(picturePath));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
