using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaosTutorial
{

    public class TemplateRecipeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI recipeText;
        [SerializeField] private GameObject iconTemplate;

        [SerializeField] private Transform iconContainer;

        /// <summary>
        /// Set the name of the recipe
        /// </summary>
        /// <param name="name"></param>
        public void SetRecipeName(string recipeName)
        {
            this.recipeText.text = recipeName;
        }

        /// <summary>
        /// Instantiate all icons given the list of sprites
        /// </summary>
        /// <param name="sprites"></param>
        public void SetIcons(List<Sprite> sprites)
        {
            foreach(var sprite in sprites)
            {
                GameObject instantiatedIcon = GameObject.Instantiate(iconTemplate, parent: iconContainer);
                if (instantiatedIcon.TryGetComponent<Image>(out Image image))
                {
                    image.sprite = sprite;
                }
            }
        }
    }
}
