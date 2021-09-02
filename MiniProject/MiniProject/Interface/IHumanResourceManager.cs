using MiniProject.Enums;
using MiniProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProject.Interface
{
    interface IHumanResourceManager
    {
        public Department[] Departaments { get; }
        public void AddDepartment(string departmentName, byte workerLimit, int salaryLimit);
        public Department[] GetDepartments();
        public void EditDepartaments(string oldName, string newName);
        public void AddEmployee(string fullname, PositionType position, int salary, Department departmentName);
        public void RemoveEmployee(string No, string departmentName);
        public void EditEmployee(string No,  int salary, PositionType position);
    }
}
