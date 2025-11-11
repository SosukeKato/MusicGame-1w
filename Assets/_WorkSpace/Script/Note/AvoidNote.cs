using UnityEngine;

public class AvoidNote : MonoBehaviour
{
    GameController _gc;
    Transform _tr;

    [SerializeField]
    float _enemyAD = 120;
    [SerializeField]
    float _noteSpeed;

    Vector3 _moveDirection;

    void Awake()
    {
        _gc = FindAnyObjectByType<GameController>();
        _tr = GetComponent<Transform>();
        _moveDirection = new Vector3(0, 0, _noteSpeed * Time.deltaTime * -1);
    }

    public void NoteMove()
    {
        _tr.position += _moveDirection;
    }

    void EnemyAttack()
    {
        _gc._playerHP -= _enemyAD;
    }
}
