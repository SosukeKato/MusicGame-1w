using System.Collections.Generic;
using UnityEngine;

public class NotePool : MonoBehaviour
{
    GameController _gc;

    [SerializeField]
    GameObject _avoidNoteLPrefab;
    [SerializeField]
    GameObject _avoidNoteRPrefab;
    [SerializeField]
    GameObject _attackNotePrefab;
    [SerializeField]
    int _poolSize;
    [SerializeField]
    Transform _parent;

    Queue<GameObject> _notePool = new Queue<GameObject>();

    void Awake()
    {
        _gc = FindAnyObjectByType<GameController>();
        InitializePool();
    }

    /// <summary>
    /// pool‚ğ‰Šú‰»‚·‚éˆ—
    /// </summary>
    void InitializePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject note = Instantiate(_avoidNoteLPrefab);
            note.SetActive(false);
            note.transform.SetParent(_parent);
            _notePool.Enqueue(note);
        }
    }
    /// <summary>
    /// pool‚©‚çƒm[ƒc‚ğæ“¾
    /// </summary>
    /// <returns></returns>
    public GameObject GetNote()
    {
        GameObject note;

        if (_notePool.Count > 0)
        {
            note = _notePool.Dequeue();
        }
        else
        {
            note = Instantiate(_avoidNoteLPrefab);
            note.transform.SetParent(_parent);
            Debug.LogWarning("‘«‚è‚È‚©‚Á‚½‚©‚çì‚Á‚½‚â‚Å");
        }

        note.SetActive(true);

        _gc.AddNote(note);
        
        return note;
    }

    /// <summary>
    /// pool‚Éƒm[ƒc‚ğ•Ô‹p‚·‚éˆ—
    /// </summary>
    /// <param name="note"></param>
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
}
