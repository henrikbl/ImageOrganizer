using ImageOrganizer.Model;
using System;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace ImageOrganizer.GUI.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        // TODO move all add methods to ViewModel.
        public async void AddPicture()
        {
            var picture = new Picture();
            picture.Title = ViewModel.CurrentPictureTitle;
            picture.FilePath = ViewModel.SelectedPicture.Path;

            int selectedGroupId = ViewModel.SelectedGroup.GroupId;

            try
            {
                await DataSource.Pictures.Instance.AddPicture(picture);

                // TODO need to retrieve the pictureId from database. so it can be used here.
                try
                {
                    await DataSource.Pictures.Instance.AddPictureToGroup(picture.PictureId, selectedGroupId);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // TODO move to viewmodel and add handling.
        public async void CreateNewGroup()
        {
            var group = new Group();
            AddGroupContentDialog.DataContext = group;

            var result = await AddGroupContentDialog.ShowAsync();
            if(result == ContentDialogResult.Primary)
            {
                try
                {
                    if(await DataSource.Groups.Instance.AddGroup(group))
                    {
                        ViewModel.GroupList.Add(group);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}