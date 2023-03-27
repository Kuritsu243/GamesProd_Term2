using System;
using Cards;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Vignette = UnityEngine.Rendering.Universal.Vignette;

namespace PostProcessing
{//meow
    public class PostProcessingController : MonoBehaviour
    {
        [Header("Buff Colours")]
        [SerializeField] private Color damageBuffColour;
        [SerializeField] private Color speedBuffColour;
        [SerializeField] private Color jumpBuffColour;
        [SerializeField] private Color dashBuffColour;
        
        
        private Volume _sceneVolume;
        private Vignette _sceneVignette;
        // Start is called before the first frame update
        private void Start()
        {
            _sceneVolume = GetComponent<Volume>();
            _sceneVolume.profile.TryGet(out _sceneVignette);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void ToggleVignette(cardObject.BuffType currentBuff)
        {
            _sceneVignette.color.Override(GetBuffColour(currentBuff));
            _sceneVignette.active = true;
            _sceneVignette.intensity = new ClampedFloatParameter(1f, 0f, 1f);
            // _sceneVolume.profile.TryGet<Vignette>(out _sceneVignette);
        }

        public void DisableVignette()
        {
            _sceneVignette.active = false;
            
        }

        private Color GetBuffColour(cardObject.BuffType currentBuff)
        {
            var tempColor = currentBuff switch
            {
                cardObject.BuffType.Dash => dashBuffColour,
                cardObject.BuffType.Damage => damageBuffColour,
                cardObject.BuffType.JumpHeight => jumpBuffColour,
                cardObject.BuffType.Speed => speedBuffColour,
                _ => throw new ArgumentOutOfRangeException(nameof(currentBuff), currentBuff, null)
            };
            return tempColor;
        }
    }
}
