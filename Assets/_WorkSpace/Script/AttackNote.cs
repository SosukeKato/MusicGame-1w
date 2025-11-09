using UnityEngine;

public class AttackNote : MonoBehaviour
{
    GameController _gc;

    [SerializeField]
    float _playerAD = 200;
    [SerializeField]
    float _noteSpeed;
    [SerializeField]
    float _noteMaxSize = 3;

    float _attackNoteSize = 0;

    void Awake()
    {
        _gc = FindAnyObjectByType<GameController>();
        while (_attackNoteSize <= _noteMaxSize)
        {
            _attackNoteSize += _noteSpeed * Time.deltaTime;
        }
    }

    void PlayerAttack()
    {
        _gc._enemyHP -= _playerAD;
    }
}
