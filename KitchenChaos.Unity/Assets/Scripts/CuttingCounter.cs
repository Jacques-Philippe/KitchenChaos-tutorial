using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class CuttingCounter : BaseCounter
    {
        
        public override void Interact(Player player)
        {
            throw new System.NotImplementedException();
        }

        public override void AlternateInteract()
        {
            //Cut the thing
            Debug.Log("Cut!");
        }
    }

}