using ProblemSolver.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProblemSolver
{
    public class Constraint
    {
        public List<double> Coefficients { get; set; }
        public SymbolEnum SymbolEnum { get; set; }
        public double FreeValue { get; set; }

        public Constraint()
        {

        }

        public Constraint(List<double> coefficients, SymbolEnum symbolEnum, double freeValue)
        {
            Coefficients = coefficients;
            SymbolEnum = symbolEnum;
            FreeValue = freeValue;
        }

        public override string ToString()
        {
            string coefs = "";
            for (int i = 0; i < Coefficients.Count; i++)
            {
                coefs += Coefficients[i] + " ";
            }
            return coefs + SymbolEnum.ToDescriptionString() + " " + FreeValue;
        }

        public List<string> ToStringList()
        {
            var result = new List<string>();
            foreach(var coef in Coefficients)
            {
                result.Add(coef.ToString());
            }
            result.Add(SymbolEnum.ToDescriptionString());
            result.Add(FreeValue.ToString());
            return result;
        }
    }
}
