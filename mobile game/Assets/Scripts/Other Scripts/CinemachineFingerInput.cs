using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class CinemachineFingerInput : MonoBehaviour
{
    public float horizontalRotationSpeed = 2.0f;
    public float verticalRotationSpeed = 2.0f;
    private Quaternion originalRotation;
    private float rotX = 0.0f;
    private float rotY = 0.0f;

    public GameObject CameraRoot;

    public UnityEngine.InputSystem.Joystick joystick;

    void Start()
    {
        originalRotation = transform.localRotation;
    }

    public void Move(int TouchNo)
    {
        Touch touch = Input.GetTouch(TouchNo);
        if (touch.phase == TouchPhase.Moved)
        {
            rotX += touch.deltaPosition.x * horizontalRotationSpeed * Time.deltaTime;
            rotY += touch.deltaPosition.y * verticalRotationSpeed * Time.deltaTime;
            rotY = Mathf.Clamp(rotY, -80f, 80f);
            transform.localRotation = originalRotation * Quaternion.Euler(0, rotX, 0);
            CameraRoot.transform.rotation = originalRotation * Quaternion.Euler(-rotY, rotX, 0);
        }

        /*
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                rotX += touch.deltaPosition.x * horizontalRotationSpeed * Time.deltaTime;
                rotY += touch.deltaPosition.y * verticalRotationSpeed * Time.deltaTime;
                rotY = Mathf.Clamp(rotY, -80f, 80f);
                transform.localRotation = originalRotation * Quaternion.Euler(0, rotX, 0);
                CameraRoot.transform.rotation = originalRotation * Quaternion.Euler(-rotY, rotX, 0);
            }
           
            Touch touch1 = Input.GetTouch(1);
            if (touch1.phase == TouchPhase.Moved)
            {
                rotX += touch1.deltaPosition.x * horizontalRotationSpeed * Time.deltaTime;
                rotY += touch1.deltaPosition.y * verticalRotationSpeed * Time.deltaTime;
                rotY = Mathf.Clamp(rotY, -80f, 80f);
                transform.localRotation = originalRotation * Quaternion.Euler(-rotY, rotX, 0);
            }

        } 
        */

    }
}
