using UnityEngine;

public class AvoidNote : MonoBehaviour
{
    GameController _gc;
    Transform _tr;

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
        _tr.position += new Vector3(0, 0, -1 * _noteSpeed * Time.deltaTime);
        _notePositionZ = _tr.position.z;
    }

    public void EnemyAttack()
    {
        _gc._playerHP -= _enemyAD;
    }
}
