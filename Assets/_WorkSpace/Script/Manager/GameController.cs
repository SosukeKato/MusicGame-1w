using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static GameController instance { get; set; }

    AvoidNote _avn;
    AttackNote _atn;

    [SerializeField]
    public float _playerHP;
    [SerializeField]
    public float _enemyHP;

    [SerializeField]
    float _playerMaxHP = 1000;
    [SerializeField]
    float _enemyMaxHP = 1000;

    [SerializeField]
    TextMeshProUGUI _comboText;
    [SerializeField]
    TextMeshProUGUI _judgeText;
    [SerializeField]
    TextMeshProUGUI _resultText;

    int _combo;
    bool _isPlayerDeath = false; 
    bool _isEnemyDeath = false;

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
