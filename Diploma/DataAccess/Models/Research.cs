using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Research
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int XCount { get; set; }
        [Required]
        public int ConstraintsCount { get; set; }
        [Required]
        public int OptimizationDirection { get; set; }
        [Required]
        public int AlternativesCount { get; set; }
        public string Comment { get; set; }
        [Required]
        public int ExperimentsCount { get; set; }
        [Required]
        public int Distribution { get; set; }
        [Required]
        public int Type { get; set; }
        [ForeignKey("FileInputResearch")]
        public int FileInputResearchId { get; set; }
        [ForeignKey("FileOutputResearch")]
        public int FileOutputResearchId { get; set; }

        public virtual File FileInputResearch { get; set; }
        public virtual File FileOutputResearch { get; set; }
    }
}
