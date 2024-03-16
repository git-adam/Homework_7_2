using Homework_7_2.Models.Converters;
using Homework_7_2.Models.Domains;
using Homework_7_2.Models.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Windows;
using System;

namespace Homework_7_2
{
    public class Repository
    {
        //???
        private const int DisimissId = 4;
        //???

        public List<Status> GetStatuses()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Statuses.ToList();
            }
        }

        public EmployeeWrapper GetEmployee(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Employees
                    .Include(x => x.Status)
                    .FirstOrDefault(x => x.Id == id)
                    .ToWrapper();
            }
        }

        public void DeleteEmployee(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var employeeToDelete = context.Employees.Find(id);
                context.Employees.Remove(employeeToDelete);
                context.SaveChanges();
            }
        }

        public void DeleteEmployees()
        {
            using (var context = new ApplicationDbContext())
            {
                var employeesToDelete = context.Employees.ToList();
                context.Employees.RemoveRange(employeesToDelete);
                context.SaveChanges();
            }
        }

        public List<EmployeeWrapper> GetEmployees(int statusId)
        {
            using (var context = new ApplicationDbContext())
            {
                var employees = context
                    .Employees
                    .Include(x => x.Status)
                    .AsQueryable();

                if (statusId != 0)
                {
                    employees = employees.Where(x => x.StatusId == statusId);
                }

                return employees
                    .ToList()
                    .Select(x => x.ToWrapper())
                    .ToList();
            }
        }

        public void DismissEmployee(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var employeeToDismiss = context.Employees.Find(id);
                employeeToDismiss.DismissDate = DateTime.Now;
                employeeToDismiss.StatusId = context.Statuses.FirstOrDefault(x => x.Id == DisimissId).Id;
                context.SaveChanges();
            }
        }
        public void UpdateEmployee(EmployeeWrapper employeeWrapper)
        {
            var employee = employeeWrapper.ToDao();

            using (var context = new ApplicationDbContext())
            {
                UpdateEmployeeProperties(context, employee);
                context.SaveChanges();
            }
        }

        private void UpdateEmployeeProperties(ApplicationDbContext context, Employee employee)
        {
            if (employee.StatusId == DisimissId)
            {
                if (employee.DismissDate == null)
                    employee.DismissDate = DateTime.Now;
            }
            else
                employee.DismissDate = null;

            var employeeToUpdate = context.Employees.Find(employee.Id);
            employeeToUpdate.FirstName = employee.FirstName;
            employeeToUpdate.LastName = employee.LastName;
            employeeToUpdate.Number = employee.Number;
            employeeToUpdate.HireDate = employee.HireDate;
            employeeToUpdate.DismissDate = employee.DismissDate;
            employeeToUpdate.Bonus = employee.Bonus;
            employeeToUpdate.Comments = employee.Comments;
            employeeToUpdate.StatusId = employee.StatusId;
        }

        public void AddEmployee(EmployeeWrapper employeeWrapper)
        {
            var employee = employeeWrapper.ToDao();

            using (var context = new ApplicationDbContext())
            {
                if (employee.StatusId == DisimissId)
                    employee.DismissDate = DateTime.Now;

                context.Employees.Add(employee);
                context.SaveChanges();
            }
        }

        public void AddRecordToStatusesTable(StatusWrapper statusWrapper)
        {
            var status = statusWrapper.ToDao();

            using (var context = new ApplicationDbContext())
            {
                context.Statuses.Add(status);
                context.SaveChanges();
            }
        }

    }
}
