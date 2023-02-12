using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class PlateKitchenObjectVisual : MonoBehaviour
    {
        /// <summary>
        /// A struct to expose a relationship between the inactive ingredient gameobjects in the plate and the incoming <see cref="KitchenObjectSO"/>
        /// </summary>
        [Serializable]
        public struct PlateKitchenObjectPrefabPairing
        {
            /// <summary>
            /// The gameobject associated to the corresponding KitchenObjectSO
            /// </summary>
            public GameObject IngredientGameobject;
            /// <summary>
            /// The KitchenObjectSO used to identify the correpsonding gameobject
            /// </summary>
            public KitchenObjectSO IngredientKitchenObjectSO;
        }

        /// <summary>
        /// A list of KitchenObjectSO-gameobject pairings exposing the relationships between them
        /// </summary>
        [SerializeField] private List<PlateKitchenObjectPrefabPairing> plateKitchenObjectPrefabPairings;
        /// <summary>
        /// A reference to the PlateKitchenObject to subscribe to its events
        /// </summary>
        [SerializeField] private PlateKitchenObject plateKitchenObject;


        // Start is called before the first frame update
        void Start()
        {
            plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        }

        /// <summary>
        /// Function to run on <see cref="plateKitchenObject"/> 's for new ingredient added to the plate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.IngredientAddedEventArgs e)
        {
            this.Show(kitchenObjectSO: e.AddedIngredient);
        }

        /// <summary>
        /// Helper to find the gameobject associated to the incoming <paramref name="kitchenObjectSO"/> and activate it
        /// </summary>
        /// <param name="kitchenObjectSO"></param>
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
