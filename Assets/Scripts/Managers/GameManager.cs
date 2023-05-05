using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    internal enum GameState
    {
        WAITING,
        STARTED,
        ENDED,
        TESTING
    }

    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]private GameState state = GameState.WAITING;

        public bool isPaused;
        public bool isKeyboardAndMouse = true; 

        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject transitionPanel, winPanel, defeatPanel;
        
        public event UnityAction<bool> GameEnded;
        public void OnGameEnded(bool win = true) => GameEnded?.Invoke(win);
        
        private void Start()
        {
            state = GameState.STARTED;
            isPaused = false;
        
            // GameEnded += EndGame;
            StartCoroutine(CallLoadingScreen());
        }
        
        private void Update()
        {
            if (state is GameState.TESTING) return;
        
            if (isPaused)
            {
                if (state != GameState.ENDED)
                {
                    pausePanel.SetActive(true);
                }
        
                AudioSystem.Instance.MuteAll();
                Time.timeScale = 0;
                return;
            }
        
            pausePanel.SetActive(false);
            AudioSystem.Instance.UnmuteAll();
            Time.timeScale = 1;
        }
        
        // private void EndGame(bool win)
        // {
        //     isPaused = true;
        //     state = GameState.ENDED;
        //
        //     if (win)
        //     {
        //         winPanel.SetActive(true);
        //         return;
        //     }
        //
        //     defeatPanel.SetActive(true);
        // }
        //
        private IEnumerator CallLoadingScreen()
        {
            transitionPanel.SetActive(true);
            var animationLength = transitionPanel.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        
            yield return new WaitForSeconds(animationLength);
            transitionPanel.SetActive(false);
        }
    }
}