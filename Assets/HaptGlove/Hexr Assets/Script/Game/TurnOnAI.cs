using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Inworld;

    public class TurnOnAI : MonoBehaviour
    {
        public GameObject JayAvatar;

    private void Start()
    {
        NPCAudioNotRecieve();
    }
    private void OnTriggerEnter(Collider collider)
        {
            //NPCAudioRecieve();
        }
        private void OnTriggerStay(Collider collider)
        {
            //NPCAudioRecieve();
        }
        private void OnTriggerExit(Collider other)
        {
           // NPCAudioNotRecieve();
        }
        public void NPCAudioRecieve()
        {
            //InworldController.CharacterHandler.ManualAudioHandling = false;
            //InworldController.Audio.AutoPush = true;
            SetLayerRecursively(JayAvatar, 0);
        }
        public void NPCAudioNotRecieve()
        {
            //InworldController.CharacterHandler.ManualAudioHandling = true;
            //InworldController.Audio.AutoPush = false;
            SetLayerRecursively(JayAvatar, 9);
        }
        public void SetLayerRecursively(GameObject obj, int newLayer)
        {
            // Check if the GameObject is not null
            if (obj == null)
            {
                return;
            }

            // Change the layer of the GameObject
            obj.layer = newLayer;

            // Iterate through each child and change its layer
            foreach (Transform child in obj.transform)
            {
                if (child == null)
                {
                    continue;
                }
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }
    }

