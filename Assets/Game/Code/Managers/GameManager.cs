using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mr_Sanmi.ThiefGame
{
    public enum GeneralGameStates
    {
        NONE,
        GAME,
        PAUSE,
        VICTORY,
        GAME_OVER
    }
    public class GameManager : MonoBehaviour
    {
        #region References
        [SerializeField] public static GameManager instance;

        [SerializeField] protected GameObject _finalCollission;

        #endregion

        #region RuntimeVariables
        [SerializeField] protected GeneralGameStates _state;
        [SerializeField] protected Coroutine _disolveCoroutine;
        #endregion

        #region Unity Methods
        private void Awake() 
        { 
            if (instance == null) instance = this;
            _state = GeneralGameStates.GAME;
        }
        #endregion

        #region Public Methods

        public void PauseResumeGame()
        {
            switch (_state)
            {
                case GeneralGameStates.GAME:
                    ChangeState(GeneralGameStates.PAUSE); 
                    break;
                case GeneralGameStates.PAUSE:
                    ChangeState(GeneralGameStates.GAME);
                    break;
            }
        }

        public void ChangeToWinState()
        {
            ChangeState(GeneralGameStates.VICTORY);
        }

        public void ChangeToGameOver()
        {
            ChangeState(GeneralGameStates.GAME_OVER);
        }

        public void CallDisolveCoroutine(GameObject p_object)
        {
            _disolveCoroutine = StartCoroutine(DisolveWallsCorroutine(p_object));
        }

        public void ActivateOrDeactivateFinalCollision(bool p_hasCollectedTheDiamond)
        {
            if (p_hasCollectedTheDiamond)
            {
                _finalCollission.SetActive(false);
            }
            else
            {
                UIManager.instance.CallUIFunction("TurnFinalMessageTextOn");
            }
        }

        #endregion

        #region LocalMethods
        protected void ChangeState(GeneralGameStates p_state)
        {
            if (p_state == _state) return;

            switch (p_state)
            {
                case GeneralGameStates.PAUSE:
                    PauseGame();
                    break;
                case GeneralGameStates.GAME:
                    ResumeGame();
                    break;
                case GeneralGameStates.VICTORY:
                    WinGame();
                    break;
                case GeneralGameStates.GAME_OVER:
                    LoseGame();
                    break;
            }
        }

        #region FSM
        protected void PauseGame()
        {
            _state = GeneralGameStates.PAUSE;

            Time.timeScale = 0.0f;

            UIManager.instance.CallUIFunction("ActivatePausePanel");
        }

        protected void ResumeGame()
        {
            _state = GeneralGameStates.GAME;

            Time.timeScale = 1.0f;

            UIManager.instance.CallUIFunction("ActivateGamePanel");
        }

        protected void WinGame()
        {
            _state = GeneralGameStates.VICTORY;
            SceneChanger.instance.ChangeSceneTo(2);
        }

        protected void LoseGame()
        {
            _state = GeneralGameStates.GAME_OVER;
            SceneChanger.instance.ChangeSceneTo(3);
        }

        #endregion

        #endregion

        #region Coroutines

        protected IEnumerator DisolveWallsCorroutine(GameObject p_object)
        {
            Debug.Log(p_object.name); 

            p_object.GetComponent<Animator>()?.Play("DisolveWall");

            yield return new WaitForSeconds(1.0f);

            if (p_object.GetComponent<Animator>() != null)
            {
                p_object.GetComponent<BoxCollider>().enabled = false;
            }
        }
        #endregion

        #region GetterAndSetters

        public GeneralGameStates GetGameState()
        {
            return _state;
        }

        #endregion
    }
}
