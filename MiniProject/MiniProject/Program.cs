using MiniProject.Enums;
using MiniProject.Models;
using MiniProject.Services;
using System;

namespace MiniProject
{
    class Program
    {
        static void Main(string[] args)
        {
            HumanResourceManagerServices humanmanager = new HumanResourceManagerServices();
            do
            {
                Console.WriteLine("                  ---------------------------EMPLOYEE INFORMATION SYSTEM---------------------------");
                Console.WriteLine("1) Departament Yarat:");
                Console.WriteLine("2) Departamentleri Yoxlayin:");
                Console.WriteLine("3) Departmamentler uzerinde deyisiklikler et:");
                Console.WriteLine("4) Departamentlerin siyahisini cixartmaq:");
                Console.WriteLine("5) Departmentdeki iscilerin siyahisini cixartmaq:");
                Console.WriteLine("6) Isci Yarat:");
                Console.WriteLine("7) Iscilerin siyahisini goster");
                Console.WriteLine("8) Iscilerin uzerinde deyisiklik et!");
                Console.WriteLine("9) Iscilerin silinmesi:");
                string read = Console.ReadLine();
                switch (read)
                {
                    case "1":
                        AddDepartment(ref humanmanager);
                        break;
                    case "2":
                        GetDepartments(ref humanmanager);
                        break;
                    case "3":
                        EditDepartments(ref humanmanager);
                        break;
                    case "4":
                        ShowAllDepartments(ref humanmanager);
                        break;
                    case "5":
                        GetDepartmentEmployee(ref humanmanager);
                        break;
                    case "6":
                        AddEmployee(ref humanmanager);
                        break;
                    case "7":
                        ShowAllEmployees(ref humanmanager);
                        break;
                    case "8":
                        EditEmployee(ref humanmanager);
                        break;
                    case "9":
                        RemoveEmployee(ref humanmanager);
                        break;
                    case "0":

                    default:
                        break;
                }
            } while (true);
        }
        //************DEPARTMENT YARATMAQ UCUN METHOD************
        static void AddDepartment(ref HumanResourceManagerServices humanmanager)
        {
        DepartmentStart:
            Console.WriteLine("Department yaradin:");
            string departmentname = Console.ReadLine();
            if (departmentname.Length < 2)
            {
                Console.WriteLine("Departmentin adi 2 herfden az ola bilmez");
                goto DepartmentStart;
            }
        WorkerLimitStart:
            Console.WriteLine("Isci limiti teyin edin: ");
            byte workerLimit = byte.Parse(Console.ReadLine());
            if (workerLimit < 1)
            {
                Console.WriteLine("Isci sayi 1 neferden az ola bilmez");
                goto WorkerLimitStart;
            }
        SalaryLimitStart:
            Console.WriteLine("Max maas limiti teyin edin:");
            int salaryLimit = int.Parse(Console.ReadLine());
            if (salaryLimit < 250)
            {
                Console.WriteLine("Max maas limiti 250-den az ola bilmez");
                goto SalaryLimitStart;
            }
            humanmanager.AddDepartment(departmentname, workerLimit, salaryLimit); ;
        }
        //*******************

        //*********ISCI YARATMAQ UCUN METHOD*************
        static void AddEmployee(ref HumanResourceManagerServices humanmanager)
        {
        EmployeeNameStart:
            Console.WriteLine("Iscinin Ad ve Soyadini daxil edin:");
            string employeeName = Console.ReadLine();
            if (employeeName.Length < 1)
            {
                Console.WriteLine("Iscinin adi duzgun daxil edilmiyib.");
                goto EmployeeNameStart;
            }
        EmployeeSalaryStart:
            Console.WriteLine("Ne qeder maas teyin edirsiniz?");
            int employeeSalary = int.Parse(Console.ReadLine());
            if (employeeSalary < 250)
            {
                Console.WriteLine("Minimum maas 250 manatdan az ola bilmez!");
                goto EmployeeSalaryStart;
            }
            Console.WriteLine("Hansi departamente elave etmek isteyirsiniz?");
            for (int i = 0; i < humanmanager.Departaments.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {humanmanager.Departaments[i].DepartmentName}");
            }
        Start:
            int temp = int.Parse(Console.ReadLine());
            if (temp < 1 || humanmanager.Departaments.Length < temp)
            {
                Console.WriteLine("Bele bir department yoxdu");
                goto Start;
            }
            Console.WriteLine("Pozisiya secin:");
            string[] position = Enum.GetNames(typeof(PositionType));
            for (int i = 0; i < position.Length; i++)
            {
                Console.WriteLine($"{i + 1}-{position[i]}");
            };
            string typeStr;
            int typeNum;
            Console.WriteLine("Secim edin:");
            typeStr = Console.ReadLine();
            while (!int.TryParse(typeStr, out typeNum) || typeNum < 0 || typeNum > position.Length)
            {
                Console.WriteLine("Bele bir vezife yoxdur!");
                Console.WriteLine("Secim edin:");
                typeStr = Console.ReadLine();
            }
            PositionType positionType = (PositionType)(typeNum);
            humanmanager.AddEmployee(employeeName, positionType, employeeSalary, humanmanager.Departaments[temp - 1]);
        }

        //*******************

        //DEPARTMENTLERIN SIYAHISINI CIXARTMAQ UCUN METHOD
        static void GetDepartments(ref HumanResourceManagerServices humanmanager) 
        {

            humanmanager.GetDepartments();
        }
        //*******************

        //DEPARTMENTDEKI ISCILERIN SIYAHISINI CIXARTMAQ UCUN METHOD
        static void GetDepartmentEmployee(ref HumanResourceManagerServices humanmanager)
        {
            if (humanmanager.Departaments.Length != 0)
            {
                foreach (var item in humanmanager.Departaments)
                {
                    if(item.Employees.Length != 0)
                    {
                        for (int i = 0; i < humanmanager.Departaments.Length; i++)
                        {
                            Console.WriteLine($"{i + 1}-{humanmanager.Departaments[i].DepartmentName}");
                        }
                        Console.WriteLine("Hansi departamenti secmek isteyirsiniz?");
                        int temp = int.Parse(Console.ReadLine());
                        for (int j = 0; j < humanmanager.Departaments[temp - 1].Employees.Length; j++)
                        {
                            if (humanmanager.Departaments[temp - 1].Employees[j] != null)
                            {
                                Console.WriteLine($"{humanmanager.Departaments[temp - 1].Employees[j].EmployeeNo} {humanmanager.Departaments[temp - 1].Employees[j].Fullname} {humanmanager.Departaments[temp - 1].Employees[j].Positon} {humanmanager.Departaments[temp - 1].Employees[j].Salary} ");
                            }
                        }
                    }
                    else
                        Console.WriteLine("Isci teyin olunmayib");
                }
                
            }
            else
            {
                Console.WriteLine("Bele bir department yoxdur!");
            }
        }
        //********************

        //DEPARTAMENTLER UZERINDE DEYISIKLIK ETMEK UCUN METHOD
        static void EditDepartments(ref HumanResourceManagerServices humanmanager)
        {
            if(humanmanager.Departaments.Length != 0)
            {
                Console.WriteLine("Hansi departamentde deyisiklikler etmek isteyirsiniz?");
                for (int i = 0; i < humanmanager.Departaments.Length; i++)
                {
                    Console.WriteLine($"{i + 1} - {humanmanager.Departaments[i].DepartmentName}");
                }
            NewStart:
                int temp = int.Parse(Console.ReadLine());
                if (temp < 1 || humanmanager.Departaments.Length < temp)
                {
                    Console.WriteLine("Bele bir department yoxdu");
                    goto NewStart;
                }
                Console.WriteLine("Yeni ad daxil edin");
            NewDepartmentStart:
                string newName = Console.ReadLine();
                if (newName.Length < 2)
                {
                    Console.WriteLine("Departmentin adi 2 herfden az ola bilmez");
                    goto NewDepartmentStart;
                }
                foreach (var item in humanmanager.Departaments)
                {
                    foreach (var item1 in item.Employees)
                    {
                        if (item1.DepartamentName == humanmanager.Departaments[temp - 1].DepartmentName)
                        {
                            item1.EmployeeNo = item1.EmployeeNo.Replace(item1.EmployeeNo.Substring(0, 2), newName.Substring(0, 2));
                            item1.DepartamentName = newName;
                            return;
                        }
                    }
                }

                humanmanager.EditDepartaments(humanmanager.Departaments[temp - 1].DepartmentName, newName);
            }else
                Console.WriteLine("Ilk once department gir");
            
            
        }
        //*********************

        //DEPARTAMENTLERIN SIYAHISINI CIXARTMAQ UCUN METHOD
        static void ShowAllDepartments(ref HumanResourceManagerServices humanmanager)
        {
            if (humanmanager.Departaments.Length > 0)
            {
                for (int i = 0; i < humanmanager.Departaments.Length; i++)
                {
                    if (humanmanager.Departaments.Length != 0)
                    {
                        Console.WriteLine($"{i + 1} - {humanmanager.Departaments[i].DepartmentName}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Department yoxdur");
                return;
            }
        }
        //*********************

        //ISCILERIN SIYAHISINI CIXARTMAQ UCUN METHOD
        static void ShowAllEmployees(ref HumanResourceManagerServices humanmanager)
        {
            foreach (var item in humanmanager.Departaments)
            {
                if(item.Employees.Length != 0)
                {
                    foreach (var item1 in item.Employees)
                    {
                        if (item1 != null)
                        {
                            Console.WriteLine($"{item1.EmployeeNo} {item1.Fullname} {item1.DepartamentName} {item1.Salary} {item1.Positon}");
                        }
                    }
                }
                else
                    Console.WriteLine("Isci yoxdur");
                
            }
        }
        //*********************

        //ISCILER UZERINDE DEYISIKLIKLER ETMEK UCUN METHOD
        static void EditEmployee(ref HumanResourceManagerServices humanmanager)
        {
            Console.WriteLine("Hansi iscinin uzerinde deyisiklik etmek isteyirsiniz?");
            foreach (var item in humanmanager.Departaments)
            {
                foreach (var item1 in item.Employees)
                {
                    Console.WriteLine($"{item1.EmployeeNo} {item1.Fullname} {item1.Positon} {item1.Salary}");
                }
            }
            string No = Console.ReadLine();
        EmployeeSalaryStart2:
            Console.WriteLine("Ne qeder maas teyin edirsiniz?");
            int employeeSalary = int.Parse(Console.ReadLine());
            if (employeeSalary < 250)
            {
                Console.WriteLine("Minimum maas 250 manatdan az ola bilmez!");
                goto EmployeeSalaryStart2;
            }
            Console.WriteLine("Pozisiyalar:");
            string[] position = Enum.GetNames(typeof(PositionType));
            for (int i = 0; i < position.Length; i++)
            {
                Console.WriteLine($"{i + 1}-{position[i]}");
            };
            string typeStr;
            int typeNum;
            Console.WriteLine("Vezife secin:");
            typeStr = Console.ReadLine();
            while (!int.TryParse(typeStr, out typeNum) || typeNum < 0 || typeNum > position.Length)
            {
                Console.WriteLine("Bele bir vezife yoxdur!");
                Console.WriteLine("Secim edin:");
                typeStr = Console.ReadLine();
            }
            PositionType positionType = (PositionType)(typeNum);
            humanmanager.EditEmployee(No, employeeSalary, positionType);
        }
        //*********************

        //ISCI SILMEK UCUN METHOD
        static void RemoveEmployee(ref HumanResourceManagerServices humanmanager)
        {
            Console.WriteLine("Silinecek iscinin departamenitin daxil edin");
            string DepartmentName = Console.ReadLine();
            Console.WriteLine("Hansi nomreli iscini silmek isteyirsiniz?");
            string No = Console.ReadLine();
            humanmanager.RemoveEmployee(No, DepartmentName);
        }
        //**********************
    }
}
