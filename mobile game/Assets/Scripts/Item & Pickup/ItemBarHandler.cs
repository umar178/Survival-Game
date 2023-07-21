using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBarHandler : MonoBehaviour
{
    public GameObject[] Slots;
    public ItemController itemProperties;
    [SerializeField] PlayerDetails playerDetails;

    private void Start()
    {
        foreach (var item in Slots)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                item.SetActive(false);
                //Slots[i].GetComponentInChildren<ItemController>().InHand = false;
            }
        }
        Slots[0].SetActive(true);

        //playerDetails.ActiveSlot = 0;
    }

    public void Slot1()
    {
        foreach (var item in Slots)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                item.SetActive(false);
                if (Slots[i].GetComponentInChildren<ItemController>() != null)
                {
                    Slots[i].GetComponentInChildren<ItemController>().InHand = false;
                }
            }
        }
        Slots[0].SetActive(true);
        Slots[0].GetComponentInChildren<ItemController>().InHand = true;
        Debug.Log("1");
        playerDetails.ActiveSlot = 0;
    }

    public void Slot2()
    {
        foreach (var item in Slots)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                item.SetActive(false);
                if (Slots[i].GetComponentInChildren<ItemController>() != null)
                {
                    Slots[i].GetComponentInChildren<ItemController>().InHand = false;
                }
            }
        }
        Slots[1].SetActive(true);
        Slots[1].GetComponentInChildren<ItemController>().InHand = true;
        Debug.Log("1");
        playerDetails.ActiveSlot = 1;
    }

    public void Slot3()
    {
        foreach (var item in Slots)
        {
            for(int i = 0; i < Slots.Length; i++)
            {
                item.SetActive(false);
                if (Slots[i].GetComponentInChildren<ItemController>() != null)
                {
                    Slots[i].GetComponentInChildren<ItemController>().InHand = false;
                }
            }
        }
        Slots[2].SetActive(true);
        Slots[2].GetComponentInChildren<ItemController>().InHand = true;
        playerDetails.ActiveSlot = 2;
    }

    public void Slot4()
    {
        foreach (var item in Slots)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                item.SetActive(false);
                if (Slots[i].GetComponentInChildren<ItemController>() != null)
                {
                    Slots[i].GetComponentInChildren<ItemController>().InHand = false;
                }
            }
        }
        Slots[3].SetActive(true);
        Slots[3].GetComponentInChildren<ItemController>().InHand = true;
        playerDetails.ActiveSlot = 3;
    }

    public void Slot5()
    {
        foreach (var item in Slots)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                item.SetActive(false);
                if (Slots[i].GetComponentInChildren<ItemController>() != null)
                {
                    Slots[i].GetComponentInChildren<ItemController>().InHand = false;
                }
            }
        }
        Slots[4].SetActive(true);
        Slots[4].GetComponentInChildren<ItemController>().InHand = true;
        playerDetails.ActiveSlot = 4;
    }

    public void Slot6()
    {
        foreach (var item in Slots)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                item.SetActive(false);
                if (Slots[i].GetComponentInChildren<ItemController>() != null)
                {
                    Slots[i].GetComponentInChildren<ItemController>().InHand = false;
                }
            }
        }
        Slots[5].SetActive(true);
        Slots[5].GetComponentInChildren<ItemController>().InHand = true;
        playerDetails.ActiveSlot = 5;
    }

    public void Slot7()
    {
        foreach (var item in Slots)
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                item.SetActive(false);
                if (Slots[i].GetComponentInChildren<ItemController>() != null)
                {
                    Slots[i].GetComponentInChildren<ItemController>().InHand = false;
                }
            }
        }
        Slots[6].SetActive(true);
        Slots[6].GetComponentInChildren<ItemController>().InHand = true;
        playerDetails.ActiveSlot = 6;
    }
}
