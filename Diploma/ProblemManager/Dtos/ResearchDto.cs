using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemManager.Dtos
{
    public class ResearchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int XCount { get; set; }
        public int ConstraintsCount { get; set; }
        public int OptimizationDirection { get; set; }
        public int AlternativesCount { get; set; }
        public string Comment { get; set; }
        public int ExperimentsCount { get; set; }
        public int Distribution { get; set; }
        public int Type { get; set; }
        public int FileInputResearchId { get; set; }
        public string FileInputResearchName { get; set; }
        public int FileOutputResearchId { get; set; }
        public string FileOutputResearchName { get; set; }
    }
}
