﻿#pragma checksum "C:\Users\hen_b\Documents\Henrik\Skole\6.semester\dotNet\Projects\MyProject\ImageOrganizer\ImageOrganizer.GUI\Views\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "ED9E85B953919B1D61F45DD726E3481C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImageOrganizer.GUI.Views
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page
    {


        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::ImageOrganizer.GUI.ViewModels.MainPageViewModel ViewModel; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::ImageOrganizer.GUI.SampleData DesignTimeData; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.RelativePanel mainLayout; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Template10.Controls.PageHeader pageHeader; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.RelativePanel Menu; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.RelativePanel imageView; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock messageBoard; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.FlipView imageBlock; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock messageBoardmessage; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.StackPanel RadiobuttonPanel; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.RelativePanel ChangableInterface; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.RelativePanel AddPicturePanel; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.ContentDialog AddGroupContentDialog; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.Button DeletePictureButton; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.TextBox PictureName; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.Button AddPictureButton; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.ComboBox ChooseGroupMenu; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.Button CreateGroup; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.RelativePanel LocalInterface; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.RelativePanel DatabaseInterface; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.ComboBox PicturelistFromGroup; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.TextBlock FolderPath; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.Button BrowseForFolder; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.RadioButton LocalRadioButton; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private global::Windows.UI.Xaml.Controls.RadioButton DatabaseRadioButton; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;

            global::System.Uri resourceLocator = new global::System.Uri("ms-appx:///Views/MainPage.xaml");
            global::Windows.UI.Xaml.Application.LoadComponent(this, resourceLocator, global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
        }

        partial void UnloadObject(global::Windows.UI.Xaml.DependencyObject unloadableObject);

        private interface IMainPage_Bindings
        {
            void Initialize();
            void Update();
            void StopTracking();
            void DisconnectUnloadedObject(int connectionId);
        }
#pragma warning disable 0169    //  Proactively suppress unused field warning in case Bindings is not used.
        private IMainPage_Bindings Bindings;
#pragma warning restore 0169
    }
}


