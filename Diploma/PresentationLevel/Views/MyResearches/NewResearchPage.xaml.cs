using PresentationLevel.Structs;
using ProblemSolver.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ProblemSolver;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PresentationLevel.Views.MyResearches
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewResearchPage : Page
    {
        public NewResearchPage()
        {
            this.InitializeComponent();
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
        }
        private void DecreaseEnumeratorCount_Click(object sender, RoutedEventArgs e)
        {
            Enumerators.Children.Remove(Enumerators.Children.Last());
            EnumeratorCount.Text = Enumerators.Children.Count.ToString();
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

            Ws.Children.Add(GetNewBound());
            Ls.Children.Add(GetNewBound());

            var x = int.Parse(XCount.Text);
            if (x == 0)
            {
                MakeTitlesVisible();
                IncreaseEnumeratorCountButton.Visibility = Visibility.Visible;
                IncreaseConstraintCountButton.Visibility = Visibility.Visible;
                DecreaseEnumeratorCountButton.Visibility = Visibility.Visible;
                DecreaseConstraintCountButton.Visibility = Visibility.Visible;
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

                Ws.Children.Remove(Ws.Children.Last());
                Ls.Children.Remove(Ls.Children.Last());

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

        private void Solve_Click(object sender, RoutedEventArgs e)
        {
            //SolutionTextBlock.Text = _viewModel.SetProblemResult(GetInputEnumerators(), GetInputDenominator(), GetInputConstraints(), GetInputWs(), GetInputLs(), GetOptDirecton());
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

        private List<Tuple<double, double>> GetInputWs()
        {
            var wsList = new List<Tuple<double,double>>();
            foreach (var element in Ws.Children)
            {
                wsList.Add(new Tuple<double,double>(double.Parse(((TextBox)((StackPanel)element).Children[0]).Text), double.Parse(((TextBox)((StackPanel)element).Children[1]).Text)));
            }
            return wsList;
        }
        private List<Tuple<double, double>> GetInputLs()
        {
            var lsList = new List<Tuple<double, double>>();
            foreach (var element in Ws.Children)
            {
                lsList.Add(new Tuple<double, double>(double.Parse(((TextBox)((StackPanel)element).Children[0]).Text), double.Parse(((TextBox)((StackPanel)element).Children[1]).Text)));
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

        }
    }
}
