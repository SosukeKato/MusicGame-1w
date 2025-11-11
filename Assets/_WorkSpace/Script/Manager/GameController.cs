using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static GameController instance { get; set; }

    AvoidNote _avn;
    AttackNote _atn;

    [SerializeField]
    public float _playerHP = 1000;
    [SerializeField]
    public float _enemyHP = 1000;

    [SerializeField]
    float _playerMaxHP = 1000;
    [SerializeField]
    float _enemyMaxHP = 1000;

    int _combo;
    bool _isPlayerDeath = false; 
    bool _isEnemyDeath = false;
    GameObject _comboTextObj;
    GameObject _judgeTextObj;
    GameObject _resultTextObj;
    TextMeshProUGUI _comboText;
    TextMeshProUGUI _judgeText;
    TextMeshProUGUI _resultText;
    List<GameObject> _activeNote;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _comboTextObj = GameObject.FindGameObjectWithTag("ComboText");
        _judgeTextObj = GameObject.FindGameObjectWithTag("JudgwText");
        _resultTextObj = GameObject.FindGameObjectWithTag("ResultText");
        _comboText = _comboTextObj.GetComponent<TextMeshProUGUI>();
        _judgeText = _judgeTextObj.GetComponent<TextMeshProUGUI>();
        _resultText = _resultTextObj.GetComponent<TextMeshProUGUI>();
    }

    void PlayerHealth()
    {
        if (_playerHP >= _playerMaxHP)
        {
            _playerHP = _playerMaxHP;
        }
        else if (_playerHP <= 0)
        {
            _isPlayerDeath = true;
        }
    }

    void EnemyHealth()
    {
        if ( _enemyHP >= _enemyMaxHP)
        {
            _enemyHP = _enemyMaxHP;
        }
        else if(_enemyHP <= 0)
        {
            _isEnemyDeath = true;
        }
    }

    #region ノーツのソート処理
    void AddNote(GameObject note)
    {
        _activeNote.Add(note);
        _activeNote.Sort(NoteAscendingOrder);
    }

    int NoteAscendingOrder(GameObject a, GameObject b)
    {
        return a.transform.position.z.CompareTo(b.transform.position.z);
    }

    GameObject GetCrosestNote()
    {
        return _activeNote.Count > 0 ? _activeNote[0] : null;
    }
    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {

        }
        if (Input.GetKeyDown(KeyCode.J))
        {

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }
}
