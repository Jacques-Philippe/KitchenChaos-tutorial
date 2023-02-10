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

        //Spawn a plate every four seconds
        //Unless there are already four plates

        private void Update()
        {
            if (mSpawnTimer >= mTimeToSpawnPlate)
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectSO: mPlateKitchenObjectSO, this);
            }
            else
            {
                mSpawnTimer += Time.deltaTime;
            }
        }

        public override void Interact(Player player)
        {
            throw new System.NotImplementedException();
        }

    }
}
