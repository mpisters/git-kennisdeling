using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipesAPI.Domain;
using RecipesAPI.Models;

namespace RecipesAPI.Controllers
{
    [Route("api/Recipes")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private RecipesDomain _recipesDomain;
        public RecipesController(RecipesDomain domain)
        {
            _recipesDomain = domain;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Recipe>>> GetRecipes()
        {
            return await _recipesDomain.GetRecipes();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipeById(long id)
        {
            var recipe = await _recipesDomain.GetRecipe(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return recipe;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Recipe>> PatchRecipe(long id, UpdatedRecipeDto updatedRecipe)
        {
            if (id != updatedRecipe.Id)
            {
                return BadRequest();
            }

            var currentRecipe = await _recipesDomain.GetRecipe(id);
            if (currentRecipe == null)
            {
                return NotFound();
            }

            var recipe = await _recipesDomain.UpdateRecipe(currentRecipe.Value, updatedRecipe);
            return CreatedAtAction(nameof(GetRecipeById), recipe.Value.Id, recipe);
        }

        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(CreatedRecipeDto newCreatedRecipe)
        {
            var recipe = await _recipesDomain.CreateRecipe(newCreatedRecipe);
            return CreatedAtAction(nameof(GetRecipeById), recipe.Value.Id, recipe);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRecipe(long id)
        {
            var recipe = await _recipesDomain.GetRecipe(id);
            if (recipe == null)
            {
                return NotFound();
            }
            await _recipesDomain.RemoveRecipe(recipe.Value);
            return NoContent();
        }
    }
    

}