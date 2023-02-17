using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class FinishLoading : MonoBehaviour
    {
        /// <summary>
        /// The time, in seconds, until the game scene should load (this is an arbitrary number)
        /// </summary>
        private float mTimeUntilLoaded = 1.0f;


        private void Update()
        {
            //if the timer is still greater than 0
            if (mTimeUntilLoaded > 0)
            {
                mTimeUntilLoaded -= Time.deltaTime;
            }
            //else the timer is elapsed
            else
            {
                //Load the game scene
                Loader.Load(Loader.GAME_SCENE);
            }
        }
    }

}