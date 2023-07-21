using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//controls item behaviour and details
public class ItemController : MonoBehaviour
{
    public Item item;
    public int currentamount = 1;

    public Vector3 Position;
    public Quaternion Rotation;
    public bool InHand;
    public bool IsEquipped;

    public int AttackDamage = 2;
    public int TreeAttackDamage = 2;
    public int RockAttackDamage = 2;
    public int ItemHealth = 100;
    public int maxHealth = 100;
    public bool Building = false;

    private void Start()
    {
        ItemHealth = maxHealth;
    }

    public void takeDamage(int i)
    {
        ItemHealth -= i;
    }

    private void Update()
    {
        if(ItemHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
