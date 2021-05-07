using ProblemSolver;
using ProblemSolver.Enums;
using System;
using System.Collections.Generic;

namespace PresentationLevel.Services
{
    public class Research : IResearch
    {
        private readonly ISolveHelper _solveHelper;
        public Research(ISolveHelper solveHelper)
        {
            _solveHelper = solveHelper;
        }

        //public List<int> GetDeltas(List<List<double>> numerators, List<double> denominator, List<Constraint> _constraints, Tuple<List<double>, double> ls, Tuple<List<double>, double> ws, OptDirectionEnum _optDirection, int problemsCount = 100)
        //{
        //    var deltas = new List<int>();
        //    for(int i = 0; i<problemsCount; i++)
        //    {

        //    }
        //}
    }
}
