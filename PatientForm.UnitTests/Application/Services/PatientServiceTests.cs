using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using PatientForm.Application.DTOs;
using PatientForm.Domain.Entities;
using PatientForm.Domain.Repositories;
using PatientForm.UnitTests.Builders;

namespace PatientForm.UnitTests.Application.Services;

[TestFixture]
public class PatientServiceTests
{
    [Test]
    public async Task GetAll_ShouldReturn_AllPatientsRecordsPaginated()
    {
        //arrange
        var patientRepository = A.Fake<IPatientRepository>(builder => builder.Strict());
        A.CallTo(() => patientRepository.Get(A<int>._, A<int>._))
         .Returns(_patientList);

        // Act
        var sut = new PatientServiceBuilder()
                 .WithPatientRepository(patientRepository)
                 .Build();

        await sut.GetAll(1, 5);

        // Assert
        A.CallTo(() => patientRepository.Get(1, 5))
         .MustHaveHappened();

    }
    
    [Test]
    public async Task Save_Should_CreateNewPatientRecord()
    {
        //arrange
        var patientRepository = A.Fake<IPatientRepository>(builder => builder.Strict());
        var patient = new Patient(Guid.NewGuid().ToString(),
                                  "Name1",
                                  "Lastname1",
                                  "PhoneNumber",
                                  "Email");
        
        A.CallTo(() => patientRepository.Save(patient))
         .Returns(Task.CompletedTask);

        // Act
        var sut = new PatientServiceBuilder()
                 .WithPatientRepository(patientRepository)
                 .Build();

        await sut.Save(PatientDto.FromEntity(patient));

        // Assert
        A.CallTo(() => patientRepository.Save(patient))
         .MustHaveHappened();

    }

    private readonly IEnumerable<Patient> _patientList = new Patient[]
                                                        {
                                                            new(Guid.NewGuid().ToString(), "Name1", "Lastname1", "PhoneNumber", "Email"),
                                                            new(Guid.NewGuid().ToString(), "Name2", "Lastname2", "PhoneNumber", "Email"),
                                                            new(Guid.NewGuid().ToString(), "Name3", "Lastname3", "PhoneNumber", "Email"),
                                                            new(Guid.NewGuid().ToString(), "Name4", "Lastname4", "PhoneNumber", "Email"),
                                                            new(Guid.NewGuid().ToString(), "Name5", "Lastname5", "PhoneNumber", "Email")
                                                        };
}