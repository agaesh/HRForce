using Moq;
using Microsoft.AspNetCore.Mvc;
using HRForce.ApiService.API.Controllers;
using HRForce.ApiService.Application.Interfaces;
using HRForce.ApiService.Application.DTO;
using HRForce.ApiService.Domain;

namespace HRForce.ApiService.Tests.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _mockService;
        private readonly EmployeeController _controller;

        public EmployeeControllerTests()
        {
            _mockService = new Mock<IEmployeeService>();
            _controller = new EmployeeController(_mockService.Object);
        }

        [Fact]
        public async Task PutEmployee_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            int routeId = 1;

            var dto = new UpdateEmployeeDto
            {
                Id = 2,
                EmployeeCode = "EMP001",
                FullName = "John Doe",
                Email = "john@test.com",
                PhoneNumber = "0123456789",
                DepartmentId = 1,
                Status = EmployeeStatus.Active
            };

            // Act
            var result = await _controller.PutEmployee(routeId, dto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

            Assert.NotNull(badRequestResult);
        }

        [Fact]
        public async Task PutEmployee_ReturnsOk_WhenUpdateSuccessful()
        {
            // Arrange
            int employeeId = 1;

            var dto = new UpdateEmployeeDto
            {
                Id = 1,
                EmployeeCode = "EMP001",
                FullName = "John Doe Updated",
                Email = "johnupdated@test.com",
                PhoneNumber = "0123456789",
                DepartmentId = 1,
                Status = EmployeeStatus.Active
            };

            var responseDto = new EmployeeDto
            {
                Id = 1,
                EmployeeCode = dto.EmployeeCode,
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                DepartmentId = dto.DepartmentId,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow
            };

            _mockService
                .Setup(s => s.UpdateEmployeeAsync(employeeId, dto))
                .ReturnsAsync(responseDto);

            // Act
            var result = await _controller.PutEmployee(employeeId, dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

            Assert.NotNull(okResult);
        }

        [Fact]
        public async Task PutEmployee_ReturnsNotFound_WhenEmployeeDoesNotExist()
        {
            // Arrange
            int employeeId = 99;

            var dto = new UpdateEmployeeDto
            {
                Id = 99,
                EmployeeCode = "EMP099",
                FullName = "Ghost User",
                Email = "ghost@test.com",
                PhoneNumber = "0000000000",
                DepartmentId = 1,
                Status = EmployeeStatus.Active
            };

            _mockService
                .Setup(s => s.UpdateEmployeeAsync(employeeId, dto))
                .ThrowsAsync(new KeyNotFoundException(
                    $"Employee with ID {employeeId} not found."));

            // Act
            var result = await _controller.PutEmployee(employeeId, dto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

            Assert.NotNull(notFoundResult);
        }
    }
}