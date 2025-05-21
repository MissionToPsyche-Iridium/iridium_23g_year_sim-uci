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
   
    public void setPopUpText(string text)
    {
        NotificationText.text = text;
    }
}
