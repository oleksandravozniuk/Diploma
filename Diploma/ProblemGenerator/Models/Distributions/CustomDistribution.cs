using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemGenerator.Models.Distributions
{
    abstract public class CustomDistribution
    {
        abstract public double Generate();
    }
}
