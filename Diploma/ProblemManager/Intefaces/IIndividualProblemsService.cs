using ProblemManager.Dtos;
using System.Collections.Generic;

namespace ProblemManager.Intefaces
{
    public interface IIndividualProblemsService
    {
        IEnumerable<IndividualProblemDto> GetAll();
        IndividualProblemDto GetById(int id);
        void Delete(int id);
        void UpdateComment(int id, string comment);
        void Save(IndividualProblemDto individualProblemDto);
    }
}
