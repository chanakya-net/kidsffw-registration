// NOT available for public  
// namespace kidsffw.Api.Controllers;
//
//     using System.Threading.Tasks;
//     using kidsffw.Application.Interfaces.Service;
//     using kidsffw.Common.DTO;
//     using Microsoft.AspNetCore.Mvc;
//
// [Route("api/[controller]")]
// [ApiController]
// public class SalesPartnerController : ControllerBase
// {
//     private readonly ISalesPartnerService _salesPartnerService;
//
//     public SalesPartnerController(ISalesPartnerService salesPartnerService)
//     {
//         _salesPartnerService = salesPartnerService;
//     }
//
//     [HttpPost("AddSalesPartner")]
//     [ProducesResponseType(typeof(CreateSalesPartnerResponseDto), StatusCodes.Status200OK)]
//     public async Task<IActionResult> AddSalesPartner([FromBody] CreateSalesPartnerRequestDto salesPartner)
//     {
//
//         var result = await _salesPartnerService.CreateSalesPartner(salesPartner);
//         return Ok(result);
//     }
//
//     [HttpGet("GetSalesPartnerContact/{Id}")]
//     [ProducesResponseType(typeof(SalesPartnerContactDto), StatusCodes.Status200OK)]
//     public async Task<IActionResult> GetSalesPartner(int Id)
//     {
//         var result = await _salesPartnerService.GetSalesPartnerContactByPartnerId(Id);
//         return Ok(result);
//     }
//     
//     [HttpGet("GetSalesPartnerContactByCoupon/{code}")]
//     [ProducesResponseType(typeof(SalesPartnerContactDto), StatusCodes.Status200OK)]
//     public async Task<IActionResult> GetSalesPartner(string code)
//     {
//         var result = await _salesPartnerService.GetSalesPartnerContactByCouponId(code);
//         return Ok(result);
//     }
// }
//
