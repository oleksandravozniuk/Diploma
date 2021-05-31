using Microsoft.Extensions.DependencyInjection;
using PresentationLevel.ViewModels;
using ProblemManager.Dtos;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace PresentationLevel.Pages.MyProblems
{
    public sealed partial class MyProblemsListPage : Page
    {
        private IndividualProblemDto individualProblem;
        private readonly MyProblemsListPageViewModel _viewModel;

        public MyProblemsListPage()
        {
            InitializeComponent();
            var container = ((App)Application.Current).Container;
            _viewModel = (MyProblemsListPageViewModel)ActivatorUtilities.GetServiceOrCreateInstance(container, typeof(MyProblemsListPageViewModel));
            
        }

        private void FileInput_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LaunchFile(individualProblem.FileInputIndividualProblemName);
        }

        private void FileOutput_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LaunchFile(individualProblem.FileOutputIndividualProblemName);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            individualProblem = _viewModel.GetIndividualProblem((int)e.Parameter);

            ProblemName.Text = individualProblem.Name;
            AltCount.Text = "Кількість альтернатив: " + individualProblem.AlternativesCount;
            XCount.Text = "Кількість змінних: " + individualProblem.XCount;
            ConstrCount.Text = "Кількість обмежень: " + individualProblem.ConstraintsCount;
            OptDirection.Text = "Напрям оптимізації: " + (individualProblem.OptimizationDirection == 0 ? "max" : "min");
            Comment.Text = "Коментар: " + individualProblem.Comment;

            base.OnNavigatedTo(e);
        }
    }
}
