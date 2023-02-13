using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaosTutorial
{

    public class PlateIconUI : MonoBehaviour
    {
        [SerializeField] private Image image;

        public void SetIcon(KitchenObjectSO kitchenObjectSO)
        {
            image.sprite = kitchenObjectSO.Sprite;
        }
    }
}
