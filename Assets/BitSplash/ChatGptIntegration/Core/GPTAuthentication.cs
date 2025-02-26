using BitSplash.AI.GPT;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GPTAuthentication : MonoBehaviour
{
    public const string defaultUrl = @"https://api.openai.com/v1/chat/completions";
    public ChatModels Model = ChatModels.GPT_3_5_TURBO;
    public string CompletionUrl = defaultUrl;
    public string PrivateApiKey;
    public string Organization;
    public string ProjectId;
    public InputField textMeshProUGUI;
    public TMP_Dropdown Chatmodel;
    // Start is called before the first frame update
    void Start()
    {
        PopulateDropdown();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateAPI()
    {
        PrivateApiKey = textMeshProUGUI.text;
    }
    // Populate the dropdown with chat model options
    public void PopulateDropdown()
    {
        // Clear existing options
        Chatmodel.ClearOptions();

        // Create a list of options
        var options = new System.Collections.Generic.List<string>();

        foreach (ChatModels model in System.Enum.GetValues(typeof(ChatModels)))
        {
            options.Add(model.ToString()); // Add model names to the options list
        }

        // Add the options to the dropdown
        Chatmodel.AddOptions(options);
    }
    public void UpdateSelectedModel()
    {
        // Get the currently selected option's text
        string selectedOption = Chatmodel.options[Chatmodel.value].text;

        // Parse the selected option to update the Model
        if (System.Enum.TryParse(selectedOption, out ChatModels selectedModel))
        {
            Model = selectedModel;
        }
        Debug.Log("Selected Model: " + Model.ToString()); // Optional: Log the selected model
    }

}
