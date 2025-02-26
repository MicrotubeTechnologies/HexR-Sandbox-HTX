using System.Collections;
using UnityEngine;
using HaptGlove;


namespace HexR
{
    public class HapticFingerTrigger : MonoBehaviour
    {
        private HaptGloveHandler gloveHandler;
        public GameObject HexrLeftOrRight;
        private PressureTrackerMain pressureTrackerMain;
        public HandType handType;
        public FingerType fingertype;
        private Haptics.Finger HapticsFingertype;

        [HideInInspector]
        public bool VibrationReadyToOff = false;

        public Material transparentMaterial; // Assign a transparent material in the inspector
        private bool enableVisualizer = false; // Toggle to enable/disable visualization
        public enum FingerType
        {
            Index,
            Middle,
            Ring,
            Thumb,
            Little,
            Palm
        };
        public enum HandType
        {
            Left,
            Right
        };

        //This controls the haptic being send to the individual fingers after it has been triggered by a collider that is
        //on the gameobject that the hand is touching

        // Start is called before the first frame update
        void Start()
        {
            gloveHandler = HexrLeftOrRight.GetComponent<HaptGloveHandler>();
            pressureTrackerMain = HexrLeftOrRight.GetComponent<PressureTrackerMain>();
            if (fingertype == FingerType.Thumb)
            {
                HapticsFingertype = Haptics.Finger.Thumb;
            }
            else if (fingertype == FingerType.Index)
            {
                HapticsFingertype = Haptics.Finger.Index;
            }
            else if (fingertype == FingerType.Middle)
            {
                HapticsFingertype = Haptics.Finger.Middle;
            }
            else if (fingertype == FingerType.Ring)
            {
                HapticsFingertype = Haptics.Finger.Ring;
            }
            else if (fingertype == FingerType.Little)
            {
                HapticsFingertype = Haptics.Finger.Pinky;
            }
            else if (fingertype == FingerType.Palm)
            {
                HapticsFingertype = Haptics.Finger.Palm;
            }

            if (enableVisualizer)
            {
                StartCoroutine(DelayedVisualizer());
            }

        }
        IEnumerator DelayedVisualizer()
        {
            yield return new WaitForSeconds(1); // Wait for 4 seconds

            if (TryGetComponent(out BoxCollider box))
            {
                CreateColliderVisualizer(PrimitiveType.Cube, box.size, box.center);
            }
            else if (TryGetComponent(out CapsuleCollider capsule))
            {
                CreateColliderVisualizer(PrimitiveType.Capsule, new Vector3(capsule.radius * 2, capsule.height, capsule.radius * 2), Vector3.zero);
            }
        }
        void CreateColliderVisualizer(PrimitiveType shape, Vector3 size, Vector3 centerOffset)
        {
            GameObject visualizer = GameObject.CreatePrimitive(shape);
            Destroy(visualizer.GetComponent<Collider>()); // Remove unnecessary collider
            visualizer.transform.SetParent(transform);

            // Adjust the position to match the collider's center
            visualizer.transform.localPosition = centerOffset;
            visualizer.transform.localRotation = Quaternion.identity;
            visualizer.transform.localScale = size;

            if (transparentMaterial != null)
                visualizer.GetComponent<Renderer>().material = transparentMaterial;
        }


        public void TriggerFixPressure(float TargetPressure)
        {

                pressureTrackerMain.TriggerSingleHapticsIncrease(HapticsFingertype,true, TargetPressure, 1, true);
        }
        public void TriggerVibrationPressure(float Frequency, float Intensity)
        {
            byte[] btData = gloveHandler.haptics.HEXRVibration(HapticsFingertype, true, Frequency, Intensity);
            gloveHandler.BTSend(btData);
            VibrationReadyToOff = true;
        }
        public void RemoveVibration()
        {
            byte[] btData = gloveHandler.haptics.HEXRVibration(HapticsFingertype, false, 0, 0);
            gloveHandler.BTSend(btData);
            VibrationReadyToOff = true;
        }

        IEnumerator RemoveVibrationDelay()
        {
            // Wait for the specified delay time
            yield return new WaitForSeconds(0.3f);

            if (VibrationReadyToOff == true)
            {
                RemoveVibration();
            }
            else
            {
                StartCoroutine(RemoveVibrationDelay());
            }
        }
        public void RemoveHaptics()
        {
            pressureTrackerMain.RemoveSingleHaptics(HapticsFingertype, true);
        }

    }
}
