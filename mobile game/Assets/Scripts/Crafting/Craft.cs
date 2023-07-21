using UnityEngine;
using UnityEngine.UI;

// Main crafting button script

public class Craft : MonoBehaviour
{
    public CraftingRecipes Recipe;
    public GameObject[] Inventory;
    public GameObject[] InventoryUI;

    public RecipeButton Recipebutton;

    WeaponPickup ItemPickup;

    private void Start()
    {
        ItemPickup = FindObjectOfType<WeaponPickup>();
    }

    public void DoCrafting()
    {
        int[] AmountsOfItems = new int[Recipe.ingredients.Length];
        bool craftable = true;

        //loops through the AmountOfItem Instance and adds respective item amount
        for (int j = 0; j < Recipe.ingredients.Length; j++)
        {
            foreach (var item in Inventory)
            {
                if(item.transform.childCount == 0) continue;
                if(Recipe.ingredients[j].gameObjectItem.GetComponent<ItemController>().item.id != item.GetComponentInChildren<ItemController>().item.id) continue;
               
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

        //Debug.Log(craftable);
        if (craftable) { GiveOutput(); }
    }

    void GiveOutput()
    {
        for (int j = 0; j < Recipe.ingredients.Length; j++)
        {
            for(int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i].transform.childCount == 0) continue;
                if (Recipe.ingredients[j].gameObjectItem.GetComponent<ItemController>().item.id != Inventory[i].GetComponentInChildren<ItemController>().item.id) continue;

                if (Recipe.ingredients[j].gameObjectItem.GetComponent<ItemController>().item.id == Inventory[i].GetComponentInChildren<ItemController>().item.id)
                {
                    if (Inventory[i].GetComponentInChildren<ItemController>().currentamount >= Recipe.ingredients[j].amount)
                    {
                        Debug.Log("Crafted");
                        Inventory[i].GetComponentInChildren<ItemController>().currentamount -= Recipe.ingredients[j].amount;
                        break;
                    }
                }
            }
        }

        GameObject Output = Instantiate(Recipe.Output);
        Output.transform.position = FindObjectOfType<Attack>().transform.position;
        ItemPickup.itemCollider = Output.GetComponent<Collider>();
        ItemPickup.pickup();
        Recipebutton.CraftOnClick();
    }
}
