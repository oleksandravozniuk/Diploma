using Microsoft.Extensions.DependencyInjection;
using PresentationLevel.Structs;
using PresentationLevel.ViewModels;
using ProblemGenerator.Models.Distributions;
using ProblemSolver;
using ProblemSolver.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace PresentationLevel.Pages.MyProblems
{
    public sealed partial class NewProblemPage : Page
    {
        private readonly NewProblemInputViewModel _viewModel;
        public NewProblemPage()
        {
            this.InitializeComponent();
            var container = ((App)App.Current).Container;
            _viewModel = (NewProblemInputViewModel)ActivatorUtilities.GetServiceOrCreateInstance(container, typeof(NewProblemInputViewModel));
        }
        private void IncreaseConstraintCount_Click(object sender, RoutedEventArgs e)
        {
            var newStackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            var xStackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            IncreaseXCount(xStackPanel, int.Parse(XCount.Text));
            newStackPanel.Children.Add(xStackPanel);
            var comboBox = new ComboBox();
            comboBox.Items.Add(ConstraintSymbolStruct.LessOrEqual);
            comboBox.Items.Add(ConstraintSymbolStruct.Equal);
            comboBox.Items.Add(ConstraintSymbolStruct.MoreOrEqual);
            comboBox.SelectedIndex = 0;
            newStackPanel.Children.Add(comboBox);
            newStackPanel.Children.Add(new TextBox());
            Constraints.Children.Add(newStackPanel);

            ConstraintCount.Text = Constraints.Children.Count.ToString();
        }
        private void DecreaseConstraintCount_Click(object sender, RoutedEventArgs e)
        {
            Constraints.Children.Remove(Constraints.Children.Last());
            ConstraintCount.Text = Constraints.Children.Count.ToString();
        }
        private void IncreaseEnumeratorCount_Click(object sender, RoutedEventArgs e)
        {
            var newStackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            IncreaseXCount(newStackPanel, int.Parse(XCount.Text));

            Enumerators.Children.Add(newStackPanel);

            IncreaseXCount(Ws, 1);
            IncreaseXCount(Ls, 1);

            EnumeratorCount.Text = Enumerators.Children.Count.ToString();
        }
        private void DecreaseEnumeratorCount_Click(object sender, RoutedEventArgs e)
        {
            Enumerators.Children.Remove(Enumerators.Children.Last());
            EnumeratorCount.Text = Enumerators.Children.Count.ToString();
            DecreaseXCount(Ws, 1);
            DecreaseXCount(Ls, 1);
        }
        private void IncrementX_Click(object sender, RoutedEventArgs e)
        {
            if(Enumerators.Children.Count==0)
            {
                var newStackPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal
                };
                IncreaseXCount(newStackPanel, int.Parse(XCount.Text));
                Enumerators.Children.Add(newStackPanel);
            }

            foreach (var enumerator in Enumerators.Children)
            {
                IncreaseXCount((StackPanel)enumerator, 1);
            }

            IncreaseXCount(Denominators,1);

            if (Constraints.Children.Count == 0)
            {
                var newStackPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal
                };
                newStackPanel.Children.Add(new StackPanel()
                {
                    Orientation = Orientation.Horizontal
                });
                var comboBox = new ComboBox();
                comboBox.Items.Add(ConstraintSymbolStruct.LessOrEqual);
                comboBox.Items.Add(ConstraintSymbolStruct.Equal);
                comboBox.Items.Add(ConstraintSymbolStruct.MoreOrEqual);
                comboBox.SelectedIndex = 0;
                newStackPanel.Children.Add(comboBox);
                newStackPanel.Children.Add(new TextBox());
                Constraints.Children.Add(newStackPanel);
            }

            foreach(var constraint in Constraints.Children)
            {
                IncreaseXCount((StackPanel)((StackPanel)constraint).Children.First(), 1);
            }

            var x = int.Parse(XCount.Text);
            if(x==0)
            {
                MakeTitlesVisible();
                IncreaseEnumeratorCountButton.Visibility = Visibility.Visible;
                IncreaseConstraintCountButton.Visibility = Visibility.Visible;
                DecreaseEnumeratorCountButton.Visibility = Visibility.Visible;
                DecreaseConstraintCountButton.Visibility = Visibility.Visible;
                IncreaseXCount(Ws, 1);
                IncreaseXCount(Ls, 1);
            }
            XCount.Text = (x+1).ToString();

            SolveButton.Visibility = Visibility.Visible;
        }
        private void DecrementX_Click(object sender, RoutedEventArgs e)
        {
            var x = int.Parse(XCount.Text);
            if (x > 0)
            {
                if (x == 1)
                {
                    MakeTitlesUnvisible();
                    IncreaseEnumeratorCountButton.Visibility = Visibility.Collapsed;
                    IncreaseConstraintCountButton.Visibility = Visibility.Collapsed;
                    DecreaseEnumeratorCountButton.Visibility = Visibility.Collapsed;
                    DecreaseConstraintCountButton.Visibility = Visibility.Collapsed;
                    DecreaseXCount(Ws, 1);
                    DecreaseXCount(Ls, 1);
                }
                foreach(var enumerator in Enumerators.Children)
                {
                    DecreaseXCount((StackPanel)enumerator, 1);
                }
                DecreaseXCount(Denominators,1);
                foreach(var constraint in Constraints.Children)
                {
                    DecreaseXCount((StackPanel)((StackPanel)constraint).Children.First(), 1);
                }

                XCount.Text = (x-1).ToString();
                if(x-1 == 0)
                {
                    SolveButton.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void IncreaseXCount(StackPanel stackPanel, int increaseValue)
        {
            for(int i = 0; i < increaseValue; i++)
            {
                stackPanel.Children.Add(new TextBox() { Width = 15, Height = 15, HorizontalAlignment = HorizontalAlignment.Left });
            }
        }
        private void DecreaseXCount(StackPanel stackPanel, int decreaseValue)
        {
            for(int i = 0; i < decreaseValue; i++)
            {
                stackPanel.Children.Remove(stackPanel.Children.Last());
            }
        }
        private void MakeTitlesVisible()
        {
            EnumeratorTitle.Text = "Кількість альтернативних наборів коефіцієнтів чисельника:";
            DenominatorTitle.Text = "Коефіцієнти знаменника";
            ConstraintTitle.Text = "Обмеження задачі:";
            WeightsTitle.Text = "Експертні вагові коефіцієнти";
            LsTitle.Text = "Величина, що показує наскільки оптимальне значення часткової цільової функції може максимально відрізнятися від компромісного розв'язку";
            EnumeratorCount.Text = Enumerators.Children.Count.ToString();
            ConstraintCount.Text = Constraints.Children.Count.ToString();
        }
        private void MakeTitlesUnvisible()
        {
            EnumeratorTitle.Text = string.Empty;
            DenominatorTitle.Text = string.Empty;
            ConstraintTitle.Text = string.Empty;
            WeightsTitle.Text = string.Empty;
            LsTitle.Text = string.Empty;
            EnumeratorCount.Text = string.Empty;
            ConstraintCount.Text = string.Empty;
        }

        private void Solve_Click(object sender, RoutedEventArgs e)
        {
            SolutionTextBlock.Text = _viewModel.SetProblemResult(GetInputEnumerators(), GetInputDenominator(), GetInputConstraints(), GetInputWs(), GetInputLs(), GetOptDirecton());
            ResultsSaveStackPanel.Visibility = Visibility.Visible;
        }

        private List<List<double>> GetInputEnumerators()
        {
            var enumeratorsList = new List<List<double>>();
            foreach(var enumerator in Enumerators.Children)
            {
                var list = new List<double>();
                foreach(var enumeratorElement in ((StackPanel)enumerator).Children)
                {
                    list.Add(double.Parse(((TextBox)enumeratorElement).Text));
                }
                enumeratorsList.Add(list);
            }
            return enumeratorsList;
        }

        private List<double> GetInputDenominator()
        {
            var denominatorList = new List<double>();
            foreach(var element in Denominators.Children)
            {
                denominatorList.Add(double.Parse(((TextBox)element).Text));
            }
            return denominatorList;
        }

        private List<Constraint> GetInputConstraints()
        {
            var constraintsList = new List<Constraint>();
            foreach (var constraint in Constraints.Children)
            {
                var xList = new List<double>();
                foreach (var x in ((StackPanel)((StackPanel)constraint).Children.First()).Children)
                {
                    xList.Add(double.Parse(((TextBox)x).Text));
                }
                constraintsList.Add(new Constraint()
                {
                    Coefficients = xList,
                    FreeValue = double.Parse(((TextBox)((StackPanel)constraint).Children.Last()).Text),
                    SymbolEnum = MapSymbol((string)((ComboBox)((StackPanel)constraint).Children.ElementAt(((StackPanel)constraint).Children.Count - 2)).SelectedItem)
                });
            }
            return constraintsList;
        }

        private List<double> GetInputWs()
        {
            var wsList = new List<double>();
            foreach (var element in Ws.Children)
            {
                wsList.Add(double.Parse(((TextBox)element).Text));
            }
            return wsList;
        }
        private List<double> GetInputLs()
        {
            var lsList = new List<double>();
            foreach (var element in Ls.Children)
            {
                lsList.Add(double.Parse(((TextBox)element).Text));
            }
            return lsList;
        }

        private string GetOptDirecton()
        {
            return OptDirectionComboBox.SelectedItem.ToString();
        }
        public SymbolEnum MapSymbol(string symbol)
        {
            switch (symbol)
            {
                case "=": return SymbolEnum.Equal;
                case "<=": return SymbolEnum.LessOrEqual;
                case ">=": return SymbolEnum.MoreOrEqual;
                default: throw new Exception("Symbol was not defined");
            }
        }

        private void TypeOfInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = TypeOfInput.SelectedIndex;
            switch (index)
            {
                case 0:
                    ManualInput.Visibility = Visibility.Visible;
                    RandomInput.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    ManualInput.Visibility = Visibility.Collapsed;
                    RandomInput.Visibility = Visibility.Visible;
                    break;
                default:
                    ManualInput.Visibility = Visibility.Visible;
                    RandomInput.Visibility = Visibility.Collapsed;
                    break;
            }
            ResultsSaveStackPanel.Visibility = Visibility.Collapsed;
            SolutionTextBlock.Text = string.Empty;
        }

        private void DistributionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(BetaParameters != null)
            {
                int index = DistributionType.SelectedIndex;
                switch (index)
                {
                    case 0:
                        BetaParameters.Visibility = Visibility.Visible;
                        ChiParameters.Visibility = Visibility.Collapsed;
                        GammaParameters.Visibility = Visibility.Collapsed;
                        NormalParameters.Visibility = Visibility.Collapsed;
                        UniformParameters.Visibility = Visibility.Collapsed;
                        break;
                    case 1:
                        BetaParameters.Visibility = Visibility.Collapsed;
                        ChiParameters.Visibility = Visibility.Visible;
                        GammaParameters.Visibility = Visibility.Collapsed;
                        NormalParameters.Visibility = Visibility.Collapsed;
                        UniformParameters.Visibility = Visibility.Collapsed;
                        break;
                    case 2:
                        BetaParameters.Visibility = Visibility.Collapsed;
                        ChiParameters.Visibility = Visibility.Collapsed;
                        GammaParameters.Visibility = Visibility.Visible;
                        NormalParameters.Visibility = Visibility.Collapsed;
                        UniformParameters.Visibility = Visibility.Collapsed;
                        break;
                    case 3:
                        BetaParameters.Visibility = Visibility.Collapsed;
                        ChiParameters.Visibility = Visibility.Collapsed;
                        GammaParameters.Visibility = Visibility.Collapsed;
                        NormalParameters.Visibility = Visibility.Visible;
                        UniformParameters.Visibility = Visibility.Collapsed;
                        break;
                    case 4:
                        BetaParameters.Visibility = Visibility.Collapsed;
                        ChiParameters.Visibility = Visibility.Collapsed;
                        GammaParameters.Visibility = Visibility.Collapsed;
                        NormalParameters.Visibility = Visibility.Collapsed;
                        UniformParameters.Visibility = Visibility.Visible;
                        break;
                }
            }
            
        }

        private void GenerateIndividual_Click(object sender, RoutedEventArgs e)
        {
            var xCount = int.Parse(XCountRandom.Text);
            var alternativesCount = int.Parse(AltCountRandom.Text);
            var constraintsCount = int.Parse(ConstraintCountRandom.Text);
            var optDirection = OptDirectionComboBoxRandom.SelectedIndex == 0 ? "max" : "min";
            var distributionTypeIndex = DistributionType.SelectedIndex;
            CustomDistribution distribution;
            switch (distributionTypeIndex)
            {
                case 0: distribution = new BetaDistribution(double.Parse(BetaA.Text), double.Parse(BetaB.Text)); break;
                case 1: distribution = new ChiDistribution(int.Parse(ChiK.Text)); break;
                case 2: distribution = new GammaDistribution(double.Parse(GammaShape.Text), double.Parse(GammaRate.Text)); break;
                case 3: distribution = new NormalDistribution(double.Parse(NormalMean.Text), double.Parse(NormalStdDev.Text)); break;
                case 4: distribution = new UniformDistribution(double.Parse(UniformMin.Text), double.Parse(UniformMax.Text)); break;
                default: distribution = new NormalDistribution(double.Parse(NormalMean.Text), double.Parse(NormalStdDev.Text)); break;
            }
            _viewModel.GenerateIndividualProblem(xCount, alternativesCount, constraintsCount, optDirection, distribution);
            ResultsSaveStackPanel.Visibility = Visibility.Visible;
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            if(TypeOfInput.SelectedIndex == 0)
            {
                var xCount = int.Parse(XCount.Text);
                var alternativesCount = int.Parse(EnumeratorCount.Text);
                var constraintsCount = int.Parse(ConstraintCount.Text);
                var optDirection = OptDirectionComboBox.SelectedIndex;
                var distributionTypeIndex = -1;
                _viewModel.SaveIndividualProblem(xCount, alternativesCount, constraintsCount, optDirection, distributionTypeIndex,Comment.Text);
            }
            else
            {
                var xCount = int.Parse(XCountRandom.Text);
                var alternativesCount = int.Parse(AltCountRandom.Text);
                var constraintsCount = int.Parse(ConstraintCountRandom.Text);
                var optDirection = OptDirectionComboBoxRandom.SelectedIndex;
                var distributionTypeIndex = DistributionType.SelectedIndex;
                _viewModel.SaveIndividualProblem(xCount, alternativesCount, constraintsCount, optDirection, distributionTypeIndex, Comment.Text);
            }
            var messageDialog = new MessageDialog("Задача успішно збережена");
            await messageDialog.ShowAsync();
        }
    }
}
