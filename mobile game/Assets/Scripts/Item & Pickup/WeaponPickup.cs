using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Image PickUpImage ;
    [SerializeField] TextMeshProUGUI PickUpText;
    [SerializeField] public GameObject PickUpIcon;
    public Collider itemCollider;

    BagPackPickup bagPackPickup;
    public GameObject[] CloseButtons;

    public GameObject[] ItemSlots;
    public GameObject[] ItemBars;

    private void Start()
    {
        bagPackPickup = GetComponent<BagPackPickup>();
    }

    private void Update()
    {
        ZeroAmountItem();
        for (int i = 0; i <= ItemSlots.Length; i++)
        {
            if (ItemSlots[i].transform.childCount == 0)
            {
                ItemBars[i].GetComponent<Image>().enabled = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<ItemController>() == null) { return; }
        PickUpIcon.SetActive(true);
        Sprite image = other.gameObject.GetComponent<ItemController>().item.Image;
        PickUpImage.sprite = image;
        string itemName = other.gameObject.GetComponent<ItemController>().item.name;
        PickUpText.text = itemName;
        itemCollider = other;

        if (Input.GetKeyDown(KeyCode.E))
        {
            pickup();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickUpIcon.SetActive(false);
    }

    public void pickup()
    {
        bool conditionmet = false;

        Rigidbody rb = itemCollider.GetComponent<Rigidbody>();
        int remainingAmount;

        foreach (GameObject slot in ItemSlots)
        {
            //deal with Physical Pickup
            if (slot.gameObject.transform.childCount > 0)
            {
                if (slot.GetComponentInChildren<ItemController>().item.id ==
                    itemCollider.GetComponentInChildren<ItemController>().item.id)
                {
                    if (slot.GetComponentInChildren<ItemController>().currentamount <
                    itemCollider.GetComponentInChildren<ItemController>().item.MaxCount)
                    {
                        int Capacity = itemCollider.GetComponentInChildren<ItemController>().item.MaxCount
                            - slot.GetComponentInChildren<ItemController>().currentamount;

                        if (Capacity > itemCollider.GetComponentInChildren<ItemController>().currentamount)
                        {
                            slot.GetComponentInChildren<ItemController>().currentamount += itemCollider.GetComponentInChildren<ItemController>().currentamount;
                        }
                        else
                        {
                            remainingAmount = itemCollider.GetComponentInChildren<ItemController>().currentamount
                            - Capacity;

                            slot.GetComponentInChildren<ItemController>().currentamount += Capacity;

                            if (remainingAmount > 0)
                            {
                                //conditionmet = true;
                                RemainingItems(remainingAmount);
                                return;
                            }
                        }

                        //conditionmet = true;
                        rb.isKinematic = true;
                        PickUpIcon.SetActive(false);
                        itemCollider.GetComponent<ItemController>().InHand = true;
                        itemCollider.GetComponent<ItemController>().IsEquipped = true;
                        itemCollider.GetComponent<ItemController>().enabled = true;

                        itemCollider.enabled = false;
                        Destroy(rb.gameObject);
                        return;
                    }
                }
            }

            else if (slot.transform.childCount == 0 && slot.gameObject.transform.childCount == 0)
            {
                //conditionmet = true;
                itemCollider.transform.parent = slot.transform;
                itemCollider.transform.localRotation = itemCollider.GetComponent<ItemController>().Rotation;
                itemCollider.transform.localPosition = itemCollider.GetComponent<ItemController>().Position;
                break;
            }

        }

        /*if (conditionmet == false)
        {
            bagPackPickup.pickup(itemCollider);
            return;
        }*/

        rb.isKinematic = true;
        itemCollider.enabled = false;
        PickUpIcon.SetActive(false);
        itemCollider.GetComponent<ItemController>().InHand = true;
        itemCollider.GetComponent<ItemController>().IsEquipped = true;
        itemCollider.GetComponent<ItemController>().enabled = true;

        foreach (GameObject Bar in ItemBars)
        {
            //deals with UI
            if (Bar.GetComponent<Image>().enabled == false)
            {
                Bar.GetComponent<Image>().enabled = true;
                Bar.GetComponent<Image>().sprite = itemCollider.gameObject.GetComponent<ItemController>().item.Image;
                break;
            }
        }
    }

    void RemainingItems(int amount)
    {
        foreach(GameObject slot in ItemSlots)
        {
            if (slot.transform.childCount == 0 && slot.gameObject.transform.childCount == 0)
            {
                itemCollider.transform.parent = slot.transform;
                itemCollider.transform.localRotation = itemCollider.GetComponent<ItemController>().Rotation;
                itemCollider.transform.localPosition = itemCollider.GetComponent<ItemController>().Position;
                itemCollider.GetComponent<ItemController>().currentamount = amount;
                break;
            }
        }

        foreach (GameObject Bar in ItemBars)
        {
            if (Bar.GetComponent<Image>().enabled == false)
            {
                Bar.GetComponent<Image>().enabled = true;
                Bar.GetComponent<Image>().sprite = itemCollider.gameObject.GetComponent<ItemController>().item.Image;
                break;
            }
        }
    }

    public void ItemRemoverFromInventory(int i)
    {
        GameObject Item = ItemSlots[i].GetComponentInChildren<ItemController>().gameObject;
        Item.GetComponent<Rigidbody>().isKinematic = false;
        Item.GetComponent<BoxCollider>().enabled = true;
        Item.GetComponent<ItemController>().IsEquipped = false;
        Item.transform.parent = null;

        GameObject Bar = ItemBars[i].GetComponentInChildren<Image>().gameObject;
        Bar.GetComponent<Image>().sprite = null;
        Bar.GetComponent<Image>().enabled = false;
    }

    void ZeroAmountItem()
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if(ItemSlots[i].transform.childCount == 0) { return; }
            if (ItemSlots[i].GetComponentInChildren<ItemController>().currentamount <= 0)
            {
                GameObject item = ItemSlots[i].GetComponentInChildren<ItemController>().gameObject;
                Destroy(item);
            }
        }
    }

    public void CloseButtonActivator()
    {
        foreach (GameObject Bar in CloseButtons)
        {
            GameObject button = Bar.GetComponentInChildren<Button>().gameObject;

            if (button.activeSelf == false)
            {
                button.SetActive(true);
            }
            else if (button.activeSelf == true)
            {
                button.SetActive(false);
            }
        }
    }
}
