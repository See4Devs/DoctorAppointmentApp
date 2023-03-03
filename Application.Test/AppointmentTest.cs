using System.Threading.Tasks;
using Application.API.Controllers;
using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Application.API.Tests.Controllers
{
    public class AppointmentTest
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public AppointmentTest()
        {
            _appointmentRepository = Substitute.For<IAppointmentRepository>();
            _appointmentService = Substitute.For<IAppointmentService>();
            _mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WhenCalledWithValidId()
        {
            // Arrange
            var controller = new AppointmentController(_appointmentRepository, _appointmentService, _mapper);
            var appointmentId = 1;
            var appointment = new Appointment();
            var expected = new AppointmentDetailsDto();
            _appointmentRepository.GetAsync(appointmentId).Returns(appointment);
            _mapper.Map<AppointmentDetailsDto>(appointment).Returns(expected);

            // Act
            var result = await controller.Get(appointmentId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<AppointmentDetailsDto>(okResult.Value);
            Assert.Equal(expected, model);
        }

        [Fact]
        public async Task Post_ReturnsOkResult_WhenModelStateIsValid()
        {
            // Arrange
            var controller = new AppointmentController(_appointmentRepository, _appointmentService, _mapper);
            var appointment = new AppointmentBodyDto();
            var expected = "Appointment created successfully";
            _mapper.Map<Appointment>(appointment).Returns(new Appointment());
            _appointmentRepository.AddAsync(Arg.Any<Appointment>()).Returns(expected);

            // Act
            var result = await controller.Post(appointment);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<string>(okResult.Value);
            Assert.Equal(expected, model);
        }
    }
}
