using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        if(Instance!=null && Instance==this)
        {
            Destroy(gameObject);
            return;
        }    
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadNextSence()
    {
        int _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int _nextSceneIndex = _currentSceneIndex + 1;

        if(_nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(_nextSceneIndex);
        }
    }
    public void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }
}
