using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void NewMission() {
        SceneManager.LoadSceneAsync("RocketScene");
    }

    public void ContinueMission() {

    }

    public void OpenCredits() {

    }

    public void ExitGame() {
        Application.Quit();
    }
}
