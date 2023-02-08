using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class LookAtCamera : MonoBehaviour
    {
        private enum Mode {
            /// <summary>
            /// Face the camera
            /// </summary>
            LookAt,
            /// <summary>
            /// Look in the same direction as the camera
            /// </summary>
            LookAtInverted,
            /// <summary>
            /// Face the camera's forward vector
            /// </summary>
            CameraForward,
            /// <summary>
            /// Face the opposite of the camera's forward vector
            /// </summary>
            CameraForwardInverted
        }

        [SerializeField] private Mode mode;

        private void LateUpdate()
        {
            switch (mode)
            {
                case Mode.LookAt:
                    {
                        this.transform.LookAt(target: Camera.main.transform);
                        break;
                    }
                    case Mode.LookAtInverted:
                    {
                        Vector3 toCamera = Camera.main.transform.position - this.transform.position;
                        this.transform.LookAt(worldPosition: this.transform.position - toCamera);
                        break;
                    }
                    case Mode.CameraForward:
                    {
                        this.transform.forward = Camera.main.transform.forward;
                        break;
                    }
                    case Mode.CameraForwardInverted:
                    {
                        this.transform.forward = -Camera.main.transform.forward;
                        break;
                    }
            }
        }
    }
}
