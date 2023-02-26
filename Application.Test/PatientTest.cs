using Application.API.Controllers;
using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test
{
    public class PatientTest
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        private readonly PatientController _controller;

        public PatientTest()
        {
            _patientRepository = Substitute.For<IPatientRepository>();
            _patientService = Substitute.For<IPatientService>();
            _mapper = Substitute.For<IMapper>();
            _controller = new PatientController(_patientRepository, _patientService, _mapper);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WhenPatientRepositoryReturnsPatient()
        {
            // Arrange
            var patientId = 1;
            var patient = new Patient();
            var expected = new PatientDto();
            _patientRepository.GetAsync(patientId).Returns(patient);
            _mapper.Map<PatientDto>(patient).Returns(expected);

            // Act
            var result = await _controller.Get(patientId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actual = Assert.IsType<PatientDto>(okResult.Value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithPatientDto()
        {
            // Arrange
            var patientRepository = Substitute.For<IPatientRepository>();
            var mapper = Substitute.For<IMapper>();
            var patientService = Substitute.For<IPatientService>();
            var controller = new PatientController(patientRepository, patientService, mapper);
            var patientId = 1;
            var patient = new Patient();
            var expected = new PatientDto();

            patientRepository.GetAsync(patientId).Returns(patient);
            mapper.Map<PatientDto>(patient).Returns(expected);

            // Act
            var result = await controller.Get(patientId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actual = Assert.IsType<PatientDto>(okResult.Value);
            Assert.Equal(expected, actual);
        }
    }
}