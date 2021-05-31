using ProblemManager.Dtos;
using ProblemSolver;
using ProblemSolver.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProblemManager.Intefaces
{
    public interface IResearchesService
    {
        IEnumerable<ResearchDto> GetAll();
        ResearchDto GetById(int id);
        void Delete(int id);
        void UpdateComment(int id, string comment);
        Task<List<List<double>>> GetZFromWExperimentResult(Tuple<List<List<double>>, List<double>, List<Constraint>, List<double>, List<double>, OptDirectionEnum> problem, List<Tuple<double, double>> parametrChanges, int experimentsCount);
        Task<List<List<double>>> GetZFromLExperimentResultAsync(Tuple<List<List<double>>, List<double>, List<Constraint>, List<double>, List<double>, OptDirectionEnum> problem, List<Tuple<double, double>> parametrChanges, int experimentsCount);
    }
}
