﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class TitleController : MonoBehaviour
    {

        /*public void Title_Audio()
        {
            var title_audio = GameObject.Find("TitleAudioController");
            title_audio.GetComponent<TitleAudioController>().TitleSpace();
        }*/


        private bool SPACE_UP() { return (Input.GetKeyDown(KeyCode.Space)); }
        private bool _dbutton() { return (Input.GetKeyDown(KeyCode.JoystickButton4)); }
        private Scene _scene;

        void Update()
        {
            _scene = SceneManager.GetActiveScene();

            if (SPACE_UP() || _dbutton())
            {
                //Title_Audio();
                //SceneManager.LoadScene(Constants.Scenes.InstructionsScene);
                if (_scene.name == Constants.Scenes.TitleScene)
                {
                    SceneManager.LoadScene(Constants.Scenes.InstructionsScene);
                }
                else if (_scene.name == Constants.Scenes.InstructionsScene)
                {
                    SceneManager.LoadScene(Constants.Scenes.CheateSheet);
                }
                else if (_scene.name == Constants.Scenes.CheateSheet)
                {
                    SceneManager.LoadScene(Constants.Scenes.GameScene);
                }
            }
        }
    }
}
