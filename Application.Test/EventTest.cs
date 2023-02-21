using Application.API.Controllers;
using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class EventTest
{
    private readonly EventController _controller;
    private readonly Mock<IDataRepository<Event>> _mockRepository;
    private readonly Mock<IEventService> _mockService;

    public EventTest()
    {
        _mockRepository = new Mock<IDataRepository<Event>>();
        _mockService = new Mock<IEventService>();
        _controller = new EventController(_mockRepository.Object, _mockService.Object);
    }

    //Test Get method with valid filter
    [Fact]
    public void Get_ReturnsOkResult_WithValidFilter()
    {
        //Arrange
        var filter = new FilterModel();
        var mockEventService = new Mock<IEventService>();
        mockEventService.Setup(repo => repo.GetEvents(filter)).Returns(new PaginationResponseModel<Event>());
        var controller = new EventController(Mock.Of<IDataRepository<Event>>(), mockEventService.Object);

        //Act
        var result = controller.Get(filter);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<PaginationResponseModel<Event>>(okResult.Value);
        Assert.NotNull(model);
    }

    //Test Get method with invalid filter
    [Fact]
    public void Get_ReturnsBadRequest_WithInvalidFilter()
    {
        //Arrange
        var filter = new FilterModel() { page = -1 };
        var mockEventService = new Mock<IEventService>();
        var controller = new EventController(Mock.Of<IDataRepository<Event>>(), mockEventService.Object);
        controller.ModelState.AddModelError("page", "page must be greater than 0");

        //Act
        var result = controller.Get(filter);

        //Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var error = Assert.IsType<SerializableError>(badRequestResult.Value);
        Assert.True(error.ContainsKey("page"));
    }

    //Test Get method with valid eventId
    [Fact]
    public void Get_ReturnsOkResult_WithValidEventId()
    {
        //Arrange
        var eventId = 1;
        var mockEventRepository = new Mock<IDataRepository<Event>>();
        mockEventRepository.Setup(repo => repo.Get(eventId)).Returns(new Event());
        var controller = new EventController(mockEventRepository.Object, Mock.Of<IEventService>());

        //Act
        var result = controller.Get(eventId);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<Event>(okResult.Value);
        Assert.NotNull(model);
    }
}