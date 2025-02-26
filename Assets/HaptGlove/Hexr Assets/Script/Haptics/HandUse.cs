
using Oculus.Interaction.HandGrab;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using TMPro;
using HaptGlove;
using Oculus;
using HexR;
namespace Oculus.Interaction
{
        public class HandUse : MonoBehaviour, IHandGrabUseDelegate
        {
        [SerializeField]
        private GameObject _squishableObject;
        public PressureTrackerMain RightpressureTrackerMain, LeftpressureTrackerMain;
        [SerializeField]
        [Range(0.01f, 1)]
        private float _maxSquish = 0.25f;
        [SerializeField]
        [Range(0.01f, 1)]
        private float _maxStretch = 0.15f, _CurrentPressure = 0f;
        protected bool _started;
        private Vector3 _initialScale;
        protected virtual void Start()
        {
            this.AssertField(_squishableObject, nameof(_squishableObject));

            _initialScale = _squishableObject.transform.localScale;
        }

        public void BeginUse()
        {
        }

        public void EndUse()
        {
            _squishableObject.transform.localScale = _initialScale;
        }

        public float ComputeUseStrength(float strength)
        {
            if(strength >= 0.5 &&_CurrentPressure != 50)
            {

                RightpressureTrackerMain.TriggerAllHapticsIncrease(50);
                LeftpressureTrackerMain.TriggerAllHapticsIncrease(50);
                _CurrentPressure = 50;
            }
            else if (strength <= 0.49 && _CurrentPressure != 0)
            {
                RightpressureTrackerMain.RemoveAllHaptics();
                LeftpressureTrackerMain.RemoveAllHaptics();
                _CurrentPressure = 0;
            }


            float squishAmount = Mathf.Lerp(1, 1 - _maxSquish, strength);
            float stretchAmount = Mathf.Lerp(1, 1 + _maxStretch, strength);

            // Perform a cheap axis squish and stretch effect
            _squishableObject.transform.localScale = new Vector3(_initialScale.x * stretchAmount, _initialScale.y * squishAmount, _initialScale.z * stretchAmount);
            return strength;
        }


    }

}
