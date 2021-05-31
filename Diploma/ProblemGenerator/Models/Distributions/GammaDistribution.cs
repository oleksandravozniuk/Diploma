using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemGenerator.Models.Distributions
{
    public class GammaDistribution : CustomDistribution
    {
        private readonly double shape;
        private readonly double rate;

        public GammaDistribution(double shape, double rate)
        {
            this.shape = shape;
            this.rate = rate;
        }

        public override double Generate()
        {
            return Gamma.Sample(shape, rate);
        }
    }
}
