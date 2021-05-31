using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemManager.Dtos
{
    public class IndividualProblemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int XCount { get; set; }
        public int ConstraintsCount { get; set; }
        public int OptimizationDirection { get; set; }
        public int AlternativesCount { get; set; }
        public string Comment { get; set; }
        public int FileInputIndividualProblemId { get; set; }
        public string FileInputIndividualProblemName { get; set; }
        public int FileOutputIndividualProblemId { get; set; }
        public string FileOutputIndividualProblemName { get; set; }
    }
}
