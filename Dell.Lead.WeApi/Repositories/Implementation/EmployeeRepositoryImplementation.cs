using Dell.Lead.WeApi.Exceptions;
using Dell.Lead.WeApi.Models;
using Dell.Lead.WeApi.Models.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dell.Lead.WeApi.Repositories.Implementation
{
    public class EmployeeRepositoryImplementation : IEmployeeRepository
    {
        private readonly SqlServerContext _context;

        public EmployeeRepositoryImplementation(SqlServerContext context)
        {
            _context = context;
        }
        public Employee Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();

            return employee;
        }

        public void Delete(long cpf)
        {
            var result = _context.Employees.SingleOrDefault(e => e.Cpf.Equals(cpf));
            if (result != null)
            {
                try
                {
                    _context.Addresses.Remove(result.Address);
                    _context.Employees.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public List<Employee> FindAll()
        {
            return _context.Employees.Include(a => a.Address).OrderBy(a => a.NameFull).ToList();            
        }

        public Employee FindByCpf(long cpf)
        {
            return _context.Employees.Include(a => a.Address).Where(e => e.Cpf.Equals(cpf)).FirstOrDefault();
        }

        public Employee Update(Employee employee)
        {
            if (Exists(employee.Cpf))
            {
                var result = _context.Employees.Include(a => a.Address).Where(e => e.Cpf.Equals(employee.Cpf)).FirstOrDefault();
                if (result != null)
                {
                    try
                    {
                        _context.Entry(result).CurrentValues.SetValues(employee);
                        _context.Entry(result.Address).CurrentValues.SetValues(employee.Address);
                        _context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    return result;
                }

            }
            return null;
        }

        public bool Exists(long cpf)
        {
            return _context.Employees.Any(e => e.Cpf.Equals(cpf));
        }
    }
}