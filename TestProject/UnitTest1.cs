using DocumentFormat.OpenXml.Drawing.Diagrams;
using Ecommerce.Controllers;
using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using TestProject.MockData;
using Ecommerce.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace TestProject
{
    public class UnitTest1 : IDisposable

    {
        private readonly EcommStoreContext _context;
        private readonly ICategory category;
        public UnitTest1()
        {

            var option = new DbContextOptionsBuilder<EcommStoreContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            _context = new EcommStoreContext(option);

            _context.Database.EnsureCreated();


        }
        [Fact]
        public async Task GetAllCategoryReturnCategoryCollecction()
        {
            //Arrange
            _context.Categories.AddRange(UseMockData.GetAll());
            _context.SaveChanges();
            var sut = new CategoryRepository(_context);

            //Act
            var record = await sut.GetAllCategory();

            //Assert
            record.Should().HaveCount(UseMockData.GetAll().Count);

        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }


    }
}