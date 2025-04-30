using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public static UIManager instance;

    [SerializeField] protected GameObject _gamePanel;
    [SerializeField] protected GameObject _pausePanel;
    private void Awake()
    {
        if (instance == null) instance = this;
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
}
