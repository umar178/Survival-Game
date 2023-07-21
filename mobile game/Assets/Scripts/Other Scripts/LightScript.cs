using UnityEngine;
using UnityEngine.UI;

public class LightScript : MonoBehaviour
{
    [SerializeField]
    Light FlashLight;

    [SerializeField]
    Slider Slider;

    [SerializeField]bool IsOn = false;

    public float minRange;
    public float maxRange;
    public float minSpotAngle;
    public float maxSpotAngle;



    private void Start()
    {
        Slider.gameObject.SetActive(false);
        FlashLight.enabled = false;
        IsOn = false;
    }

    public void flashToggle()
    {
        if (IsOn)
        {
            Slider.gameObject.SetActive(false);
            FlashLight.enabled = false;
            IsOn = false;
        }
        else
        {
            Slider.gameObject.SetActive(true);
            FlashLight.enabled = true;
            IsOn = true;
        }
    }

    private void Update()
    {
        UpdateLightProperties();
    }

    private void UpdateLightProperties()
    {
        // Calculate the new range and spot angle based on the slider value
        float range = Mathf.Lerp(minRange, maxRange, Slider.value);
        float spotAngle = Mathf.Lerp(maxSpotAngle, minSpotAngle, Slider.value);

        // Update the light component's properties
        FlashLight.range = range;
        FlashLight.spotAngle = spotAngle;
    }
}
