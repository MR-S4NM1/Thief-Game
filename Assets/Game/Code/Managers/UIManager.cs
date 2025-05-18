using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public static UIManager instance;

    [SerializeField] protected GameObject _gamePanel;
    [SerializeField] protected GameObject _pausePanel;

    [SerializeField] protected GameObject _creditsPanel;
    [SerializeField] protected GameObject _mainMenuPanel;


    [SerializeField] protected TextMeshProUGUI _numberOfCollectibles;
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

         _numberOfCollectibles.text = $"0";
    }

    public void ActivateOrDeactivateGamePanel(bool p_activationBool)
    {
        switch (p_activationBool)
        {
            case true:
                _gamePanel?.SetActive(true);
                break;
            case false:
                _gamePanel?.SetActive(false);
                break;
        }
    }

    public void ActivateOrDeactivatePausePanel(bool p_activationBool)
    {
        switch (p_activationBool)
        {
            case true:
                _gamePanel?.SetActive(true);
                break;
            case false:
                _gamePanel?.SetActive(false);
                break;
        }
    }

    public void ChangeToGameScene()
    {
        SceneChanger.instance.ChangeSceneTo(1);
    }

    public void ActivateMainMenuCanvas()
    {
        _creditsPanel?.SetActive(false);
        _mainMenuPanel?.SetActive(true);
    }

    public void ActivateCreditsPanel()
    {
        _creditsPanel?.SetActive(true);
        _mainMenuPanel?.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateCollectibles(int p_numberOfCollectibles)
    {
        _numberOfCollectibles.text = p_numberOfCollectibles.ToString();
    }
}
