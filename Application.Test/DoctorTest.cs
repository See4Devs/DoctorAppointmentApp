using System;
using Application.API.Controllers;
using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class DoctorTest
{
    private readonly DoctorController _controller;
    private readonly Mock<IDataRepository<Doctor>> _mockRepository;
    private readonly Mock<IDoctorService> _mockService;

    public DoctorTest()
    {
        _mockRepository = new Mock<IDataRepository<Doctor>>();
        _mockService = new Mock<IDoctorService>();
        _controller = new DoctorController(_mockRepository.Object, _mockService.Object);
    }

    //Test for Get method:
    [Fact]
    public void Get_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var filter = new FilterModel();

        // Act
        var result = _controller.Get(filter);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    //Test for Get method when there is an exception
    [Fact]
    public void Get_WhenExceptionThrown_ReturnsBadRequest()
    {
        // Arrange
        _mockService.Setup(x => x.GetDoctors(It.IsAny<FilterModel>())).Throws(new Exception());

        // Act
        var result = _controller.Get(new FilterModel());

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    //Test for Post method when ModelState is invalid
    [Fact]
    public void Post_WhenModelStateIsInvalid_ReturnsBadRequest()
    {
        // Arrange
        _controller.ModelState.AddModelError("Name", "Name is required");
        var doctor = new DoctorModel();

        // Act
        var result = _controller.Post(doctor);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}