using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void NewJourney() {
        SceneManager.LoadSceneAsync("RocketScene");
    }
}
