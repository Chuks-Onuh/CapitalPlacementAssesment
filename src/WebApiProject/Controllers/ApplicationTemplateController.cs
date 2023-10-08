using CapitalPlacementAssesementTaskApi.Contracts.Interfaces;
using ConsoleProject.Dtos.ServiceRequestModels.Application;
using ConsoleProject.Dtos.ServiceRequestModels.Question;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementAssesementTaskApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationTemplateController : ControllerBase
    {
        private readonly IApplicationRepository _applicationTemplateService;

        public ApplicationTemplateController(IApplicationRepository applicationTemplateService)
        {
            _applicationTemplateService = applicationTemplateService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateApplicationAsync([FromBody] CreateApplication applicationModel)
        {
            var response = await _applicationTemplateService.CreateApplicationAsync(applicationModel);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetApplicationAsync(string Id)
        {
            var response = await _applicationTemplateService.GetApplicationAsync(Id);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetApplicationsAsync()
        {
            var response = await _applicationTemplateService.GetApplicationsAsync();
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplicationAsync([FromBody] UpdateApplication updateApplicationModel, [FromRoute] string id)
        {
            var response = await _applicationTemplateService.UpdateApplicationAsync(updateApplicationModel, id);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteApplicationAsync([FromQuery] string id)
        {
            var response = await _applicationTemplateService.DeleteApplicationAsync(id);
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}
