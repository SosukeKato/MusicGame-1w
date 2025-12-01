using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    static SceneController instance { get; set; }

    AudioController _ac;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
        _ac = FindAnyObjectByType<AudioController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "02_Play")
        {
            
        }
    }

    public void SceneChange(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
