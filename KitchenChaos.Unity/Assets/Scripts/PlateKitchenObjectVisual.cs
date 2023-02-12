using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class PlateKitchenObjectVisual : MonoBehaviour
    {
        [Serializable]
        public struct PlateKitchenObjectPrefabPairing
        {
            public GameObject IngredientGameobject;
            public KitchenObjectSO IngredientKitchenObjectSO;
        }

        [SerializeField] private List<PlateKitchenObjectPrefabPairing> plateKitchenObjectPrefabPairings;
        [SerializeField] private PlateKitchenObject plateKitchenObject;


        // Start is called before the first frame update
        void Start()
        {
            plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        }

        private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.IngredientAddedEventArgs e)
        {
            this.Show(kitchenObjectSO: e.AddedIngredient);
        }

        private void Show(KitchenObjectSO kitchenObjectSO)
        {
            foreach(var pairing in plateKitchenObjectPrefabPairings)
            {
                if (pairing.IngredientKitchenObjectSO == kitchenObjectSO)
                {
                    pairing.IngredientGameobject.SetActive(true);
                    return;
                }
            }
        }

    }
}
