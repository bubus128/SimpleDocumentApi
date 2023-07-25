using DocumentApi.Filters;
using DocumentApi.Modles;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DocumentApi.Controllers
{
    [ApiController]
    [Route("api/test/{x}")]
    public class DocumentController : Controller
    {
        [HttpPost]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> Post(int x, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty or not provided.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var fileContent = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());

                // You can perform any processing on the fileContent here if needed

                // Return the file content in JSON format
                try
                {
                    DocumentsSetModel documentsSet = new(fileContent.ToString(), x);
                    return Ok(Json(documentsSet).Value);
                }
                catch(ArgumentException e)
                {
                    return BadRequest(e.Message);
                }
                
            }
        }
    }
}
