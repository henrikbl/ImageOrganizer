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

        // TODO add handling.
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
    }
}