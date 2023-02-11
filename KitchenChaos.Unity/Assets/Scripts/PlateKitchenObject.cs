using System.Collections.Generic;

namespace KitchenChaosTutorial
{

    public class PlateKitchenObject : KitchenObject
    {
        private List<KitchenObjectSO> ingredients = new List<KitchenObjectSO>();

        public void AddIngredient(KitchenObjectSO kitchenObjectSO)
        {
            this.ingredients.Add(kitchenObjectSO);
        }
    }
}
