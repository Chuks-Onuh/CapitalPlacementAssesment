using ConsoleProject.Dtos.ServiceRequestModels.Question;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Contracts.Interfaces;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _questionService;
        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionService = questionRepository;
        }

        [HttpPost("question")]
        public async Task<IActionResult> CreateQuestionAsync([FromBody] BaseQuestionRequestModel requestModel)
        {
            var response = await _questionService.CreateQuestionAsync(requestModel);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("question/{Id}")]
        public async Task<IActionResult> GetQuestionAsync(string Id)
        {
            var response = await _questionService.GetQuestionAsync(Id);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestionsAsync()
        {
            var response = await _questionService.GetQuestionsAsync();
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPut("question/{id}")]
        public async Task<IActionResult> UpdateQuestionAsync([FromBody] UpdateQuestionModel updateQuestionModel, [FromRoute] string id)
        {
            var response = await _questionService.UpdateQuestionAsync(updateQuestionModel, id);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("question")]
        public async Task<IActionResult> DeleteQuestionAsync([FromQuery] string id)
        {
            var response = await _questionService.DeleteQuestionAsync(id);
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}
