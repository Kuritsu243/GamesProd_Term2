using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace PostProcessing
{
    public class PostProcessingController : MonoBehaviour
    {
        [Header("Buff Colours")]
        [SerializeField] private ColorParameter damageBuffColour;
        [SerializeField] private ColorParameter speedBuffColour;
        [SerializeField] private ColorParameter jumpBuffColour;
        [SerializeField] private ColorParameter dashBuffColour;

        private Volume _sceneVolume;
        private Vignette _sceneVignette;
        // Start is called before the first frame update
        private void Start()
        {
            _sceneVolume = GetComponent<Volume>();
            if (_sceneVolume.profile.TryGet<Vignette>(out var tmp))
            {
                _sceneVignette = tmp;
            }
            ToggleVignette();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void ToggleVignette()
        {
            _sceneVignette.active = true;
            _sceneVignette.color = damageBuffColour;
            _sceneVignette.intensity = new ClampedFloatParameter(1f, 0f, 1f);

        }
    }
}
