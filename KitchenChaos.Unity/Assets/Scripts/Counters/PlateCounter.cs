using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class PlateCounter : BaseCounter
    {
        [SerializeField] private KitchenObjectSO mPlateKitchenObjectSO;

        /// <summary>
        /// The timer responsible for spawning plater
        /// </summary>
        private float mSpawnTimer = 0.0f;
        /// <summary>
        /// The interval between each plate spawn
        /// </summary>
        private float mTimeToSpawnPlate = 4.0f;

        /// <summary>
        /// Event fired for a new plate spawned
        /// </summary>
        public event EventHandler OnPlateSpawned;

        //Spawn a plate every four seconds
        //Unless there are already four plates

        private void Update()
        {
            if (mSpawnTimer >= mTimeToSpawnPlate)
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectSO: mPlateKitchenObjectSO, this);
                mSpawnTimer = 0.0f;
                this.OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                mSpawnTimer += Time.deltaTime;
            }
        }

        public override void Interact(Player player)
        {
            KitchenObject counterKitchenObject = this.GetKitchenObject();
            KitchenObject playerKitchenObject = player.GetKitchenObject();
            
            //if there is a plate on the counter
            
            if (counterKitchenObject != null)
            {
                //the player doesn't have a kitchen object,

                if (playerKitchenObject == null)
                {
                    //give it to the player
                    counterKitchenObject.setKitchenObjectParent(player);
                }
            }
        }

    }
}
