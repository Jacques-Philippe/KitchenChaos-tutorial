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
        [SerializeField] private GameObject templateRecipeUI;


        private void Start()
        {
            DeliveryManager.Instance.OnRecipeAdded += Instance_OnRecipeAdded;
            DeliveryManager.Instance.OnRecipeRemoved += Instance_OnRecipeRemoved;

            this.templateRecipeUI.SetActive(false);
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
            //Clear existing UI, destroy all but the template UI
            foreach (Transform child in this.transform)
            {
                if (child.gameObject != this.templateRecipeUI)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
            //Create new UI
            foreach (var recipe in DeliveryManager.Instance.GetListOfWaitingRecipes())
            {
                GameObject templateInstance = GameObject.Instantiate(original: this.templateRecipeUI, parent: this.transform);
                if (templateInstance.TryGetComponent<TemplateRecipeUI>(out TemplateRecipeUI templateRecipeUI))
                {
                    templateRecipeUI.SetRecipeSO(recipe);
                }
                templateInstance.SetActive(true);
            }
        }


    }
}
