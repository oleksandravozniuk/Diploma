using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemGenerator.Models.Distributions
{
    public class BetaDistribution : CustomDistribution
    {
        private readonly double a;
        private readonly double b;

        public BetaDistribution(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public override double Generate()
        {
            return Beta.Sample(a, b);
        }
    }
}
