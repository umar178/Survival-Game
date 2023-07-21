using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildRecipe : MonoBehaviour
{
    public CraftingRecipes Recipe;
    public Build BuildButton;

    //for the button itself
    [SerializeField] Image image;

    //for the main crafting panel item
    [SerializeField] Image ItemImage;
    [SerializeField] TextMeshProUGUI ItemText;
    [SerializeField] GameObject detailsPanel;

    public GameObject ObjectInstance;
    public GameObject PlayerCameraRoot;

    public float Distance;

    public bool Buildable;

    [SerializeField] TextMeshProUGUI[] DetailsTexts; 

    private void Start()
    {
        image.sprite = Recipe.PreviewOutput.GetComponent<ItemController>().item.Image;
        BuildButton = FindObjectOfType<Build>();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;
        }

        Vector3 position = hit.point;
        position.y = hit.point.y + Distance;

        ObjectInstance.transform.position = position;
    }

    public void CraftOnClick()
    {
        foreach (TextMeshProUGUI UI in DetailsTexts)
        {
            UI.gameObject.SetActive(false);
        }

        for (int i = 0; i < Recipe.ingredients.Length; i++)
        {
            DetailsTexts[i].gameObject.SetActive(true);
            DetailsTexts[i].text = Recipe.ingredients[i].gameObjectItem.GetComponent<ItemController>().item.ItemName;
        }

        ItemImage.sprite = Recipe.PreviewOutput.GetComponent<ItemController>().item.Image;
        ItemText.text = Recipe.PreviewOutput.GetComponent<ItemController>().item.name;
        detailsPanel.SetActive(true);

        DoBuilding();
    }

    public void DoBuilding()
    {
        Debug.Log("1");

        BuildRecipe[] Instances = FindObjectsOfType<BuildRecipe>();

        foreach (BuildRecipe instance in Instances)
        {
            if (instance != null)
            {
                Destroy(instance.ObjectInstance);
            }
        }


        Debug.Log("2");
        Destroy(ObjectInstance);
        ObjectInstance = Instantiate(Recipe.PreviewOutput);

        int[] AmountsOfItems = new int[Recipe.ingredients.Length];
        Buildable = true;

        //loops through the new recipe instance and adds respective item amount
        for (int j = 0; j < Recipe.ingredients.Length; j++)
        {
            foreach (var item in BuildButton.Inventory)
            {
                if (item.transform.childCount == 0) continue;
                if (Recipe.ingredients[j].gameObjectItem.GetComponent<ItemController>().item.id != item.GetComponentInChildren<ItemController>().item.id) continue;

                if (Recipe.ingredients[j].gameObjectItem.GetComponent<ItemController>().item.id == item.GetComponentInChildren<ItemController>().item.id)
                {
                    AmountsOfItems[j] += item.GetComponentInChildren<ItemController>().currentamount;
                    Debug.Log(Recipe.ingredients[j].gameObjectItem.name + AmountsOfItems[j]);
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

        for (int i = 0; i < Recipe.ingredients.Length; i++)
        {
            string Text = Recipe.ingredients[i].gameObjectItem.GetComponent<ItemController>().item.name + "(" + Recipe.ingredients[i].amount + "/" + AmountsOfItems[i] + ")";
            DetailsTexts[i].text = Text;
        }


        Debug.Log(Buildable);
        if (Buildable) { Debug.Log(Recipe.ingredients.Length); }
        if (!Buildable) { Debug.Log(Recipe.ingredients.Length); }

        BuildButton.BuildButton = null;
        BuildButton.Recipe = null;

        BuildButton.BuildButton = this;
        BuildButton.Recipe = Recipe;
        BuildButton.Buildable = Buildable;
    }
}
