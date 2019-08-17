using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.DAO.Database;
using Microsoft.EntityFrameworkCore;

namespace FoodService.DAO.DataAccess
{
    public class CustomerDao : IBaseDao<Customer>
    {
        private readonly FoodServiceContext _dataSource;
        private readonly DbSet<Customer> _table;


        public CustomerDao()
        {
            _dataSource = new FoodServiceContext();
            _table = _dataSource.Customer;
        }

        public void Delete(Customer dto) => Delete(new List<Customer>{dto});

        public void Delete(IList<Customer> dtos)
        {
            _dataSource.Customer.AddRange(dtos);
            _dataSource.SaveChanges();
        }

        public void Insert(Customer dto) => Insert(new List<Customer> { dto });

        public void Insert(IList<Customer> dtos)
        {
            _dataSource.Customer.AddRange(dtos);
            _dataSource.SaveChanges();
        }

        public Customer SelectById(Customer dto)
        {
            return _table.FirstOrDefault(x => x.CustomerId == dto.CustomerId);
        }


        public void Update(Customer dto)
        {
            var before = SelectById(dto);
            if (before == null) throw new ArgumentException("Not found in database.");
            _table.Update(dto);
            _dataSource.SaveChanges();
        }

        public void Update(IList<Customer> dtos)
        {
            foreach (var dto in dtos)
            {
                //context.Entry(existingBlog).State = EntityState.Modified;
                _table.Update(dto);
            }

            _dataSource.SaveChanges();
        }
    }
}
