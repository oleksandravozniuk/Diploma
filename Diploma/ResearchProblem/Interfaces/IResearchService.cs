using LiveCharts;
using ProblemSolver.Enums;
using System;
using System.Collections.Generic;
using ProblemSolver;

namespace ProblemResearcher.Interfaces
{
    public interface IResearchService
    {
        ChartValues<double> GetZFromWExperimentResult(Tuple<List<List<double>>, List<double>, List<Constraint>, List<double>, List<double>, OptDirectionEnum> problem, List<Tuple<double, double>> parametrChanges, int experimentsCount);
    }
}
