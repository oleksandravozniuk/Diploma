using CSVManager.Interfaces;
using ProblemManager.Dtos;
using ProblemManager.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLevel.ViewModels
{
    public class MyProblemsListPageViewModel
    {
        private readonly IIndividualProblemsService _individualProblemService;
        private readonly IDataWriter _dataWriter;
        public MyProblemsListPageViewModel(IIndividualProblemsService individualProblemService, IDataWriter dataWriter)
        {
            _individualProblemService = individualProblemService;
            _dataWriter = dataWriter;
        }

        public IndividualProblemDto GetIndividualProblem(int id)
        {
            return _individualProblemService.GetById(id);
        }

        public void LaunchFile(string filename)
        {
            _dataWriter.LaunchCSVFile(filename);
        }
    }
}
