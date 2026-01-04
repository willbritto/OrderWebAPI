using Microsoft.EntityFrameworkCore;
using OrderWebAPI.Data;
using OrderWebAPI.Models;
using OrderWebAPI.Services;

namespace OrderWebAPI.Tests.CategoryUnitTests
{
    public class PostCategoryUnitTests
    {
        [Fact]

        public async Task CreateCategory_ShouldAddCategory() 
        {


            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            using var context = new ApplicationDbContext(options);

            var service = new CategoryService(context);
            var category = new CategoryModel { CategoryId = 2, Service_Type = "Upgrade" };

            //Act
            var result = await service.CreateCategory(category);
            //Assert
            Assert.NotNull(result);
            Assert.Equal("Upgrade", result.Service_Type);
            Assert.Single(context.categoryModels);
        }

    }
}
