using Microsoft.Extensions.Logging;
using PatientForm.Domain.Repositories;

namespace PatientForm.Domain.Initializers.Steps;

internal class SeedPatientTableStep(IDatabaseInitializerRepository repository,
                                    ILogger<SeedPatientTableStep> logger) : IInitializationStep
{
    public async Task Execute()
    {
        logger.LogInformation("Adding seed data on tblPatients");
        
        await repository.RunQuery(@"
            INSERT INTO tblPatient (Name, LastName, PhoneNumber, Email, InsuranceId) VALUES
            ('Raul', 'Montero', '123456789', 'Raul@clarika.ar', NULL),
            ('Randy', 'Larson', '123456789', 'Randy@clarika.ar', NULL),
            ('Amy', 'Cuevas', '123456789', 'Amy@clarika.ar', NULL),
            ('Kain', 'Fuentes', '123456789', 'Kain@clarika.ar', NULL),
            ('Olly', 'Pope', '123456789', 'Olly@clarika.ar', NULL),
            ('Lloyd', 'Norman', '123456789', 'Lloyd@clarika.ar', NULL),
            ('Sanaa', 'Sherman', '123456789', 'Sanaa@clarika.ar', NULL),
            ('Rosalie', 'Hughes', '123456789', 'Rosalie@clarika.ar', NULL),
            ('Athena', 'Durham', '123456789', 'Athena@clarika.ar', NULL),
            ('Livia', 'Price', '123456789', 'Livia@clarika.ar', NULL),
            ('Sophia', 'Gray', '123456789', 'Sophia@clarika.ar', NULL),
            ('Kelsey', 'Barlow', '123456789', 'Kelsey@clarika.ar', NULL),
            ('Declan', 'Vang', '123456789', 'Declan@clarika.ar', NULL),
            ('Malik', 'Li', '123456789', 'Malik@clarika.ar', NULL),
            ('Lydia', 'Shepherd', '123456789', 'Lydia@clarika.ar', NULL),
            ('Dewey', 'Kemp', '123456789', 'Dewey@clarika.ar', NULL),
            ('Saskia', 'Powers', '123456789', 'Saskia@clarika.ar', NULL),
            ('Trystan', 'Hancock', '123456789', 'Trystan@clarika.ar', NULL),
            ('Angelina', 'Ross', '123456789', 'Angelina@clarika.ar', NULL),
            ('Maja', 'Spencer', '123456789', 'Maja@clarika.ar', NULL),
            ('Beatrix', 'Reed', '123456789', 'Beatrix@clarika.ar', NULL),
            ('Ana', 'Delgado', '123456789', 'Ana@clarika.ar', NULL),
            ('Bill', 'Norman', '123456789', 'Bill@clarika.ar', NULL),
            ('Archie', 'Huffman', '123456789', 'Archie@clarika.ar', NULL),
            ('Carrie', 'Hopkins', '123456789', 'Carrie@clarika.ar', NULL),
            ('Aurora', 'Clements', '123456789', 'Aurora@clarika.ar', NULL),
            ('Cory', 'Benton', '123456789', 'Cory@clarika.ar', NULL),
            ('Gwen', 'Shannon', '123456789', 'Gwen@clarika.ar', NULL),
            ('Chester', 'Leach', '123456789', 'Chester@clarika.ar', NULL),
            ('Ishaan', 'Kaufman', '123456789', 'Ishaan@clarika.ar', NULL)
            ");
    }
}