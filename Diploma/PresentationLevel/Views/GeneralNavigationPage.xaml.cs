using PresentationLevel.Views.MyResearches;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PresentationLevel.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GeneralNavigationPage : Page
    {
        public GeneralNavigationPage()
        {
            this.InitializeComponent();
            mainContentFrame.Navigate(typeof(MainPage));
            TitleTextBlock.Text = "Головна";

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Main.IsSelected)
            {
                mainContentFrame.Navigate(typeof(MainPage));
                TitleTextBlock.Text = "Головна";
            }
            else if (MyProblems.IsSelected)
            {
                mainContentFrame.Navigate(typeof(MyProblemsPage));
                TitleTextBlock.Text = "Мої задачі";
            }
            else if (MyResearches.IsSelected)
            {
                mainContentFrame.Navigate(typeof(MyResearchesPage));
                TitleTextBlock.Text = "Мої дослідження";
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            CheckSelection();
            mySplitView.IsPaneOpen = !mySplitView.IsPaneOpen;
        }

        private void CheckSelection()
        {
            if(!Main.IsSelected && !MyProblems.IsSelected && !MyResearches.IsSelected)
            {
                mainContentFrame.Navigate(typeof(MainPage));
                TitleTextBlock.Text = "Головна";
                Main.IsSelected = true;
            }
        }
    }
}
