namespace Web;

using Domain;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ImportController(ImportService service) : ControllerBase
{
    [HttpPost]
    public async Task Import([FromForm] IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        await service.ImportRecipes(stream);
    }
}
