using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour
{
    public CraftingRecipes Recipe;
    public GameObject[] Inventory;

    public BuildRecipe BuildButton;
    public bool Buildable;

    public void DoCrafting()
    {
        int[] AmountsOfItems = new int[Recipe.ingredients.Length];
        bool Buildable = true;

        //loops through the AmountOfItem Instance and adds respective item amount
        for (int j = 0; j < Recipe.ingredients.Length; j++)
        {
            foreach (var item in Inventory)
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
                Buildable = false;
            }
        }

        //Debug.Log(craftable);
        if (Buildable) { GiveOutput(); }
    }

    void GiveOutput()
    {
        for (int j = 0; j < Recipe.ingredients.Length; j++)
        {
            for (int i = 0; i < Inventory.Length; i++)
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
        Output.transform.position = BuildButton.ObjectInstance.transform.position;
        Output.transform.rotation = BuildButton.ObjectInstance.transform.rotation;
        BuildButton.CraftOnClick();
    }

    public void ResetRecipe()
    {
        Destroy(BuildButton.ObjectInstance);
    }

    public void Rotate(Vector3 Rot)
    {
        BuildButton.ObjectInstance.transform.Rotate(Rot);
    }

    public void Move(float amount)
    {
        BuildButton.Distance += amount;
    }
}
