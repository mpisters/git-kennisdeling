using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipesAPI.Controllers;
using RecipesAPI.Domain;
using RecipesAPI.Models;
using Xunit;

namespace RecipesAPI.Tests
{
    
    [Collection("Database collection")]
    public class TestPostRecipes
    {
        private readonly DatabaseFixture _fixture;

        public TestPostRecipes(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task PostRecipeWithoutIngredients()
        {
            // ToDo fix   System.NullReferenceException : Object reference not set to an instance of an object.
            // ToDo fix running tests in rider
            var ingredientList = new List<CreatedIngredientDto>();
            var controller = CreateRecipesController();
            var newRecipe = new CreatedRecipeDto("Test recipe", "Test description", ingredientList);
            
            var createdRecipe = await controller.PostRecipe(newRecipe);
            
            Assert.Equal("Test recipe", createdRecipe.Value.Name);
            Assert.Equal("Test description", createdRecipe.Value.Description);
            Assert.Empty(createdRecipe.Value.Ingredients);
        }

        [Fact]
        public async Task PostRecipeWithIngredient()
        {
            var controller = CreateRecipesController();
            var newIngredient = new CreatedIngredientDto("ui", 2, "unit");
            var ingredientList = new[] {newIngredient};
            var newRecipe = new CreatedRecipeDto("Test recipe", "Test description", ingredientList);
            
            var createdRecipe = await controller.PostRecipe(newRecipe);
            
            Assert.Equal("Test recipe", createdRecipe.Value.Name);
            Assert.Equal("Test description", createdRecipe.Value.Description);
            var firstIngredient = createdRecipe.Value.Ingredients.ToList()[0];
            Assert.Equal("ui", firstIngredient.Name);
            Assert.Equal(2, firstIngredient.Amount);
            Assert.Equal("unit", firstIngredient.Unit);
        }
        [Fact]
        public async Task PostRecipeWithIngredients()
        {
            var controller = CreateRecipesController();
            var newIngredient = new CreatedIngredientDto("ui", 2, "unit");
            var newIngredient2 = new CreatedIngredientDto("knoflook", 3, "unit");
            var ingredientList = new[] {newIngredient, newIngredient2};
        
            var newRecipe = new CreatedRecipeDto("Test recipe", "Test description", ingredientList);
            var createdRecipe = await controller.PostRecipe(newRecipe);
            Assert.Equal("Test recipe", createdRecipe.Value.Name);
            Assert.Equal("Test description", createdRecipe.Value.Description);
            Assert.Equal(2, createdRecipe.Value.Ingredients.Count);
        }

        private RecipesController CreateRecipesController()
        {
            var options = _fixture.Options;
            var recipesContext = new RecipesContext(options);
            var databaseActions = new DatabaseActions(recipesContext);
            var ingredientDomain = new IngredientDomain(databaseActions);
            var recipesDomain = new RecipesDomain(databaseActions, ingredientDomain);
            return new RecipesController(recipesDomain);
        }
    }
    
}