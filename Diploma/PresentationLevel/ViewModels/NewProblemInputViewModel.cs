using CSVManager.Interfaces;
using PresentationLevel.Mappers;
using ProblemGenerator.Interfaces;
using ProblemGenerator.Models.Distributions;
using ProblemManager.Intefaces;
using ProblemSolver;
using ProblemSolver.Exceptions;
using System;
using System.Collections.Generic;

namespace PresentationLevel.ViewModels
{
    public class NewProblemInputViewModel
    {
        private readonly ISolveHelper _solveHelper;
        private readonly IMapper _mapper;
        private readonly IDataWriter _dataWriter;
        private readonly IGenerateService _generateService;
        private readonly IIndividualProblemsService _individualProblemService;
        public string fileInput;
        public string fileOutput;
        public NewProblemInputViewModel(ISolveHelper solveHelper, IMapper mapper, IDataWriter dataWriter, IGenerateService generateService, IIndividualProblemsService individualProblemService)
        {
            _solveHelper = solveHelper;
            _mapper = mapper;
            _dataWriter = dataWriter;
            _generateService = generateService;
            _individualProblemService = individualProblemService;
        }
        
        public string SetProblemResult(List<List<double>> numerators, List<double> denominator, List<Constraint> _constraints, List<double> ls, List<double> ws, string _optDirection)
        {
            int alternativesCount = numerators.Count;
            var str = string.Empty;
            try
            {
                var result = _solveHelper.SolveProblem(numerators, denominator, _constraints, ls, ws, _mapper.MapOptDirection(_optDirection));
                for (int i = 0; i < alternativesCount; i++)
                {
                    str += "Problem" + (i + 1) + Environment.NewLine;
                    str += "Optimum: " + result.Item1[i] + Environment.NewLine;
                    for (int j = 0; j < result.Item2[i].Count; j++)
                    {
                        str += "x" + (j + 1) + ": " + result.Item2[i][j] + Environment.NewLine;
                    }
                }

                str += "Undetermined Problem" + Environment.NewLine;
                str += "Optimum: " + result.Item1[alternativesCount] + Environment.NewLine;

                for (int j = 0; j < result.Item2[alternativesCount].Count; j++)
                {
                    str += "x" + (j + 1) + ": " + result.Item2[alternativesCount][j] + Environment.NewLine;
                }

                for (int j = 0; j < result.Item3[alternativesCount].Count; j++)
                {
                    str += "z" + (j + 1) + ": " + result.Item3[alternativesCount][j] + Environment.NewLine;
                }

                for (int j = 0; j < result.Item4.Count; j++)
                {
                    str += "delta" + (j + 1) + ": " + result.Item4[j] + Environment.NewLine;
                }
                fileInput = WriteInputToFile(numerators,denominator, _constraints,ls, ws, _optDirection);
                fileOutput = WriteOutputToFile(result.Item1, result.Item2, result.Item3, result.Item4);
                return str;
            }
            catch (Y0IsNullException)
            {
                return "This problem has no solution";
            }
            catch (NoOptimumException)
            {
                return "This problem has no solution";
            }
        }

        public void GenerateIndividualProblem(int xCount, int alternativesCount, int constraintsCount, string optDirectionString, CustomDistribution distribution)
        {
            bool flag = false;
            while(!flag)
            {
                try
                {
                    var problem = _generateService.GenerateProblem(xCount, alternativesCount, constraintsCount, optDirectionString, distribution);
                    var result = _solveHelper.SolveProblem(problem.Item1, problem.Item2, problem.Item3, problem.Item4, problem.Item5, problem.Item6);
                    fileInput = WriteInputToFile(problem.Item1, problem.Item2, problem.Item3, problem.Item4, problem.Item5, problem.Item6.ToString());
                    fileOutput = WriteOutputToFile(result.Item1, result.Item2, result.Item3, result.Item4);
                    flag = true;
                }
                catch (Y0IsNullException)
                {
                    flag = false;
                }
                catch (NoOptimumException)
                {
                    flag = false;
                }
            }
        }

        public string WriteInputToFile(List<List<double>> numerators, List<double> denominator, List<Constraint> constraints, List<double> ls, List<double> ws, string optDirection)
        {
            var filename = "InputIndividual_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv";
            _dataWriter.WriteRecordsToCSV(_mapper.MapProblemInputToStringList(numerators, denominator, constraints, ls, ws, optDirection), filename);
            _dataWriter.LaunchCSVFile(filename);
            return filename;
        }

        public string WriteOutputToFile(List<double> opts, List<List<double>> xs, List<List<double>> zs, List<double> deltas)
        {
            var filename = "OutputIndividual_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv";
            _dataWriter.WriteRecordsToCSV(_mapper.MapProblemOutputToStringList(opts, xs, zs, deltas), filename);
            _dataWriter.LaunchCSVFile(filename);
            return filename;
        }

        public void SaveIndividualProblem(int XCount, int alternativesCount, int constraintsCount, int optDirection, int distributionTypeIndex, string comment)
        {
            _individualProblemService.Save(new ProblemManager.Dtos.IndividualProblemDto()
            {
                OptimizationDirection = optDirection,
                AlternativesCount = alternativesCount,
                Comment = comment,
                ConstraintsCount = constraintsCount,
                XCount = XCount,
                FileInputIndividualProblemName = fileInput,
                FileOutputIndividualProblemName = fileOutput
            });
        }
    }
}
