using Courses.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Courses.Services
{
    public interface ITraineeService
    {
        Trainee Create(Trainee trainee);
    }
    public class TraineeService : ITraineeService
    {
        private readonly Courses_DBEntities1 courses_DBEntity;

        public TraineeService()
        {
            courses_DBEntity = new Courses_DBEntities1();
        }

        public Trainee Create(Trainee trainee)
        {
            courses_DBEntity.Trainees.Add(trainee);

            int savingResult = courses_DBEntity.SaveChanges();
            if(savingResult > 0)
            {
                return trainee;
            }
            return null;
        }
    }
}