using ProblemSolver.Enums;
using System;
using System.Collections.Generic;
using ProblemSolver;
using PresentationLevel.Models;

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

        public IEnumerable<IEnumerable<string>> MapProblemInputToStringList(List<List<double>> numerators, List<double> denominator, List<Constraint> constraints, List<double> ls, List<double> ws, string _optDirection)
        {
            var result = new List<List<string>>();
            
            for(int i = 0;i<numerators.Count;i++)
            {
                var line = new List<string>();
                for(int j=0;j<numerators[i].Count;j++)
                {
                    line.Add(numerators[i][j].ToString());
                }

                foreach(var coef in denominator)
                {
                    line.Add(coef.ToString());
                }

                foreach(var constr in constraints)
                {
                    line.AddRange(constr.ToStringList());
                }
                
                line.Add(ls[i].ToString());
                line.Add(ws[i].ToString());
                line.Add(_optDirection);
                result.Add(line);
            }
            return result;
        }

        public IEnumerable<IEnumerable<string>> MapProblemOutputToStringList(List<double> opts, List<List<double>> xs, List<List<double>> zs, List<double> deltas)
        {
            var result = new List<List<string>>();
            for (int i = 0; i<xs.Count;i++)
            {
                var line = new List<string>();
                line.Add(opts[i].ToString());
                for(int j=0;j<xs[i].Count;j++)
                {
                    line.Add(xs[i][j].ToString());
                }
                if(i==xs.Count-1)
                {
                    foreach(var z in zs[xs.Count-1])
                    {
                        line.Add(z.ToString());
                    }
                    foreach(var delta in deltas)
                    {
                        line.Add(delta.ToString());
                    }
                }
                result.Add(line);
            }
            return result;
        }

        public IEnumerable<IEnumerable<string>> MapResearchToCSV(List<Tuple<double, double>> parametersChange, List<List<ChartValue>> result, int experimentCount)
        {
            var csv = new List<List<string>>();
            for(int i=0;i<experimentCount;i++)
            {
                var list = new List<string>();
                for(int j=0;j<parametersChange.Count;j++)
                {
                    list.Add((parametersChange[j].Item1 + (i * parametersChange[j].Item2)).ToString());
                }
                for(int k=0;k<result.Count;k++)
                {
                    list.Add(result[k][i].Y.ToString());
                }
                csv.Add(list);
            }
            return csv;
        }
    }
}
