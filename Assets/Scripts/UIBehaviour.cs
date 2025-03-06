using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBehaviour : MonoBehaviour
{
    public GameObject solarSystemButton;
    // [SerializeField] private Camera mainCamera;
    // [SerializeField] private Camera solarCamera;
    // private bool viewSolarSystem = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // mainCamera.enabled = true;
        // solarCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void viewSolarSystemView() {
        // viewSolarSystem = true;
        // mainCamera.enabled = false;
        // solarCamera.enabled = viewSolarSystem;
       SceneManager.LoadSceneAsync("SolarSystemViewScene");
    }

    public void viewPsycheWorld() {
        // viewSolarSystem = false;
        // solarCamera.enabled = viewSolarSystem;
        // mainCamera.enabled = true;
    }
}
