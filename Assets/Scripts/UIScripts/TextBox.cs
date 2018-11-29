using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour {

    public TextMeshProUGUI TextArea;
    public Button NextButton;

    public void DisplaySentence(string sentence, bool dramatic = false)
    {
        TextArea.text = "";
        StartCoroutine(TypeSentence(sentence, dramatic));
        NextButton.gameObject.SetActive(false);
    }

    IEnumerator TypeSentence(string sentence, bool dramatic)
    {
        foreach(char letter in sentence.ToCharArray())
        {
            TextArea.text += letter;
            if (TextArea.text == sentence) { NextButton.gameObject.SetActive(true); }

            if (!dramatic) { yield return new WaitForEndOfFrame(); }
            else { yield return new WaitForSeconds(0.25f); }
        }
    }
}
