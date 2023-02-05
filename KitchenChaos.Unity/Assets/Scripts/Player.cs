using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class Player : MonoBehaviour
    {
        /// <summary>
        /// The speed at which the player should move
        /// </summary>
        [SerializeField]
        private float mSpeed;

        /// <summary>
        /// The speed at which the player should turn to face the direction in which they're moving
        /// </summary>
        [SerializeField]
        private float mRotateSpeed;

        /// <summary>
        /// A reference to the GameInput instance
        /// </summary>
        [SerializeField] private GameInput mGameInput;

        /// <summary>
        /// The layermask associated to Counter gameobjects in the scene
        /// </summary>
        [SerializeField] private LayerMask mCountersLayerMask;

        /// <summary>
        /// Event fired for selected counter changed
        /// </summary>
        public event EventHandler<SelectedCounterChangedEventArgs> OnSelectedCounterChanged;

        /// <summary>
        /// A class to define the EventArgs fired on the <see cref="OnSelectedCounterChanged"/> event, where the <see cref="SelectedCounterChangedEventArgs.selectedCounter"/> reflects the newly selected counter
        /// </summary>
        public class SelectedCounterChangedEventArgs : EventArgs
        {
            /// <summary>
            /// The newly selected counter the player chooses
            /// </summary>
            public ClearCounter selectedCounter;
        }

        /// <summary>
        /// Whether the player is moving
        /// </summary>
        public bool IsWalking { private set; get; }

        /// <summary>
        /// The singleton instance of the player.
        /// </summary>
        public static Player Instance { get; set; }



        /// <summary>
        /// The counter the player currently has selected
        /// </summary>
        private ClearCounter mSelectedCounter;

        /// <summary>
        /// A vector representation of the direction in which the last movement was.
        /// </summary>
        private Vector3 mLastInteractionDirection = new Vector3();

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There exists more than one Player instance");
            }
            Instance = this;
        }

        private void OnEnable()
        {
            mGameInput.OnInteract += OnInteraction;
        }


        private void Update()
        {
            this.HandleMovement();
            this.HandleInteraction();
        }


        /// <summary>
        /// Function to invoke on Interact input received in <see cref="GameInput"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnInteraction(object sender, EventArgs e)
        {
            this.mSelectedCounter?.Interact();
        }

        /// <summary>
        /// A function to handle interaction treatment given player input <br />
        /// Sets the currently selected counter.
        /// </summary>
        private void HandleInteraction()
        {
            //Get movement direction to find relevant interactable
            Vector2 movementInput = mGameInput.GetPlayerMovementDirectionNormalized();

            Vector3 movementDirection = new Vector3(movementInput.x, 0.0f, movementInput.y);

            if (movementDirection != Vector3.zero)
            {
                this.mLastInteractionDirection = movementDirection;
            }

            float interactionRange = 2.0f;
            //Find interactable via raycast in direction of last movement
            if (Physics.Raycast(origin: this.transform.position, direction: mLastInteractionDirection, maxDistance: interactionRange, hitInfo: out RaycastHit raycastHit, layerMask: mCountersLayerMask))
            {
                //if we hit an interactable, invoke its interact function
                if (raycastHit.transform.TryGetComponent<ClearCounter>(out ClearCounter clearCounter))
                {
                    this.SetSelectedCounter(newSelectedCounter: clearCounter);
                }
                else
                {
                    this.SetSelectedCounter(newSelectedCounter: null);
                }
            }
            else
            {
                this.SetSelectedCounter(newSelectedCounter: null);
            }
        }

        /// <summary>
        /// A function to update the currently selected counter, ensuring an event with the corresponding <paramref name="newSelectedCounter"/> is fired on change.
        /// </summary>
        /// <param name="newSelectedCounter"></param>
        private void SetSelectedCounter(ClearCounter newSelectedCounter)
        {
            this.mSelectedCounter = newSelectedCounter;
            this.OnSelectedCounterChanged?.Invoke(sender: this, e: new SelectedCounterChangedEventArgs(){
                selectedCounter = newSelectedCounter
            });
        }

        /// <summary>
        /// Helper to parse all incoming player movement input
        /// </summary>
        private void HandleMovement()
        {
            Vector2 movementInput = mGameInput.GetPlayerMovementDirectionNormalized();

            Vector3 movementDirection = new Vector3(movementInput.x, 0.0f, movementInput.y);
            float movementDistance = this.mSpeed * Time.deltaTime;

            if (IsPlayerMovementObstructed(movementDirection: movementDirection, movementDistance: movementDistance))
            {
                Vector3 movementDirectionX = new Vector3(movementDirection.x, 0.0f, 0.0f).normalized;

                //if the player can move in X, move in X
                if (!IsPlayerMovementObstructed(movementDirection: movementDirectionX, movementDistance))
                {
                    movementDirection = movementDirectionX;
                }
                //else the player cannot move in Z, so try to move in Z
                else
                {
                    Vector3 movementDirectionZ = new Vector3(0.0f, 0.0f, movementDirection.z).normalized;
                    //if the player can move in Z, move in Z
                    if (!IsPlayerMovementObstructed(movementDirection: movementDirectionZ, movementDistance: movementDistance))
                    {
                        movementDirection = movementDirectionZ;
                    }
                }
            }

            this.IsWalking = movementDirection != Vector3.zero;

            this.transform.position += movementDirection * movementDistance;
            this.transform.forward = Vector3.Lerp(this.transform.forward, movementDirection, mRotateSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Helper to tell us whether the player's movement in direction <paramref name="movementDirection"/> for distance <paramref name="movementDistance"/> is obstructed
        /// </summary>
        /// <param name="movementDirection"></param>
        /// <param name="movementDistance"></param>
        /// <returns></returns>
        private bool IsPlayerMovementObstructed(Vector3 movementDirection, float movementDistance)
        {
            float playerHeight = 2.0f;
            float playerRadius = 0.7f;


            bool isPlayerObstructed = Physics.CapsuleCast(point1: this.transform.position, point2: this.transform.position + (Vector3.up * playerHeight), radius: playerRadius, direction: movementDirection, maxDistance: movementDistance);
            return isPlayerObstructed;
        }
    }

}