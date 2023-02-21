using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace KitchenChaosTutorial
{

    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        private void Start()
        {
            playButton.onClick.AddListener(() =>
            {
                //load the loading scene
                Loader.Load(Loader.LOADING_SCENE);
            });

            //Quit the application for quit button clicked
            //Note: this won't work in the Unity Editor, but will in final builds
            quitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });

            Time.timeScale = 1.0f;
            //Mark as active the Play button for controller navigation
            playButton.Select();
        }

    }

}