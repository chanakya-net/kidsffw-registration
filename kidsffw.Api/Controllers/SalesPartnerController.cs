using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kidsffw.Application.Interfaces.Service;
using kidsffw.Common.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kidsffw.Api.Controllers
{
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
}
