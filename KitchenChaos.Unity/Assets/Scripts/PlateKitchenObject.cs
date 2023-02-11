using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class PlateKitchenObject : KitchenObject
    {
        /// <summary>
        /// The list of valid kitchenObjectSOs which can be added to a plate
        /// </summary>
        [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList; 
        /// <summary>
        /// The list of all ingredients on the plate
        /// </summary>
        private List<KitchenObjectSO> ingredientList = new List<KitchenObjectSO>();

        /// <summary>
        /// Function to try to add an ingredient to the list of ingredients on the plate <br />
        /// Note this will fail for the ingredient already on the plate, or the wrong kind of ingredient added to the plate
        /// </summary>
        /// <param name="kitchenObjectSO"></param>
        /// <returns></returns>
        public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
        {
            //if the incoming kitchen object SO is not part of the list of valid kitchenObjectSOs
            if (!this.validKitchenObjectSOList.Contains(kitchenObjectSO))
            {
                //fail
                return false;
            }
            //if the ingredient has already been added
            if (this.ingredientList.Contains(kitchenObjectSO))
            {
                //fail
                return false;
            }
            this.ingredientList.Add(kitchenObjectSO);
            return true;
        }
    }
}
