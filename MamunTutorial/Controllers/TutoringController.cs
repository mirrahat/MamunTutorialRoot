using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[AllowAnonymous]
[Route("api/tutor")]
[ApiController]
public class TutoringController : ControllerBase
{
    private readonly OpenAiService _openAiService;

    public TutoringController(OpenAiService openAiService)
    {
        _openAiService = openAiService;
    }

    [AllowAnonymous]
    [HttpPost("ask")]
    public async Task<IActionResult> AskTutor([FromBody] TutorRequest request)
    {
       /* if (request == null || string.IsNullOrWhiteSpace(request.Question))
        {
            return BadRequest(new { error = "The 'Question' field is required." });
        }*/

        try
        {
            string response = await _openAiService.GetOpenAiResponse(request.Question);
            return Ok(new { response });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in AskTutor: {ex.Message}");
            return StatusCode(500, new { error = ex.Message });
        }
    }

}

public class TutorRequest
{
    public string Question { get; set; }
}
