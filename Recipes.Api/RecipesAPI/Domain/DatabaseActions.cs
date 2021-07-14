using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipesAPI.Models;

namespace RecipesAPI.Domain
{
    public class DatabaseActions
    {
        private readonly RecipesContext _context;

        public DatabaseActions(RecipesContext context)
        {
            _context = context;
        }

        public async Task CreateRecipe(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<ICollection<Recipe>>> GetRecipes()
        {
            return  await _context.Recipes.ToListAsync();
        }

        public async Task UpdateRecipe(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<Recipe>> GetRecipe(long id)
        {
            return await _context.Recipes.FindAsync(id);
        }

        public async void SaveIngredients(ICollection<Ingredient> ingredients)
        {
            await _context.Ingredients.AddRangeAsync(ingredients);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();
        }

        public async  Task<ActionResult<Ingredient>> GetIngredient(long id)
        {
            return await _context.Ingredients.FindAsync(id);
        }

        public async Task RemoveRecipe(Recipe recipe)
        {
             _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}