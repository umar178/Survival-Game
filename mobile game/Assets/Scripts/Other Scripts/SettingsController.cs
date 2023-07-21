using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class SettingsController : MonoBehaviour
{
    public Slider slider;
    CinemachineVirtualCamera Camera;

    private void Start()
    {
        Camera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        Camera.m_Lens.FarClipPlane = slider.value;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
