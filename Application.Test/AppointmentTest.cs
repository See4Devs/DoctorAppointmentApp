using System;
using System.Collections.Generic;
using System.Linq;
using Application.API.Controllers;
using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Application.API.Tests.Controllers
{
    public class AppointmentTest
    {
        private readonly AppointmentController _controller;
        private readonly Mock<IDataRepository<Appointment>> _appointmentRepositoryMock;
        private readonly Mock<IAppointmentService> _appointmentServiceMock;

        public AppointmentTest()
        {
            _appointmentRepositoryMock = new Mock<IDataRepository<Appointment>>();
            _appointmentServiceMock = new Mock<IAppointmentService>();
            _controller = new AppointmentController(_appointmentRepositoryMock.Object, _appointmentServiceMock.Object);
        }

        [Fact]
        public void Get_ReturnsOkResult_WithListOfAppointments()
        {
            // Arrange
            var appointments = new List<Appointment>
            {
                new Appointment
                {
                    AppointmentId = 1,
                    PatientId = 1,
                    EventId = 1,
                    DoctorId = 1,
                    Name = "Appointment 1",
                    Attended = false,
                    NotifyByEmail = false,
                    NotifyBySMS = false,
                    RemindBefore = 10
                },
                new Appointment
                {
                    AppointmentId = 2,
                    PatientId = 2,
                    EventId = 2,
                    DoctorId = 2,
                    Name = "Appointment 2",
                    Attended = false,
                    NotifyByEmail = false,
                    NotifyBySMS = false,
                    RemindBefore = 5
                },
            };
            var filter = new FilterModel();

            _appointmentServiceMock.Setup(x => x.GetAppointments(filter)).Returns(new PaginationResponseModel<Appointment>
            {
                Items = appointments
            });

            // Act
            var result = _controller.Get(filter);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<PaginationResponseModel<Appointment>>(okResult.Value);
            Assert.Equal(appointments.Count, model.Items.Count());
        }

        [Fact]
        public void Get_WithValidId_ReturnsOkResult_WithAppointment()
        {
            // Arrange
            int appointmentId = 1;
            var appointment = new Appointment
            {
                AppointmentId = 2,
                PatientId = 2,
                EventId = 2,
                DoctorId = 2,
                Name = "Appointment 2",
                Attended = false,
                NotifyByEmail = false,
                NotifyBySMS = false,
                RemindBefore = 5
            };

            _appointmentRepositoryMock.Setup(x => x.Get(appointmentId)).Returns(appointment);

            // Act
            var result = _controller.Get(appointmentId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Appointment>(okResult.Value);
            Assert.Equal(appointment.AppointmentId, model.AppointmentId);
        }

        [Fact]
        public void Post_WithValidAppointment_ReturnsOkResult()
        {
            // Arrange
            var appointment = new AppointmentModel
            {
                PatientId = 2,
                EventId = 2,
                DoctorId = 2,
                Name = "Appointment 2",
                Attended = false,
                NotifyByEmail = false,
                NotifyBySMS = false,
                RemindBefore = 5
            };
            var appointmentDto = appointment.TransformToDto();

            _appointmentRepositoryMock.Setup(x => x.Add(appointmentDto));

            // Act
            var result = _controller.Post(appointment);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Successfully created", okResult.Value);
        }

    }
}