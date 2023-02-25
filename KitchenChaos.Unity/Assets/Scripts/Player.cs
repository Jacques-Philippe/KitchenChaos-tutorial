using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class Player : MonoBehaviour, IKitchenObjectParent
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
        /// A transform defining where KitchenObjects should be held when the player grabs one
        /// </summary>
        [SerializeField] private Transform mKitchenObjectHoldPoint;

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
            public BaseCounter selectedCounter;
        }

        /// <summary>
        /// Event fired for player moving
        /// </summary>
        public event EventHandler OnMovement;

        /// <summary>
        /// Event fired for player picked up something
        /// </summary>
        public event EventHandler OnPickedUpSomething;


        /// <summary>
        /// Whether the player is moving
        /// </summary>
        public bool IsWalking { private set; get; }

        /// <summary>
        /// The singleton instance of the player.
        /// </summary>
        public static Player Instance { get; set; }


        /// <summary>
        /// The kitchen object associated to the player, if any. <br /> 
        /// A kitchen object is associated to the player for the player is holding the kitchen object
        /// </summary>
        private KitchenObject mKitchenObject;

        /// <summary>
        /// The counter the player currently has selected
        /// </summary>
        private BaseCounter mSelectedCounter;

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
            mGameInput.OnAltInteract += OnAltInteract;
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
            GameManager.State gameState = GameManager.Instance.state;

            if (gameState == GameManager.State.GAME_PLAYING)
            {
                this.mSelectedCounter?.Interact(player: this);
            }
        }

        /// <summary>
        /// Function to invoke on alternate interaction input received in <see cref="GameInput"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAltInteract(object sender, EventArgs e)
        {
            GameManager.State gameState = GameManager.Instance.state;

            if (gameState == GameManager.State.GAME_PLAYING)
            {
                this.mSelectedCounter?.AlternateInteract(player: this);
            }
        }

        ///// <summary>
        ///// A function to handle interaction treatment given player input <br />
        ///// Sets the currently selected counter.
        ///// </summary>
        //private void HandleInteraction()
        //{
        //    //Get movement direction to find relevant interactable
        //    Vector2 movementInput = mGameInput.GetPlayerMovementDirectionNormalized();

        //    Vector3 movementDirection = new Vector3(movementInput.x, 0.0f, movementInput.y);

        //    if (movementDirection != Vector3.zero)
        //    {
        //        this.mLastInteractionDirection = movementDirection;
        //    }

        //    float interactionRange = 2.0f;
        //    //Find interactable via raycast in direction of last movement
        //    if (Physics.Raycast(origin: this.transform.position, direction: mLastInteractionDirection, maxDistance: interactionRange, hitInfo: out RaycastHit raycastHit, layerMask: mCountersLayerMask))
        //    {
        //        //if we hit an interactable, invoke its interact function
        //        if (raycastHit.transform.TryGetComponent<BaseCounter>(out BaseCounter counter))
        //        {
        //            this.SetSelectedCounter(newSelectedCounter: counter);
        //        }
        //        else
        //        {
        //            this.SetSelectedCounter(newSelectedCounter: null);
        //        }
        //    }
        //    else
        //    {
        //        this.SetSelectedCounter(newSelectedCounter: null);
        //    }
        //}

        /// <summary>
        /// A function to handle interaction treatment given player input <br />
        /// Sets the currently selected counter.
        /// </summary>
        private void HandleInteraction()
        {
            Vector2 inputVector = mGameInput.GetPlayerMovementDirectionNormalized();

            Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

            if (moveDir != Vector3.zero)
            {
                mLastInteractionDirection = moveDir;
            }

            float interactDistance = 2f;
            if (Physics.Raycast(transform.position, mLastInteractionDirection, out RaycastHit raycastHit, interactDistance, mCountersLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
                {
                    // Has ClearCounter
                    if (baseCounter != this.mSelectedCounter)
                    {
                        SetSelectedCounter(baseCounter);
                    }
                }
                else
                {
                    SetSelectedCounter(null);

                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }

        /// <summary>
        /// A function to update the currently selected counter, ensuring an event with the corresponding <paramref name="newSelectedCounter"/> is fired on change.
        /// </summary>
        /// <param name="newSelectedCounter"></param>
        private void SetSelectedCounter(BaseCounter newSelectedCounter)
        {
            this.mSelectedCounter = newSelectedCounter;
            this.OnSelectedCounterChanged?.Invoke(sender: this, e: new SelectedCounterChangedEventArgs(){
                selectedCounter = newSelectedCounter
            });
        }

        ///// <summary>
        ///// Helper to parse all incoming player movement input
        ///// </summary>
        //private void HandleMovement()
        //{
        //    Vector2 movementInput = mGameInput.GetPlayerMovementDirectionNormalized();

        //    Vector3 movementDirection = new Vector3(movementInput.x, 0.0f, movementInput.y);
        //    Vector3 faceDirection = movementDirection;

        //    float movementDistance = this.mSpeed * Time.deltaTime;

        //    if (Mathf.Abs(movementDirection.x) > 0.5f && IsPlayerMovementObstructed(movementDirection: movementDirection, movementDistance: movementDistance))
        //    {
        //        Vector3 movementDirectionX = new Vector3(movementDirection.x, 0.0f, 0.0f).normalized;

        //        //if the player can move in X, move in X
        //        if (!IsPlayerMovementObstructed(movementDirection: movementDirectionX, movementDistance))
        //        {
        //            movementDirection = movementDirectionX;
        //        }
        //        //else the player cannot move in X, so try to move in Z
        //        else
        //        {
        //            Vector3 movementDirectionZ = new Vector3(0.0f, 0.0f, movementDirection.z).normalized;
        //            //if the player can move in Z, move in Z
        //            if (Mathf.Abs(movementDirection.z) > 0.5f && !IsPlayerMovementObstructed(movementDirection: movementDirectionZ, movementDistance: movementDistance))
        //            {
        //                movementDirection = movementDirectionZ;
        //            }
        //        }
        //    }

        //    this.IsWalking = movementDirection != Vector3.zero;

        //    if (this.IsWalking)
        //    {
        //        this.OnMovement?.Invoke(sender: this, EventArgs.Empty);
        //        this.transform.position += movementDirection * movementDistance;
        //        this.transform.forward = Vector3.Lerp(this.transform.forward, faceDirection, mRotateSpeed * Time.deltaTime);
        //    }
        //}

        /// <summary>
        /// Helper to parse all incoming player movement input
        /// </summary>
        private void HandleMovement()
        {
            Vector2 inputVector = mGameInput.GetPlayerMovementDirectionNormalized();

            Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

            float moveDistance = mSpeed * Time.deltaTime;
            float playerRadius = .7f;
            float playerHeight = 2f;
            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

            if (!canMove)
            {
                // Cannot move towards moveDir

                // Attempt only X movement
                Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
                canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

                if (canMove)
                {
                    // Can move only on the X
                    moveDir = moveDirX;
                }
                else
                {
                    // Cannot move only on the X

                    // Attempt only Z movement
                    Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                    canMove = (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                    if (canMove)
                    {
                        // Can move only on the Z
                        moveDir = moveDirZ;
                    }
                    else
                    {
                        // Cannot move in any direction
                    }
                }
            }

            if (canMove)
            {
                transform.position += moveDir * moveDistance;
            }

            IsWalking = moveDir != Vector3.zero;

            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
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

        /// <inheritdoc/>
        public Transform GetKitchenObjectFollowTransform()
        {
            return this.mKitchenObjectHoldPoint;
        }

        /// <inheritdoc/>
        public void SetKitchenObject(KitchenObject newKitchenObject)
        {
            this.mKitchenObject = newKitchenObject;
            this.OnPickedUpSomething?.Invoke(sender: this, e: EventArgs.Empty);
        }

        /// <inheritdoc/>
        public KitchenObject GetKitchenObject()
        {
            return this.mKitchenObject;
        }

        /// <inheritdoc/>
        public void ClearKitchenObject()
        {
            this.mKitchenObject = null;
        }

        /// <inheritdoc/>
        public bool HasKitchenObject()
        {
            return this.mKitchenObject != null;
        }
    }

}