using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    static AudioController instance { get;set; }

    [SerializeField]
    AudioSource _as;

    [SerializeField]
    AudioClip _titleBGM;
    [SerializeField]
    AudioClip _inGameBGM;

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
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        _as.Stop();
        if (scene.name == "00_Title")
        {
            _as.clip = _titleBGM;
        }
        if (scene.name == "02_Play")
        {
            _as.clip = _inGameBGM;
        }
        _as.Play();
    }
}
