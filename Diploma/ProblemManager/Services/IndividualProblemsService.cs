using DataAccess.Models;
using DatabaseAccess;
using Microsoft.EntityFrameworkCore;
using ProblemManager.Dtos;
using ProblemManager.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemManager.Services
{
    public class IndividualProblemsService : IIndividualProblemsService
    {
        public IEnumerable<IndividualProblemDto> GetAll()
        {
            using (DataContext db = new DataContext())
            {
                var individualProblems = db.IndividualProblems.Include(x => x.FileInputIndividualProblem).Include(x => x.FileOutputIndividualProblem).ToList();
                foreach (var individualProblem in individualProblems)
                {
                    yield return new IndividualProblemDto()
                    {
                        Id = individualProblem.Id,
                        Name = "Problem" + individualProblem.Id,
                        OptimizationDirection = individualProblem.OptimizationDirection,
                        AlternativesCount = individualProblem.AlternativesCount,
                        Comment = individualProblem.Comment,
                        ConstraintsCount = individualProblem.ConstraintsCount,
                        XCount = individualProblem.XCount,
                        FileInputIndividualProblemId = individualProblem.FileInputIndividualProblemId,
                        FileInputIndividualProblemName = individualProblem.FileInputIndividualProblem.Name,
                        FileOutputIndividualProblemId = individualProblem.FileOutputIndividualProblemId,
                        FileOutputIndividualProblemName = individualProblem.FileOutputIndividualProblem.Name
                    };
                }
            }
        }

        public IndividualProblemDto GetById(int id)
        {
            using (DataContext db = new DataContext())
            {
                var individualProblem = db.IndividualProblems.Include(x=>x.FileInputIndividualProblem).Include(x=>x.FileOutputIndividualProblem).Where(x=>x.Id == id).First();

                return new IndividualProblemDto()
                {
                    Id = individualProblem.Id,
                    Name = "Problem" + individualProblem.Id,
                    OptimizationDirection = individualProblem.OptimizationDirection,
                    AlternativesCount = individualProblem.AlternativesCount,
                    Comment = individualProblem.Comment,
                    ConstraintsCount = individualProblem.ConstraintsCount,
                    XCount = individualProblem.XCount,
                    FileInputIndividualProblemId = individualProblem.FileInputIndividualProblemId,
                    FileInputIndividualProblemName = individualProblem.FileInputIndividualProblem.Name,
                    FileOutputIndividualProblemId = individualProblem.FileOutputIndividualProblemId,
                    FileOutputIndividualProblemName = individualProblem.FileOutputIndividualProblem.Name
                };
            }
        }

        public void Delete(int id)
        {
            using (DataContext db = new DataContext())
            {
                var individualProblem = db.IndividualProblems.Where(x => x.Id == id).First();

                db.IndividualProblems.Remove(individualProblem);
                db.SaveChanges();
            }
        }

        public void UpdateComment(int id, string comment)
        {
            using (DataContext db = new DataContext())
            {
                var individualProblem = db.IndividualProblems.Where(x => x.Id == id).First();
                individualProblem.Comment = comment;

                db.IndividualProblems.Update(individualProblem);
                db.SaveChanges();
            }
        }

        public void Save(IndividualProblemDto individualProblemDto)
        {
            var inputFile = new File() { Name = individualProblemDto.FileInputIndividualProblemName };
            var outputFile = new File() { Name = individualProblemDto.FileOutputIndividualProblemName };
            IndividualProblem individualProblem = new IndividualProblem()
            {
                Name = string.Empty,
                OptimizationDirection = individualProblemDto.OptimizationDirection,
                AlternativesCount = individualProblemDto.AlternativesCount,
                Comment = individualProblemDto.Comment,
                ConstraintsCount = individualProblemDto.ConstraintsCount,
                XCount = individualProblemDto.XCount,
                FileInputIndividualProblem = inputFile,
                FileOutputIndividualProblemId = individualProblemDto.FileOutputIndividualProblemId,
                FileOutputIndividualProblem = outputFile
            };
            using (DataContext db = new DataContext())
            {
                db.Files.Add(inputFile);
                db.Files.Add(outputFile);
                db.IndividualProblems.Add(individualProblem);
                db.SaveChanges();
            }
        }
    }
}
