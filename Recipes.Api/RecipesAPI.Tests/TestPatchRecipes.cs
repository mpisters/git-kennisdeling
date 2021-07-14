// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using RecipesAPI.Controllers;
// using RecipesAPI.Domain;
// using RecipesAPI.Models;
// using Xunit;
//
// namespace RecipesAPI.Tests
// {
//   public class TestPatchRecipes
//   {
//     private readonly DatabaseFixture _fixture;
//
//     public TestPatchRecipes(DatabaseFixture fixture)
//     {
//       _fixture = fixture;
//     }
//     
//     [Fact]
//     public async Task TestReturnsBadRequestWhenRecipeIdIsUnknown()
//     {
//       var options = _fixture.options;
//       var recipesContext = new RecipesContext(options);
//       var controller = CreateRecipesController(recipesContext);
//       var ingredientList = new List<UpdatedIngredientDto>();
//       var newRecipe = new UpdatedRecipeDTO("Updated recipe", "Updated description", ingredientList);
//       
//       var result = await controller.PatchRecipe(9999, newRecipe);
//       
//       Assert.IsType<BadRequestResult>(result.Result);
//     }
//
//     [Fact]
//     public async Task TestReturnsNotFoundWhenRecipeIdIsNotFound()
//     {
//       var options = _fixture.options;
//       var recipesContext = new RecipesContext(options);
//       var controller = CreateRecipesController(recipesContext);
//       var ingredientList = new List<UpdatedIngredientDto>();
//       var newRecipe = new UpdatedRecipeDTO("Updated recipe", "Updated description", ingredientList) {Id = 9999};
//       
//       var result = await controller.PatchRecipe(9999, newRecipe);
//       
//       Assert.IsType<NotFoundResult>(result.Result);
//     }
//
//     [Fact]
//     public async Task TestUpdatesRecipeWithoutIngredients()
//     {
//       var options = _fixture.options;
//       var recipesContext = new RecipesContext(options);
//       var controller = CreateRecipesController(recipesContext);
//       var emptyIngredientList = new List<Ingredient>();
//       var existingIngredient = new Recipe
//           ( "existing description", "existing title",  emptyIngredientList){Id = 9};
//       await recipesContext.Recipes.AddAsync(existingIngredient);
//       await recipesContext.SaveChangesAsync();
//
//       var emptyList = new List<UpdatedIngredientDto>();
//       var newRecipe = new UpdatedRecipeDTO("Updated recipe", "Updated description", emptyList) {Id = 9};
//       
//       await controller.PatchRecipe(9, newRecipe);
//       
//       var updatedRecipe = await recipesContext.Recipes.FindAsync((long) 9);
//       Assert.Equal("Updated recipe", updatedRecipe.Name);
//       Assert.Equal("Updated description", updatedRecipe.Description);
//       Assert.Empty(updatedRecipe.Ingredients);
//     }
//
//     [Fact]
//     public async Task TestUpdatesRecipeWithNewIngredients()
//     {
//       var options = _fixture.options;
//       var recipesContext = new RecipesContext(options);
//       var controller = CreateRecipesController(recipesContext);
//       // create existing recipe without ingredients
//   
//       var emptyIngredientList = new List<Ingredient>();
//       var existingRecipe = new Recipe
//           ("existing description", "existing title", emptyIngredientList){ Id = 8};
//       await recipesContext.Recipes.AddAsync(existingRecipe);
//       await recipesContext.SaveChangesAsync();
//
//       var ingredientList = new List<UpdatedIngredientDto>();
//       var ingredient1 = new UpdatedIngredientDto("Ingredient1", 100, "gr");
//       var ingredient2 = new UpdatedIngredientDto("Ingredient2", 200, "kg");
//       ingredientList.Add(ingredient1);
//       ingredientList.Add(ingredient2);
//       var newRecipe = new UpdatedRecipeDTO("Updated recipe", "Updated description", ingredientList) {Id = 8};
//       
//       await controller.PatchRecipe(8, newRecipe);
//       
//       var updatedRecipe = await  recipesContext.Recipes.FindAsync((long) 8);
//       Assert.Equal("Updated recipe", updatedRecipe.Name);
//       Assert.Equal("Updated description", updatedRecipe.Description);
//       Assert.Equal(2, updatedRecipe.Ingredients.Count);
//       Assert.Equal("Ingredient1", updatedRecipe.Ingredients.ToList()[0].Name);
//       Assert.Equal(100, updatedRecipe.Ingredients.ToList()[0].Amount);
//       Assert.Equal("gr", updatedRecipe.Ingredients.ToList()[0].Unit);
//       Assert.Equal("Ingredient2", updatedRecipe.Ingredients.ToList()[1].Name);
//       Assert.Equal(200, updatedRecipe.Ingredients.ToList()[1].Amount);
//       Assert.Equal("kg", updatedRecipe.Ingredients.ToList()[1].Unit);
//     }
//
//     [Fact]
//     public async Task TestUpdatesRecipeWithExistingIngredients()
//     {
//       var options = _fixture.options;
//       var recipesContext = new RecipesContext(options);
//       var controller = CreateRecipesController(recipesContext);
//
//       // add existing ingredients to database
//       var ingredientList = new List<Ingredient>();
//       var existingIngredient1 = new Ingredient("Ingredient1",100,  "gr") {Id = 6};
//       var existingIngredient2 = new Ingredient("Ingredient2",  200, "kg"){ Id = 7};
//       ingredientList.Add(existingIngredient1);
//       ingredientList.Add(existingIngredient2);
//       await recipesContext.Ingredients.AddAsync(existingIngredient1);
//       await recipesContext.Ingredients.AddAsync(existingIngredient2);
//
//       // have existing recipe with ingredients
//       var existingRecipe = new Recipe(
//           "existing description","existing title", ingredientList){ Id = 4};
//       await recipesContext.Recipes.AddAsync(existingRecipe);
//       await recipesContext.SaveChangesAsync();
//
//
//       var updatedIngredientList = new List<UpdatedIngredientDto>();
//       var ingredient1 = new UpdatedIngredientDto("Ingredient1 updated", 333, "gr") {Id = 6};
//       var ingredient2 = new UpdatedIngredientDto("Ingredient2 updated", 555, "kg") {Id = 7};
//       updatedIngredientList[0] = ingredient1;
//       updatedIngredientList[1] = ingredient2;
//       var newRecipe = new UpdatedRecipeDTO("Updated recipe", "Updated description", updatedIngredientList) {Id = 4};
//       
//       await controller.PatchRecipe(4, newRecipe);
//       
//       var updatedRecipe = await recipesContext.Recipes.FindAsync((long) 4);
//       Assert.Equal("Updated recipe", updatedRecipe.Name);
//       Assert.Equal("Updated description", updatedRecipe.Description);
//       Assert.Equal(2, updatedRecipe.Ingredients.Count);
//       Assert.Equal("Ingredient1 updated", updatedRecipe.Ingredients.ToList()[0].Name);
//       Assert.Equal(333, updatedRecipe.Ingredients.ToList()[0].Amount);
//       Assert.Equal("gr", updatedRecipe.Ingredients.ToList()[0].Unit);
//       Assert.Equal("Ingredient2 updated", updatedRecipe.Ingredients.ToList()[1].Name);
//       Assert.Equal(555, updatedRecipe.Ingredients.ToList()[1].Amount);
//       Assert.Equal("kg", updatedRecipe.Ingredients.ToList()[1].Unit);
//     }
//     
//     private RecipesController CreateRecipesController(RecipesContext recipesContext)
//     {
//       var databaseActions = new DatabaseActions(recipesContext);
//       var ingredientDomain = new IngredientDomain(databaseActions);
//       var recipesDomain = new RecipesDomain(databaseActions, ingredientDomain);
//       return new RecipesController(recipesDomain);
//     }
//   }
// }