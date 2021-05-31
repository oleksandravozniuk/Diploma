using PresentationLevel.Structs;
using ProblemSolver.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ProblemSolver;
using PresentationLevel.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using LiveCharts;
using LiveCharts.Uwp;
using Syncfusion.UI.Xaml.Charts;
using PresentationLevel.Models;

namespace PresentationLevel.Views.MyResearches
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewResearchPage : Page
    {
        private bool lDependency = true;
        private readonly NewResearchInputViewModel _viewModel;
        private List<ChartValue> StatResults;
        public SeriesCollection SeriesCollection { get; set; }
        public NewResearchPage()
        {
            this.InitializeComponent();
            var container = ((App)Application.Current).Container;
            _viewModel = (NewResearchInputViewModel)ActivatorUtilities.GetServiceOrCreateInstance(container, typeof(NewResearchInputViewModel));
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

            EnumeratorCount.Text = Enumerators.Children.Count.ToString();

            if (lDependency)
            {
                IncreaseXCount(Ws2, 1);
                Ls1.Children.Add(GetNewBound());
            }
            else
            {
                IncreaseXCount(Ls2, 1);
                Ws1.Children.Add(GetNewBound());
            }
        }
        private void DecreaseEnumeratorCount_Click(object sender, RoutedEventArgs e)
        {
            Enumerators.Children.Remove(Enumerators.Children.Last());
            EnumeratorCount.Text = Enumerators.Children.Count.ToString();
            if (lDependency)
            {
                DecreaseXCount(Ws2, 1);
                Ls1.Children.Remove(Ls1.Children.Last());
            }
            else
            {
                DecreaseXCount(Ws2, 1);
                Ls1.Children.Remove(Ls1.Children.Last());
            }  
        }
        private void IncrementX_Click(object sender, RoutedEventArgs e)
        {
            if (Enumerators.Children.Count == 0)
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

            IncreaseXCount(Denominators, 1);

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

            foreach (var constraint in Constraints.Children)
            {
                IncreaseXCount((StackPanel)((StackPanel)constraint).Children.First(), 1);
            }

            var x = int.Parse(XCount.Text);
            if (x == 0)
            {
                MakeTitlesVisible();
                IncreaseEnumeratorCountButton.Visibility = Visibility.Visible;
                IncreaseConstraintCountButton.Visibility = Visibility.Visible;
                DecreaseEnumeratorCountButton.Visibility = Visibility.Visible;
                DecreaseConstraintCountButton.Visibility = Visibility.Visible;
                if (lDependency)
                {
                    IncreaseXCount(Ws2, 1);
                    Ls1.Children.Add(GetNewBound());
                }
                else
                {
                    IncreaseXCount(Ls2, 1);
                    Ws1.Children.Add(GetNewBound());
                }
            }
            XCount.Text = (x + 1).ToString();

            ResearchButton.Visibility = Visibility.Visible;
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
                    if (lDependency)
                    {
                        DecreaseXCount(Ws2, 1);
                        Ls1.Children.Remove(Ls1.Children.Last());
                    }
                    else
                    {
                        DecreaseXCount(Ws2, 1);
                        Ls1.Children.Remove(Ls1.Children.Last());
                    }
                }
                foreach (var enumerator in Enumerators.Children)
                {
                    DecreaseXCount((StackPanel)enumerator, 1);
                }
                DecreaseXCount(Denominators, 1);
                foreach (var constraint in Constraints.Children)
                {
                    DecreaseXCount((StackPanel)((StackPanel)constraint).Children.First(), 1);
                }

                XCount.Text = (x - 1).ToString();
                if (x - 1 == 0)
                {
                    ResearchButton.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void IncreaseXCount(StackPanel stackPanel, int increaseValue)
        {
            for (int i = 0; i < increaseValue; i++)
            {
                stackPanel.Children.Add(new TextBox() { Width = 15, Height = 15, HorizontalAlignment = HorizontalAlignment.Left });
            }
        }
        private void DecreaseXCount(StackPanel stackPanel, int decreaseValue)
        {
            for (int i = 0; i < decreaseValue; i++)
            {
                stackPanel.Children.Remove(stackPanel.Children.Last());
            }
        }
        private void MakeTitlesVisible()
        {
            EnumeratorTitle.Text = "Чисельник:";
            DenominatorTitle.Text = "Знаменник";
            ConstraintTitle.Text = "Обмеження:";
            WeightsTitle.Text = "Ваги";
            LsTitle.Text = "Ls";
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

        private List<List<double>> GetInputEnumerators()
        {
            var enumeratorsList = new List<List<double>>();
            foreach (var enumerator in Enumerators.Children)
            {
                var list = new List<double>();
                foreach (var enumeratorElement in ((StackPanel)enumerator).Children)
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
            foreach (var element in Denominators.Children)
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

        private List<Tuple<double, double>> GetInputWs1()
        {
            var wsList = new List<Tuple<double, double>>();
            foreach (var element in Ws1.Children)
            {
                wsList.Add(new Tuple<double, double>(double.Parse(((TextBox)((StackPanel)element).Children[1]).Text), double.Parse(((TextBox)((StackPanel)element).Children[3]).Text)));
            }
            return wsList;
        }
        private List<Tuple<double, double>> GetInputLs1()
        {
            var lsList = new List<Tuple<double, double>>();
            foreach (var element in Ls1.Children)
            {
                lsList.Add(new Tuple<double, double>(double.Parse(((TextBox)((StackPanel)element).Children[1]).Text), double.Parse(((TextBox)((StackPanel)element).Children[3]).Text)));
            }
            return lsList;
        }

        private List<double> GetInputWs2()
        {
            var wsList = new List<double>();
            foreach (var element in Ws2.Children)
            {
                wsList.Add(double.Parse(((TextBox)element).Text));
            }
            return wsList;
        }
        private List<double> GetInputLs2()
        {
            var lsList = new List<double>();
            foreach (var element in Ls2.Children)
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

        private StackPanel GetNewBound()
        {
            var newSP = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            newSP.Children.Add(new TextBlock()
            {
                Text = "Починаючи з "
            });
            newSP.Children.Add(new TextBox());
            newSP.Children.Add(new TextBlock()
            {
                Text = " із кроком "
            });
            newSP.Children.Add(new TextBox());
            return newSP;
        }

        private void Research_Click(object sender, RoutedEventArgs e)
        {
            int selectedExperimentType = ExperimentTypeComboBox.SelectedIndex;
            var result = new List<List<ChartValue>>();

            switch (selectedExperimentType)
            {
                case 0: result = _viewModel.GetZFromLChartValues(GetInputEnumerators(), GetInputDenominator(), GetInputConstraints(), GetInputWs2(), GetInputLs1().Select(x => x.Item1).ToList(), GetOptDirecton(), GetInputLs1(), int.Parse(ExperimentsCount.Text)); break;
                case 1: result = _viewModel.GetZFromWChartValues(GetInputEnumerators(), GetInputDenominator(), GetInputConstraints(), GetInputWs1().Select(x => x.Item1).ToList(), GetInputLs2(), GetOptDirecton(), GetInputWs1(), int.Parse(ExperimentsCount.Text)); break;
                case 2: result = _viewModel.GetZFromLChartValues(GetInputEnumerators(), GetInputDenominator(), GetInputConstraints(), GetInputWs2(), GetInputLs1().Select(x => x.Item1).ToList(), GetOptDirecton(), GetInputLs1(), int.Parse(ExperimentsCount.Text)); break;
                case 3: result = _viewModel.GetZFromWChartValues(GetInputEnumerators(), GetInputDenominator(), GetInputConstraints(), GetInputWs1().Select(x => x.Item1).ToList(), GetInputLs2(), GetOptDirecton(), GetInputWs1(), int.Parse(ExperimentsCount.Text)); break;
                default: result = _viewModel.GetZFromLChartValues(GetInputEnumerators(), GetInputDenominator(), GetInputConstraints(), GetInputWs2(), GetInputLs1().Select(x => x.Item1).ToList(), GetOptDirecton(), GetInputLs1(), int.Parse(ExperimentsCount.Text)); break;
            }
            //result.Add(new List<ChartValue>() { new ChartValue() { X = 1, Y = 2 }, new ChartValue() { X = 2, Y = 1 } });

            for (int i = 0; i<result.Count;i++)
            {
                if (lDependency)
                {
                    SplineSeries series = new SplineSeries()
                    {
                        ItemsSource = result[i],
                        XBindingPath = "X",
                        YBindingPath = "Y"
                    };
                    Chart.Series.Add(series);
                }
                else
                {
                    SplineSeries series = new SplineSeries()
                    {
                        ItemsSource = result[i],
                        XBindingPath = "X",
                        YBindingPath = "Y"
                    };
                    Chart.Series.Add(series);
                }
            }
        }

        private void ExperimentType_Click(object sender, SelectionChangedEventArgs e)
        {
            int index = ExperimentTypeComboBox.SelectedIndex;
            if (index == 0 || index == 2)
            {
                if(Ws1 != null)
                {
                    Ws1.Visibility = Visibility.Collapsed;
                }
                if (Ws2 != null)
                {
                    Ws2.Visibility = Visibility.Visible;
                }
                if(Ls1 != null)
                {
                    Ls1.Visibility = Visibility.Visible;
                }
                if(Ls2!=null)
                {
                    Ls2.Visibility = Visibility.Collapsed;
                }
                lDependency = true;
            }
            else
            {
                if (Ws1 != null)
                {
                    Ws1.Visibility = Visibility.Visible;
                }
                if (Ws2 != null)
                {
                    Ws2.Visibility = Visibility.Collapsed;
                }
                if (Ls1 != null)
                {
                    Ls1.Visibility = Visibility.Collapsed;
                }
                if (Ls2 != null)
                {
                    Ls2.Visibility = Visibility.Visible;
                }
                lDependency = false;
            }
        }
    }
}
