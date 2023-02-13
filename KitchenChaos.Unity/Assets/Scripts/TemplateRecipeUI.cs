using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaosTutorial
{

    public class TemplateRecipeUI : MonoBehaviour
    {
        /// <summary>
        /// The text which should represent the recipe's name
        /// </summary>
        [SerializeField] private TextMeshProUGUI recipeText;
        /// <summary>
        /// A gameobject instance to represent the icon templates to spawn
        /// </summary>
        [SerializeField] private GameObject iconTemplate;
        /// <summary>
        /// The transform into which to spawn the icons
        /// </summary>
        [SerializeField] private Transform iconContainer;

        /// <summary>
        /// Set the name of the recipe
        /// </summary>
        /// <param name="name"></param>
        public void SetRecipeName_UI(string recipeName)
        {
            this.recipeText.text = recipeName;
        }

        /// <summary>
        /// Instantiate all icons given the list of sprites
        /// </summary>
        /// <param name="sprites"></param>
        public void SetRecipeSprites_UI(List<Sprite> sprites)
        {
            foreach(var sprite in sprites)
            {
                GameObject instantiatedIcon = GameObject.Instantiate(iconTemplate, parent: iconContainer);
                if (instantiatedIcon.TryGetComponent<Image>(out Image image))
                {
                    image.sprite = sprite;
                    instantiatedIcon.SetActive(true);
                }
            }
        }
    }
}
