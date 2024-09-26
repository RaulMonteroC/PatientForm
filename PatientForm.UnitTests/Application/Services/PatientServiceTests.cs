using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
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

    private readonly IEnumerable<Patient> _patientList = new Patient[]
                                                        {
                                                            new(Guid.NewGuid(), "Name1", "Lastname1", "PhoneNumber", "Email"),
                                                            new(Guid.NewGuid(), "Name2", "Lastname2", "PhoneNumber", "Email"),
                                                            new(Guid.NewGuid(), "Name3", "Lastname3", "PhoneNumber", "Email"),
                                                            new(Guid.NewGuid(), "Name4", "Lastname4", "PhoneNumber", "Email"),
                                                            new(Guid.NewGuid(), "Name5", "Lastname5", "PhoneNumber", "Email")
                                                        };
}