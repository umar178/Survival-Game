using UnityEngine.UI;
using UnityEngine;

//bagpack pickup system
//Inventory clear button handler
public class BagPackPickup : MonoBehaviour
{
    public GameObject[] ItemSlots;
    //this item bars is the image icon of the bar
    public GameObject[] ItemBars;
    //this one is for close buttons of bag
    public GameObject[] CloseButtonsBag;
    //this one is for close buttons of inventory
    public GameObject[] CloseButtonsInventory;

    WeaponPickup weaponPickup;

    Collider itemcollider;

    private void Start()
    {
        weaponPickup = GetComponent<WeaponPickup>();
    }

    private void Update()
    {
        itemcollider = weaponPickup.itemCollider;
        ZeroAmountItem();
    }

    public void pickup(Collider item)
    {
        Rigidbody rb = itemcollider.GetComponent<Rigidbody>();
        int remainingAmount;

        foreach (GameObject slot in ItemSlots)
        {
            //deal with Physical Pickup
            if (slot.gameObject.transform.childCount > 0)
            {
                if (slot.GetComponentInChildren<ItemController>().item.id ==
                    itemcollider.GetComponentInChildren<ItemController>().item.id)
                {
                    if (slot.GetComponentInChildren<ItemController>().currentamount <
                    itemcollider.GetComponentInChildren<ItemController>().item.MaxCount)
                    {
                        int Capacity = itemcollider.GetComponentInChildren<ItemController>().item.MaxCount
                            - slot.GetComponentInChildren<ItemController>().currentamount;

                        if (Capacity > itemcollider.GetComponentInChildren<ItemController>().currentamount)
                        {
                            slot.GetComponentInChildren<ItemController>().currentamount += itemcollider.GetComponentInChildren<ItemController>().currentamount;
                        }
                        else
                        {
                            remainingAmount = itemcollider.GetComponentInChildren<ItemController>().currentamount
                            - Capacity;

                            slot.GetComponentInChildren<ItemController>().currentamount += Capacity;

                            if (remainingAmount > 0)
                            {
                                RemainingItems(remainingAmount, itemcollider);
                                return;
                            }
                        }


                        rb.isKinematic = true;
                        weaponPickup.PickUpIcon.SetActive(false);
                        itemcollider.GetComponent<ItemController>().InHand = true;
                        itemcollider.GetComponent<ItemController>().enabled = true;

                        itemcollider.enabled = false;
                        Destroy(rb.gameObject);
                        return;
                    }
                }
            }

            else if (slot.transform.childCount == 0 && slot.gameObject.transform.childCount == 0)
            {
                itemcollider.transform.parent = slot.transform;
                itemcollider.transform.localRotation = itemcollider.GetComponent<ItemController>().Rotation;
                itemcollider.transform.localPosition = itemcollider.GetComponent<ItemController>().Position;
                break;
            }
        }

        rb.isKinematic = true;
        itemcollider.enabled = false;
        weaponPickup.PickUpIcon.SetActive(false);
        itemcollider.GetComponent<ItemController>().InHand = true;
        itemcollider.GetComponent<ItemController>().enabled = true;

        foreach (GameObject Bar in ItemBars)
        {
            //deals with UI
            if (Bar.GetComponent<Image>().enabled == false)
            {
                Debug.Log("123");
                Bar.GetComponent<Image>().enabled = true;
                Bar.GetComponent<Image>().sprite = itemcollider.gameObject.GetComponent<ItemController>().item.Image;
                break;
            }
        }
    }

    void RemainingItems(int amount, Collider item)
    {
        foreach (GameObject slot in ItemSlots)
        {
            if (slot.transform.childCount == 0 && slot.gameObject.transform.childCount == 0)
            {
                item.transform.parent = slot.transform;
                item.transform.localRotation = item.GetComponent<ItemController>().Rotation;
                item.transform.localPosition = item.GetComponent<ItemController>().Position;
                item.GetComponent<ItemController>().currentamount = amount;
                break;
            }
        }

        foreach (GameObject Bar in ItemBars)
        {
            //deals with UI
            if (Bar.GetComponent<Image>().enabled == false)
            {
                Debug.Log("123");
                Bar.GetComponent<Image>().enabled = true;
                Bar.GetComponent<Image>().sprite = itemcollider.gameObject.GetComponent<ItemController>().item.Image;
                break;
            }
        }
    }

    public void ItemRemoverFromBag(int i)
    {
        GameObject Item = ItemSlots[i].GetComponentInChildren<ItemController>().gameObject;
        Item.GetComponent<Rigidbody>().isKinematic = false;
        Item.GetComponent<BoxCollider>().enabled = true;
        Item.transform.parent = null;

        GameObject Bar = ItemBars[i].GetComponentInChildren<Image>().gameObject;
        Bar.GetComponent<Image>().sprite = null;
        Bar.GetComponent<Image>().enabled = false;
    }

    public void CloseButtonActivator()
    {
        foreach(GameObject Bar in CloseButtonsBag)
        {
            GameObject button = Bar.GetComponentInChildren<Button>().gameObject;

            if(button.activeSelf == false)
            {
                button.SetActive(true);
            }
            else if (button.activeSelf == true)
            {
                button.SetActive(false);
            }
        }
        
        foreach(GameObject Bar in CloseButtonsInventory)
        {
            GameObject button = Bar.GetComponentInChildren<Button>().gameObject;

            if(button.activeSelf == false)
            {
                button.SetActive(true);
            }
            else if (button.activeSelf == true)
            {
                button.SetActive(false);
            }
        }
    }

    void ZeroAmountItem()
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if (ItemSlots[i].transform.childCount == 0) { return; }
            if (ItemSlots[i].GetComponentInChildren<ItemController>().currentamount <= 0)
            {
                GameObject item = ItemSlots[i].GetComponentInChildren<ItemController>().gameObject;
                Destroy(item);
                ItemBars[i].GetComponent<Image>().sprite = null;
                ItemBars[i].GetComponent<Image>().enabled = false;
            }
        }
    }
}
