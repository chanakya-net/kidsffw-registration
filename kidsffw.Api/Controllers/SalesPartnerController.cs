namespace kidsffw.Api.Controllers;

    using System.Threading.Tasks;
    using kidsffw.Application.Interfaces.Service;
    using kidsffw.Common.DTO;
    using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class SalesPartnerController : ControllerBase
{
    private readonly ISalesPartnerService _salesPartnerService;

    public SalesPartnerController(ISalesPartnerService salesPartnerService)
    {
        _salesPartnerService = salesPartnerService;
    }

    [HttpPost("AddSalesPartner")]
    public async Task<IActionResult> AddSalesPartner([FromBody] CreateSalesPartnerRequestDto salesPartner)
    {

        var result = await _salesPartnerService.CreateSalesPartner(salesPartner);
        return Ok(result);
    }

    [HttpGet("GetSalesPartnerContact/{Id}")]
    public async Task<IActionResult> GetSalesPartner(int Id)
    {
        var result = await _salesPartnerService.GetSalesPartnerContact(Id);
        return Ok(result);
    }
}

