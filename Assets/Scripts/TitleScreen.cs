using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public GameObject creditsPanel;

    void Start() {
        creditsPanel.SetActive(false);
    }

    public void NewMission() {
        SceneManager.LoadSceneAsync("LaunchScene");
    }

    public void ContinueMission() {

    }

    public void OpenCredits() {
        creditsPanel.SetActive(true);
    }

    public void CloseCreditsMenu() {
        creditsPanel.SetActive(false);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
