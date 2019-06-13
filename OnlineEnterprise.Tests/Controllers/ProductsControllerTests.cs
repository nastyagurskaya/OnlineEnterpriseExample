using System;
using NSubstitute;
using NUnit.Framework;
using OnlineEnterprice.Domain.Entities;
using OnlineEnterprise.Data.Interfaces;
using OnlineEnterprise.Web.Controllers;

namespace OnlineEnterprise.Tests
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private ProductsController _productsController;
        private IMongoRepository<Product> _productRpository;
        private string _id = "507f191e810c19729de860ea";

        [SetUp]
        public void SetUp()
        {
            _productRpository = Substitute.For<IMongoRepository<Product>>();
            _productsController = new ProductsController(_productRpository);
        }

        [Test]
        public void ShouldGetProduct()
        {
            var prod = new Product{ Id = _id, Name = "SomeName", Price = 66.7M };
            _productRpository.Get(_id).Returns(prod);

            var received = _productsController.Get(_id).Value;

            _productRpository.Received().Get(_id);
            Assert.AreEqual(prod, received);
        }
    }
}
