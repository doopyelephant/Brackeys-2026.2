using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    public TMP_Text text;
    private bool IsComplete = false;
    public Func<bool> OnComplete;
    public void Message(string message)
    {
        text.text = "";
        StartCoroutine(PromptAsync(message));
    }

    IEnumerator PromptAsync(string message)
    {
        foreach (var c in message)
        {
            text.text += c;
            yield return new WaitForSecondsRealtime(0.04f);
        }
        IsComplete = true;
    }
    public void Close()
    {
        if (!IsComplete)
        {
            return;
        }
        OnComplete?.Invoke();
        Destroy(gameObject);
    }
    public void Skip()
    {
        OnComplete?.Invoke();
        Destroy(gameObject);
    }
}
