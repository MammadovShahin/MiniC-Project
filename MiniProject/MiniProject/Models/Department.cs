using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProject.Models
{
    class Department
    {
        public string DepartmentName; 
        public byte WorkerLimit;
        public int SalaryLimit;
        public Employee[] Employees;

        public Department(string departmentName, byte workerLimit, int salaryLimit )
        {
            DepartmentName = departmentName;
            WorkerLimit = workerLimit;
            SalaryLimit = salaryLimit;
            Employees = new Employee[0];
        }

        public void AddEmployee(Employee employee)
        {

            Array.Resize(ref Employees, Employees.Length + 1);
            Employees[Employees.Length - 1] = employee;

        }
        
        public int CalcSum()
        {
            int Sum = 0;
            foreach (var item in Employees)
            {
                if(item != null)
                {
                    Sum += item.Salary;
                }
            }
            return Sum;
        }
    }
}
