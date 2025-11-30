using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static GameController instance { get; set; }

    AvoidNote _avn;
    AttackNote _atn;
    NotePool _np;
    PoolData _pd;
    NoteSpawner _ns;

    [SerializeField]
    public float _playerHP = 1000;
    [SerializeField]
    public float _enemyHP = 1000;

    [SerializeField]
    float _playerMaxHP = 1000;
    [SerializeField]
    float _enemyMaxHP = 1000;

    [SerializeField]
    float _lateBad = -2;
    [SerializeField]
    float _fastBad = 12;
    [SerializeField]
    float _lateGood = 0;
    [SerializeField]
    float _fastGood = 10;
    [SerializeField]
    float _lateGreat = 2;
    [SerializeField]
    float _fastGreat = 8;
    [SerializeField]
    float _latePerfect = 4;
    [SerializeField]
    float _fastPerfect = 6;

    int _combo;
    int _score;
    bool _isPlayerDeath = false;
    bool _isEnemyDeath = false;
    GameObject _comboTextObj;
    GameObject _judgeTextObj;
    GameObject _resultTextObj;
    TextMeshProUGUI _comboText;
    TextMeshProUGUI _judgeText;
    TextMeshProUGUI _resultText;
    List<GameObject> _activeNote = new();

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
        if (scene.name == "02_Play")
        {
            _np = FindAnyObjectByType<NotePool>();
            _ns = FindAnyObjectByType<NoteSpawner>();
            _comboTextObj = GameObject.FindGameObjectWithTag("ComboText");
            _judgeTextObj = GameObject.FindGameObjectWithTag("JudgeText");
            _resultTextObj = GameObject.FindGameObjectWithTag("ResultText");
            _comboText = _comboTextObj.GetComponent<TextMeshProUGUI>();
            _judgeText = _judgeTextObj.GetComponent<TextMeshProUGUI>();
            _resultText = _resultTextObj.GetComponent<TextMeshProUGUI>();
        }
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
        if (_enemyHP >= _enemyMaxHP)
        {
            _enemyHP = _enemyMaxHP;
        }
        else if (_enemyHP <= 0)
        {
            _isEnemyDeath = true;
        }
    }

    #region ノーツのソート処理
    /// <summary>
    /// ActiveなノーツをListに追加する処理
    /// </summary>
    /// <param name="note"></param>
    public void AddNote(GameObject note)
    {
        _activeNote.Add(note);
        _activeNote.Sort(NoteAscendingOrder);
    }

    /// <summary>
    /// Listからfalseになったノーツを削除する処理
    /// </summary>
    /// <param name="note"></param>
    public void RemoveNote(GameObject note)
    {
        _activeNote.Remove(note);
    }

    /// <summary>
    /// Listの中のノーツを昇順に並べる処理
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    int NoteAscendingOrder(GameObject a, GameObject b)
    {
        return a.transform.position.z.CompareTo(b.transform.position.z);
    }

    /// <summary>
    /// 一番手前にあるノーツを取得する
    /// </summary>
    /// <returns></returns>
    GameObject GetCrosestNote()
    {
        //?はtrueの時左辺falseの時右辺を実行する演算子
        return _activeNote.Count > 0 ? _activeNote[0] : null;
    }
    #endregion
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "02_Play")
        {
            if (_activeNote[0] == _np._pdArray[0].prefab)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _atn = _activeNote[0].GetComponent<AttackNote>();
                    if ((_atn._notePositionZ >= _latePerfect) && (_atn._notePositionZ >= _fastPerfect))
                    {
                        _combo += 1;
                        _score += 1900;
                    }
                    else if ((_atn._notePositionZ >= _lateGreat) && (_atn._notePositionZ <= _fastGreat))
                    {
                        _combo += 1;
                        _score += 1640;
                    }
                    else if ((_atn._notePositionZ >= _lateGood) && (_atn._notePositionZ <= _fastGood))
                    {
                        _combo = 0;
                        _score += 910;
                    }
                    else if ((_atn._notePositionZ >= _lateBad) && (_atn._notePositionZ <= _fastBad))
                    {
                        _combo = 0;
                        _score += 470;
                    }
                    else
                    {
                        _combo = 0;
                        _atn.EnemyAttack();
                        PlayerHealth();
                    }
                    _comboText.text = _combo.ToString();
                }
            }
            if (_activeNote[0] == _np._pdArray[1].prefab)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    _avn = _activeNote[0].GetComponent<AvoidNote>();
                    if ((_avn._notePositionZ >= _latePerfect) && (_avn._notePositionZ >= _fastPerfect))
                    {
                        _combo += 1;
                        _score += 1200;
                    }
                    else if ((_avn._notePositionZ >= _lateGreat) && (_avn._notePositionZ <= _fastGreat))
                    {
                        _combo += 1;
                        _score += 1080;
                    }
                    else if ((_avn._notePositionZ >= _lateGood) && (_avn._notePositionZ <= _fastGood))
                    {
                        _combo = 0;
                        _score += 600;
                    }
                    else if ((_avn._notePositionZ >= _lateBad) && (_avn._notePositionZ <= _fastBad))
                    {
                        _combo = 0;
                        _score += 230;
                    }
                    else
                    {
                        _combo = 0;
                        _avn.EnemyAttack();
                        PlayerHealth();
                    }
                    _comboText.text = _combo.ToString();
                }
            }
            if (_activeNote[0] == _np._pdArray[2].prefab)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    _avn = _activeNote[0].GetComponent<AvoidNote>();
                    if ((_avn._notePositionZ >= _latePerfect) && (_avn._notePositionZ >= _fastPerfect))
                    {
                        _combo += 1;
                        _score += 1200;
                    }
                    else if ((_avn._notePositionZ >= _lateGreat) && (_avn._notePositionZ <= _fastGreat))
                    {
                        _combo += 1;
                        _score += 1080;
                    }
                    else if ((_avn._notePositionZ >= _lateGood) && (_avn._notePositionZ <= _fastGood))
                    {
                        _combo = 0;
                        _score += 600;
                    }
                    else if ((_avn._notePositionZ >= _lateBad) && (_avn._notePositionZ <= _fastBad))
                    {
                        _combo = 0;
                        _score += 230;
                    }
                    else
                    {
                        _combo = 0;
                        _avn.EnemyAttack();
                        PlayerHealth();
                    }
                    _comboText.text = _combo.ToString();
                }
            }
        }
    }
}
