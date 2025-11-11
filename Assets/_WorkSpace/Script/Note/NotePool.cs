using System.Collections.Generic;
using UnityEngine;

public class NotePool : MonoBehaviour
{
    GameController _gc;
    Transform _tr;

    [SerializeField]
    GameObject _notePrefab;
    [SerializeField]
    int _poolSize;

    Transform _parent;

    Queue<GameObject> _notePool = new Queue<GameObject>();

    void Awake()
    {
        _gc = FindAnyObjectByType<GameController>();
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject note = Instantiate(_notePrefab);
            note.SetActive(false);
            note.transform.SetParent(_parent);
            _notePool.Enqueue(note);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
