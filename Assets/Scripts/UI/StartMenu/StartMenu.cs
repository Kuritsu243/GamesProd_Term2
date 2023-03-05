using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

namespace UI.StartMenu
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button aboutButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Scene mainScene;
        [SerializeField] private GameObject crossFadeObject;

        private Animator crossFadeAnim;
        private Image crossFadeImage;
        private bool _hasFaded;

        private void Start()
        {
            startButton.onClick.AddListener(LoadMainScene);
            aboutButton.onClick.AddListener(ShowAboutUI);
            settingsButton.onClick.AddListener(ShowSettingsUI);
            quitButton.onClick.AddListener(QuitGame);

            crossFadeImage = crossFadeObject.GetComponent<Image>();
            crossFadeAnim = crossFadeObject.GetComponent<Animator>();
            
            if (!crossFadeObject.activeSelf) crossFadeObject.SetActive(true);
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
                case RuntimePlatform.WindowsEditor:
                    EditorApplication.ExitPlaymode();
                    break;
                case RuntimePlatform.WindowsPlayer:
                    Application.Quit();
                    break;
            }
        }

        private void ShowAboutUI()
        {
            
        }

        private void ShowSettingsUI()
        {
            
        }

        private IEnumerator MainSceneTransition()
        {
            crossFadeObject.SetActive(true);
            crossFadeAnim.SetTrigger("Start");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadSceneAsync(nameof(mainScene));
        }
    }
    
    
}
