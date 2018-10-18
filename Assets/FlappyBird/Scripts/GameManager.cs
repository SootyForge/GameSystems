using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance = null;
        private void Awake()
        {
            // Reference the first instance of GameManager
            Instance = this;
            // OPTIONAL
            // IF instance is not null
                // Destroy the instance
        }
        private void OnDestroy()
        {
            // Dereference the destroyed instance
            Instance = null;
        }
        #endregion
        public int score = 0;
        public float timeScale = 1;
        public bool isGameOver = false;

        public delegate void IntCallback(int number);
        public IntCallback scoreAdded;
        
        public void AddScore(int scoreToAdd)
        {
            // Is the game over?
            if (isGameOver)
                return; // Exit the function

            // Add score
            score += scoreToAdd;

            // Call subscribers
            scoreAdded.Invoke(score);
        }

        public void GameOver()
        {
            timeScale = 0;
            isGameOver = true;
        }
    } 
}
