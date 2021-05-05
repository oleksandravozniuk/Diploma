using ProblemSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLevel.Services
{
    public class Research : IResearch
    {
        private readonly ISolveHelper _solveHelper;
        public Research(ISolveHelper solveHelper)
        {
            _solveHelper = solveHelper;
        }

        public List<int> GetDeltas()
        {

        }
    }
}
