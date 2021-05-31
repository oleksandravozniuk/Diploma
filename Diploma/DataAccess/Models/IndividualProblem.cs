using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class IndividualProblem
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
        [ForeignKey("FileInputIndividualProblem")]
        public int FileInputIndividualProblemId { get; set; }
        [ForeignKey("FileOutputIndividualProblem")]
        public int FileOutputIndividualProblemId { get; set; }

        public virtual File FileInputIndividualProblem { get; set; }
        public virtual File FileOutputIndividualProblem { get; set; }
    }
}
