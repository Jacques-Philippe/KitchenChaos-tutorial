using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{
    [CreateAssetMenu()]
    public class RecipeListSO : ScriptableObject
    {
        public List<RecipeSO> recipeSOList;
    }
}
