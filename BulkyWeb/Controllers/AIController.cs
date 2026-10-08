using BulkyWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class AIController : Controller
    {
        private readonly GeminiService _geminiService;

        public AIController(GeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        public async Task<IActionResult> Ask(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                return Content("Please provide a question.");
            }

            string response = await _geminiService.AskAsync(question);

            return Content(response);
        }
    }
}