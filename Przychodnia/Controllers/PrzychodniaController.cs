using Microsoft.AspNetCore.Mvc;
using Przychodnia.DTOs;
using Przychodnia.Exceptions;
using Przychodnia.Services;

namespace Przychodnia.Controllers;
[ApiController]
[Route("[controller]")]
public class ApiController(IDbService service) : ControllerBase
{
    [HttpPost("prescriptions")]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionCreateDto dto)
    {
        try
        {
            var pres = await service.CreatePrescriptionAsync(dto);
            return CreatedAtAction(
                nameof(GetPatientDetails),
                new { id = pres.IdPrescription },
                pres);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("patients/{id}")]
    public async Task<IActionResult> GetPatientDetails([FromRoute] int id)
    {
        try
        {
            return Ok(await service.GetPatientDetailsAsync(id));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}
