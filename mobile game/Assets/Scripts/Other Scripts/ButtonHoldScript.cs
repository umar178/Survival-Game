using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoldScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isButtonHeld;

    Build PlaceBuildButton;
    public Vector3 RotationAxis;
    [SerializeField] float Quantity;

    [SerializeField] bool Rot;
    [SerializeField] bool Move;

    private void Start()
    {
        PlaceBuildButton = FindObjectOfType<Build>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonHeld = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonHeld = false;
    }

    private void Update()
    {
        if (isButtonHeld && Rot)
        {
            PlaceBuildButton.Rotate(RotationAxis);
        }
        
        if (isButtonHeld && Move)
        {
            PlaceBuildButton.Move(Quantity);
        }
    }
}
