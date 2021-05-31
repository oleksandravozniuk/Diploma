using CSVManager.Interfaces;
using LiveCharts;
using ProblemGenerator.Interfaces;
using ProblemManager.Intefaces;
using System.Collections.Generic;
using ProblemSolver;
using System;
using ProblemSolver.Enums;
using PresentationLevel.Models;
using System.Linq;
using PresentationLevel.Mappers;

namespace PresentationLevel.ViewModels
{
    public class NewResearchInputViewModel
    {
        private readonly IDataWriter _dataWriter;
        private readonly IGenerateService _generateService;
        private readonly IResearchesService _researchService;
        private readonly IMapper _mapper;
        public string fileInput;
        public string fileOutput;
        public NewResearchInputViewModel(IDataWriter dataWriter, IGenerateService generateService, IResearchesService researchService, IMapper mapper)
        {
            _dataWriter = dataWriter;
            _generateService = generateService;
            _researchService = researchService;
            _mapper = mapper;
        }

        public List<List<ChartValue>> GetZFromWChartValues(List<List<double>> numerators, List<double> denominator, List<Constraint> _constraints, List<double> ls, List<double> ws, string _optDirection, List<Tuple<double, double>> parametrChanges, int experimentsCount)
        {
            var result = new List<List<ChartValue>>();

            OptDirectionEnum optDirection;

            switch (_optDirection)
            {
                case "min": { optDirection = OptDirectionEnum.min; break; }
                case "max": { optDirection = OptDirectionEnum.max; break; }
                default: { optDirection = OptDirectionEnum.max; break; }
            }

            var list = _researchService.GetZFromWExperimentResult(new Tuple<List<List<double>>, List<double>, List<Constraint>, List<double>, List<double>, OptDirectionEnum>(numerators, denominator, _constraints, ls, ws, optDirection), parametrChanges, experimentsCount).Result;
            for(int i=0;i<list.Count;i++)
            {
                result.Add(list[i].Select(y => new ChartValue() { X = i + 1, Y = y }).ToList());
            }
            fileInput = WriteInputToFile(numerators, denominator, _constraints, ls, ws, _optDirection);
            fileOutput = WriteOutputToFile(parametrChanges,result,experimentsCount);
            return result;
        }

        public List<List<ChartValue>> GetZFromLChartValues(List<List<double>> numerators, List<double> denominator, List<Constraint> _constraints, List<double> ls, List<double> ws, string _optDirection, List<Tuple<double, double>> parametrChanges, int experimentsCount)
        {
            var result = new List<List<ChartValue>>();

            OptDirectionEnum optDirection;

            switch (_optDirection)
            {
                case "min": { optDirection = OptDirectionEnum.min; break; }
                case "max": { optDirection = OptDirectionEnum.max; break; }
                default: { optDirection = OptDirectionEnum.max; break; }
            }

            var list = _researchService.GetZFromLExperimentResultAsync(new Tuple<List<List<double>>, List<double>, List<Constraint>, List<double>, List<double>, OptDirectionEnum>(numerators, denominator, _constraints, ls, ws, optDirection), parametrChanges, experimentsCount).Result;
            for (int i = 0; i < list.Count; i++)
            {
                result.Add(list[i].Select(y => new ChartValue() { X = i + 1, Y = y }).ToList());
            }
            return result;
        }

        public string WriteInputToFile(List<List<double>> numerators, List<double> denominator, List<Constraint> constraints, List<double> ls, List<double> ws, string optDirection)
        {
            var filename = "InputResearch_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv";
            _dataWriter.WriteRecordsToCSV(_mapper.MapProblemInputToStringList(numerators, denominator, constraints, ls, ws, optDirection), filename);
            _dataWriter.LaunchCSVFile(filename);
            return filename;
        }

        public string WriteOutputToFile(List<Tuple<double, double>> parametrChanges, List<List<ChartValue>> result, int experimentCount)
        {
            var filename = "OutputResearch_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv";
            _dataWriter.WriteRecordsToCSV(_mapper.MapResearchToCSV(parametrChanges,result,experimentCount), filename);
            _dataWriter.LaunchCSVFile(filename);
            return filename;
        }
    }
}
