using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textbox;
    [SerializeField] public TextMeshProUGUI speakerbox;
    public List<string> lines;
    public List<string> speakers;
    public float textSpeed;
    private int index;
    

    public void SetLines(Dialogue dialogue)
    {
        foreach (var item in dialogue.dialogues) {
            speakers.Add(item.Speaker);
            lines.Add(item.Line);
        }
    }

    public void OnEnable()
    {
        textbox.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textbox.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textbox.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        speakerbox.text = speakers[index];
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textbox.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Count - 1) 
        {
            index++;
            speakerbox.text = speakers[index];
            textbox.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
