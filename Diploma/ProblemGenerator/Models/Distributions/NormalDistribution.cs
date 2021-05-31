using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemGenerator.Models.Distributions
{
    public class NormalDistribution : CustomDistribution
    {
        private readonly double mean;
        private readonly double stddev;

        public NormalDistribution(double mean, double stddev)
        {
            this.mean = mean;
            this.stddev = stddev;
        }

        public override double Generate()
        {
            return Normal.Sample(mean, stddev);
        }
    }
}
