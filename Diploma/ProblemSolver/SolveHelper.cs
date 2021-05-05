using Google.OrTools.LinearSolver;
using ProblemSolver.Enums;
using ProblemSolver.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProblemSolver
{
    public class SolveHelper : ISolveHelper
    {
        public Tuple<List<double>, List<List<double>>, List<List<double>>, List<double>> SolveProblem(List<List<double>> numerators, List<double> denominator, List<Constraint> _constraints, List<double> ls, List<double> ws, OptDirectionEnum _optDirection)
        {
            var fs = new List<double>();
            var xs = new List<List<double>>();
            var zs = new List<List<double>>();
            var deltas = new List<double>();
            var constraints = ConvertToDualConstraints(_constraints, denominator);

            for (int i = 0; i < numerators.Count; i++)
            {
                List<double> func = new List<double>
                {
                    0
                };
                func.AddRange(numerators[i]);
                try
                {
                    Tuple<double, List<double>, List<double>> problem1 = Solve(
                     constraints,
                      func,
                     _optDirection,
                     denominator.Count,
                        0);
                    fs.Add(problem1.Item1);
                    xs.Add(problem1.Item2);
                    zs.Add(problem1.Item3);
                }
                catch (NoOptimumException ex)
                {
                    throw ex;
                }
                catch (Y0IsNullException ex)
                {
                    throw ex;
                }
            }

            List<double> fun = new List<double>();
            for (int i = 0; i < denominator.Count + 1; i++)
            {
                fun.Add(0);
            }
            fun.AddRange(ws);

            try
            {
                Tuple<double, List<double>, List<double>> problem = Solve(
              ConvertToDualConstraintsWithZ(constraints, numerators, ls, fs, _optDirection),
              fun,
              OptDirectionEnum.min,
              denominator.Count,
              ls.Count);

                fs.Add(problem.Item1);
                xs.Add(problem.Item2);
                zs.Add(problem.Item3);

                deltas = FindDeltas(problem.Item2, numerators, denominator, fs);
            }
            catch (NoOptimumException ex)
            {
                throw ex;
            }
            catch (Y0IsNullException ex)
            {
                throw ex;
            }


            return new Tuple<List<double>, List<List<double>>, List<List<double>>, List<double>>(fs, xs, zs, deltas);
        }

        private List<double> FindDeltas(List<double> xs, List<List<double>> numerators, List<double> denominator, List<double> fOpts)
        {
            var deltas = new List<double>();

            double denominatorValue = 0.0;
            for (int k = 0; k < denominator.Count; k++)
            {
                denominatorValue += (denominator[k] * xs[k]);
            }
            for (int i = 0; i < numerators.Count; i++)
            {
                double numeratorValue = 0.0;
                for (int j = 0; j < numerators[i].Count; j++)
                {
                    numeratorValue += (numerators[i][j] * xs[j]);
                }
                deltas.Add((numeratorValue / denominatorValue) - fOpts[i]);
            }
            return deltas;
        }
        private List<Constraint> ConvertToDualConstraintsWithZ(List<Constraint> _constraints, List<List<double>> numerators, List<double> ls, List<double> fOpts, OptDirectionEnum optDirection)
        {
            List<Constraint> constraintStructs = new List<Constraint>();
            for (int i = 0; i < _constraints.Count; i++)
            {
                for (int j = 0; j < numerators.Count; j++)
                {
                    _constraints[i].Coefficients.Add(0);
                }
                constraintStructs.Add(_constraints[i]);
            }
            for (int i = 0; i < numerators.Count; i++)
            {
                var coefs = new List<double>();
                coefs.Add(0);
                coefs.AddRange(numerators[i]);
                for (int j = 0; j < numerators.Count; j++)
                {
                    if (i == j)
                    {
                        if (optDirection == OptDirectionEnum.min)
                        {
                            coefs.Add(-1);
                        }
                        else
                        {
                            coefs.Add(1);
                        }
                    }
                    else
                    {
                        coefs.Add(0);
                    }
                }
                if (optDirection == OptDirectionEnum.min)
                {
                    constraintStructs.Add(new Constraint(coefs, SymbolEnum.LessOrEqual, ls[i] + fOpts[i]));
                }
                else
                {
                    constraintStructs.Add(new Constraint(coefs, SymbolEnum.MoreOrEqual, fOpts[i] - ls[i]));
                }

            }
            return constraintStructs;
        }
        private List<Constraint> ConvertToDualConstraints(List<Constraint> _constraints, List<double> denominators)
        {
            List<Constraint> constraintStructs = new List<Constraint>();
            for (int i = 0; i < _constraints.Count; i++)
            {
                List<double> coefs = new List<double>();
                coefs.Add(_constraints[i].FreeValue * (-1));
                for (int j = 0; j < _constraints[i].Coefficients.Count; j++)
                {
                    coefs.Add(_constraints[i].Coefficients[j]);
                }
                constraintStructs.Add(new Constraint(coefs, _constraints[i].SymbolEnum, 0));
            }
            var coefs2 = new List<double>();
            coefs2.Add(0);
            coefs2.AddRange(denominators);
            constraintStructs.Add(new Constraint(coefs2, SymbolEnum.Equal, 1));
            return constraintStructs;
        }

        private Tuple<double, List<double>, List<double>> Solve(List<Constraint> _constraints, List<double> _function, OptDirectionEnum _optDirection, int xCount, int zCount)
        {
            Solver solver = Solver.CreateSolver("GLOP");

            List<Variable> variables = new List<Variable>();
            //add y
            for (int i = 0; i < _function.Count; i++)
            {
                variables.Add(solver.MakeNumVar(0.0, double.PositiveInfinity, "y" + i));
            }

            List<Google.OrTools.LinearSolver.Constraint> constraints = CreateConstraints(solver, _constraints);
            SetConstraints(constraints, _constraints, variables);

            //setup function
            Objective objective = solver.Objective();
            for (int i = 0; i < _function.Count; i++)
            {
                objective.SetCoefficient(variables[i], _function[i]);
            }
            if (_optDirection == OptDirectionEnum.max)
            {
                objective.SetMaximization();
            }
            else
            {
                objective.SetMinimization();
            }


            Solver.ResultStatus resultStatus = solver.Solve();

            // Check that the problem has an optimal solution.
            if (resultStatus != Solver.ResultStatus.OPTIMAL)
            {
                throw new NoOptimumException();
                //Console.WriteLine("The problem does not have an optimal solution!");
            }

            if (variables[0].SolutionValue() == 0)
            {
                throw new Y0IsNullException();
            }

            List<double> ys = new List<double>();


            for (int i = 0; i < xCount + 1; i++)
            {
                ys.Add(variables[i].SolutionValue());
            }
            List<double> zs = new List<double>();
            for (int i = 0; i < zCount; i++)
            {
                zs.Add(variables[xCount + 1 + i].SolutionValue());
            }

            return new Tuple<double, List<double>, List<double>>(solver.Objective().Value(), ConvertToX(ys), zs);
        }

        private List<Google.OrTools.LinearSolver.Constraint> CreateConstraints(Solver solver, List<Constraint> _constraints)
        {
            //create constraints
            List<Google.OrTools.LinearSolver.Constraint> constraints = new List<Google.OrTools.LinearSolver.Constraint>();
            for (int i = 0; i < _constraints.Count; i++)
            {
                Google.OrTools.LinearSolver.Constraint c;
                SymbolEnum symbolEnum = _constraints[i].SymbolEnum;
                switch (symbolEnum)
                {
                    case SymbolEnum.Equal: { c = solver.MakeConstraint(_constraints[i].FreeValue, _constraints[i].FreeValue); constraints.Add(c); break; }
                    case SymbolEnum.LessOrEqual: { c = solver.MakeConstraint(double.NegativeInfinity, _constraints[i].FreeValue); constraints.Add(c); break; }
                    case SymbolEnum.MoreOrEqual: { c = solver.MakeConstraint(_constraints[i].FreeValue, double.PositiveInfinity); constraints.Add(c); break; }
                }
            }
            return constraints;
        }

        private void SetConstraints(List<Google.OrTools.LinearSolver.Constraint> constraints, List<Constraint> constraints2, List<Variable> variables)
        {
            for (int i = 0; i < constraints.Count; i++)
            {
                for (int j = 0; j < variables.Count; j++)
                {
                    constraints[i].SetCoefficient(variables[j], constraints2[i].Coefficients[j]);
                }
            }
        }

        private List<double> ConvertToX(List<double> ys)
        {
            List<double> xs = new List<double>();
            for (int i = 0; i < ys.Count - 1; i++)
            {
                xs.Add(ys[i + 1] / ys[0]);
            }

            return xs;
        }

    }
}
