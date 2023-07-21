using UnityEngine.UI;
using UnityEngine;

public class HealthBarHandler : MonoBehaviour
{
    [SerializeField] Image HealthBar;

    private void Start()
    {
        HealthBar = GetComponent<Image>();
    }

    public void UpdateHealth(int maxHealth, int currentHealth)
    {
        float Chealth = currentHealth;
        float Mhealth = maxHealth;
        HealthBar.fillAmount = Chealth / Mhealth;
    }
}
