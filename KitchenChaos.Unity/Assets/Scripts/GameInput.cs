using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class GameInput : MonoBehaviour
    {
        private PlayerInputActions playerInputActions;

        /// <summary>
        /// Event fired on player pressed the Interact input
        /// </summary>
        public event EventHandler OnInteract;

        /// <summary>
        /// Event fired on player pressed the Alt Interact input
        /// </summary>
        public event EventHandler OnAltInteract;

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();

            playerInputActions.Player.Interact.performed += Interact_performed;
            playerInputActions.Player.AltInteract.performed += AltInteract_performed;
        }

        /// <summary>
        /// Function invoked for Alt Interact playeraction performed
        /// </summary>
        /// <param name="obj"></param>
        private void AltInteract_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            this.OnAltInteract?.Invoke(this, EventArgs.Empty);
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
