using MiniProject.Enums;
using MiniProject.Interface;
using MiniProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniProject.Services
{
    class HumanResourceManagerServices : IHumanResourceManager
    {
        private Department[] _departments;
        public Department[] Departaments => _departments;

        public HumanResourceManagerServices()
        {
            _departments = new Department[0];
        }
        public void AddDepartment(string departmentName, byte workerLimit, int salaryLimit)
        {
            Department department = new Department(departmentName, workerLimit, salaryLimit);

            foreach (Department item in Departaments)
            {
                if (item.DepartmentName == department.DepartmentName)
                {
                    Console.WriteLine("Bu departament artiq movcuddur.");
                    return;
                }
            }
            Array.Resize(ref _departments, _departments.Length + 1);
            _departments[_departments.Length - 1] = department;
        }
         
        public void AddEmployee(string fullname, PositionType position, int salary, Department departmentName)
        {
            if (departmentName.Employees.Length < departmentName.WorkerLimit)
            {
                if (departmentName.SalaryLimit < departmentName.CalcSum() + salary)
                {
                    Console.WriteLine("Maas limitini asdiniz");
                }
            }
            else
            {
                Console.WriteLine("Isci limitini asdiniz. Isci elave etmek mumkun deyil");
            }
            Employee employee = new Employee(fullname, position, salary, departmentName);
            departmentName.AddEmployee(employee);
        }

        public void EditDepartaments(string oldName, string newName)
        {
            foreach (var item in Departaments)
            {
                if (newName == item.DepartmentName)
                {
                    Console.WriteLine("Bu adli department movcuddur");
                    return;
                }
                if (item.DepartmentName == oldName)
                {
                    item.DepartmentName = newName;
                    return;
                }
            }
        }
        public void EditEmployee(string No, int salary, PositionType position)
        {
            foreach (var item in Departaments)
            {
                foreach (var item1 in item.Employees)
                {
                    if (No == item1.EmployeeNo)
                    {
                        if (item.SalaryLimit > item.CalcSum() + salary)
                        {
                            item1.Salary = salary;
                            item1.Positon = position;
                            Console.WriteLine("Deyisiklikler ugurlu yerine yetirildi!");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Maas limiti asilmisdir!");
                            return;
                        }

                    }
                }
            }
            Console.WriteLine("Bele bir isci tapilmadi!");

        }
        public Department[] GetDepartments()
        {
            if (_departments.Length != 0)
            {
                SumEmployee();
            }
            else
                Console.WriteLine("Department yoxdur");
            return _departments;
        }
        //GET DEPARTAMENTE LAZIMI MELUMATLARI CIXARTMAQ UCUN METHOD
        public int SumEmployee()
        {
            int count = 0;
            foreach (var item in Departaments)
            {
                foreach (var item1 in item.Employees)
                {
                    int index = Array.IndexOf(item.Employees, item1);
                    if (item.Employees[index] != null)
                    {
                        count++;
                    }

                }
                if (item.Employees.Length != 0)
                {
                    Console.WriteLine($"Departamentin adi {item.DepartmentName}, Iscilerin sayi {count}, Maas ortalamasi {(item.CalcSum() / count)}");
                    count = 0;
                }
                else
                    Console.WriteLine("isci yoxdur");
                
                
            }
            return count;
        }
        //******************
        public void RemoveEmployee(string No, string departmentName)
        {
            Department department = null;
            foreach (var item in Departaments)
            {
                if (item.DepartmentName.ToLower() == departmentName.ToLower())
                {
                    department = item;
                }

            }
            Employee employee = null;
            if (department != null)
            {
                foreach (var item1 in department.Employees)
                {
                    if (item1.EmployeeNo.ToLower() == No.ToLower())
                    {
                        employee = item1;
                    }
                }
            }
            else
            {
                Console.WriteLine("Bele bir department tapilmadi");
                return;
            }
            if (employee != null)
            {
                int index = Array.IndexOf(department.Employees, employee);
                Array.Clear(department.Employees, index, 1);
                Console.WriteLine("Isci ugurla silindi");
            }
            else
            {
                Console.WriteLine("Bele bir isci tapilmadi");
                return;
            }
        }
    }
}
