using ProblemSolver.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLevel.Mappers
{
    public interface IMapper
    {
        OptDirectionEnum MapOptDirection(string optDirection);
    }
}
