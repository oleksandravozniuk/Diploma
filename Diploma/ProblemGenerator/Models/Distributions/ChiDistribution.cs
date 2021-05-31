using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemGenerator.Models.Distributions
{
    public class ChiDistribution : CustomDistribution
    {
        private readonly int k;

        public ChiDistribution(int k)
        {
            this.k = k;
        }

        public override double Generate()
        {
            return Chi.Sample(k);
        }
    }
}
