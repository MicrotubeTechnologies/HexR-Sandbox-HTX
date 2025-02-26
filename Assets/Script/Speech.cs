using Microsoft.CognitiveServices.Speech;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Net.NetworkInformation;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using System.Threading;

public class Speech : MonoBehaviour
{
    public CaptionsGenerator textUpdater; // Reference to the TextUpdater component

    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    private const string speechKey = "12d826d7553e4ad48e1c8baa41807349";
    private const string speechRegion = "southeastasia";

    private string finalOutput;
    public static AudioSource audioSource;
    public async void RecognizeSpeech(CancellationToken token)
    {
        // Check if cancellation is requested
        if (token.IsCancellationRequested)
        {
            return;
        }
        var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
        speechConfig.SpeechRecognitionLanguage = "en-CA"; // Language setting for speech recognition
        using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);
        while (!token.IsCancellationRequested)
        {
            var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();

            if (speechRecognitionResult.Reason == ResultReason.RecognizedSpeech)
            {
                print("Speech Recognized: " + speechRecognitionResult.Text);
                finalOutput = $"You: {speechRecognitionResult.Text}";
                textUpdater.UpdateText($"{finalOutput}");
            }
        }
    }

    public void CancelSpeechListening()
    {
        cancellationTokenSource.Cancel();
        StopAllCoroutines();
    }

    // Method for starting speech listening
    public void StartListening()
    {
        textUpdater.isListening();
        cancellationTokenSource.Cancel();
        cancellationTokenSource = new CancellationTokenSource();
        RecognizeSpeech(cancellationTokenSource.Token);
    }
    public void Update()
    {

    }
}
