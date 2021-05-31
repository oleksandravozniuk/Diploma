using ProblemGenerator.Models.Distributions;
using ProblemSolver;
using ProblemSolver.Enums;
using System;
using System.Collections.Generic;

namespace ProblemGenerator.Interfaces
{
    public interface IGenerateService
    {
        Tuple<List<List<double>>, List<double>, List<Constraint>, List<double>, List<double>, OptDirectionEnum> GenerateProblem(int xCount, int alternativesCount, int constraintsCount, string optDirectionString, CustomDistribution distribution);
    }
}
