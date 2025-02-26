using UnityEngine;
using UnityEngine.UI;  // Ensure this is included
using TMPro;
using BitSplash.AI.GPT.Extras;
public class CaptionsGenerator : MonoBehaviour
{
    public TextMeshProUGUI uiText;
    public GPT gpt;
    public void UpdateText(string newText)
    {
        newText = ReplaceHexWords(newText);
        if (uiText.text == "Listening...")
        {
            uiText.text = "";
        }
        if (uiText != null)
        {
            uiText.text += $"{newText}\n";
            gpt.SendClicked();
        }
    }
    public void isListening()
    {
        uiText.text = "Listening...";
    }
    public void RestartText()
    {
        uiText.text = "";
    }
    string ReplaceHexWords(string text)
    {
        string[] words = text.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].ToLower().Contains("hex"))
            {
                words[i] = "HexR";
            }
        }

        return string.Join(" ", words);
    }
}
