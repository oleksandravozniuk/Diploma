using DatabaseAccess;
using ProblemManager.Dtos;
using ProblemManager.Intefaces;
using System.Collections.Generic;
using System.Linq;
using LiveCharts;
using ProblemSolver;
using ProblemSolver.Enums;
using System;
using ProblemSolver.Exceptions;
using Windows.UI.Popups;
using System.Threading.Tasks;

namespace ProblemManager.Services
{
    public class ResearchesService : IResearchesService
    {
        private SolveHelper solveHelper = new SolveHelper();
        public IEnumerable<ResearchDto> GetAll()
        {
            using (DataContext db = new DataContext())
            {
                var individualProblems = db.Researches.ToList();
                foreach (var research in individualProblems)
                {
                    yield return new ResearchDto()
                    {
                        Id = research.Id,
                        Name = research.Name,
                        OptimizationDirection = research.OptimizationDirection,
                        AlternativesCount = research.AlternativesCount,
                        Comment = research.Comment,
                        ConstraintsCount = research.ConstraintsCount,
                        XCount = research.XCount,
                        Distribution = research.Distribution,
                        ExperimentsCount = research.ExperimentsCount,
                        Type = research.Type,
                        FileInputResearchId = research.FileInputResearchId,
                        FileInputResearchName = research.FileInputResearch.Name,
                        FileOutputResearchId = research.FileOutputResearchId,
                        FileOutputResearchName = research.FileOutputResearch.Name
                    };
                }
            }
        }

        public ResearchDto GetById(int id)
        {
            using (DataContext db = new DataContext())
            {
                var research = db.Researches.Where(x => x.Id == id).First();

                return new ResearchDto()
                {
                    Id = research.Id,
                    Name = research.Name,
                    OptimizationDirection = research.OptimizationDirection,
                    AlternativesCount = research.AlternativesCount,
                    Comment = research.Comment,
                    ConstraintsCount = research.ConstraintsCount,
                    XCount = research.XCount,
                    FileInputResearchId = research.FileInputResearchId,
                    FileInputResearchName = research.FileInputResearch.Name,
                    FileOutputResearchId = research.FileOutputResearchId,
                    FileOutputResearchName = research.FileOutputResearch.Name
                };
            }
        }

        public void Delete(int id)
        {
            using (DataContext db = new DataContext())
            {
                var research = db.Researches.Where(x => x.Id == id).First();

                db.Researches.Remove(research);
            }
        }

        public void UpdateComment(int id, string comment)
        {
            using (DataContext db = new DataContext())
            {
                var research = db.Researches.Where(x => x.Id == id).First();
                research.Comment = comment;

                db.Researches.Update(research);
            }
        }

        public async Task<List<List<double>>> GetZFromWExperimentResult(Tuple<List<List<double>>, List<double>, List<Constraint>, List<double>, List<double>, OptDirectionEnum> problem, List<Tuple<double, double>> parametrChanges, int experimentsCount)
        {
            var zs = new List<List<double>>();
            try
            {
                for (int k = 0; k < problem.Item1.Count; k++)
                {
                    var z = new List<double>();
                    for (int i = 0; i < experimentsCount; i++)
                    {
                        for (int j = 0; j < problem.Item1.Count; j++)
                        {
                            problem.Item5[j] = parametrChanges[j].Item1 + (i * parametrChanges[j].Item2);
                        }
                        z.Add(solveHelper.SolveProblem(problem.Item1, problem.Item2, problem.Item3, problem.Item4, problem.Item5, problem.Item6).Item3[problem.Item1.Count][k]);
                    }
                    zs.Add(z);
                }
                return zs;
            }
            catch (Y0IsNullException)
            {
                var messageDialog = new MessageDialog("Задача не має розв'язку");
                await messageDialog.ShowAsync();
                return zs;
            }
            catch (NoOptimumException)
            {
                var messageDialog = new MessageDialog("Задача не має розв'язку");
                await messageDialog.ShowAsync();
                return zs;
            }
        }

        public async Task<List<List<double>>> GetZFromLExperimentResultAsync(Tuple<List<List<double>>, List<double>, List<Constraint>, List<double>, List<double>, OptDirectionEnum> problem, List<Tuple<double, double>> parametrChanges, int experimentsCount)
        {
            var zs = new List<List<double>>();
            try
            {
                for (int k = 0; k < problem.Item1.Count; k++)
                {
                    var z = new List<double>();
                    for (int i = 0; i < experimentsCount; i++)
                    {
                        for (int j = 0; j < problem.Item1.Count; j++)
                        {
                            problem.Item4[j] = parametrChanges[j].Item1 + (i * parametrChanges[j].Item2);
                        }
                        z.Add(solveHelper.SolveProblem(problem.Item1, problem.Item2, problem.Item3, problem.Item4, problem.Item5, problem.Item6).Item3[problem.Item1.Count][k]);
                    }
                    zs.Add(z);
                }
                return zs;
            }
            catch (Y0IsNullException)
            {
                var messageDialog = new MessageDialog("Задача не має розв'язку");
                await messageDialog.ShowAsync();
                return zs;
            }
            catch (NoOptimumException)
            {
                var messageDialog = new MessageDialog("Задача не має розв'язку");
                await messageDialog.ShowAsync();
                return zs;
            }
           
        }
    }
}
