using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Meta.WitAi.TTS.Utilities;
using TMPro;
namespace Meta.WitAi.TTS.UX
{
    public class TTSSpeakerInput2 : MonoBehaviour
    {
        // Speaker
        [SerializeField] private TTSSpeaker _speaker;

        // Default input
        [SerializeField] private TextMeshProUGUI AnswerBank,Answer;
        [SerializeField] private Button _stopButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _speakButton;

        // Queue button that will not stop previous clip
        [SerializeField] private Toggle _queueButton;
        // Async toggle that will play a clip on completion
        [SerializeField] private Toggle _asyncToggle;
        [SerializeField] private AudioClip _asyncClip;

        [SerializeField] private string _dateId = "[DATE]";
        [SerializeField] private string[] _queuedText;

        // States
        private string _voice;
        private bool _loading;
        private bool _speaking;
        private bool _paused;
        private bool Loadinganimation, isTalking;
        public Animator animator;
        private string Talk = "Talking";
        // Add delegates
        private void OnEnable()
        {
            RefreshStopButton();
            RefreshPauseButton();
            _stopButton.onClick.AddListener(StopClick);
            _pauseButton.onClick.AddListener(PauseClick);
            _speakButton.onClick.AddListener(SpeakClick);
        }
        // Stop click
        private void StopClick() => _speaker.Stop();
        // Pause click
        private void PauseClick()
        {
            if (_speaker.IsPaused)
            {
                _speaker.Resume();
            }
            else
            {
                _speaker.Pause();
            }
        }
        // Speak phrase click
        public void SpeakClick()
        {
            Loadinganimation = true;
            // Speak phrase
            string phrase = FormatText(AnswerBank.text);
            bool queued = _queueButton != null && _queueButton.isOn;
            bool async = _asyncToggle != null && _asyncToggle.isOn;

            // Speak async
            if (async)
            {
                StartCoroutine(SpeakAsync(phrase, queued));
            }
            // Speak queued
            else if (queued)
            {
                _speaker.SpeakQueued(phrase);
            }
            // Speak
            else
            {
                _speaker.Speak(phrase);
            }

            // Queue additional phrases
            if (_queuedText != null && _queuedText.Length > 0 && queued)
            {
                foreach (var text in _queuedText)
                {
                    _speaker.SpeakQueued(FormatText(text));
                }
            }
        }
        // Speak async
        private IEnumerator SpeakAsync(string phrase, bool queued)
        {

            // Queue
            if (queued)
            {
                yield return _speaker.SpeakQueuedAsync(new string[] { phrase });
            }
            // Default
            else
            {
                yield return _speaker.SpeakAsync(phrase);
            }

            // Play complete clip
            if (_asyncClip != null)
            {
                _speaker.AudioSource.PlayOneShot(_asyncClip);
            }

        }
        // Format text with current datetime
        private string FormatText(string text)
        {
            string result = text;
            if (result.Contains(_dateId))
            {
                DateTime now = DateTime.UtcNow;
                string dateString = $"{now.ToLongDateString()} at {now.ToLongTimeString()}";
                result = text.Replace(_dateId, dateString);
            }
            return result;
        }
        // Remove delegates
        private void OnDisable()
        {
            _stopButton.onClick.RemoveListener(StopClick);
            _speakButton.onClick.RemoveListener(SpeakClick);
        }

        // Preset text fields
        private void Update()
        {
            if(_speaker.IsSpeaking && Loadinganimation == true)
            {
                isTalking = true;
                Loadinganimation = false;
                animator.SetTrigger("Talking");
                StartCoroutine(ChunkWords(AnswerBank.text));
            }
            if(!_speaker.IsSpeaking && isTalking == true)
            {
                isTalking = false;
                animator.SetTrigger("DoneTalking");
            }
            // On preset voice id update
            if (!string.Equals(_voice, _speaker.VoiceID))
            {
                _voice = _speaker.VoiceID;
                //_input.text = $"Write something to say in {_voice}'s voice";
            }
            // On state changes
            if (_loading != _speaker.IsLoading)
            {
                RefreshStopButton();
            }
            if (_speaking != _speaker.IsSpeaking)
            {
                RefreshStopButton();
            }
            if (_paused != _speaker.IsPaused)
            {
                RefreshPauseButton();
            }
        }
        // Refresh interactable based on states
        private IEnumerator ChunkWords(string text)
        {
            // Split the text into words
            string[] words = text.Split(new char[] { ' ', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

            // Check the number of words
            Answer.text = ""; // Reset output text
                              // Iterate through each word
            foreach (string word in words)
            {
                Answer.text += word + " "; // Add the word to output
                yield return new WaitForSeconds(0.3f); // Wait for 0.2 seconds
            }

            // Optionally, trim the output string
            Answer.text = Answer.text.Trim();

        }
        private void RefreshStopButton()
        {
            _loading = _speaker.IsLoading;
            _speaking = _speaker.IsSpeaking;
            _stopButton.interactable = _loading || _speaking;
        }
        // Refresh text based on pause state
        private void RefreshPauseButton()
        {
            _paused = _speaker.IsPaused;
            _pauseButton.GetComponentInChildren<Text>().text = _paused ? "Resume" : "Pause";
        }
    }
}