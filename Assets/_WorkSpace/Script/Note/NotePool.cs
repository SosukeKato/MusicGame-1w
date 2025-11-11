using System.Collections.Generic;
using UnityEngine;

public class NotePool : MonoBehaviour
{
    GameController _gc;

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

    GameObject GetNote()
    {
        GameObject note;

        if (_notePool.Count > 0)
        {
            note = _notePool.Dequeue();
        }
        else
        {
            note = Instantiate(_notePrefab);
            note.transform.SetParent(_parent);
            Debug.LogWarning("ë´ÇËÇ»Ç©Ç¡ÇΩÇ©ÇÁçÏÇ¡ÇΩÇ‚Ç≈");
        }

        note.SetActive(true);

        _gc.AddNote(note);
        
        return note;
    }

    void ReturnNote(GameObject note)
    {
        if (note == null)
        {
            return;
        }

        note.SetActive(false);
        note.transform.SetParent(_parent);
        _notePool.Enqueue(note);

        _gc.RemoveNote(note);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
