using MiniProject.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProject.Models
{
    class Employee
    {
        public string EmployeeNo;
        public string Fullname;
        public PositionType Positon;
        public int Salary;
        public string DepartamentName;
        private static int Count = 1000;
            
        public Employee( string fullname, PositionType position, int salary, Department departmentname)
        {
            Count++;
            EmployeeNo = departmentname.DepartmentName.Substring(0, 2).ToUpper()+Count;
            Fullname = fullname;
            Positon = position;
            Salary = salary;
            DepartamentName = departmentname.DepartmentName;
        }
    }
}
