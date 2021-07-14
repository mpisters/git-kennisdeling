using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipesAPI.Models;

namespace RecipesAPI.Domain
{
    public class RecipesDomain
    {
        private readonly DatabaseActions _databaseActions;
        private readonly IngredientDomain _ingredientDomain;

        public RecipesDomain(DatabaseActions databaseActions, IngredientDomain ingredientDomain)
        {
            _databaseActions = databaseActions;
            _ingredientDomain = ingredientDomain;
        }

        public async Task<ActionResult<Recipe>> UpdateRecipe(Recipe currentRecipe, UpdatedRecipeDto updatedRecipe)
        {
            if (updatedRecipe.Name != null)
            {
                currentRecipe.Name = updatedRecipe.Name;
            }

            if (updatedRecipe.Description != null)
            {
                currentRecipe.Description = updatedRecipe.Description;
            }

            if (updatedRecipe.Ingredients != null)
            {
                currentRecipe.Ingredients =
                        await _ingredientDomain.UpdateOrCreateIngredientList(updatedRecipe.Ingredients);
            }

            await _databaseActions.UpdateRecipe(currentRecipe);
            return currentRecipe;
        }
        
        public async Task<ActionResult<Recipe>> GetRecipe(long id)
        {
            return await _databaseActions.GetRecipe(id);
        }

        public async Task<ActionResult<ICollection<Recipe>>> GetRecipes()
        {
            return await _databaseActions.GetRecipes();
        }

        public async Task RemoveRecipe(Recipe recipe)
        {
            await _databaseActions.RemoveRecipe(recipe);
            await Task.CompletedTask;
        }

        public async Task<ActionResult<Recipe>> CreateRecipe(CreatedRecipeDto newCreatedRecipe)
        {
            var ingredients = _ingredientDomain.CreateIngredientList(newCreatedRecipe.Ingredients);
            var recipe = new Recipe
            {
                    Name = newCreatedRecipe.Name,
                    Description = newCreatedRecipe.Description,
                    Ingredients = ingredients
            };
            await _databaseActions.CreateRecipe(recipe);
            return recipe;
        }
    }
}