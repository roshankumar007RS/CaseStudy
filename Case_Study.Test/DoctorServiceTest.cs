using Case_Study.Controllers;
using Case_Study.Models;
using Case_Study.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Case_Study.test
{
    public class DoctorServiceTest
    {
        public Mock<IDoctorService> mock = new Mock<IDoctorService>();
        [Fact]
        public async void GetDoctor_specialized_return_list()
        {
            mock.Setup(p => p.GetDoctorBySpecialization("Dermatologist")).ReturnsAsync(GetTestData());

            DoctorsController doc = new DoctorsController(mock.Object);

            var actual_result = await doc.GetDoctor("Dermatologist");
            Assert.Equal(2, actual_result.Value.Count);


        }
        [Fact]
        public async void GetDoctor_specialized_UnknownID_ReturnsNotFoundResultAsync()
        {
            mock.Setup(p => p.GetDoctorBySpecialization("hello"));
            DoctorsController doc = new DoctorsController(mock.Object);
            var actual_result = await doc.GetDoctor("hello");

            Assert.IsType<NotFoundResult>(actual_result.Result);
        }
        [Fact]
        public async void PostDoctor_return_on_succesfull()
        {
            var doctorDto = new Doctor()
            {
                DoctorID = 1,
                Name = "madan",
                Contact = 1234567890,
                Email = "madan123@gmail.com",
                Password = "12345",
                Specialization = "Dermatologist"
            };
            mock.Setup(x => x.PostDoctorByService(doctorDto)).ReturnsAsync(doctorDto);
            var _controller = new DoctorsController(mock.Object);
            var createdResponse = _controller.PostDoctor(doctorDto);
            Assert.Equal(1, createdResponse.Result.Value.DoctorID);
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequestOnDoctor()
        {
            // Arrange
            var doctorDto = new Doctor()
            {
                DoctorID = 1,
                
                Contact = 1234567890,
                Email = "madan123@gmail.com",
                Password = "12345",
                Specialization = "Dermatologist"
            };
            mock.Setup(x => x.PostDoctorByService(doctorDto)).ReturnsAsync(doctorDto);
            var _controller = new DoctorsController(mock.Object);
            var createdResponse = _controller.PostDoctor(doctorDto);
            // Assert
            Assert.False(doctorDto.Equals(createdResponse));
        }
        public List<Doctor> GetTestData()
        {
            var Doctorlist = new List<Doctor>();
            Doctorlist.Add(new Doctor()
            {
                DoctorID = 1,
                Name = "polo",
                Contact = 1234567890,
                Email = "polo@gmail.com",
                Password = "1234567890",
                Specialization = "Dermatologist"

            });
            Doctorlist.Add(new Doctor()
            {
                DoctorID = 1,
                Name = "ram",
                Contact = 1234567890,
                Email = "ram@gmail.com",
                Password = "1234567890",
                Specialization = "Dermatologist"

            });

            return Doctorlist;
        }
    }
}
