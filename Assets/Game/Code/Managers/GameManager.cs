using System.Collections;
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

        public void CallDisolveCoroutine(GameObject p_object)
        {
            _disolveCoroutine = StartCoroutine(DisolveWallsCorroutine(p_object));
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

            UIManager.instance.ActivateOrDeactivateGamePanel(false);
            UIManager.instance.ActivateOrDeactivatePausePanel(true);
        }

        protected void ResumeGame()
        {
            _state = GeneralGameStates.GAME;

            Time.timeScale = 1.0f;

            UIManager.instance.ActivateOrDeactivateGamePanel(true);
            UIManager.instance.ActivateOrDeactivatePausePanel(false);
        }

        protected void WinGame()
        {
            _state = GeneralGameStates.VICTORY;
            UIManager.instance.ActivateOrDeactivateGamePanel(false);
            UIManager.instance.ActivateOrDeactivatePausePanel(false);
        }

        protected void LoseGame()
        {
            _state = GeneralGameStates.GAME_OVER;
            //SceneChanger.instance.ChangeSceneTo(2);
        }

        #endregion

        #endregion

        #region Coroutines

        protected IEnumerator DisolveWallsCorroutine(GameObject p_object)
        {
            Debug.Log(p_object.name); 

            p_object.GetComponent<Animator>()?.Play("DisolveWall");

            yield return new WaitForSeconds(2.0f);

            if (p_object.GetComponent<Animator>() != null)
            {
                p_object.GetComponent<BoxCollider>().enabled = false;
            }
        }

        #endregion
    }
}
