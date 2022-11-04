using Ecommerce.Controllers;
using Ecommerce.Data;
using Ecommerce.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.MockData;


namespace TestProject.Controller
{
    public class CategoryControllertest { 
        

        [Fact]
        public async Task GetAllCategory_ShouldReturn200Status()
        {
            // Arrange
            var category = new Mock<ICategory>();
            category.Setup(_ => _.GetAllCategory()).ReturnsAsync(UseMockData.GetAll());
            var sut = new CategoryController(category.Object);

            //Act
            var record = await sut.GetAllRecord();

            //Assert
            record.GetType().Should().Be(typeof(OkObjectResult));
            (record as OkObjectResult).StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task SaveAsync_ShouldCall_ITodoService_SaveAsync_AtleastOnce()
        {
            /// Arrange
            var todoService = new Mock<ICategory>();
            var newTodo = UseMockData.AddCategory();
            var sut = new CategoryController(todoService.Object);

            /// Act
            var result = await sut.Add(newTodo);

            /// Assert
            todoService.Verify(_ => _.AddCategoryAsync(newTodo), Times.Exactly(1));
        }
    }
}
