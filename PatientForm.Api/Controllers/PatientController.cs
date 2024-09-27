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
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll([FromRoute] string id) =>
        Ok(await patientService.Get(id));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] PatientDto patient)
    {
        await patientService.Save(patient);

        return Created();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put([FromBody] PatientDto patient)
    {
        await patientService.Update(patient);

        return Accepted();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        await patientService.Delete(id);

        return Accepted();
    }
}