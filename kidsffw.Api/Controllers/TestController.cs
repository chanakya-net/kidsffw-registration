using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kidsffw.Api.Controllers
{
    using Application.Interfaces.Service;

    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IRazorPayService _razorPayService;

        public TestController(IRazorPayService razorPayService) => _razorPayService = razorPayService;
        
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(decimal amout)
        {
            var order = _razorPayService.CreateOrder(amout);
            return Ok(order);
        }
    }
}
