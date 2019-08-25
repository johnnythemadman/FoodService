using FoodService.DataAccess.DAO.DataAccess;
using FoodService.DAO.DataAccess;
using FoodService.DAO.Database;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FoodService.DataAccess.Test
{
    [TestFixture]
    public class CustomerDaoTest
    {
        private CustomerDao _dao = new CustomerDao();
        private FoodServiceContext _dataSource = new FoodServiceContext();

        [Test]
        public void InsertTest()
        {
            var context = new FoodServiceContext();
            Transaction.WithRollback(context, () =>
            {
                var customer = new Customer
                {
                    Email = "xd@gmail.com",
                    FirstName = "Lolek",
                    LastName = "Heheszek"
                };

                context.Customer.Add(customer);
                context.SaveChanges();

                var result = _dao.SelectById(customer);
                Assert.IsNotNull(result);
            });
;
        }
    }
}