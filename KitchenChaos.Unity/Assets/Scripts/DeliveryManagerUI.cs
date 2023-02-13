using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaosTutorial
{

    public class DeliveryManagerUI : MonoBehaviour
    {
        [SerializeField] private GameObject templateRecipeUIPrefab;


        private void Start()
        {
            DeliveryManager.Instance.OnRecipeAdded += DeliveryManager_OnRecipeAdded;
            DeliveryManager.Instance.OnRecipeRemoved += DeliveryManager_OnRecipeRemoved;
        }

        private void DeliveryManager_OnRecipeRemoved(object sender, DeliveryManager.OnRecipeRemovedEventArgs e)
        {
            this.UpdateUI();
        }

        private void DeliveryManager_OnRecipeAdded(object sender, DeliveryManager.OnRecipeAddedEventArgs e)
        {
            this.UpdateUI();
        }

        private void UpdateUI()
        {
            //Clear existing UI
            foreach(Transform child in this.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            //Create new UI
            foreach(var recipe in DeliveryManager.Instance.GetListOfWaitingRecipes())
            {
                this.DisplayRecipe(recipe);
            }
        }

        private void DisplayRecipe(RecipeSO recipe)
        {
            GameObject recipeDisplayGameobj = GameObject.Instantiate(original: this.templateRecipeUIPrefab, parent: this.transform);
            if (recipeDisplayGameobj.TryGetComponent<TemplateRecipeUI>(out TemplateRecipeUI templateRecipeUI))
            {
                templateRecipeUI.SetRecipeName(recipe.recipeName);
                List<Sprite> sprites = recipe.kitchenObjectSOList.Select(kitchenObjectSO => kitchenObjectSO.Sprite).ToList();
                templateRecipeUI.SetIcons(sprites);
            }
        }
    }
}
