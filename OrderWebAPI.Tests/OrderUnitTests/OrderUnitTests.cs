using AutoMapper;
using Moq;
using OrderWebAPI.DTOs.EntitieDTOs;
using OrderWebAPI.Models;
using OrderWebAPI.Repositories.Interfaces;
using OrderWebAPI.Services;

namespace OrderWebAPI.Tests.OrderUnitTests
{
    public class OrderUnitTests
    
        //Metodo_ShouldResultado_WhenCodicao -> sem exceção
        //Metodo_ShouldThrowException_WhenCodicao -> com exceção
    {
        [Fact]
        public async Task GetById_ShouldOrder_WhenExists()
        {
            //Arrange
            var repoMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var orderDto = new OrderDTO
            {
                NameFull = "Ana Paula"
            };
            var orderEntity = new OrderModel
            {
                OrderId = 4,
                NameFull = "Ana Paula"
            };


            //Mapper : DTO -> Entity
            mapperMock.Setup(m => m.Map<OrderModel>(orderDto)).Returns(orderEntity);
            //Repository : GetById
            repoMock.Setup(r => r.GetById(4)).ReturnsAsync(orderEntity);
            //Mapper: Entity -> DTO
            mapperMock.Setup(m => m.Map<OrderDTO>(orderEntity)).Returns(orderDto);

            var service = new OrderService(repoMock.Object, mapperMock.Object);

            //Act
            var result = await service.GetById(4);
            //Assert
            Assert.NotNull(result);
            Assert.Equal("Ana Paula", result.NameFull);

            repoMock.Verify(r => r.GetById(4), Times.Once);
            mapperMock.Verify(m => m.Map<OrderDTO>(orderEntity), Times.Once);

        }

        [Fact]
        public async Task GetById_ShouldThrowException_WhenNotFound()
        {
            //Arrange
            var repoMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            repoMock.Setup(r => r.GetById(It.IsAny<int>())).ReturnsAsync((OrderModel)null);

            var service = new OrderService(repoMock.Object, mapperMock.Object);

            //Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetById(99));

        }

        [Fact]
        public async Task CreateAsync_ShouldOrder_WhenExists()
        {
            var repoMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var orderDto = new OrderDTO
            {
                NameFull = "Lucas Santos",
                Description = "Formatação do notebook, com windows 10",
                Price = 120,

            };

            var orderEntity = new OrderModel
            {
                NameFull = "Lucas Santos",
                Description = "Formatação do notebook, com windows 10",
                Price = 120,
            };

            //Mapper : DTO -> Entity
            mapperMock.Setup(m => m.Map<OrderModel>(orderDto)).Returns(orderEntity);
            //Repository : CreateAsync
            repoMock.Setup(r => r.CreateAsync(It.IsAny<OrderModel>())).ReturnsAsync(orderEntity);
            //Mapper : Entity -> DTO
            mapperMock.Setup(m => m.Map<OrderDTO>(orderEntity)).Returns(orderDto);

            var service = new OrderService(repoMock.Object, mapperMock.Object);

            //Act
            var result = await service.CreateAsync(orderDto);
            //assert
            Assert.NotNull(result);
            Assert.Equal("Lucas Santos", result.NameFull);
            Assert.Equal("Formatação do notebook, com windows 10", result.Description);
            Assert.Equal(120, result.Price);

            repoMock.Verify(r => r.CreateAsync(It.IsAny<OrderModel>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenOrderIsNull()
        {
            //Arrange
            var repoMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var service = new OrderService(repoMock.Object, mapperMock.Object);

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.CreateAsync(null));

            repoMock.Verify(r => r.CreateAsync(It.IsAny<OrderModel>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenDataIsInvalid()
        {
            //Arrange
            var repoMock = new Mock<IOrderRepository>();
            var mapperMock = new Mock<IMapper>();

            var dto = new OrderDTO
            {
                NameFull = "",
                Description = "",
                Price = 0
            };
            var service = new OrderService(repoMock.Object, mapperMock.Object);

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(dto));

            repoMock.Verify(r => r.CreateAsync(It.IsAny<OrderModel>()), Times.Never);




        }




    }
}
