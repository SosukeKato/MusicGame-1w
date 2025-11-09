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
    float _playerMaxHP;
    [SerializeField]
    float _enemyMaxHP;

    [SerializeField]
    TextMeshPro _comboText;
    [SerializeField]
    TextMeshPro _judgeText;

    int _combo;

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
