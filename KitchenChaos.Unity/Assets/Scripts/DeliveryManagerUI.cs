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
        /// <summary>
        /// The prefab to be instantiated to UI on new recipe order added or removed
        /// </summary>
        [SerializeField] private GameObject templateRecipeUIPrefab;


        private void Start()
        {
            DeliveryManager.Instance.OnRecipeAdded += Instance_OnRecipeAdded; ;
            DeliveryManager.Instance.OnRecipeRemoved += Instance_OnRecipeRemoved; ;
        }

        private void Instance_OnRecipeRemoved(object sender, System.EventArgs e)
        {
            this.UpdateUI();
        }

        private void Instance_OnRecipeAdded(object sender, System.EventArgs e)
        {
            this.UpdateUI();

        }

        /// <summary>
        /// Function to invoke on food order added or removed; updates the UI with the new list of orders
        /// </summary>
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
                GameObject recipeDisplayGameobj = GameObject.Instantiate(original: this.templateRecipeUIPrefab, parent: this.transform);
                if (recipeDisplayGameobj.TryGetComponent<TemplateRecipeUI>(out TemplateRecipeUI templateRecipeUI))
                {
                    templateRecipeUI.SetRecipeSO(recipe);
                }
            }
        }

    }
}
