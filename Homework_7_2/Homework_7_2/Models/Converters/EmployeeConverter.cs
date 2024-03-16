using Homework_7_2.Models.Domains;
using Homework_7_2.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_7_2.Models.Converters
{
    public static class EmployeeConverter
    {
        public static EmployeeWrapper ToWrapper(this Employee model)
        {
            return new EmployeeWrapper()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Number = model.Number,
                HireDate = model.HireDate,
                DismissDate = model.DismissDate,
                Salary = model.Salary,
                Bonus = model.Bonus,
                Comments = model.Comments,

                Status = new StatusWrapper()
                {
                    Id = model.Status.Id,
                    Name = model.Status.Name,
                }
            };
        }

        public static Employee ToDao(this EmployeeWrapper model)
        {
            return new Employee()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Number = model.Number,
                HireDate = model.HireDate,
                DismissDate = model.DismissDate,
                Salary = model.Salary,
                Bonus = model.Bonus,
                Comments = model.Comments,
                StatusId = model.Status.Id
            };
        }

    }
}
