using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using HaptGlove;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;


namespace HexR
{
    public class PressureTrackerMain : MonoBehaviour
    {
        [Tooltip("Located in OVRHands ")]

        public HandGrabInteractor handGrabInteractor;
        public PokeInteractor pokeInteractor;

        [HideInInspector]
        public int ThumbPressure, IndexPressure, MiddlePressure, RingPressure, LittlePressure, PalmPressure, TankPressure;
        [HideInInspector]
        public HaptGloveHandler gloveHandler;
        private HaptGloveManager haptGloveManager;
        [HideInInspector]
        public bool HandGrabbing, PokeHovering, CollisionNearHand;
        //This is the central control for the pressure on each finger
        //As we want them to be within 60 KPA

        // Start is called before the first frame update
        void Start()
        {
            haptGloveManager = gameObject?.GetComponentInParent<HaptGloveManager>();
            gloveHandler = gameObject.GetComponent<HaptGloveHandler>();
            ThumbPressure = 0;
            IndexPressure = 0;
            MiddlePressure = 0;
            RingPressure = 0;
            LittlePressure = 0;
            PalmPressure = 0;
            TankPressure = 0;
            CollisionNearHand = false;
            HandGrabbing = false;
            PokeHovering = false;

            if(handGrabInteractor == null)
            {
                Debug.Log("Meta hand grab interactor is not assign, drag the hand grab interactor from OVRhands, this is used to track if your left or right hand is grabbing/pinching to trigger the correct haptics.");
            }
            if (pokeInteractor == null)
            {
                Debug.Log("Meta hand poke interactor is not assign, drag the hand grab interactor from OVRhands, this is used to track if your left or right hand is poking to trigger the correct haptics.");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (haptGloveManager.XRFramework == HaptGloveManager.Options.MetaOVR)
            {
                HandGrabbing = IsHandGrabbing();
                PokeHovering = IsPokeHover();
            }

            int[] AirPressure = gloveHandler?.GetAirPressure();
            if(AirPressure!= null)
            {
                ThumbPressure = ((int)Math.Round(AirPressure[0] / 100000.0) * 100000) - 100000;
                IndexPressure = ((int)Math.Round(AirPressure[1] / 100000.0) * 100000) - 100000;
                MiddlePressure = ((int)Math.Round(AirPressure[2] / 100000.0) * 100000) - 100000;
                RingPressure = ((int)Math.Round(AirPressure[3] / 100000.0) * 100000) - 100000;
                LittlePressure = ((int)Math.Round(AirPressure[4] / 100000.0) * 100000) - 100000;
                PalmPressure = ((int)Math.Round(AirPressure[5] / 100000.0) * 100000) - 100000;
                TankPressure = ((int)Math.Round(AirPressure[6] / 100000.0) * 100000) - 100000;
            }
        }

        #region Hand Proximity Test
        private bool IsHandGrabbing()
        {
            if (handGrabInteractor != null)
            {
                // Check if the interactor is grabbing something
                return handGrabInteractor.HasInteractable;
            }
            else
            {
                return false;
            }
        }
        private bool IsPokeHover()
        {
            if (pokeInteractor != null)
            {
                // Check if the interactor is poking something
                return pokeInteractor.HasInteractable;
            }
            else
            {
                return false;
            }
        }
        public void HandGrabbingCheck(bool IsHandGrabbing)
        {
            HandGrabbing = IsHandGrabbing;
        }
        public bool IsPhysicsCollisionNear(bool CollisionNearHand)
        {
            return CollisionNearHand;
        }
        #endregion

        #region Basic Haptics Functions For Single Trigger
        public void TriggerSingleHapticsIncrease(Haptics.Finger finger, bool states, float intensity, float speed, bool ByPassHandInteractionCheck)
        {
            if (!ByPassHandInteractionCheck && IsHandNear())
            {
                byte[] btData = gloveHandler.haptics.HEXRPressure(finger, states, intensity, speed);
                gloveHandler.BTSend(btData);
            }
            else if (ByPassHandInteractionCheck)
            {
                byte[] btData = gloveHandler.haptics.HEXRPressure(finger, states, intensity, speed);
                gloveHandler.BTSend(btData);
            }
        }
        public void RemoveSingleHaptics(Haptics.Finger finger, bool ByPassHandInteractionCheck)
        {
            if (IsHandNear() == true || ByPassHandInteractionCheck == true)
            {
                byte[] btData = gloveHandler.haptics.HEXRPressure(finger, false, 0, 1);
                gloveHandler.BTSend(btData);
            }
        }
        // Single Finger vibrations increase.
        public void CustomSingleVibrations(Haptics.Finger finger, bool states, float intensity, float frequency, bool ByPassHandCheck)
        {
            if (!ByPassHandCheck && IsHandNear())
            {
                byte[] btData = gloveHandler.haptics.HEXRVibration(finger, states, frequency, intensity);
                gloveHandler.BTSend(btData);
            }
            else if (ByPassHandCheck)
            {
                byte[] btData = gloveHandler.haptics.HEXRVibration(finger, states, frequency, intensity);
                gloveHandler.BTSend(btData);
            }
        }

        #region Simple Single haptics Functions
        public void SingleThumbHaptic(float TargetPressure)
        {
            if (IsHandNear() == true)
            {
                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Thumb, true, TargetPressure, 1);
                gloveHandler.BTSend(btData);

            }
        }
        public void SingleIndexHaptic(float TargetPressure)
        {
            if (IsHandNear() == true)
            {
                // btData contains the instruction for which haptics to be triggered and the incremented pressure
                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Index, true, TargetPressure, 1);
                gloveHandler.BTSend(btData);

            }
        }
        public void SingleMiddleHaptic(float TargetPressure)
        {
            if (IsHandNear() == true)
            {
                // btData contains the instruction for which haptics to be triggered and the incremented pressure

                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Middle, true, TargetPressure, 1);
                gloveHandler.BTSend(btData);

            }
        }
        public void SingleRingHaptic(float TargetPressure)
        {
            if (IsHandNear() == true)
            {
                // btData contains the instruction for which haptics to be triggered and the incremented pressure

                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Ring, true, TargetPressure, 1);
                gloveHandler.BTSend(btData);

            }
        }
        public void SinglePinkyHaptic(float TargetPressure)
        {
            if (IsHandNear() == true)
            {
                // btData contains the instruction for which haptics to be triggered and the incremented pressure

                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Pinky, true, TargetPressure, 1);
                gloveHandler.BTSend(btData);

            }
        }
        public void SinglePalmHaptic(float TargetPressure)
        {
            if (IsHandNear() == true)
            {
                // btData contains the instruction for which haptics to be triggered and the incremented pressure

                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Palm, true, TargetPressure, 1);
                gloveHandler.BTSend(btData);
            }
        }
        public void RemoveThumbHaptics()
        {
            byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Thumb, false, 0, 1);
            gloveHandler.BTSend(btData);
        }
        public void RemoveIndexHaptics()
        {
            byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Index, false, 0, 1);
            gloveHandler.BTSend(btData);
        }
        public void RemoveMiddleHaptics()
        {
            byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Middle, false, 0, 1);
            gloveHandler.BTSend(btData);
        }
        public void RemoveRingHaptics()
        {
            byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Ring, false, 0, 1);
            gloveHandler.BTSend(btData);
        }
        public void RemovePinkyHaptics()
        {
            byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Pinky, false, 0, 1);
            gloveHandler.BTSend(btData);
        }
        public void RemovePalmHaptics()
        {
            byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Palm, false, 0, 1);
            gloveHandler.BTSend(btData);
        }
        #endregion

        #endregion

        #region Basic Haptics Function For Multiple Trigger
        public void TriggerAllHapticsIncrease(float TargetPressure)
        {
            if (IsHandNear())
            {
                Haptics.Finger[] AllFingers = new Haptics.Finger[] { Haptics.Finger.Thumb, Haptics.Finger.Index, Haptics.Finger.Middle, Haptics.Finger.Ring, Haptics.Finger.Pinky, Haptics.Finger.Palm };

                float[] ThePressure = new float[] { TargetPressure, TargetPressure, TargetPressure, TargetPressure, TargetPressure, TargetPressure };
                float[] TheSpeed = new float[] { 1, 1, 1, 1, 1, 1 };
                bool[] TheBool = new bool[] { true, true, true, true, true, true };

                byte[] btData = gloveHandler.haptics.HEXRPressure(AllFingers, TheBool, ThePressure, TheSpeed);
                gloveHandler.BTSend(btData);
            }
        }
        public void TriggerCustomHapticsIncrease(bool[] TheBool, float TargetPressure)
        {
            if (IsHandNear())
            {

                Haptics.Finger[] AllFingers = new Haptics.Finger[] { Haptics.Finger.Thumb, Haptics.Finger.Index, Haptics.Finger.Middle, Haptics.Finger.Ring, Haptics.Finger.Pinky, Haptics.Finger.Palm };

                float[] ThePressure = new float[] { TargetPressure, TargetPressure, TargetPressure, TargetPressure, TargetPressure, TargetPressure };
                float[] TheSpeed = new float[] { 1, 1, 1, 1, 1, 1 };

                byte[] btData = gloveHandler.haptics.HEXRPressure(AllFingers, TheBool, ThePressure, TheSpeed);
                gloveHandler.BTSend(btData);
            }
        }
        public void TriggerPinchPressure(float TargetPressure)
        {
            if (IsHandNear())
            {
                Haptics.Finger[] AllFingers = new Haptics.Finger[] { Haptics.Finger.Thumb, Haptics.Finger.Index };

                float[] ThePressure = new float[] { TargetPressure, TargetPressure };
                float[] TheSpeed = new float[] { 1, 1 };
                bool[] TheBool = new bool[] { true, true };

                byte[] btData = gloveHandler.haptics.HEXRPressure(AllFingers, TheBool, ThePressure, TheSpeed);
                gloveHandler.BTSend(btData);
            }
        
        }
        public void RemovePinchPressure()
        {

            Haptics.Finger[] AllFingers = new Haptics.Finger[] { Haptics.Finger.Thumb, Haptics.Finger.Index};

            float[] ThePressure = new float[] { 0, 0 };
            float[] TheSpeed = new float[] { 1, 1};
            bool[] TheBool = new bool[] { false, false};

            byte[] btData = gloveHandler.haptics.HEXRPressure(AllFingers, TheBool, ThePressure, TheSpeed);
            gloveHandler.BTSend(btData);

        }
        public void TriggerPinchPressureReverse(float TargetPressure)
        {
            Haptics.Finger[] AllFingers = new Haptics.Finger[] { Haptics.Finger.Middle, Haptics.Finger.Ring };

            float[] ThePressure = new float[] { TargetPressure, TargetPressure };
            float[] TheSpeed = new float[] { 1, 1 };
            bool[] TheBool = new bool[] { true, true };

            byte[] btData = gloveHandler.haptics.HEXRPressure(AllFingers, TheBool, ThePressure, TheSpeed);
            gloveHandler.BTSend(btData);
        }
        public void RemovePinchPressureReverse()
        {

            Haptics.Finger[] AllFingers = new Haptics.Finger[] { Haptics.Finger.Middle, Haptics.Finger.Ring };

            float[] ThePressure = new float[] { 0, 0 };
            float[] TheSpeed = new float[] { 1, 1 };
            bool[] TheBool = new bool[] { false, false };

            byte[] btData = gloveHandler.haptics.HEXRPressure(AllFingers, TheBool, ThePressure, TheSpeed);
            gloveHandler.BTSend(btData);

        }
        public void TriggerAllVibrations(float VibrationStrength)
        {
            if (IsHandNear())
            {
                Haptics.Finger[] AllFingers = new Haptics.Finger[] { Haptics.Finger.Thumb, Haptics.Finger.Index, Haptics.Finger.Middle, Haptics.Finger.Ring, Haptics.Finger.Pinky, Haptics.Finger.Palm };

                float[] TheFrequency = new float[] { VibrationStrength, VibrationStrength, VibrationStrength, VibrationStrength, VibrationStrength, VibrationStrength };
                float[] ThePressure = new float[] { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
                bool[] TheBool = new bool[] { true, true, true, true, true, true };

                byte[] btData = gloveHandler.haptics.HEXRVibration(AllFingers, TheBool, TheFrequency, ThePressure);
                gloveHandler.BTSend(btData);

            }
        }
        public void RemoveAllHaptics()
        {

            Haptics.Finger[] AllFingers = new Haptics.Finger[] { Haptics.Finger.Thumb, Haptics.Finger.Index, Haptics.Finger.Middle, Haptics.Finger.Ring, Haptics.Finger.Pinky, Haptics.Finger.Palm };

            float[] ThePressure = new float[] { 0, 0, 0, 0, 0, 0 };
            float[] TheSpeed = new float[] { 1, 1, 1, 1, 1, 1 };
            bool[] TheBool = new bool[] { false, false, false, false, false, false };

            byte[] btData = gloveHandler.haptics.HEXRPressure(AllFingers, TheBool, ThePressure, TheSpeed);

            gloveHandler.BTSend(btData);

        }
        public void RemoveAllVibrations()
        {
            Haptics.Finger[] AllFingers = new Haptics.Finger[] { Haptics.Finger.Thumb, Haptics.Finger.Index, Haptics.Finger.Middle, Haptics.Finger.Ring, Haptics.Finger.Pinky, Haptics.Finger.Palm };

            float[] TheFrequency = new float[] { 0f, 0f, 0f, 0f, 0f, 0f };
            float[] ThePressure = new float[] { 0f, 0f, 0f, 0f, 0f, 0f };
            bool[] TheBool = new bool[] { false, false, false, false, false, false };

            byte[] btData = gloveHandler.haptics.HEXRVibration(AllFingers, TheBool, TheFrequency, ThePressure);
            gloveHandler.BTSend(btData);
        }

        public async void TriggerPulseIt()
        {
            // pressure is first increase by 10 and after a delay it is reduce by 10
            // Currently used in drum scene to simulate pulse hitting a drum
            await PulseAllFinger();
        }
        public async Task PulseAllFinger()
        {
            // add pressure
            if (ThumbPressure + 10 < 60)
            {
                ThumbPressure = ThumbPressure + 10;
            }
            if (IndexPressure + 10 < 60)
            {
                IndexPressure = IndexPressure + 10;
            }
            if (MiddlePressure + 10 < 60)
            {
                MiddlePressure = MiddlePressure + 10;
            }
            if (RingPressure + 10 < 60)
            {
                RingPressure = RingPressure + 10;
            }
            if (LittlePressure + 10 < 60)
            {
                LittlePressure = LittlePressure + 10;
            }
            if (PalmPressure + 10 < 60)
            {
                PalmPressure = PalmPressure + 10;
            }

            byte[][] ClutchState = new byte[][] { new byte[] { 0, 0 }, new byte[] { 1, 0 }, new byte[] { 2, 0 }, new byte[] { 3, 0 }, new byte[] { 4, 0 }
                            , new byte[] { 5, 0 }};

            byte[] btData = gloveHandler.haptics.ApplyHaptics(ClutchState, (byte)10, false);
            gloveHandler.BTSend(btData);
            // Wait for 0.3 seconds
            await Task.Delay(100);

            // Remove pressure
            if (10 <= ThumbPressure && ThumbPressure <= 60)
            {
                ThumbPressure = ThumbPressure - 10;
            }
            if (10 <= IndexPressure && IndexPressure <= 60)
            {
                IndexPressure = IndexPressure - 10;
            }
            if (10 <= MiddlePressure && MiddlePressure <= 60)
            {
                MiddlePressure = MiddlePressure - 10;
            }
            if (10 <= RingPressure && RingPressure <= 60)
            {
                RingPressure = RingPressure - 10;
            }
            if (10 <= LittlePressure && LittlePressure <= 60)
            {
                LittlePressure = LittlePressure - 10;
            }
            if (10 <= PalmPressure && PalmPressure <= 60)
            {
                PalmPressure = PalmPressure - 10;
            }

            byte[][] RemoveClutchState = new byte[][] { new byte[] { 0, 2 }, new byte[] { 1, 2 }, new byte[] { 2, 2 }, new byte[] { 3, 2 }
                                , new byte[] { 4, 2 } , new byte[] { 5, 2 }};

            byte[] RbtData = gloveHandler.haptics.ApplyHaptics(RemoveClutchState, (byte)10, false);
            gloveHandler.BTSend(RbtData);
        }

        #endregion

        #region Helpers

        private bool IsHandNear()
        {
            if(HandGrabbing == true || PokeHovering == true || CollisionNearHand == true )
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion

    }
}

