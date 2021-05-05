using PresentationLevel.Mappers;
using ProblemSolver;
using ProblemSolver.Enums;
using ProblemSolver.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLevel.ViewModels
{
    public class NewProblemInputViewModel
    {
        private readonly ISolveHelper _solveHelper;
        private readonly IMapper _mapper;
        public NewProblemInputViewModel(ISolveHelper solveHelper, IMapper mapper)
        {
            _solveHelper = solveHelper;
            _mapper = mapper;
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
                    str += "Problem" + (i + 1);
                    str += "Optimum: " + result.Item1[i];
                    for (int j = 0; j < result.Item2[i].Count; j++)
                    {
                        str += "x" + (j + 1) + ": " + result.Item2[i][j];
                    }
                }

                str += "Undetermined Problem";
                str += "Optimum: " + result.Item1[alternativesCount];

                for (int j = 0; j < result.Item2[alternativesCount].Count; j++)
                {
                    str += "x" + (j + 1) + ": " + result.Item2[alternativesCount][j];
                }

                for (int j = 0; j < result.Item3[alternativesCount].Count; j++)
                {
                    str += "z" + (j + 1) + ": " + result.Item3[alternativesCount][j];
                }

                for (int j = 0; j < result.Item4.Count; j++)
                {
                    str += "delta" + (j + 1) + ": " + result.Item4[j];
                }

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
    }
}
