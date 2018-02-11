using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ClientApp.Models;
using ClientApp.Repositories;
namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeInfo model = new EmployeeInfo();
            CallerRepository caller = new CallerRepository();
            try
            {
                //var response = caller.Get();
                //foreach (var res in response)
                //{
                //    Console.WriteLine($"{res.Id} {res.EmpNo} {res.EmpName} {res.Salary} {res.DeptName} {res.Designation}");
                //}

                //var responseGetSingle = caller.Get(1);
                //Console.WriteLine($"{responseGetSingle.Id} {responseGetSingle.EmpNo} {responseGetSingle.EmpName} {responseGetSingle.Salary} {responseGetSingle.DeptName} {responseGetSingle.Designation}");


                var emp = new EmployeeInfo()
                {
                    EmpNo = 107,
                    Salary = 220000,
                    Designation = "Sr.Manager"
                };
                var responsePost = caller.Create(emp);
                Console.WriteLine($"{responsePost.Id} {responsePost.EmpNo} {responsePost.EmpName} {responsePost.Salary} {responsePost.DeptName} {responsePost.Designation}");

                //var emp = new EmployeeInfo()
                //{
                //    Id = 3,
                //    EmpNo = 103,
                //    Salary = 12000,
                //    DeptName = "SL",
                //};

                //var responsePut = caller.Update(3, emp);
                //Console.WriteLine(responsePut);

                //var responseDel = caller.Delete(3);
                //Console.WriteLine(responseDel);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured " +
                    $"{ex.Message}");
            }
            Console.ReadLine();
        }
    }
}
