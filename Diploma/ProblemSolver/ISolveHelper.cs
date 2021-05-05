using ProblemSolver.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProblemSolver
{
    public interface ISolveHelper
    {
        Tuple<List<double>, List<List<double>>, List<List<double>>, List<double>> SolveProblem(List<List<double>> numerators, List<double> denominators, List<Constraint> _constraints, List<double> ls, List<double> ws, OptDirectionEnum _optDirection);
    }
}
