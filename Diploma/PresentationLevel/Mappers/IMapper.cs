using ProblemSolver.Enums;
using System.Collections.Generic;
using ProblemSolver;
using System;
using PresentationLevel.Models;

namespace PresentationLevel.Mappers
{
    public interface IMapper
    {
        OptDirectionEnum MapOptDirection(string optDirection);
        IEnumerable<IEnumerable<string>> MapProblemInputToStringList(List<List<double>> numerators, List<double> denominator, List<Constraint> constraints, List<double> ls, List<double> ws, string _optDirection);
        IEnumerable<IEnumerable<string>> MapProblemOutputToStringList(List<double> opts, List<List<double>> xs, List<List<double>> zs, List<double> deltas);
        IEnumerable<IEnumerable<string>> MapResearchToCSV(List<Tuple<double, double>> parametersChange, List<List<ChartValue>> result, int experimentCount);
    }
}
