using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

namespace UI.DeathMenu
{
    public class DeathMenu : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private SceneObject mainScene;
        [SerializeField] private SceneObject startScene;
        [SerializeField] private GameObject crossFadeObject;

        private Animator crossFadeAnim;
        private Image crossFadeImage;
        private bool _hasFaded;
        private static readonly int Property = Animator.StringToHash("Start");


        private void Start()
        {
            restartButton.onClick.AddListener(LoadMainScene);
            quitButton.onClick.AddListener(QuitGame);
            mainMenuButton.onClick.AddListener(LoadStartScene);

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
            StartCoroutine(SceneTransition(mainScene));
            
        }

        private void LoadStartScene()
        {
            Time.timeScale = 1f;
            StartCoroutine(SceneTransition(startScene));
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



        private IEnumerator SceneTransition(SceneObject sceneToLoad)
        {
            crossFadeObject.SetActive(true);
            crossFadeAnim.SetTrigger(Property);
            yield return new WaitForSeconds(1f);
            SceneManager.LoadSceneAsync(sceneToLoad);
        }

    }
}

