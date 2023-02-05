using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class GameInput : MonoBehaviour
    {
        /// <summary>
        /// Return the normalized vector for player's movement direction
        /// </summary>
        public Vector2 GetPlayerMovementDirectionNormalized()
        {
            Vector2 movementInput = new Vector2();
            //Get legacy input
            if (Input.GetKey(KeyCode.D))
            {
                movementInput.x = +1.0f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movementInput.x = -1.0f;
            }

            if (Input.GetKey(KeyCode.W))
            {
                movementInput.y = +1.0f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementInput.y = -1.0f;
            }
            movementInput = movementInput.normalized;
            return movementInput;
        }
    }
}
