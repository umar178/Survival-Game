using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    PlayerDetails playerDetails;
    GameObject player;
    WeaponPickup weaponPickup;

    [SerializeField] GameObject UiPanel;
    [SerializeField] GameObject DeathUIPanel;

    int health;

    [SerializeField] AudioListener Audiolistner;

    private void Awake()
    {
        Audiolistner = FindObjectOfType<AudioListener>();

        Invoke("ActivateListner", 1f);
    }

    void ActivateListner()
    {
        Audiolistner.enabled = true;
    }

    private void Start()
    {
        playerDetails = GetComponent<PlayerDetails>();
        player = this.gameObject;
        health = playerDetails.health;
        weaponPickup = GetComponent<WeaponPickup>();
    }

    private void Update()
    {
        if(transform.position.y <= -100)
        {
            Death();
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        playerDetails.health -= damage;

        if(playerDetails.health <= 0)
        {
            playerDetails.IsAlive = false;
            InventoryClearHandling();
            DeathUIPanel.SetActive(true);
            UiPanel.SetActive(false);
            playerDetails.health = health;
            player.SetActive(false);
        }
    }

    public void Death()
    {
        //in case we need to call death without taking damage
        playerDetails.IsAlive = false;
        InventoryClearHandling();
        DeathUIPanel.SetActive(true);
        UiPanel.SetActive(false);
        playerDetails.health = health;
        player.SetActive(false);
    }

    public void Respawn()
    {
        player.transform.position = playerDetails.SpawnPosition;
        DeathUIPanel.SetActive(false);
        UiPanel.SetActive(true);
        playerDetails.IsAlive = true;
        player.SetActive(true);

        ZombieBehaviour[] zombieArray = GameObject.FindObjectsOfType<ZombieBehaviour>();

        foreach (ZombieBehaviour zombie in zombieArray)
        {
            zombie.Triggered = false;
        }
    }

    void InventoryClearHandling()
    {
        foreach (GameObject slot in weaponPickup.ItemSlots)
        {
            if (slot.gameObject.transform.childCount > 0)
            {
                if(slot.GetComponentInChildren<ItemController>() != null)
                {
                    GameObject Item = slot.GetComponentInChildren<ItemController>().gameObject;
                    Item.GetComponent<Rigidbody>().isKinematic = false;
                    Item.GetComponent<BoxCollider>().enabled = true;
                    Item.GetComponent<ItemController>().IsEquipped = false;
                    Item.transform.parent = null;
                }
            }
        }

        foreach (GameObject Bar in weaponPickup.ItemBars)
        {
            Bar.GetComponent<Image>().sprite = null;
            Bar.GetComponent<Image>().enabled = false;
        }
    }
}
