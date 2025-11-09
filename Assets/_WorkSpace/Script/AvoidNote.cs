using UnityEngine;

public class AvoidNote : MonoBehaviour
{
    GameController _gc;

    [SerializeField]
    float _enemyAD;
    [SerializeField]
    float _noteSpeed;
    [SerializeField]
    float _noteMaxSize = 3;

    float _avoidNoteSize = 0;

    void Awake()
    {
        _gc = FindAnyObjectByType<GameController>();
        while (_avoidNoteSize <= _noteMaxSize)
        {
            _avoidNoteSize += _noteSpeed * Time.deltaTime;
        }
    }

    void EnemyAttack()
    {
        _gc._playerHP -= _enemyAD;
    }
}
