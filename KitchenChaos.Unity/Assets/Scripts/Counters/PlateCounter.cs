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

        private int platesToSpawn = 4;
        private int platesSpawned = 0;

        /// <summary>
        /// Event fired for a new plate spawned
        /// </summary>
        public event EventHandler OnPlateSpawned;

        /// <summary>
        /// Event fired for a plate taken
        /// </summary>
        public event EventHandler OnPlateTaken;


        //Spawn a plate every four seconds
        //Unless there are already four plates

        private void Update()
        {
            if (GameManager.Instance.IsGameStarted())
            {
                if (mSpawnTimer >= mTimeToSpawnPlate)
                {
                    if (platesSpawned < platesToSpawn)
                    {
                        mSpawnTimer = 0.0f;
                        this.platesSpawned++;

                        this.OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                    }
                }
                else
                {
                    mSpawnTimer += Time.deltaTime;
                }
            }
        }

        public override void Interact(Player player)
        {
            //if the player isn't holding anything
            if (!player.HasKitchenObject())
            {
                //if at least a single plate has been spawned
                if (platesSpawned > 0)
                {
                    //Give the player one of the plates
                    KitchenObject.SpawnKitchenObject(kitchenObjectSO: mPlateKitchenObjectSO, parent: player);

                    platesSpawned--;

                    this.OnPlateTaken?.Invoke(sender: this, EventArgs.Empty);
                }
            }
        }

    }
}
