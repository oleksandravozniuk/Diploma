using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemGenerator.Models.Distributions
{
    public class UniformDistribution : CustomDistribution
    {
        private readonly double min;
        private readonly double max;

        public UniformDistribution(double min, double max)
        {
            this.min = min;
            this.max = max;
        }

        public override double Generate()
        {
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;
        }
    }
}
