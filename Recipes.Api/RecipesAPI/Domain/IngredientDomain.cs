using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipesAPI.Models;

namespace RecipesAPI.Domain
{
    public class IngredientDomain
    {
        private readonly DatabaseActions _databaseActions;

        public IngredientDomain(DatabaseActions databaseActions)
        {
            _databaseActions = databaseActions;
        }

        public Ingredient mapIngredientToDomain(CreatedIngredientDto newIngredient)
        {
            return new Ingredient(newIngredient.Name, newIngredient.Amount, newIngredient.Unit);
        }

        public ICollection<Ingredient> CreateIngredientList(ICollection<CreatedIngredientDto> newIngredients)
        {
            ICollection<Ingredient> ingredients = new List<Ingredient>();
            foreach (var ingredient in newIngredients)
            {
                var mappedIngredient = mapIngredientToDomain(ingredient);
                ingredients.Add(mappedIngredient);
            }
            _databaseActions.SaveIngredients(ingredients);
            return ingredients;
        }
        
        public async Task<ActionResult<Ingredient>> UpdateIngredient(UpdatedIngredientDto updatedIngredient, Ingredient currentIngredient)
        {
            if (updatedIngredient.Amount != null)
            {
                currentIngredient.Amount = (int) updatedIngredient.Amount;
            }

            if (updatedIngredient.Name != null)
            {
                currentIngredient.Name = updatedIngredient.Name;
            }

            if (updatedIngredient.Unit != null)
            {
                currentIngredient.Unit = updatedIngredient.Unit;
            }

            await _databaseActions.UpdateIngredient(currentIngredient);
            return currentIngredient;
        }

        public async Task<ICollection<Ingredient>> UpdateOrCreateIngredientList(
                ICollection<UpdatedIngredientDto> updatedIngredients)
        {
            ICollection<Ingredient> ingredients = new List<Ingredient>();
            ICollection<CreatedIngredientDto> newIngredientDtos = new List<CreatedIngredientDto>();
            foreach (var ingredient in updatedIngredients)
            {
                if (ingredient.Id == null && ingredient.Amount != null && ingredient.Unit != null && ingredient.Name != null)
                {
                    newIngredientDtos.Add(new CreatedIngredientDto(ingredient.Name,  (int) ingredient.Amount, ingredient.Unit));
                } else if (ingredient.Id != null)
                {
                    var foundIngredient = await _databaseActions.GetIngredient((long) ingredient.Id);
                    if (foundIngredient != null)
                    {
                        var updatedIngredient =  await UpdateIngredient(ingredient, foundIngredient.Value);
                        ingredients.Add(updatedIngredient.Value);
                    }
                }
            }

            var newIngredients = CreateIngredientList(newIngredientDtos);
            var allIngredients = newIngredients.Concat(ingredients) as ICollection<Ingredient>;
            return allIngredients;
        }
        
    }
}