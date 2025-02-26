using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexR;
namespace Oculus.Interaction
{
    public class DrumSound : MonoBehaviour
    {
        public GameObject HexrHand;
        private PressureTrackerMain pressureTrackerMain;
        private float Delay = 0.1f;
        private bool Ready = true;
        // Start is called before the first frame update
        void Start()
        {
            pressureTrackerMain = HexrHand.GetComponent<PressureTrackerMain>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Delay <= 0 && Ready == false)
            {
                Ready = true;
            }
            
            if(Delay > 0)
            {
                Delay -= Time.deltaTime;
            }
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent(out AudioTrigger audioTrigger))
            {
                if (Ready == true)
                {
                    audioTrigger.PlayAudio();
                    Ready = false;
                    Delay = 0.1f;
                    pressureTrackerMain.TriggerPulseIt();

                    if(other.gameObject.TryGetComponent(out Animator animator))
                    {
                        animator.SetTrigger("Move");
                    }
                }
            }
        }
    }
}

