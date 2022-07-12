// NOT Available 
// using Microsoft.AspNetCore.Mvc;
//
// namespace kidsffw.Api.Controllers
// {
//     using Application.Interfaces.Service;
//
//     [Route("api/[controller]")]
//     [ApiController]
//     public class TestController : ControllerBase
//     {
//         private readonly IRazorPayOrderService _razorPayService;
//
//         public TestController(IRazorPayOrderService razorPayService) => _razorPayService = razorPayService;
//         
//         [HttpPost("CreateOrder")]
//         public async Task<IActionResult> CreateOrder(decimal amout)
//         {
//             var order = _razorPayService.CreateOrder(amout);
//             return Ok(order);
//         }
//     }
// }
