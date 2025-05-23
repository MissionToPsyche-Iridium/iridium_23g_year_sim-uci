using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;



public class ResearchPaperLock : MonoBehaviour
{
    [System.Serializable]
    public class ResearchEntry
    {
        public string key;
        public Button button;
        public TMP_Text label;
        public bool isUnlocked = false;
        public string unlockedText;
    }

    public List<ResearchEntry> researchEntries;

    private void Start()
    {
        foreach (var entry in researchEntries)
        {
            UpdateButtonState(entry);
        }
    }

    public void UnlockPaper(string key)
    {
        foreach (var entry in researchEntries)
        {
            if (entry.key == key && !entry.isUnlocked)
            {
                entry.isUnlocked = true;
                UpdateButtonState(entry);
                // Debug.Log($"Unlocked paper: {key}");
                break;
            }
        }
    }

    public bool IsUnlocked(string key)
    {
        foreach (var entry in researchEntries)
        {
            if (entry.key == key)
                return entry.isUnlocked;
        }
        return false;
    }

    private void UpdateButtonState(ResearchEntry entry)
    {
        if (entry.isUnlocked)
        {
            entry.button.interactable = true;
            entry.label.text = entry.unlockedText;
        }
        else
        {
            entry.button.interactable = true;
            entry.label.text = "??? ";
        }
    }
    
}
