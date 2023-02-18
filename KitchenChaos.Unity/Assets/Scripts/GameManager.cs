using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// TODO delete me.<br />
        /// A debugging timer to keep track of the time since the game started
        /// </summary>
        private float gameStartedTimer = TIME_TO_PLAY_BEFORE_GAME_OVER;
        /// <summary>
        /// TODO delete me.<br />
        /// A number of seconds to keep the game alive before ending it.
        /// </summary>
        private const float TIME_TO_PLAY_BEFORE_GAME_OVER = 30.0f;

        /// <summary>
        /// A testing
        /// </summary>
        private float gameStartingTimer = TIME_UNTIL_GAME_START;

        /// <summary>
        /// The seconds before the game starts
        /// </summary>
        private const float TIME_UNTIL_GAME_START = 3.0f;

        /// <summary>
        /// Whether the game is paused
        /// </summary>
        private bool isPaused = false;

        public event EventHandler OnGamePaused;
        public event EventHandler OnGameUnpaused;

        public enum State
        {
            GAME_STARTING,
            GAME_PLAYING,
            GAME_OVER
        }

        /// <summary>
        /// The game's current state
        /// </summary>
        public State state { private set; get; }

        /// <summary>
        /// Event fired for the game's state changed
        /// </summary>
        public event EventHandler<GameStateChangedEventArgs> OnGameStateChanged;
        public class GameStateChangedEventArgs : EventArgs
        {
            /// <summary>
            /// The next state the game will be in
            /// </summary>
            public State newState;
        }

        public static GameManager Instance { private set; get; }



        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("There should be only one instance of GameManager");
            }
        }

        private void Start()
        {
            state = State.GAME_STARTING;

            GameInput.Instance.OnPausePressed += GameInput_OnPause;
        }


        private void Update()
        {
            switch (state)
            {
                case State.GAME_STARTING:
                    {
                        this.gameStartingTimer -= Time.deltaTime;
                        if (this.gameStartingTimer <= 0.0f)
                        {
                            State newState = State.GAME_PLAYING;
                            this.state = newState;
                            this.OnGameStateChanged?.Invoke(this, e: new GameStateChangedEventArgs { newState = newState });

                            this.gameStartingTimer = TIME_UNTIL_GAME_START;
                        }
                        break;
                    }
                    
                case State.GAME_PLAYING:
                    {
                        this.gameStartedTimer -= Time.deltaTime;
                        if (this.gameStartedTimer <= 0.0f)
                        {
                            State newState = State.GAME_OVER;
                            this.state = newState;
                            this.OnGameStateChanged?.Invoke(this, e: new GameStateChangedEventArgs { newState = newState });

                            this.gameStartedTimer = TIME_TO_PLAY_BEFORE_GAME_OVER;

                        }
                        break;
                    }
                case State.GAME_OVER:
                    break;

            }
            Debug.Log(state);
        }

        /// <summary>
        /// Function invoked for game paused
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameInput_OnPause(object sender, EventArgs e)
        {
            this.TogglePause();
        }

        /// <summary>
        /// Returns a value between 0 and 1 where 1 is at the beginning of the timer and 0 is at its end
        /// </summary>
        /// <returns></returns>
        public float GetGamePlayingTimerNormalized()
        {
            return this.gameStartedTimer / TIME_TO_PLAY_BEFORE_GAME_OVER;
        }
        public float GetGameStartingTimer()
        {
            return this.gameStartingTimer;
        }
        public bool IsGameOver()
        {
            return this.state == State.GAME_OVER;
        }

        /// <summary>
        /// Helper to pause the game.
        /// </summary>
        public void TogglePause()
        {
            this.isPaused = !this.isPaused;
            if (this.isPaused)
            {
                Time.timeScale = 0.0f;
                this.OnGamePaused?.Invoke(sender: this, EventArgs.Empty);
            }
            else
            {
                Time.timeScale = 1.0f;
                this.OnGameUnpaused?.Invoke(sender: this, EventArgs.Empty);

            }
        }
    }

}