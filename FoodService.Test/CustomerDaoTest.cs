using FoodService.DAO.DataAccess;
using FoodService.DAO.Database;
using NUnit.Framework;

namespace FoodService.Test
{
    [TestFixture]
    public class CustomerDaoTest
    {
        private CustomerDao _dao = new CustomerDao();
        private FoodServiceContext _dataSource = new FoodServiceContext();

        [Test]
        public void InsertTest()
        {
            var customer = new Customer
            {
                CustomerId = -1,
                Email = "xd@gmail.com",
                FirstName = "Lolek",
                LastName = "Heheszek"
            };

            _dao.Insert(customer);

            var result = _dao.SelectById(customer);
            Assert.IsNotNull(result);
        }
    }
}