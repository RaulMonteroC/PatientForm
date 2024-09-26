using Microsoft.AspNetCore.Mvc;
using PatientForm.Api.Query;
using PatientForm.Application.DTOs;
using PatientForm.Application.Patients;

namespace PatientForm.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController(IPatientService patientService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PatientDto>))]
    public async Task<IActionResult> GetAll([FromQuery] PaginatedQuery query) =>
        Ok(await patientService.GetAll(query.PageNumber, query.PageSize));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] PatientDto patient)
    {
        await patientService.Save(patient);
        
        return Ok();
    }
}