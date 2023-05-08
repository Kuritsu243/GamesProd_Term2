using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using UnityEngine.Audio;
using Object = UnityEngine.Object;

namespace UI.StartMenu
{
    public class StartMenu : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button startButton;
        [SerializeField] private Button aboutButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button aboutCloseButton;
        [SerializeField] private Button settingsCloseButton;
        [Header("Objects")]
        [SerializeField] private Object mainScene;
        [SerializeField] private GameObject crossFadeObject;
        [SerializeField] private GameObject aboutMenu;
        [SerializeField] private GameObject settingsMenu;
        [Header("Settings")] 
        [SerializeField] private Resolution[] screenResolutions;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private Toggle fullscreenToggle;
        [SerializeField] private Button applyButton;
        
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private float currentVolume;
        



    
        private Animator crossFadeAnim;
        private Image crossFadeImage;
        private bool _hasFaded;
        private static readonly int Start1 = Animator.StringToHash("Start");
        private int _currentResIndex;

        private void Start()
        {
            startButton.onClick.AddListener(LoadMainScene);
            aboutButton.onClick.AddListener(ShowAboutUI);
            settingsButton.onClick.AddListener(ShowSettingsUI);
            quitButton.onClick.AddListener(QuitGame);
            aboutCloseButton.onClick.AddListener(HideAboutUI);
            settingsCloseButton.onClick.AddListener(HideSettingsUI);

            resolutionDropdown.onValueChanged.AddListener(SetResolution);
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
            volumeSlider.onValueChanged.AddListener(SetVolume);
            applyButton.onClick.AddListener(SaveSettings);
            
            crossFadeImage = crossFadeObject.GetComponent<Image>();
            crossFadeAnim = crossFadeObject.GetComponent<Animator>();
            
            if (!crossFadeObject.activeSelf) crossFadeObject.SetActive(true);
            if (aboutMenu.activeSelf) aboutMenu.SetActive(false);
            if (settingsMenu.activeSelf) settingsMenu.SetActive(false);
            
            
            InitializeResolutions();
            LoadSettings(_currentResIndex);
        }

        private void FixedUpdate()
        {
            if (_hasFaded) return;
            if (crossFadeImage.color.a != 0) return;
            crossFadeObject.SetActive(false);
            _hasFaded = true;
        }


        private void LoadMainScene()
        {
            Time.timeScale = 1f;
            StartCoroutine(MainSceneTransition());
        }

        private static void QuitGame()
        {
            switch (Application.platform)
            {
#if UNITY_EDITOR
                case RuntimePlatform.WindowsEditor:
                    EditorApplication.ExitPlaymode();
                    break;
#endif
                case RuntimePlatform.WindowsPlayer:
                    Application.Quit();
                    break;
            }
        }

        private void ShowAboutUI()
        {
            ToggleButtonInteractivity(false);
            if (!CheckButtonInteractivity()) return;
            CloseAllMenus();
            aboutMenu.SetActive(true);
        }

        private void HideAboutUI()
        {
            ToggleButtonInteractivity(true);
            if (!aboutMenu.activeSelf) return;
            aboutMenu.SetActive(false);
        }

        private void ShowSettingsUI()
        {
            ToggleButtonInteractivity(false);
            if (!CheckButtonInteractivity()) return;
            CloseAllMenus();
            settingsMenu.SetActive(true);
        }

        private void HideSettingsUI()
        {
            ToggleButtonInteractivity(true);
            if (!settingsMenu.activeSelf) return;
            settingsMenu.SetActive(false);
        }

        private IEnumerator MainSceneTransition()
        {
            crossFadeObject.SetActive(true);
            crossFadeAnim.SetTrigger(Start1);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadSceneAsync(nameof(mainScene));
        }

        private bool CheckButtonInteractivity()
        {
            return !(settingsMenu.activeSelf || aboutMenu.activeSelf);
        }

        private void ToggleButtonInteractivity(bool yn)
        {
            startButton.interactable = yn;
            aboutButton.interactable = yn;
            quitButton.interactable = yn;
            settingsButton.interactable = yn;
        }

        private void CloseAllMenus()
        {
            settingsMenu.SetActive(false);
            aboutMenu.SetActive(false);
            ToggleButtonInteractivity(true);
        }

        private void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        private void SetResolution(int resolutionIndex)
        {
            var resolution = screenResolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        private void SetVolume(float volume)
        {
            audioMixer.SetFloat("Volume", volume);
            currentVolume = volume;
        }

        private void SaveSettings()
        {
            PlayerPrefs.SetInt("FullScreenPreference", Convert.ToInt32(Screen.fullScreen));
            PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
            PlayerPrefs.SetFloat("VolumePreference", currentVolume);
        }

        private void LoadSettings(int resIndex)
        {
            Screen.fullScreen = PlayerPrefs.HasKey("FullScreenPreference") &&
                                Convert.ToBoolean(PlayerPrefs.GetInt("FullScreenPreference"));
            
            resolutionDropdown.value = PlayerPrefs.HasKey("ResolutionPreference")
                ? PlayerPrefs.GetInt("ResolutionPreference")
                : resIndex;
            
            volumeSlider.value = PlayerPrefs.HasKey("VolumePreference") ? PlayerPrefs.GetFloat("VolumePreference") : 1f;
        }


        private void InitializeResolutions()
        {
            resolutionDropdown.ClearOptions();
            var options = new List<string>();
            screenResolutions = Screen.resolutions;
            for (var i = 0; i < screenResolutions.Length; i++)
            {
                var option = screenResolutions[i].width + " x " + screenResolutions[i].height;
                options.Add(option);
                if (screenResolutions[i].width == Screen.currentResolution.width && screenResolutions[i].height == Screen.currentResolution.height)
                    _currentResIndex = i;
            }
            
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.RefreshShownValue();
        }
    }
    
    
}
