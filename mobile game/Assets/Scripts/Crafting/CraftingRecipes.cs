using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Create New Recipe")]
public class CraftingRecipes : ScriptableObject
{
    [System.Serializable]
    public struct Ingredients
    {
        public GameObject gameObjectItem;
        public int amount;
    }

    public Ingredients[] ingredients;
    public GameObject Output;
    public GameObject PreviewOutput;
}
