using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public GameObject OnDestroyParticle;
    [SerializeField] GameObject DestroySound;
    [SerializeField] AudioSource HitSound;
    public GameObject HealthUI;

    private HealthBarHandler healthBarHandler;

    [System.Serializable]
    public struct Items
    {
        public GameObject gameObjectItem;
        public int amount;
    }

    public Items[] ItemsAndAmounts;


    private void Start()
    {
        HealthUI = GetComponentInChildren<Canvas>().gameObject;
        healthBarHandler = GetComponentInChildren<HealthBarHandler>();

        HealthUI.SetActive(false);

        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (HitSound != null) { HitSound.Play(); }

        if (health <= 0)
        {
            Vector3 Position = transform.position;
            Position.y += 2;

            if (HitSound != null) { HitSound.Play(); }

            foreach(Items Item in ItemsAndAmounts)
            {
                GameObject ItemDropped = Instantiate(Item.gameObjectItem, Position, Quaternion.identity);
                ItemDropped.GetComponentInChildren<ItemController>().currentamount = Item.amount;
            }

            Vector3 Position2 = transform.position;
            Position2.y += 0.5f;

            if (OnDestroyParticle != null) 
            {
                GameObject DestroyParticle = Instantiate(OnDestroyParticle, Position2, Quaternion.identity);
            }

            if(DestroySound != null)
            {
                Instantiate(DestroySound);
            }

            Destroy(gameObject);
        }
        else
        {
            healthBarHandler.UpdateHealth(maxHealth, health);
        }
        healthBarHandler.UpdateHealth(maxHealth, health);
    }

    private void Update()
    {
        if (HealthUI.activeSelf)
        {
            HealthUI.transform.LookAt(FindObjectOfType<WeaponPickup>().gameObject.transform);
            HealthUI.transform.Rotate(Vector3.up, 180f);
        }
    }
}
