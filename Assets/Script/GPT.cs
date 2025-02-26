using BitSplash.AI.GPT;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Meta.WitAi.Dictation;
using Oculus.Voice.Dictation;
using Meta.WitAi.TTS.UX;
namespace BitSplash.AI.GPT.Extras
{
    public class GPT : MonoBehaviour
    {
        public TTSSpeakerInput2 tTSSpeakerInput2;
        public TextMeshProUGUI QuestionField, AnswerField,AnswerFinal;
        public string NpcDirection = "You are a friendly AI assistant from Microtube Technology to help assist with a tech demo for the HexR haptic glove.";
        public string[] Facts;
        public bool TrackConversation = true, readytotalk = false;
        public int MaximumTokens = 200;
        [Range(0f, 1f)]
        public float Temperature = 0f;
        ChatGPTConversation Conversation;
        //public AppDictationExperience appDictationExperience;
        void Start()
        {
            string MicrotubeFact1 = "HexR Glove cost around 4000 Dollars each glove.";
            string MicrotubeFact2 = "HexR Glove has 6 pressure zones that can be programmed.";
            string MicrotubeFact3 = "HexR Glove has been in development for 3 years.";
            string MicrotubeFact4 = "HexR Glove is develop by mircotube technology in singapore";
            string MicrotubeFact5 = "HexR Glove is available for both hands";
            string MicrotubeFact6 = "Microtube Technology is a start up based in singapore";
            string MicrotubeFact7 = "Microtube Technologies has worked with many clients, from ST Engineering, H T X, and several hospitals in Singapore.";
            string MicrotubeFact8 = "Microtube Technologies currently have two core product, the HexR glove for haptic VR and the Aris band for fitness tracking.";
            string MicrotubeFact9 = "HexR Glove is useful for simulation, education and even gaming.";
            string MicrotubeFact10 = "HexR Glove is not capable of feeling Temperature.";
            string MicrotubeFact11 = "HexR Glove is not capable of feeling Texture.";
            string MicrotubeFact12 = "HexR Glove is compatible with most VR headset, such as Quest 3, Quest 2, Quest pro, Hololens, Pico, H T C.";
            string MicrotubeFact13 = "HexR Glove is capable of feeling softness and hardness.";
            string MicrotubeFact14 = "HexR Glove is capable of simulating vibration.";
            string MicrotubeFact15 = "HexR Glove weighs around 250grams, slightly heavier that a mobile phone.";
            string MicrotubeFact16 = "ARIS band tracks your strength training and is a gym assistant.";
            Facts = new string[] { MicrotubeFact1, MicrotubeFact2, MicrotubeFact3, MicrotubeFact4, MicrotubeFact5, MicrotubeFact6, MicrotubeFact7,
                                   MicrotubeFact8,  MicrotubeFact9, MicrotubeFact10,MicrotubeFact11,MicrotubeFact12,MicrotubeFact13,MicrotubeFact14,
                                   MicrotubeFact15, MicrotubeFact16};
            SetUpConversation();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                SendClicked();
            }

            if (AnswerField.text == "Thinking...")
            {
                readytotalk = true;
            }
            if(AnswerField.text != "Thinking..." && readytotalk == true)
            {
                readytotalk = false;
                tTSSpeakerInput2.SpeakClick();
            }
        }
        #region Gpt
        void SetUpConversation()
        {
            Conversation = ChatGPTConversation.Start(this)
                .MaximumLength(MaximumTokens)
                .SaveHistory(TrackConversation)
                .System(string.Join("\n", Facts) + "\n" + NpcDirection);
            Conversation.Temperature = Temperature;
        }
        public void SendClicked()
        {
            AnswerFinal.text = "Thinking...";
            AnswerField.text = "Thinking...";
            string Whattosend = QuestionField.text + " (when replying do not exceed 50 words)";
            Conversation.Say(Whattosend);
        }

        void OnConversationResponse(string text)
        {
            AnswerField.text = text;
        }
        void OnConversationError(string text)
        {
            Debug.Log("Error : " + text);
            Conversation.RestartConversation();
            AnswerField.text = "Sorry :" + text;
        }

        private void OnValidate()
        {
            SetUpConversation();
        }
        #endregion

        public void StartDictation()
        {
            //appDictationExperience.Activate();
        }
    }

}