using Microsoft.AspNetCore.Mvc;
using multiTenantApp.Services.TenantService;
using multiTenantApp.Services.TenantService.DTOs;

namespace multiTenantApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantsController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        // Create a new tenant
        [HttpPost]
        public IActionResult Post(CreateTenantRequest request)
        {
            var result = _tenantService.CreateTenant(request);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _tenantService.GetAll();
            return Ok(result);
        }

        [HttpDelete]
        [Route("{tenantId}")]
        public IActionResult Delete(string tenantId)
        {
            _tenantService.DeleteTenant(tenantId);
            return Ok();
        }
    }
}
