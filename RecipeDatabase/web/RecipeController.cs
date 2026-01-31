namespace Web;

using Domain;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RecipeController(Database database) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] RecipeSearch? search)
    {
        var recipes = await database.GetRecipes(search);
        return Ok(recipes);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Tags()
    {
        var tags = await database.GetAllTags();
        return Ok(tags);
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] Recipe recipe)
    {
        await database.InsertRecipe(recipe);
        return Created();
    }

    [HttpPut]
    public Task Update([FromBody] Recipe recipe)
    {
        return database.UpdateRecipe(recipe);
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> Delete([FromRoute] string name)
    {
        await database.DeleteRecipe(name);
        return NoContent();
    }
}
