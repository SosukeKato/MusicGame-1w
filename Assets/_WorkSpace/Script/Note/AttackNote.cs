using UnityEngine;

public class AttackNote : MonoBehaviour
{
    GameController _gc;
    Transform _tr;

    [SerializeField]
    float _playerAD = 200;
    [SerializeField]
    float _enemyAD = 120;
    [SerializeField]
    float _noteSpeed;
    [SerializeField]
    float _moveDirection;

    public float _notePositionZ;

    void Awake()
    {
        _gc = FindAnyObjectByType<GameController>();
        _tr = GetComponent<Transform>();
        _notePositionZ = transform.position.z;
        _moveDirection = _noteSpeed * Time.deltaTime;
    }

    void Update()
    {
        NoteMove();
    }

    public void NoteMove()
    {
        _notePositionZ += _moveDirection;
    }

    public void PlayerAttack()
    {
        _gc._enemyHP -= _playerAD;
    }

    public void EnemyAttack()
    {
        _gc._playerHP -= _enemyAD;
    }
}
