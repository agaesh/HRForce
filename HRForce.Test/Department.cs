using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using HRForce.ApiService.Application.DTO;
using HRForce.ApiService.Application.Interfaces;
using HRForce.ApiService.Domain;

public class DepartmentControllerTests
{
    private readonly Mock<IDepartmentService> _serviceMock;
    private readonly DepartmentController _controller;

    public DepartmentControllerTests()
    {
        _serviceMock = new Mock<IDepartmentService>();
        _controller = new DepartmentController(_serviceMock.Object);
    }

    [Fact]
    public async Task PostDepartment_ReturnsCreatedResult()
    {
        // Arrange
        var inputDto = new DepartmentDTO
        {
            DepartmentCode = "HR01",
            DepartmentName = "HR",
            Status = DepartmentStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        var createdDto = new DepartmentDTO
        {
            Id = 1,
            DepartmentCode = "HR01",
            DepartmentName = "HR",
            Status = DepartmentStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        _serviceMock
            .Setup(s => s.CreateDepartmentAsync(It.IsAny<CreateDepartmentDto>()))
            .ReturnsAsync(createdDto);

        // Act
        var result = await _controller.PostDepartment(inputDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<DepartmentDTO>(createdResult.Value);

        Assert.Equal(1, returnValue.Id);
        Assert.Equal("HR01", returnValue.DepartmentCode);
    }
}