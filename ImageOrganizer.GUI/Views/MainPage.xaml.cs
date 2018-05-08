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

        // Method is placed in code-behind to get access to the content dialog so the datacontext can be set.
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

        // Configures the interface for local files usage.
        private void LocalRadioButton_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LocalInterface.Visibility = Windows.UI.Xaml.Visibility.Visible;
            DatabaseInterface.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            ChooseGroupMenu.IsEnabled = true;
            AddPictureButton.Command = ViewModel.AddPictureCommand;
            AddPictureButton.Content = "Add Picture";
        }

        // Configure the interface for database files usage.
        private void DatabaseRadioButton_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LocalInterface.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            DatabaseInterface.Visibility = Windows.UI.Xaml.Visibility.Visible;

            ChooseGroupMenu.IsEnabled = false;
            AddPictureButton.Command = ViewModel.UpdatePictureCommand;
            AddPictureButton.Content = "Update";
            FolderPath.Text = "Directory";
        }
    }
}