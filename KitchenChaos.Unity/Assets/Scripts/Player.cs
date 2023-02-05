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
        /// Whether the player is moving
        /// </summary>
        public bool IsWalking { private set; get; }

        private void Update()
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

        private bool IsPlayerMovementObstructed(Vector3 movementDirection, float movementDistance)
        {
            float playerHeight = 2.0f;
            float playerRadius = 0.7f;


            bool isPlayerObstructed = Physics.CapsuleCast(point1: this.transform.position, point2: this.transform.position + (Vector3.up * playerHeight), radius: playerRadius, direction: movementDirection, maxDistance: movementDistance);
            return isPlayerObstructed;
        }
    }

}