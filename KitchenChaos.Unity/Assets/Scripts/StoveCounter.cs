using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{
    public class StoveCounter : BaseCounter
    {
        [SerializeField] private FryRecipeSO[] fryRecipes;

        private bool shouldUpdateTimer = false;
        private float fryTimer;
        private float totalTimeToFry;

        private void Update()
        {
            if (shouldUpdateTimer)
            {
                fryTimer += Time.deltaTime;
                if (fryTimer >= totalTimeToFry)
                {
                    this.Cook();
                    StopIncrementingFryTimer();
                }
            }
        }

        public override void Interact(Player player)
        {
            KitchenObject playerKitchenObject = player.GetKitchenObject();
            KitchenObject counterKitchenObject = this.GetKitchenObject();
            //if the player has an item which can be fried
            if (playerKitchenObject != null && this.KitchenObjectCanBeFried(playerKitchenObject))
            {
                FryRecipeSO fryRecipe = GetFryRecipe(playerKitchenObject);
                totalTimeToFry = fryRecipe.TimeToFry;

                //place the item on the stove
                playerKitchenObject.setKitchenObjectParent(this);

                //start incrementing timer
                this.StartIncrementingFryTimer();
            }
            //else if the player has no item and there is an item on the stove
            else if (playerKitchenObject == null && counterKitchenObject != null)
            {
                //pick up the kitchen object and give it to the player
                counterKitchenObject.setKitchenObjectParent(player);
            }
        }

        private void StartIncrementingFryTimer()
        {
            this.fryTimer = 0.0f;
            this.shouldUpdateTimer = true;
        }

        private void StopIncrementingFryTimer()
        {
            this.shouldUpdateTimer = false;
        }

        /// <summary>
        /// Whether the <paramref name="inputKitchenObject"/> can be fried
        /// </summary>
        /// <param name="inputKitchenObject"></param>
        private bool KitchenObjectCanBeFried(KitchenObject inputKitchenObject)
        {
            foreach (var recipe in fryRecipes)
            {
                if (inputKitchenObject.GetKitchenObjectSO() == recipe.inputKitchenObject)
                {
                    return true;
                }
            }
            return false;
        }

        private FryRecipeSO GetFryRecipe(KitchenObject inputKitchenObject)
        {
            foreach (var recipe in fryRecipes)
            {
                if (inputKitchenObject.GetKitchenObjectSO() == recipe.inputKitchenObject)
                {
                    return recipe;
                }
            }
            return null;
        }

        private void Cook()
        {
            //Get reference to next counter kitchen object to spawn
            KitchenObject counterKitchenObject = this.GetKitchenObject();
            FryRecipeSO fryRecipe = this.GetFryRecipe(counterKitchenObject);
            
            GameObject next = fryRecipe.outputKitchenObject.Prefab;
            //Destroy existing counter kitchen object
            counterKitchenObject.DestroySelf();
            //Spawn new counter kitchen object
            GameObject newKitchenGameObject = GameObject.Instantiate(next, parent: this.GetKitchenObjectFollowTransform());
            if (newKitchenGameObject.TryGetComponent<KitchenObject>(out KitchenObject kitchenObject))
            {
                kitchenObject.setKitchenObjectParent(this);
            }
        }
    }

}
