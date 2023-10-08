using Microsoft.AspNetCore.Mvc;
using WebApiProject.Contracts.Interfaces;
using ConsoleProject.Dtos.ServiceRequestModels.Stage;

namespace CapitalPlacementAssesementTaskApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkFlowController : ControllerBase
    {
        private readonly IWorkFlowRepository _stageService;

        public WorkFlowController(IWorkFlowRepository stageService)
        {
            _stageService = stageService;
        }

        [HttpPost("create-stage")]
        public async Task<IActionResult> CreateStageAsync([FromBody] CreateStage programRequestModel)
        {
            var response = await _stageService.CreateStageAsync(programRequestModel);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("stage")]
        public async Task<IActionResult> GetStageAsync([FromQuery] string Id)
        {
            var response = await _stageService.GetStageAsync(Id);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("stages")]
        public async Task<IActionResult> GetStagesAsync()
        {
            var response = await _stageService.GetStagesAsync();
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPut("stage")]
        public async Task<IActionResult> UpdateStageAsync([FromBody] UpdateUsualStage updateUsualStage, [FromQuery] string stageName)
        {
            var response = await _stageService.UpdateUsualStageAsync(updateUsualStage, stageName);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPut("update-video-interview")]
        public async Task<IActionResult> UpdateVideoInterviewStageAsync([FromBody] UpdateVideoInterviewStage stageUpdateRequest, [FromQuery] string stageNameToUpdate)
        {
            var response = await _stageService.UpdateVideoInterviewStageAsync(stageUpdateRequest, stageNameToUpdate);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("stage")]
        public async Task<IActionResult> DeleteStageAsync([FromQuery] string id)
        {
            var response = await _stageService.DeleteStageAsync(id);
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}
