using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KitchenChaosTutorial
{

    public class GameInput : MonoBehaviour
    {
        public enum Bindings
        {
            Move_Up,
            Move_Down,
            Move_Left,
            Move_Right,
            Interact,
            Interact_Alt,
            Pause,
            Gamepad_Interact,
            Gamepad_Interact_Alt,
            Gamepad_Pause,
        }

        /// <summary>
        /// Reference to the player input actions defined by the new input system
        /// </summary>
        private PlayerInputActions playerInputActions;

        /// <summary>
        /// Event fired on player pressed the Interact input
        /// </summary>
        public event EventHandler OnInteract;

        /// <summary>
        /// Event fired on player pressed the Alt Interact input
        /// </summary>
        public event EventHandler OnAltInteract;

        /// <summary>
        /// Event fired on player pressed the pause input
        /// </summary>
        public event EventHandler OnPausePressed;

        /// <summary>
        /// Event fired for input rebinding performed
        /// </summary>
        public event EventHandler OnRebindingComplete;

        public static GameInput Instance { private set; get; }

        /// <summary>
        /// String used to identify player input rebindings in PlayerPrefs
        /// </summary>
        private const string PLAYER_PREFS_BINDINGS = "Input Bindings";

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("There should be only one instance of GameInput");
            }

            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();

            playerInputActions.Player.Interact.performed += Interact_performed;
            playerInputActions.Player.AltInteract.performed += AltInteract_performed;
            playerInputActions.Player.Pause.performed += Pause_performed;

            //If player rebindings exist in player prefs, override the default
            if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
            {
                string stringifiedBindings = PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS);
                playerInputActions.LoadBindingOverridesFromJson(stringifiedBindings);
            }
        }
        private void OnDestroy()
        {
            playerInputActions.Player.Interact.performed -= Interact_performed;
            playerInputActions.Player.AltInteract.performed -= AltInteract_performed;
            playerInputActions.Player.Pause.performed -= Pause_performed;

            playerInputActions.Dispose();
        }

        /// <summary>
        /// Function invoked for Pause playeraction performed
        /// </summary>
        /// <param name="obj"></param>
        private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            this.OnPausePressed?.Invoke(this, EventArgs.Empty);
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

        /// <summary>
        /// Public-facing function to return the corresponding mapped input key given the <paramref name="binding"/>. <br />
        /// For instance, for binding Move_Up, should return "W" with default mappings.
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetBindingDisplayString(Bindings binding)
        {
            //Note in the following Player.Move[0] is a composite binding, and contains our WASD binding
            //Move.bindings[1] is our W binding
            //Move.bindings[2] is our S binding, and so on

            switch (binding)
            {
                case Bindings.Move_Up:
                    {
                        return playerInputActions.Player.Move.bindings[1].ToDisplayString();
                    }
                case Bindings.Move_Down:
                    {
                        return playerInputActions.Player.Move.bindings[2].ToDisplayString();
                    }
                case Bindings.Move_Left:
                    {
                        return playerInputActions.Player.Move.bindings[3].ToDisplayString();
                    }
                case Bindings.Move_Right:
                    {
                        return playerInputActions.Player.Move.bindings[4].ToDisplayString();
                    }
                case Bindings.Interact:
                    {
                        return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
                    }
                case Bindings.Interact_Alt:
                    {
                        return playerInputActions.Player.AltInteract.bindings[0].ToDisplayString();
                    }
                case Bindings.Pause:
                    {
                        return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
                    }
                case Bindings.Gamepad_Interact:
                    {
                        return playerInputActions.Player.Interact.bindings[1].ToDisplayString();
                    }
                case Bindings.Gamepad_Interact_Alt:
                    {
                        return playerInputActions.Player.AltInteract.bindings[1].ToDisplayString();
                    }
                case Bindings.Gamepad_Pause:
                    {
                        return playerInputActions.Player.Pause.bindings[1].ToDisplayString();
                    }
                default:
                    {
                        throw new Exception($"Received unexpected value {binding} in GameInput");
                    }
            }
        }

        /// <summary>
        /// Public-facing function to rebind input key mappings
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="onBindingComplete"></param>
        public void RebindBinding(Bindings binding, Action onBindingComplete)
        {
            this.playerInputActions.Player.Disable();

            InputAction inputAction;
            int bindingIndex;

            switch (binding)
            {
                case Bindings.Move_Up:
                    {
                        inputAction = playerInputActions.Player.Move;
                        bindingIndex = 1;
                        break;
                    }
                case Bindings.Move_Down:
                    {
                        inputAction = playerInputActions.Player.Move;
                        bindingIndex = 2;
                        break;
                    }
                case Bindings.Move_Left:
                    {
                        inputAction = playerInputActions.Player.Move;
                        bindingIndex = 3;
                        break;
                    }
                case Bindings.Move_Right:
                    {
                        inputAction = playerInputActions.Player.Move;
                        bindingIndex = 4;
                        break;
                    }
                case Bindings.Interact:
                    {
                        inputAction = playerInputActions.Player.Interact;
                        bindingIndex = 0;
                        break;
                    }
                case Bindings.Interact_Alt:
                    {
                        inputAction = playerInputActions.Player.AltInteract;
                        bindingIndex = 0;
                        break;
                    }
                case Bindings.Pause:
                    {
                        inputAction = playerInputActions.Player.Pause;
                        bindingIndex = 0;
                        break;
                    }
                case Bindings.Gamepad_Interact:
                    {
                        inputAction = playerInputActions.Player.Interact;
                        bindingIndex = 1;
                        break;
                    }
                case Bindings.Gamepad_Interact_Alt:
                    {
                        inputAction = playerInputActions.Player.AltInteract;
                        bindingIndex = 1;
                        break;
                    }
                case Bindings.Gamepad_Pause:
                    {
                        inputAction = playerInputActions.Player.Pause;
                        bindingIndex = 1;
                        break;
                    }
                default:
                    {
                        throw new Exception($"Received unexpected value {binding} in GameInput Rebind");
                    }
            }

            inputAction.PerformInteractiveRebinding(bindingIndex)
                .OnComplete(callback =>
                {
                    callback.Dispose();
                    playerInputActions.Player.Enable();
                    onBindingComplete?.Invoke();
                    OnRebindingComplete?.Invoke(sender: this, EventArgs.Empty);

                    //Save input rebinding to player prefs
                    PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, value: inputAction.SaveBindingOverridesAsJson());
                    PlayerPrefs.Save();
                })
                .Start();
        }
    }
}
