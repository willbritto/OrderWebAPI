using Microsoft.EntityFrameworkCore;
using OrderWebAPI.Data;
using OrderWebAPI.Models;
using OrderWebAPI.Services;

namespace OrderWebAPI.Tests.CategoryUnitTests
{
    public class GetCategoryUnitTests
    {
        [Fact]
        public async Task GetCateogryById_ReturnCategory_WhenExists() 
        {
            //Arrage

            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            using var context = new ApplicationDbContext(options);

            context.categoryModels.Add(new CategoryModel { CategoryId = 1, Service_Type = "Formatação" });
            await context.SaveChangesAsync();

            var service = new CategoryService(context);

            //Act
            var result = await service.GetCategoryById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Formatação", result.Service_Type);
        }
    }
}
