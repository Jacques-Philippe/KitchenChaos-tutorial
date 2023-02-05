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

            this.IsWalking = movementDirection != Vector3.zero;

            this.transform.position += movementDirection * this.mSpeed * Time.deltaTime;
            this.transform.forward = Vector3.Lerp(this.transform.forward, movementDirection, mRotateSpeed * Time.deltaTime);
        }
    }

}