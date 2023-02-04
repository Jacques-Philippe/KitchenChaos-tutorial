using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class Player : MonoBehaviour
    {
        [SerializeField]
        private float mSpeed;

        [SerializeField]
        private float mRotateSpeed;

        private void Update()
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

            Vector3 movementDirection = new Vector3(movementInput.x, 0.0f, movementInput.y);
            this.transform.position += movementDirection * this.mSpeed * Time.deltaTime;

            this.transform.forward = Vector3.Lerp(this.transform.forward, movementDirection, mRotateSpeed * Time.deltaTime);
        }
    }

}