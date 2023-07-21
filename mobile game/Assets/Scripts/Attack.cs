using UnityEngine;
using TMPro;
using System.Collections;

//deals with player attack
//deals with item detail UI
//deals with animation player for attack
//deals with UI image clear when item is destroyed
public class Attack : MonoBehaviour
{  
    [SerializeField] HitAreaDetection HitArea;
    public WeaponPickup Items;

    [SerializeField] int AttackDamage = 2;
    [SerializeField] int TreeAttackDamage = 2;
    [SerializeField] int RockAttackDamage = 2;
    [SerializeField] int DamageToWeapon;

    [SerializeField] GameObject ItemDetailsUI;
    [SerializeField] TextMeshProUGUI ItemNameText;
    [SerializeField] TextMeshProUGUI ItemAmountText;
    [SerializeField] TextMeshProUGUI ItemHealthText;

    public float AttackTimeout = 1;
    public bool Attacked;

    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        HitArea = FindObjectOfType<HitAreaDetection>();
        Items = GetComponent<WeaponPickup>();
    }

    
    void Update()
    {
        detailsUpdater();
    }

    void detailsUpdater()
    {
        //loops through all slots and sets damage values of the active slot item
        foreach (var item in Items.ItemSlots)
        {
            ItemController properties = GetComponentInChildren<ItemController>();
            if (properties == null)
            {
                ItemDetailsUI.SetActive(false);
                AttackDamage = 2;
                TreeAttackDamage = 2;
                RockAttackDamage = 2;
                return;
            }
            AttackDamage = properties.AttackDamage;
            TreeAttackDamage = properties.TreeAttackDamage;
            RockAttackDamage = properties.RockAttackDamage;

            ItemDetailsUI.SetActive(true);
            ItemNameText.text = "Item: " + properties.item.ItemName;
            ItemAmountText.text = "Amount: " + properties.currentamount.ToString() + "/" + properties.item.MaxCount.ToString();
            ItemHealthText.text = "Health: " + properties.ItemHealth + "/" + properties.maxHealth;
        }
    }

    public void attack()
    {
        if (HitArea.CollidedObject.gameObject.tag == "Tree")
        {
            HitArea.CollidedObject.GetComponent<Health>().TakeDamage(TreeAttackDamage);
        }
        else if (HitArea.CollidedObject.gameObject.tag == "Rock")
        {
            HitArea.CollidedObject.GetComponent<Health>().TakeDamage(RockAttackDamage);
        }
        else
        {
            HitArea.CollidedObject.GetComponent<Health>().TakeDamage(AttackDamage);
        }

        foreach (GameObject itemlots in Items.ItemSlots)
        {
            if (itemlots.activeSelf)
            {
                ItemController item = GetComponentInChildren<ItemController>();

                if (item.item.id == 1 || item.item.id == 6 || item.item.id == 2)
                {
                    item.takeDamage(DamageToWeapon);
                    return;
                }
            }
        }
    }

    public void PlayAnimation()
    {
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        if (Attacked) { yield break; }
        Attacked = true;
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(AttackTimeout);
        Attacked = false;
    }
}
