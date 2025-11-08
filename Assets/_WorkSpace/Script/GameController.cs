using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    float _playerHP;
    [SerializeField]
    float _playerMaxHP;
    [SerializeField]
    float _enemyHP;
    [SerializeField]
    float _enemyMaxHP;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void PlayerHealth()
    {
        if (_playerHP > _playerMaxHP)
        {
            _playerHP = _playerMaxHP;
        }
    }

    void EnemyHealth()
    {
        if ( _enemyHP > _enemyMaxHP)
        {
            _enemyHP = _enemyMaxHP;
        }
    }
}
