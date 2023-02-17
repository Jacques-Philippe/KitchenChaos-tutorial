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
        private float gameStartedTimer = 0.0f;
        /// <summary>
        /// TODO delete me.<br />
        /// A number of seconds to keep the game alive before ending it.
        /// </summary>
        private const float TIME_TO_PLAY_BEFORE_GAME_OVER = 10.0f;

        /// <summary>
        /// A testing
        /// </summary>
        private float gameStartingTimer = 0.0f;

        /// <summary>
        /// The seconds before the game starts
        /// </summary>
        private const float TIME_UNTIL_GAME_START = 3.0f;

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
        public event EventHandler OnGameStateChanged;
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
        }

        private void Update()
        {
            switch (state)
            {
                case State.GAME_STARTING:
                    {
                        this.gameStartingTimer += Time.deltaTime;
                        if (this.gameStartingTimer >= TIME_UNTIL_GAME_START)
                        {
                            State newState = State.GAME_PLAYING;
                            this.state = newState;
                            this.OnGameStateChanged?.Invoke(this, e: new GameStateChangedEventArgs { newState = newState });

                            this.gameStartingTimer = 0.0f;
                        }
                        break;
                    }
                    
                case State.GAME_PLAYING:
                    {
                        this.gameStartedTimer += Time.deltaTime;
                        if (this.gameStartedTimer >= TIME_TO_PLAY_BEFORE_GAME_OVER)
                        {
                            State newState = State.GAME_OVER;
                            this.state = newState;
                            this.OnGameStateChanged?.Invoke(this, e: new GameStateChangedEventArgs { newState = newState });

                            this.gameStartedTimer = 0.0f;

                        }
                        break;
                    }
                case State.GAME_OVER:
                    break;

            }
            Debug.Log(state);
        }

        public bool IsGameOver()
        {
            return this.state == State.GAME_OVER;
        }
    }

}