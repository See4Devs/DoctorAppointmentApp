using System.Threading.Tasks;
using Application.API.Controllers;
using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

public class EventTest
{
    private readonly IEventRepository _eventRepository;
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;
    private readonly EventController _controller;

    public EventTest()
    {
        _eventRepository = Substitute.For<IEventRepository>();
        _eventService = Substitute.For<IEventService>();
        _mapper = Substitute.For<IMapper>();
        _controller = new EventController(_eventRepository, _eventService, _mapper);
    }

    [Fact]
    public async Task Get_ReturnsOkObjectResult_WhenCalled()
    {
        // Arrange
        var filter = new FilterDto()
        {
            searchText = "str",
            limit = 1,
            page = 1
        };
        var paginationResponseDto = new PaginationResponseDto<EventDto>();

        _eventService.GetEventsAsync(Arg.Any<FilterDto>())
            .Returns(Task.FromResult(paginationResponseDto));

        // Act
        var result = await _controller.Get(filter);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task Get_ReturnsOkResult_WhenEventIsFound()
    {
        // Arrange
        var eventId = 1;
        var eventObj = new Event();
        var expectedResponse = new EventDto();
        _eventRepository.GetAsync(eventId).Returns(eventObj);
        _mapper.Map<EventDto>(eventObj).Returns(expectedResponse);

        // Act
        var result = await _controller.Get(eventId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualResponse = Assert.IsType<EventDto>(okResult.Value);
        Assert.Equal(expectedResponse, actualResponse);
    }

    [Fact]
    public async Task Post_ReturnsOkResult_WhenModelStateIsValid()
    {
        // Arrange
        var eventBodyDto = new EventBodyDto();
        var eventEntity = new Event();
        var expectedResponse = "Event added successfully";
        _mapper.Map<Event>(eventBodyDto).Returns(eventEntity);
        _eventRepository.AddAsync(eventEntity).Returns(expectedResponse);

        // Act
        var result = await _controller.Post(eventBodyDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualResponse = Assert.IsType<string>(okResult.Value);
        Assert.Equal(expectedResponse, actualResponse);
    }

    [Fact]
    public async Task Post_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        var eventBodyDto = new EventBodyDto();
        _controller.ModelState.AddModelError("Name", "The Name field is required");

        // Act
        var result = await _controller.Post(eventBodyDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Put_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        var eventId = 1;
        var eventBodyDto = new EventBodyDto();
        _controller.ModelState.AddModelError("Name", "The Name field is required");

        // Act
        var result = await _controller.Put(eventId, eventBodyDto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
