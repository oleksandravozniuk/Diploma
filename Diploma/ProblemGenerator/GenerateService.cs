using ProblemSolver.Enums;
using System;
using System.Collections.Generic;
using ProblemSolver;
using ProblemGenerator.Interfaces;
using ProblemGenerator.Models.Distributions;

namespace ProblemGenerator
{
    public class GenerateService : IGenerateService
    {
        public Tuple<List<List<double>>, List<double>, List<Constraint>, List<double>, List<double>, OptDirectionEnum> GenerateProblem(int xCount, int alternativesCount, int constraintsCount, string optDirectionString, CustomDistribution distribution)
        {
            //define optimization direction
            OptDirectionEnum optDirection;

            switch (optDirectionString)
            {
                case "min": { optDirection = OptDirectionEnum.min; break; }
                case "max": { optDirection = OptDirectionEnum.max; break; }
                default: { optDirection = GenerateOptDirection(); break; }
            }

            //define ws
            List<double> ws = new List<double>();
            for (int i = 0; i < alternativesCount; i++)
            {
                ws.Add(distribution.Generate());
            }

            //define ls
            List<double> ls = new List<double>();
            for (int i = 0; i < alternativesCount; i++)
            {
                ls.Add(distribution.Generate());
            }

            //define numerators
            List<List<double>> numerators = new List<List<double>>();
            for (int i = 0; i < alternativesCount; i++)
            {
                List<double> c = new List<double>();
                for (int j = 0; j < xCount; j++)
                {
                    c.Add(distribution.Generate());
                }
                numerators.Add(c);
            }

            //define denominator
            List<double> denominator = new List<double>();
            for (int i = 0; i < xCount; i++)
            {
                denominator.Add(distribution.Generate());
            }

            //define constraints
            List<Constraint> constraints = new List<Constraint>();
            for (int i = 0; i < constraintsCount; i++)
            {
                List<double> constrCoefs = new List<double>();
                for (int j = 0; j < xCount; j++)
                {
                    constrCoefs.Add(distribution.Generate());
                }
                double freeValue = distribution.Generate();
                SymbolEnum symbol = GenerateSymbolEnum();
                constraints.Add(new Constraint(constrCoefs, symbol, freeValue));
            }

            return new Tuple<List<List<double>>, List<double>, List<Constraint>, List<double>, List<double>, OptDirectionEnum>(numerators, denominator, constraints, ls, ws, optDirection);
        }



        private OptDirectionEnum GenerateOptDirection()
        {
            Random random = new Random();
            int value = random.Next(2);
            if (value == 0)
                return OptDirectionEnum.min;
            else
                return OptDirectionEnum.max;
        }

        private SymbolEnum GenerateSymbolEnum()
        {
            Random random = new Random();
            int value = random.Next(3);
            if (value == 0)
                return SymbolEnum.LessOrEqual;
            else
                if (value == 1)
                return SymbolEnum.Equal;
            else
                return SymbolEnum.MoreOrEqual;
        }
    }
}
