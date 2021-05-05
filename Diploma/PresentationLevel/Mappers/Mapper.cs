using ProblemSolver.Enums;
using System;

namespace PresentationLevel.Mappers
{
    public class Mapper : IMapper
    {
        public OptDirectionEnum MapOptDirection(string optDirection)
        {
            switch (optDirection)
            {
                case "Max": return OptDirectionEnum.max;
                case "Min": return OptDirectionEnum.min;
                default: throw new Exception("Direction of optimization was not defined");
            }
        }
    }
}
