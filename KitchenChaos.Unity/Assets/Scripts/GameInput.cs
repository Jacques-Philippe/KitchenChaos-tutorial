using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class GameInput : MonoBehaviour
    {
        private PlayerInputActions playerInputActions;

        public event EventHandler OnInteract;

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();

            playerInputActions.Player.Interact.performed += Interact_performed;
        }

        /// <summary>
        /// Function invoked for Interact playeraction performed
        /// </summary>
        /// <param name="obj"></param>
        private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            this.OnInteract?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Return the normalized vector for player's movement direction
        /// </summary>
        public Vector2 GetPlayerMovementDirectionNormalized()
        {
            Vector2 movementInput = playerInputActions.Player.Move.ReadValue<Vector2>();
            
            movementInput = movementInput.normalized;
            return movementInput;
        }
    }
}
