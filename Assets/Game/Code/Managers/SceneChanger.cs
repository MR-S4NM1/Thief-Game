using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] public static SceneChanger instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void ChangeSceneTo(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
