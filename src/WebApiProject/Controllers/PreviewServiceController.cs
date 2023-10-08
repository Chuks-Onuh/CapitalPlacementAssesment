using Microsoft.AspNetCore.Mvc;
using WebApiProject.Contracts.Interfaces;

namespace WebAPIProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreviewServiceController : ControllerBase
    {
        private readonly IPreviewRepository _previewService;

        // Constructor injection for the service interface.
        public PreviewServiceController(IPreviewRepository previewService)
        {
            _previewService = previewService;
        }

        [HttpGet("GetPreview")]
        public async Task<IActionResult> GetPreviewAsync([FromQuery] string programTitle)
        {
            var response = await _previewService.GetProgramPreviewAsync(programTitle);
            return response.Status ? Ok(response) : BadRequest(response.Message);
        }
    }
}
