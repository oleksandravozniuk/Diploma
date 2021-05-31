using ProblemManager.Dtos;
using ProblemManager.Intefaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PresentationLevel.ViewModels
{
    public class MyProblemsViewModel
    {
        private readonly IIndividualProblemsService _individualProblemService;
        public MyProblemsViewModel(IIndividualProblemsService individualProblemService)
        {
            _individualProblemService = individualProblemService;
        }

        public IEnumerable<IndividualProblemDto> GetIndividualProblems()
        {
            return new ObservableCollection<IndividualProblemDto>(_individualProblemService.GetAll());
        }

        public void DeleteIndividualProblem(int id)
        {
            _individualProblemService.Delete(id);
        }
    }
}
