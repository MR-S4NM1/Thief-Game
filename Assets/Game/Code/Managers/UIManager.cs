using Mr_Sanmi.ThiefGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    #region References
    [SerializeField] public static UIManager instance;

    [Header("Game Panels")]
    [SerializeField] protected GameObject _gamePanel;
    [SerializeField] protected GameObject _pausePanel;
    [SerializeField] protected GameObject _controlsPanel;

    [Header("Title Screen")]
    [SerializeField] protected GameObject _creditsPanel;
    [SerializeField] protected GameObject _mainMenuPanel;

    [SerializeField] protected TextMeshProUGUI _numberOfCollectibles;
    [SerializeField] protected TextMeshProUGUI _reminderAboutTheDiamond;
    [SerializeField] protected GameObject _diamondImage;
    #endregion

    #region UnityMethods
    private void Awake()
    {
        if (instance == null) instance = this;

        if (_mainMenuPanel != null)
        {
            _mainMenuPanel.SetActive(true);
        }

        if (_creditsPanel != null)
        {
            _creditsPanel.SetActive(false);
        }

        if (_gamePanel != null)
        {
            _gamePanel.SetActive(true);
        }

        if (_pausePanel != null)
        {
            _pausePanel.SetActive(false);
        }

        if(_controlsPanel != null)
        {
            _controlsPanel.SetActive(false);
        }

        if(_diamondImage != null)
        {
            _diamondImage.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if(_numberOfCollectibles != null)
        {
            _numberOfCollectibles.text = $"0";
        }
    }

    #endregion

    #region PublicMethods

    public void ChangeToGameScene()
    {
        SceneChanger.instance.ChangeSceneTo(1);
    }

    public void CallUIFunction(string p_functionName)
    {
        switch (p_functionName)
        {
            case "ActivateGamePanel":
                ActivateGamePanel();
                break;

            case "ActivatePausePanel":
                ActivatePausePanel();
                break;

            case "ActivateControlsPanel":
                ActivateControlsPanel();
                break;

            case "ActivateMainMenuPanel":
                ActivateMainMenuPanel();
                break;

            case "ActivateCreditsPanel":
                ActivateCreditsPanel();
                break;

            case "BackToTitleScreen":
                BackToTitleScreen();
                break;

            case "ResumeGame":
                ResumeGame();
                break;

            case "QuitGame":
                QuitGame();
                break;

            case "SetDiamond":
                SetDiamond();
                break;

            case "TurnFinalMessageTextOn":
                TurnFinalMessageTextOn();
                break;

            case "GoToGameScene":
                GoToGameScene();
                break;

        }
    }
    public void UpdateCollectibles(int p_numberOfCollectibles)
    {
        _numberOfCollectibles.text = p_numberOfCollectibles.ToString();
    }

    #endregion

    #region LocalMethods
    #region TitleScreen
    protected void ActivateMainMenuPanel()
    {
        _creditsPanel?.SetActive(false);
        _mainMenuPanel?.SetActive(true);
    }

    protected void ActivateCreditsPanel()
    {
        _creditsPanel?.SetActive(true);
        _mainMenuPanel?.SetActive(false);
    }

    protected void GoToGameScene()
    {
        SceneChanger.instance.ChangeSceneTo(1);
    }

    #endregion

    #region GameScene
    protected void ActivateGamePanel()
    {
        _gamePanel?.SetActive(true);
        _pausePanel?.SetActive(false);
        _controlsPanel?.SetActive(false);
    }

    protected void ActivatePausePanel()
    {
        _gamePanel?.SetActive(false);
        _pausePanel?.SetActive(true);
        _controlsPanel?.SetActive(false);
    }

    protected void ActivateControlsPanel() 
    {
        _gamePanel?.SetActive(false);
        _pausePanel?.SetActive(false);
        _controlsPanel?.SetActive(true);
    }

    protected void ResumeGame()
    {
        GameManager.instance.PauseResumeGame();
        _gamePanel?.SetActive(true);
        _pausePanel?.SetActive(false);
        _controlsPanel?.SetActive(false);
    }

    public void BackToTitleScreen()
    {
        SceneChanger.instance.ChangeSceneTo(0);
    }
    protected void SetDiamond()
    {
        _diamondImage.SetActive(true);
    }

    protected void TurnFinalMessageTextOn()
    {
        _reminderAboutTheDiamond.gameObject.SetActive(true);
        _reminderAboutTheDiamond.text = $"I can't leave!\nI need to collect that diamond!";
    }

    #endregion

    protected void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
