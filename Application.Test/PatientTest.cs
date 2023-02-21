using System;
using Application.API.Controllers;
using Application.Domain.Dao;
using Application.Domain.Dto;
using Application.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;


namespace Application.Test
{
	public class PatientTest
	{
        private readonly PatientController _patientController;
        private readonly Mock<IDataRepository<Patient>> _patientRepositoryMock;
        private readonly Mock<IPatientService> _patientServiceMock;

        public PatientTest()
        {
            _patientRepositoryMock = new Mock<IDataRepository<Patient>>();
            _patientServiceMock = new Mock<IPatientService>();
            _patientController = new PatientController(_patientRepositoryMock.Object, _patientServiceMock.Object);
        }

        [Fact]
        public void Get_Returns_NotFound_When_Patient_Is_Null()
        {
            //Arrange
            int patientId = 1;
            _patientRepositoryMock.Setup(x => x.Get(patientId)).Returns(() => null);

            //Act
            var result = _patientController.Get(patientId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Get_Returns_Ok_When_Patient_Is_Not_Null()
        {
            //Arrange
            int patientId = 1;
            var expected = new Patient() { PatientId = patientId };
            _patientRepositoryMock.Setup(x => x.Get(patientId)).Returns(expected);

            //Act
            var result = _patientController.Get(patientId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(expected, okResult.Value);
        }

        [Fact]
        public void Post_Returns_BadRequest_When_ModelState_Is_Invalid()
        {
            //Arrange
            var patient = new PatientModel();
            _patientController.ModelState.AddModelError("Name", "Name is required");

            //Act
            var result = _patientController.Post(patient);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Post_Returns_Ok_When_ModelState_Is_Valid()
        {
            //Arrange
            var patient = new PatientModel() { Name = "John" };
            var expected = "Successfully created";

            //Act
            var result = _patientController.Post(patient);

            //Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(expected, okResult.Value);
        }
    }
}

