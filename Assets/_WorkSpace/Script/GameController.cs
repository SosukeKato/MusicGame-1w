using UnityEngine;

public class GameController : MonoBehaviour
{
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
