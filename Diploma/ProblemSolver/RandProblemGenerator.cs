using ProblemSolver.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProblemSolver
{
    public class RandProblemGenerator
    {
        public Tuple<List<List<double>>, List<double>, List<Constraint>, List<double>, List<double>, OptDirectionEnum> GenerateProblem(List<Tuple<double, double>> coefBounds, List<Tuple<double, double>> bBounds, int constraintCount, string optDirectionString, List<Tuple<double, double>> lsBounds, List<Tuple<double, double>> wsBounds)
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
            for (int i = 0; i < wsBounds.Count; i++)
            {
                ws.Add(DoubleRand(wsBounds[i].Item1, wsBounds[i].Item2));
            }

            //define ls
            List<double> ls = new List<double>();
            for (int i = 0; i < lsBounds.Count; i++)
            {
                ls.Add(DoubleRand(lsBounds[i].Item1, lsBounds[i].Item2));
            }

            //define numerators
            List<List<double>> numerators = new List<List<double>>();
            for (int i = 0; i < wsBounds.Count; i++)
            {
                List<double> c = new List<double>();
                for (int j = 0; j < coefBounds.Count; j++)
                {
                    c.Add(DoubleRand(coefBounds[j].Item1, coefBounds[j].Item2));
                }
                numerators.Add(c);
            }

            //define denominator
            List<double> denominator = new List<double>();
            for (int i = 0; i < coefBounds.Count; i++)
            {
                denominator.Add(DoubleRand(coefBounds[i].Item1, coefBounds[i].Item2));
            }

            //define constraints
            List<Constraint> constraints = new List<Constraint>();
            for (int i = 0; i < constraintCount; i++)
            {
                List<double> constrCoefs = new List<double>();
                for (int j = 0; j < coefBounds.Count; j++)
                {
                    constrCoefs.Add(DoubleRand(coefBounds[j].Item1, coefBounds[j].Item2));
                }
                double freeValue = DoubleRand(bBounds[i].Item1, bBounds[i].Item2);
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

        private double DoubleRand(double min, double max)
        {
            Random random = new Random();
            return min + (random.NextDouble() * (max - min));
        }
    }
}
