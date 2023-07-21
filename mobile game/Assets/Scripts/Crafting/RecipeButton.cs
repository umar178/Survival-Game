using UnityEngine.UI;
using UnityEngine;

// Recipe button script

public class RecipeButton : MonoBehaviour
{
    public CraftingRecipes Recipe;
    public Craft CraftingButton;

    //for the button itself
    [SerializeField] Image image;
    [SerializeField] Text ItemName;

    //for the main crafting panel item
    [SerializeField] Image ItemImage;
    [SerializeField] Text ItemText;

    [SerializeField] Image ColorToChange;

    public GameObject[] RecipeDisplay;

    private void Start()
    {
        image.sprite = Recipe.Output.GetComponent<ItemController>().item.Image;
        ItemName.text = Recipe.Output.GetComponent<ItemController>().item.name;
    }

    public void CraftOnClick()
    {
        foreach (GameObject UI in RecipeDisplay)
        {
            UI.SetActive(false);
        }

        for(int i = 0; i < Recipe.ingredients.Length; i++)
        {
            RecipeDisplay[i].SetActive(true);
            RecipeDisplay[i].GetComponent<ItemDetails>().Image.sprite = Recipe.ingredients[i].gameObjectItem.GetComponent<ItemController>().item.Image;
            RecipeDisplay[i].GetComponent<ItemDetails>().Name.text = Recipe.ingredients[i].gameObjectItem.GetComponent<ItemController>().item.ItemName;
            RecipeDisplay[i].GetComponent<ItemDetails>().Amount.text = "(" + Recipe.ingredients[i].amount + ")";
        }



        ItemImage.sprite = Recipe.Output.GetComponent<ItemController>().item.Image;
        ItemText.text = Recipe.Output.GetComponent<ItemController>().item.name;
        CraftingButton.Recipe = Recipe;

        DoCrafting();
    }

    public void DoCrafting()
    {
        int[] AmountsOfItems = new int[Recipe.ingredients.Length];
        bool craftable = true;

        //loops through the new recipe instance and adds respectie item amount
        for (int j = 0; j < Recipe.ingredients.Length; j++)
        {
            foreach (var item in CraftingButton.Inventory)
            {
                if (item.transform.childCount == 0) continue;
                if (Recipe.ingredients[j].gameObjectItem.GetComponent<ItemController>().item.id != item.GetComponentInChildren<ItemController>().item.id) continue;

                if (Recipe.ingredients[j].gameObjectItem.GetComponent<ItemController>().item.id == item.GetComponentInChildren<ItemController>().item.id)
                {
                    AmountsOfItems[j] += item.GetComponentInChildren<ItemController>().currentamount;
                    //Debug.Log(Recipe.ingredients[j].gameObjectItem.name + AmountsOfItems[j]);
                }
            }
        }

        for (int k = 0; k < Recipe.ingredients.Length; k++)
        {
            if (Recipe.ingredients[k].amount > AmountsOfItems[k])
            {
                craftable = false;
            }
        }

        for (int i = 0; i < Recipe.ingredients.Length; i++)
        {
            RecipeDisplay[i].GetComponent<ItemDetails>().Amount.text = "(" + Recipe.ingredients[i].amount + "/" + AmountsOfItems[i] + ")";
        }

        Debug.Log(craftable);
        if (craftable) { ColorToChange.color = Color.green; }
        if (!craftable) { ColorToChange.color = Color.red; }

        CraftingButton.Recipebutton = this;
    }
}
