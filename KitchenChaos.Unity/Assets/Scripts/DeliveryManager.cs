using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace KitchenChaosTutorial
{

    public class DeliveryManager : MonoBehaviour
    {
        /// <summary>
        /// The list of recipes from which to choose from
        /// </summary>
        [SerializeField] private RecipeListSO recipeListSO;

        /// <summary>
        /// Singleton instance of the DeliveryManager class.
        /// </summary>
        public static DeliveryManager Instance { private set; get; }

        /// <summary>
        /// Event fired for a new recipe needs to be handled by the player
        /// </summary>
        public event EventHandler OnOrderAdded;

        /// <summary>
        /// Event fired for a recipe handled by the player
        /// </summary>
        public event EventHandler OnOrderRemoved;

        /// <summary>
        /// Event fired for the player successfully completes an order
        /// </summary>
        public event EventHandler OnOrderSuccess;

        /// <summary>
        /// Event fired for the player unsuccessfully completes an order
        /// </summary>
        public event EventHandler OnOrderFailure;

        /// <summary>
        /// The time until the next recipe is selected
        /// </summary>
        private float recipeSelectionTimer = 0.0f;
        /// <summary>
        /// The delay, in seconds, between each recipe selection
        /// </summary>
        private float recipeSelectionDelay = 4.0f;
        /// <summary>
        /// The maximum number of recipes to be active at a time
        /// </summary>
        private int maxActiveRecipeAmount = 4;

        /// <summary>
        /// The list of recipes currently waiting to be addressed
        /// </summary>
        private List<RecipeSO> waitingRecipes = new List<RecipeSO>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("More than one instance of DeliveryManager has been detected when there should be only one");
            }
        }

        private void Update()
        {
            //if there are less than the max number of active recipes
            if (waitingRecipes.Count < maxActiveRecipeAmount)
            {
                //if recipe selection timer is greater than the delay
                if (recipeSelectionTimer >= recipeSelectionDelay)
                {
                    //Choose a recipe
                    RecipeSO recipe = recipeListSO.recipeSOList[UnityEngine.Random.Range(minInclusive: 0, maxExclusive: recipeListSO.recipeSOList.Count)];
                    Debug.Log($"New order: {recipe.recipeName}");

                    //Add it to the list of currently waiting recipes
                    this.waitingRecipes.Add(recipe);
                    this.OnOrderAdded?.Invoke(this, EventArgs.Empty);

                    recipeSelectionTimer = 0.0f;
                }
                //else the recipe selection timer is less than the delay
                else
                {
                    recipeSelectionTimer += Time.deltaTime;
                }
            }
        }

        /// <summary>
        /// Function invoked for plate delivered via <see cref="DeliveryCounter"/>
        /// </summary>
        /// <param name="deliveredPlateIngredients"></param>
        public void Deliver(List<KitchenObjectSO> deliveredPlateIngredients)
        {
            //if ingredients correspond to a recipe
            foreach (var recipe in this.recipeListSO.recipeSOList)
            {
                if (IngredientsMatchRecipe(recipe: recipe, incomingIngredients: deliveredPlateIngredients))
                {
                    //Ingredients match the recipe!
                    Debug.Log($"Ingredients match recipe {recipe.recipeName}!");
                    this.waitingRecipes.Remove(recipe);
                    this.OnOrderRemoved?.Invoke(this, EventArgs.Empty);
                    this.OnOrderSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
            //else if ingredients do not correspond to a recipe
            this.OnOrderFailure?.Invoke(this, EventArgs.Empty);

        }

        /// <summary>
        /// Return true for incoming ingredients correspond to recipe
        /// </summary>
        /// <param name="recipe"></param>
        /// <param name="incomingIngredients"></param>
        /// <returns></returns>
        private bool IngredientsMatchRecipe(RecipeSO recipe, List<KitchenObjectSO> incomingIngredients)
        {
            List<KitchenObjectSO> recipeIngredients = recipe.kitchenObjectSOList;
            if (incomingIngredients.Count != recipeIngredients.Count)
            {
                return false;
            }
            foreach(var incomingIngredient in incomingIngredients)
            {
                if (!recipeIngredients.Contains(incomingIngredient))
                {
                    return false;
                }
            }
            return true;
        }

        public List<RecipeSO> GetListOfWaitingRecipes()
        {
            return this.waitingRecipes;
        }
    }

}