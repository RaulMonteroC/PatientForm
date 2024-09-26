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

    private static readonly IEnumerable<Patient> _patientList = new Patient[]
                                                                {
                                                                    new(Guid.NewGuid(), "Name1", "Lastname1", "PhoneNumber", "Email"),
                                                                    new(Guid.NewGuid(), "Name2", "Lastname2", "PhoneNumber", "Email"),
                                                                    new(Guid.NewGuid(), "Name3", "Lastname3", "PhoneNumber", "Email"),
                                                                    new(Guid.NewGuid(), "Name4", "Lastname4", "PhoneNumber", "Email"),
                                                                    new(Guid.NewGuid(), "Name5", "Lastname5", "PhoneNumber", "Email")
                                                                };
}