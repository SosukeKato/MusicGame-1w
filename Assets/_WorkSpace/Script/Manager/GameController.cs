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
    AudioController _ac;

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
    [SerializeField]
    float _spawnAtn;
    [SerializeField]
    float _spawnAvnL;
    [SerializeField]
    float _spawnAvnR;

    int _combo;
    int _score;
    int _sampleStep;
    float[] _data;
    bool _isPlayerDeath = false;
    bool _isEnemyDeath = false;
    AudioSource _gameMusicSource;
    AudioClip _gameMusic;
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
        _ac = FindAnyObjectByType<AudioController>();
        if (scene.name == "02_Play")
        {
            _gameMusic = _ac._inGameBGM;
            _gameMusicSource = _ac._as;
            //オーディオクリップの全サンプルデータを変数に保存
            //clip.channels* clip.samples

            //clip.channels：チャンネル数（モノラル = 1、ステレオ = 2）
            //clip.samples：1チャンネルあたりのサンプル数
            //掛け算することで全オーディオデータの総サンプル数を計算

            //例えば、ステレオ（2チャンネル）で44100Hz、1秒のオーディオなら 2 * 44100 = 88200 要素の配列が作成されます。
            _data = new float[_gameMusic.channels * _gameMusic.samples];
            //オーディオクリップから実際のサンプルデータを配列に格納
            //これにより後でtimeSamplesの位置から必要な区間のデータを取り出せる
            _gameMusic.GetData(_data, 0);
            _np = FindAnyObjectByType<NotePool>();
            _ns = FindAnyObjectByType<NoteSpawner>();
            _comboTextObj = GameObject.FindGameObjectWithTag("ComboText");
            _judgeTextObj = GameObject.FindGameObjectWithTag("JudgeText");
            _resultTextObj = GameObject.FindGameObjectWithTag("ResultText");
            _comboText = _comboTextObj.GetComponent<TextMeshProUGUI>();
            _judgeText = _judgeTextObj.GetComponent<TextMeshProUGUI>();
            _resultText = _resultTextObj.GetComponent<TextMeshProUGUI>();

            Prepare(_gameMusicSource, _data);
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

    #region
    /// <summary>
    /// 波形を取得して処理をするために必要な数値を保存する処理
    /// </summary>
    /// <param name="source"></param>
    /// <param name="monoData"></param>
    public void Prepare(AudioSource source, float[] monoData)
    {
        this._gameMusicSource = source;
        this._data = monoData;

        float fps = Mathf.Max(60 , 1 / Time.deltaTime);
        //1フレームで読み込むサンプル数を計算
        this._sampleStep = (int)(_gameMusic.frequency / fps);
    }

    /// <summary>
    /// startからendの音量の差分を取得するメソッド
    /// </summary>
    /// <param name="data"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    float DetectVolumeLevel(float[] data, int start, int end)
    {
        float max = 0f;
        float min = 0f;

        for (int i = start; i < end; i++)
        {
            if (max < data[i])
            {
                max = data[i];
            }
            if (min > data[i])
            {
                min = data[i];
            }
        }

        return max - min;
    }
    #endregion

    void Update()
    {
        #region ノーツの生成処理
        if (_gameMusicSource.isPlaying && _gameMusicSource.timeSamples < _data.Length)
        {
            int startIndex = _gameMusicSource.timeSamples;
            int endIndex = Mathf.Min(_gameMusicSource.timeSamples + _sampleStep, _data.Length);

            float volume = DetectVolumeLevel(_data, startIndex, endIndex);

            int index = -1;
            if (volume > _spawnAtn)
            {
                index = 0;
            }
            else if (volume > _spawnAvnL)
            {
                index = 1;
            }
            else if (volume > _spawnAvnR)
            {
                index = 2;
            }

            _ns.SpawnNote(index);
        }
        #endregion

        #region 判定処理
        if (SceneManager.GetActiveScene().name == "02_Play")
        {
            if (_activeNote[0] == _np._pdArray[0].prefab)
            {
                GetCrosestNote();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _atn = _activeNote[0].GetComponent<AttackNote>();
                    if ((_atn._notePositionZ >= _latePerfect) && (_atn._notePositionZ >= _fastPerfect))
                    {
                        _combo += 1;
                        _score += 1900;
                        _atn.PlayerAttack();
                    }
                    else if ((_atn._notePositionZ >= _lateGreat) && (_atn._notePositionZ <= _fastGreat))
                    {
                        _combo += 1;
                        _score += 1640;
                        _atn.PlayerAttack();
                    }
                    else if ((_atn._notePositionZ >= _lateGood) && (_atn._notePositionZ <= _fastGood))
                    {
                        _combo = 0;
                        _score += 910;
                        _atn.PlayerAttack();
                    }
                    else if ((_atn._notePositionZ >= _lateBad) && (_atn._notePositionZ <= _fastBad))
                    {
                        _combo = 0;
                        _score += 470;
                        _atn.PlayerAttack();
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
                    GetCrosestNote();
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
                    GetCrosestNote();
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
        #endregion
    }
}
