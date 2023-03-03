using System.Threading.Tasks;
using Application.API.Controllers;
using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

public class DoctorTest
{
    private readonly IDoctorRepository _doctorRepository = Substitute.For<IDoctorRepository>();
    private readonly IDoctorService _doctorService = Substitute.For<IDoctorService>();
    private readonly IMapper _mapper = Substitute.For<IMapper>();

    private readonly DoctorController _controller;

    public DoctorTest()
    {
        _controller = new DoctorController(_doctorRepository, _doctorService, _mapper);
    }

    [Fact]
    public async Task Get_ReturnsOkResultWithDoctorDto()
    {
        // Arrange
        var doctorId = 123;
        var doctor = new Doctor();
        var expected = new DoctorDto();
        _doctorRepository.GetAsync(doctorId).Returns(doctor);
        _mapper.Map<DoctorDto>(doctor).Returns(expected);

        // Act
        var result = await _controller.Get(doctorId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actual = Assert.IsAssignableFrom<DoctorDto>(okResult.Value);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Post_ReturnsOkResult()
    {
        // Arrange
        var doctor = new DoctorBodyDto();
        var doctorEntity = new Doctor();
        var response = "success";
        _mapper.Map<Doctor>(doctor).Returns(doctorEntity);
        _doctorRepository.AddAsync(doctorEntity).Returns(response);

        // Act
        var result = await _controller.Post(doctor);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);
    }

    [Fact]
    public async Task Put_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        var doctorId = 10;
        var doctor = new DoctorBodyDto()
        {
            Name = "test1",
            Specialty = "anything"
        };
        _controller.ModelState.AddModelError("Name", "Name is required");

        // Act
        var result = await _controller.Put(doctorId, doctor);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
} 