using REST_Api.Models;
using System.Collections.Generic;
using System.Linq;
using Unity.Attributes;

namespace REST_Api.Repositories
{
    public interface IRepository<TEntity,TPk> where TEntity: class
    {
        IEnumerable<TEntity> Get();
        TEntity Get(TPk id);
        TEntity Create(TEntity entity);
        bool Update(TPk id, TEntity entity);
        bool Delete(TPk id);
    }

    public class EmpRepository : IRepository<EmployeeInfo, int>
    {
        [Dependency]
        public AppDataEntities ctx { get; set; }
        public EmployeeInfo Create(EmployeeInfo entity)
        {
            ctx.Employees.Add(entity);
            ctx.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var res = false;
            var emp = ctx.Employees.Find(id);
            if (emp != null)
            {
                ctx.Employees.Remove(emp);
                ctx.SaveChanges();
                res = true;
            }
            return res;
        }

        public IEnumerable<EmployeeInfo> Get()
        {
            return ctx.Employees.ToList();
        }

        public EmployeeInfo Get(int id)
        {
            var emp = ctx.Employees.Find(id);
            return emp;
        }

        public bool Update(int id, EmployeeInfo entity)
        {
            var res = false;
            var emp = ctx.Employees.Find(id);
            if (emp != null)
            {
                emp.EmpName = entity.EmpName;
                emp.Salary = entity.Salary;
                emp.DeptName = entity.DeptName;
                emp.Designation = entity.Designation;
                ctx.SaveChanges();
                res = true;
            }
            return res;
        }
    }
}