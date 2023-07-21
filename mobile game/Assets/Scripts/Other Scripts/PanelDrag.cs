using UnityEngine.EventSystems;
using UnityEngine;

public class PanelDrag : MonoBehaviour, IDragHandler
{
    [SerializeField] CinemachineFingerInput _fingerInput;

    private void Start()
    {
        _fingerInput = FindObjectOfType<CinemachineFingerInput>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 0)
        {
            // Loop through all the active touches
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                int touchNumber = touch.fingerId;
                //Debug.Log("Touch Number: " + touchNumber);
                _fingerInput.Move(touchNumber);
            }
        }
    }
}
