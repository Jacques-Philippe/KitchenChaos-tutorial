using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KitchenChaosTutorial
{

    public static class Loader
    {
        public const string MAIN_MENU_SCENE = "MainMenuScene";
        public const string LOADING_SCENE = "LoadingScene";
        public const string GAME_SCENE = "GameScene";

        public static void Load(string sceneName)
        {
            switch (sceneName)
            {
                case MAIN_MENU_SCENE:
                    {
                        SceneManager.LoadScene(sceneName: MAIN_MENU_SCENE);
                        break;
                    }
                case LOADING_SCENE:
                    {
                        SceneManager.LoadScene(sceneName: LOADING_SCENE);
                        break;
                    }
                case GAME_SCENE:
                    {
                        SceneManager.LoadScene(sceneName: GAME_SCENE);
                        break;
                    }
                default:
                    {
                        Debug.LogError($"Received improper input {sceneName} for Loader.Load(scene name)");
                        break;
                    }
            }
        }
    }
}
