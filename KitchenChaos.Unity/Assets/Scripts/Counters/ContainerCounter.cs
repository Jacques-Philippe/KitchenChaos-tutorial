using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class ContainerCounter : BaseCounter
    {
        /// <summary>
        /// A reference to the <see cref="KitchenObjectSO"/> object which should drive this table's functionality
        /// </summary>
        [SerializeField] private KitchenObjectSO mKitchenObjectSO;

        /// <summary>
        /// Event fired for container opened
        /// </summary>
        public event EventHandler ContainerOpened;


        public override void Interact(Player player)
        {
            KitchenObject counterKitchenObject = this.GetKitchenObject();
            KitchenObject playerKitchenObject = player.GetKitchenObject();

            //if the player has a kitchen object
            if (player.HasKitchenObject())
            {
                //if there is no kitchen object on the counter
                if (counterKitchenObject == null)
                {
                    //put the player kithen object onto the counter
                    playerKitchenObject.setKitchenObjectParent(this);
                }
                //if there is a kitchen object on the counter
                else
                {
                    //if the player has a plate
                    if (playerKitchenObject.TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        //if the ingredient can be added to the plate
                        if (plateKitchenObject.TryAddIngredient(counterKitchenObject.GetKitchenObjectSO()))
                        {
                            counterKitchenObject.DestroySelf();
                        }
                    }
                }
            }
            //else if the player doesn't have a kitchen object
            else
            {
                //if there is no kitchen object on the counter
                if (counterKitchenObject == null)
                {
                    //spawn a kitchen object
                    GameObject kitchenGameObj = Instantiate(original: mKitchenObjectSO.Prefab);
                    if (kitchenGameObj.TryGetComponent<KitchenObject>(out KitchenObject _kitchenObject))
                    {
                        //give it to the player
                        _kitchenObject.setKitchenObjectParent(player);
                        this.ContainerOpened?.Invoke(sender: this, e: EventArgs.Empty);
                    }
                }
                //else if there is a kitchen object on the counter
                else
                {
                    //let player pick up counter kitchen object
                    counterKitchenObject.setKitchenObjectParent(player);
                }
            }
        }
    }

}