using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using OrderWebAPI.Data;
using OrderWebAPI.DTOs.EntitieDTOs;
using OrderWebAPI.Models;
using OrderWebAPI.Repositories.Interfaces;
using OrderWebAPI.Services;
using System.ComponentModel.DataAnnotations;

namespace OrderWebAPI.Tests.CategoryUnitTests
{
    public class CategoryUnitTests
    {

        [Fact]
        public async Task GetCateogryById_ReturnCategory_WhenExists()
        {
            //Arrange
            var repoMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();

            var categoryDto = new CategoryDTO
            {
                Service_Type = "Formatação"
            };
            var categoryEntity = new CategoryModel
            {
                CategoryId = 1,
                Service_Type = "Formatação"
            };

            //Mapper: DTO -> Entity
            mapperMock.Setup(m => m.Map<CategoryModel>(categoryDto)).Returns(categoryEntity);

            //Repository
            repoMock.Setup(r => r.GetById(1)).ReturnsAsync(categoryEntity);

            //Mapper: Entity -> DTO
            mapperMock.Setup(m => m.Map<CategoryDTO>(categoryEntity)).Returns(categoryDto);


            var service = new CategoryService(repoMock.Object, mapperMock.Object);

            //Act
            var result = await service.GetById(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Formatação", result.Service_Type);

            repoMock.Verify(r => r.GetById(1), Times.Once);
            mapperMock.Verify(m => m.Map<CategoryDTO>(categoryEntity), Times.Once);
        }

        [Fact]
        public async Task GetById_ShouldThrowException_WhenNotFound()
        {
            //Arrange
            var repoMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();

            var service = new CategoryService(repoMock.Object, mapperMock.Object);

            //Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetById(1));
        }

        [Fact]
        public async Task CreateCategory_ShouldAddCategory() 
        {
            //Arrange
            var repoMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();

            var categoryDto = new CategoryDTO
            {

                Service_Type = "Upgrade"
            };

            var categoryEntity = new CategoryModel
            {
                CategoryId = 1,
                Service_Type = "Upgrade"
            };

            //Mapper: DTO -> Entity
            mapperMock.Setup(m => m.Map<CategoryModel>(categoryDto)).Returns(categoryEntity);

            //Repository : CreateAsync
            repoMock.Setup(r => r.CreateAsync(categoryEntity)).ReturnsAsync(categoryEntity);

            //Mapper: Entity -> DTO
            mapperMock.Setup(m => m.Map<CategoryDTO>(categoryEntity)).Returns(categoryDto);

            var service = new CategoryService(repoMock.Object, mapperMock.Object);

            //Act
            var result = await service.CreateAsync(categoryDto);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Upgrade", result.Service_Type);

            repoMock.Verify(r => r.CreateAsync(It.IsAny<CategoryModel>()), Times.Once);
        }

        [Fact]
        public async Task CreateCategory_ShouldThrowException_WhenServiceTypeIsEmpty() 
        {
            //Arrange
            var repoMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();

            var dto = new CategoryDTO { Service_Type = "" };
            var service = new CategoryService(repoMock.Object, mapperMock.Object);

            //Act & Assert
            await Assert.ThrowsAnyAsync<ArgumentException>(() => service.CreateAsync(dto));

            repoMock.Verify(r => r.CreateAsync(It.IsAny<CategoryModel>()), Times.Never);


        }

        [Fact]
        public async Task CreateCategory_ShouldThrowExecption_WhenDtoNull() 
        {
            //Arrange
            var repoMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();
            var service = new CategoryService(repoMock.Object, mapperMock.Object);

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.CreateAsync(null));

            repoMock.Verify(r => r.CreateAsync(It.IsAny<CategoryModel>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnCategory_WhenCategoryExists() 
        {
            var repoMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();

            var categoryDto = new CategoryDTO
            {
                Service_Type = "Formatação"
            };
            var categoryEntity = new CategoryModel
            {
                CategoryId = 1,
                Service_Type = "Formatação"
            };

            //Mapper : DTO -> Entity
            mapperMock.Setup(m => m.Map<CategoryModel>(categoryDto)).Returns(categoryEntity);

            //Repository : DeleteAsync
            repoMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(categoryEntity);

            //Mapper: Entity -> DTO
            mapperMock.Setup(m => m.Map<CategoryDTO>(categoryEntity)).Returns(categoryDto);

            var service = new CategoryService(repoMock.Object, mapperMock.Object);

            //Act
            var result = await service.DeleteAsync(1);
           
            //Assert
            Assert.NotNull(result);
            Assert.Equal("Formatação", result.Service_Type);

            repoMock.Verify(r => r.DeleteAsync(1), Times.Once);
            mapperMock.Verify(m => m.Map<CategoryDTO>(categoryEntity), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenNotFound() 
        {
            //Arrange
            var repoMock = new Mock<ICategoryRepository>();
            var mapperMock = new Mock<IMapper>();

            repoMock.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync((CategoryModel)null);

            var service = new CategoryService(repoMock.Object, mapperMock.Object);

            //Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.DeleteAsync(1));

            repoMock.Verify(r => r.DeleteAsync(1), Times.Once);

        }
    }
}
