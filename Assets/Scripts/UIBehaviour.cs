using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBehaviour : MonoBehaviour
{
    public GameObject solarSystemButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toSolarSystemView() {
       SceneManager.LoadSceneAsync("SolarSystemViewScene");
    }
}
