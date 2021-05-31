using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using PresentationLevel.Pages.MyProblems;
using PresentationLevel.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Linq;
using SymbolIconSource = Microsoft.UI.Xaml.Controls.SymbolIconSource;
using System.Collections.ObjectModel;
using ProblemManager.Dtos;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PresentationLevel.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyProblemsPage : Page
    {
        private readonly MyProblemsViewModel _viewModel;
        public MyProblemsPage()
        {
            this.InitializeComponent();

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;

            Window.Current.SetTitleBar(CustomDragRegion);

            var container = ((App)Application.Current).Container;
            _viewModel = (MyProblemsViewModel)ActivatorUtilities.GetServiceOrCreateInstance(container, typeof(MyProblemsViewModel));
            MyProblemsList.ItemsSource = _viewModel.GetIndividualProblems();
        }
        private void Tabs_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);
        }
        private void NewProblem_Click(object sender, RoutedEventArgs e)
        {
            var newTab = new TabViewItem();
            newTab.IconSource = new SymbolIconSource() { Symbol = Symbol.Document };
            newTab.Header = "Нова задача";

            Frame frame = new Frame();
            newTab.Content = frame;
            frame.Navigate(typeof(NewProblemPage));
            MyProblemsTabView.TabItems.Add(newTab);
            MyProblemsTabView.SelectedItem = newTab;
        }
        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            if (FlowDirection == FlowDirection.LeftToRight)
            {
                CustomDragRegion.MinWidth = sender.SystemOverlayRightInset;
                ShellTitlebarInset.MinWidth = sender.SystemOverlayLeftInset;
            }
            else
            {
                CustomDragRegion.MinWidth = sender.SystemOverlayLeftInset;
                ShellTitlebarInset.MinWidth = sender.SystemOverlayRightInset;
            }

            CustomDragRegion.Height = ShellTitlebarInset.Height = sender.Height;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = int.Parse(btn.Tag.ToString());
            _viewModel.DeleteIndividualProblem(index);
            MyProblemsList.ItemsSource = _viewModel.GetIndividualProblems();
        }

        private void MoreButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = int.Parse(btn.Tag.ToString());

            var newTab = new TabViewItem();
            newTab.IconSource = new SymbolIconSource() { Symbol = Symbol.Document };
            newTab.Header = "Problem" + id;

            Frame frame = new Frame();
            newTab.Content = frame;
            frame.Navigate(typeof(MyProblemsListPage), id);
            MyProblemsTabView.TabItems.Add(newTab);
            MyProblemsTabView.SelectedItem = newTab;
        }
    }
}
