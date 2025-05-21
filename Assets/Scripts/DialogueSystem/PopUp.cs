using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text NotificationText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void setPopUpText(string text)
    {
        NotificationText.text = text;
    }
}
