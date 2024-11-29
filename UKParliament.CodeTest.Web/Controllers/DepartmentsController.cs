using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Services.RequestResponseModels;

namespace UKParliament.CodeTest.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentResponse>>> GetDepartments()
        {
            var departments = await _departmentService.GetDepartments();
            
            return Ok(departments.Value);
        }
    }
}
