using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;


namespace PresentationLevel.Pages.MyProblems
{
    public sealed partial class MyProblemsListPage : Page
    {
        private ObservableCollection<int> MyProblems = new ObservableCollection<int>();
        public MyProblemsListPage()
        {
            this.InitializeComponent();
        }
    }
}
