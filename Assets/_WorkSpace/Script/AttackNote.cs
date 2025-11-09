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

    float _noteSize = 0;
    void Awake()
    {
        _gc = FindAnyObjectByType<GameController>();
        while (_noteSize <= _noteMaxSize)
        {
            _noteSize += _noteSpeed * Time.deltaTime;
        }
    }
}
