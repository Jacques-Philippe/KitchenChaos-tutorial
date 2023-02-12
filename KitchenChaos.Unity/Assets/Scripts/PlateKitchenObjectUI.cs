using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaosTutorial
{

    public class PlateKitchenObjectUI : MonoBehaviour
    {
        //Every time we add a new ingredient, instantiate a new UI sprite for all ingredients

        [SerializeField] private PlateKitchenObject plateKitchenObject;
        [SerializeField] private GameObject iconTemplate;

        private List<GameObject> spawnedIcons = new List<GameObject>(); 

        private void Start()
        {
            this.plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

            iconTemplate.SetActive(false);
        }

        private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.IngredientAddedEventArgs e)
        {
            this.UpdateVisual();
        }

        private void UpdateVisual()
        {
            foreach(var spawned in spawnedIcons)
            {
                GameObject.Destroy(spawned);
            }
            this.spawnedIcons.Clear();

            foreach(var ingredient in this.plateKitchenObject.GetIngredientList())
            {
                GameObject icon = GameObject.Instantiate(iconTemplate, parent: this.transform);
                if (icon.TryGetComponent<PlateIconUI>(out PlateIconUI plateIconUI))
                {
                    plateIconUI.SetIcon(ingredient);
                    this.spawnedIcons.Add(icon.gameObject);
                    icon.SetActive(true);
                }
            }
        }
    }
}
