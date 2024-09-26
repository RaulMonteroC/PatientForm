using FluentAssertions;
using NUnit.Framework;
using PatientForm.Application.DTOs;
using PatientForm.Domain.Entities;

namespace PatientForm.UnitTests.Application.DTOs;

[TestFixture]
public class PatientDtoTests
{
    [TestCaseSource(nameof(_patientList))]
    public void FromEntity_ShouldReturn_PatientDtoWithSameValues(Patient patient)
    {
        // Act
        var dto = PatientDto.FromEntity(patient);
        
        //Assert
        dto.Id
           .Should()
           .Be(patient.Id);

        dto.Name
           .Should()
           .Be(patient.Name);

        dto.LastName
           .Should()
           .Be(patient.LastName);

        dto.PhoneNumber
           .Should()
           .Be(patient.PhoneNumber);

        dto.Email
           .Should()
           .Be(patient.Email);
    }
    
    [TestCaseSource(nameof(_patientDtoList))]
    public void ToEntity_ShouldReturn_PatientWithSameValues(PatientDto dto)
    {
        // Act
        var entity = dto.ToEntity();
        
        //Assert
        entity.Id
           .Should()
           .Be(dto.Id);

        entity.Name
           .Should()
           .Be(dto.Name);

        entity.LastName
           .Should()
           .Be(dto.LastName);

        entity.PhoneNumber
           .Should()
           .Be(dto.PhoneNumber);

        entity.Email
           .Should()
           .Be(dto.Email);
    }

    private static readonly IEnumerable<Patient> _patientList = new Patient[]
                                                                {
                                                                    new(Guid.NewGuid(), "Name1", "Lastname1", "PhoneNumber", "Email"),
                                                                    new(Guid.NewGuid(), "Name2", "Lastname2", "PhoneNumber", "Email"),
                                                                    new(Guid.NewGuid(), "Name3", "Lastname3", "PhoneNumber", "Email"),
                                                                    new(Guid.NewGuid(), "Name4", "Lastname4", "PhoneNumber", "Email"),
                                                                    new(Guid.NewGuid(), "Name5", "Lastname5", "PhoneNumber", "Email")
                                                                };

    private static readonly IEnumerable<PatientDto> _patientDtoList = new PatientDto[]
                                                                      {
                                                                          new()
                                                                          {
                                                                              Id = Guid.NewGuid(),
                                                                              Name = "Name1",
                                                                              LastName = "LastName1",
                                                                              PhoneNumber = "phone",
                                                                              Email = "email"
                                                                          },
                                                                          new()
                                                                          {
                                                                              Id = Guid.NewGuid(),
                                                                              Name = "Name2",
                                                                              LastName = "LastName2",
                                                                              PhoneNumber = "phone",
                                                                              Email = "email"
                                                                          },
                                                                          new()
                                                                          {
                                                                              Id = Guid.NewGuid(),
                                                                              Name = "Name2",
                                                                              LastName = "LastName3",
                                                                              PhoneNumber = "phone",
                                                                              Email = "email"
                                                                          }
                                                                      };
}