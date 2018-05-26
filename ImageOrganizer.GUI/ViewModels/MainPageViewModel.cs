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
using ImageOrganizer.GUI.DataSource;
using System.Net.Http;

namespace ImageOrganizer.GUI.ViewModels
{

    public class MainPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        // path to currently picked folder.
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

        // message board textinput.
        private string _MessageBoardText;
        public string MessageBoardText
        {
            get => _MessageBoardText;
            set
            {
                if(value != _MessageBoardText)
                {
                    _MessageBoardText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // picture which is currently showing.
        private PictureForView _SelectedPicture;
        public PictureForView SelectedPicture
        {
            get => _SelectedPicture;
            set
            {
                if (value != _SelectedPicture)
                {
                    _SelectedPicture = value;
                    if (SelectedPicture != null)
                    {
                        CurrentPictureTitle = _SelectedPicture.Title;
                    }

                    NotifyPropertyChanged();
                }
            }
        }

        // current title of the selected picture from the title textbox.
        private string _CurrentPictureTitle;
        public string CurrentPictureTitle
        {
            get => _CurrentPictureTitle;
            set
            {
                if (value != _CurrentPictureTitle)
                {
                    _CurrentPictureTitle = value;
                    SelectedPicture.Title = CurrentPictureTitle;
                    NotifyPropertyChanged();
                }
            }
        }

        // group which is selected when viewing pictures from database.
        private Group _PickedGroup;
        public Group PickedGroup
        {
            get => _PickedGroup;
            set
            {
                if (value != _PickedGroup)
                {
                    _PickedGroup = value;
                    NotifyPropertyChanged();
                    CreatePictureListFromDatabaseAsync(_PickedGroup.GroupId);
                }
            }
        }

        // group which is selected to add picture in.
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

        /// <summary>
        /// The group list
        /// </summary>
        ObservableCollection<Group> groupList;
        /// <summary>
        /// Gets or sets the group list.
        /// </summary>
        /// <value>
        /// The group list.
        /// </value>
        public ObservableCollection<Group> GroupList { get { return groupList; } set { Set(ref groupList, value); } }

        /// <summary>
        /// The picture list
        /// </summary>
        ObservableCollection<PictureForView> pictureList;
        /// <summary>
        /// Gets or sets the picture list.
        /// </summary>
        /// <value>
        /// The picture list.
        /// </value>
        public ObservableCollection<PictureForView> PictureList { get { return pictureList; } set { Set(ref pictureList, value); } }

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        public MainPageViewModel()
        {
            _PathToFolder = "Directory";
            _MessageBoardText = ".....";

            PictureList = new ObservableCollection<PictureForView>();
        }

        /// <summary>
        /// Called when [navigated to asynchronous].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if(GroupList == null)
            {
                GroupList = new ObservableCollection<Group>(await DataSource.Groups.Instance.GetGroups());
            }

            if(PictureList == null)
            {
                PictureList = new ObservableCollection<PictureForView>();
            }
            
            if(state.Any())
            {

            }
            await Task.CompletedTask;
        } 

        public ICommand FindFolderCommand { get { return new FindFolderCommand(async ()=> await FindFolderAsync()); } }
        public ICommand AddPictureCommand { get { return new AddPicture(async () => await AddPictureAsync()); } }
        public ICommand UpdatePictureCommand { get { return new UpdatePicture(async () => await UpdatePictureAsync()); } }
        public ICommand DeletePictureCommand { get { return new DeletePicture(async () => await DeletePictureAsync()); } }

        // Delete picture from database
        public async Task DeletePictureAsync()
        {
            string title = SelectedPicture.Title;

            try
            {
                await DataSource.Pictures.Instance.DeletePicture(SelectedPicture.PictureId);

                PictureList.Remove(PictureList.Where(i => i.PictureId == SelectedPicture.PictureId).Single());
                MessageBoardText = $"Picture {title} was deleted from the database!";
            }
            catch (HttpRequestException)
            {
                MessageBoardText = "oops...seems like there is something wrong with the connection!";
            }
            catch(Exception)
            {
                MessageBoardText = "hmm...something went wrong.";
            }
            
        }

        // Update picture in database
        public async Task UpdatePictureAsync()
        {
            Picture picture = new Picture(CurrentPictureTitle, SelectedPicture.Base64ImageString);
            picture.PictureId = SelectedPicture.PictureId;

            try
            {
                await DataSource.Pictures.Instance.UpdatePicture(picture);

                MessageBoardText = $"Title of current picture was changed to {picture.Title}";
            }
            catch (HttpRequestException)
            {
                MessageBoardText = "oops...seems like there is something wrong with the connection!";
            }
            catch(Exception)
            {
                MessageBoardText = "hmm...something went wrong.";
            }
            
        }

        // Add picture to database and group.
        public async Task AddPictureAsync()
        {
            var picture = new Picture(CurrentPictureTitle, SelectedPicture.Base64ImageString);

            try
            {
                if (SelectedGroup != null)
                {
                    int selectedGroupId = SelectedGroup.GroupId;

                    await DataSource.Pictures.Instance.AddPicture(picture);
                    await DataSource.Pictures.Instance.AddPictureToGroup(Pictures.CurrentPictureId, selectedGroupId);

                    MessageBoardText = $"Yay....{picture.Title} is added to {SelectedGroup.Name}!";
                }

                else
                {
                    MessageBoardText = "Oops.. you did not set a group to add picture to!";
                }
            }
            catch (HttpRequestException)
            {
                MessageBoardText = "oops...seems like there is something wrong with the connection!";
            }
            catch (Exception)
            {
                MessageBoardText = "hmm...something went wrong.";
            }

        }

        // Folder picker method which creates a list of storagefiles with .png, .jpg and .jpeg extensions.
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

                options = new QueryOptions(CommonFileQuery.DefaultQuery, new[] { ".png", ".jpg", ".jpeg", ".JPG" });
                StorageFileQueryResult sfqr = folder.CreateFileQueryWithOptions(options);

                IReadOnlyList<StorageFile> picture_files = await sfqr.GetFilesAsync();
                await CreatePictureListFromFolderAsync(picture_files);

                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                PathToFolder = folder.Path;
            }
        }

        // Creates a list of pictures from local for viewing.
        public async Task CreatePictureListFromFolderAsync(IReadOnlyList<StorageFile> files)
        {
            PictureList.Clear();

            foreach(var f in files)
            {
                string imageString = await ConvertToBase64StringAsync(f);
                Picture p = new Picture(f.DisplayName.ToString(), imageString);

                BitmapImage image = new BitmapImage();
                image = await ConvertToBitmapImageAsync(imageString);

                PictureForView pfv = new PictureForView(p, image);
                PictureList.Add(pfv);  
            }
        }

        // Creates a list of pictures from database based on groups
        public async Task CreatePictureListFromDatabaseAsync(int id)
        {
            PictureList.Clear();

            try
            {
                Picture[] list = await DataSource.Pictures.Instance.GetPictures(id);

                for (int i = 0; i < list.Length; i++)
                {
                    BitmapImage image = await ConvertToBitmapImageAsync(list[i].Base64ImageString);
                    PictureForView forView = new PictureForView(list[i], image);

                    PictureList.Add(forView);
                }
            }
            catch (HttpRequestException)
            {
                MessageBoardText = "oops...seems like there is something wrong with the connection!";
            }
            catch(Exception)
            {
                MessageBoardText = "hmm...something went wrong.";
            }
            
         }

        // Convert image to a base64String. Method is found at https://blogs.msdn.microsoft.com/msgulfcommunity/2014/11/02/c-encoding-and-decoding-base-64-strings/
        public async Task<string> ConvertToBase64StringAsync(StorageFile file)
        {
            try
            {
                var bitmapImage = new BitmapImage();
                var stream = (FileRandomAccessStream)await file.OpenAsync(FileAccessMode.Read);

                bitmapImage.SetSource(stream);

                Byte[] pictureAttachment = new Byte[0];
                var reader = new DataReader(stream.GetInputStreamAt(0));
                pictureAttachment = new Byte[stream.Size];

                await reader.LoadAsync((uint)stream.Size);
                reader.ReadBytes(pictureAttachment);

                string imageAsBase64String = Convert.ToBase64String(pictureAttachment);
                return imageAsBase64String;
            }
            catch (Exception)
            {
                MessageBoardText = "Something is wrong with this file";
                return string.Empty;
            }
            
        }

        // Convert base64String to BitmapImage. Method found at https://social.msdn.microsoft.com/Forums/sqlserver/en-US/09a07285-c03d-42e9-901f-e7ded45944b7/wp81chow-to-convert-base64-string-to-image-or-bitmap-image-in-windows-phone-81-store-app?forum=wpdevelop
        public async Task<BitmapImage> ConvertToBitmapImageAsync(string value)
        {
            try
            {
                var byteArray = Convert.FromBase64String(value);
                var stream = new InMemoryRandomAccessStream();

                var dataWriter = new DataWriter(stream);
                dataWriter.WriteBytes(byteArray);
                await dataWriter.StoreAsync();
                stream.Seek(0);

                BitmapImage image = new BitmapImage();
                image.SetSource(stream);

                return image;
            }
            catch (Exception)
            {
                MessageBoardText = "Something is wrong with this file";
                return null;
            }
            
        }

        // Method for updating UI elements using INotifyPropertyChanged
        private void NotifyPropertyChanged([CallerMemberName] string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /*
     * 
     * Commands 
     * 
     */

    // Command for adding a picture to database.
    public class AddPicture : ICommand
    {
        private Action _action;

        public AddPicture(Action action)
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

    // Command for updating a picture which exist in database.
    public class UpdatePicture : ICommand
    {
        private Action _action;

        public UpdatePicture(Action action)
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

    // Command for deleting a picture from database.
    public class DeletePicture : ICommand
    {
        private Action _action;

        public DeletePicture(Action action)
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
