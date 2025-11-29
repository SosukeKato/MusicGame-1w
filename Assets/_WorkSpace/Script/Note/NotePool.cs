using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PoolData
{
    public GameObject prefab;
    public int _poolSize;
}

public class NotePool : MonoBehaviour
{
    GameController _gc;

    [SerializeField]
    public PoolData[] _pdArray;
    [SerializeField]
    Transform _parent;

    Queue<GameObject>[] _notePoolArray;

    void Awake()
    {
        _gc = FindAnyObjectByType<GameController>();
        InitializePool();
    }

    /// <summary>
    /// poolを初期化する処理
    /// </summary>
    void InitializePool()
    {
        _notePoolArray = new Queue<GameObject>[_pdArray.Length];

        for (int i = 0; i < _pdArray.Length; i++)
        {
            _notePoolArray[i] = new Queue<GameObject>();

            for (int j = 0; j < _pdArray[i]._poolSize; j++)
            {
                GameObject note = Instantiate(_pdArray[i].prefab);
                note.SetActive(false);
                note.transform.SetParent(_parent);
                note.name = $"{_pdArray[i].prefab.name}_{j}";
                _notePoolArray[i].Enqueue(note);
            }
        }
    }
    /// <summary>
    /// poolからノーツを取得
    /// </summary>
    /// <returns></returns>
    public GameObject GetNote(int index)
    {
        if (index < 0 || index >= _notePoolArray.Length)
        {
            Debug.LogError($"インデックス'{index}'が範囲外までいっちゃったやで");
            return null;
        }

        GameObject note;

        if (_notePoolArray[index].Count > 0)
        {
            note = _notePoolArray[index].Dequeue();
        }
        else
        {
            note = Instantiate(_pdArray[index].prefab);
            note.transform.SetParent(_parent);
            Debug.LogWarning("足りなかったから作ったやで");
        }

        note.SetActive(true);

        _gc.AddNote(note);
        
        return note;
    }

    /// <summary>
    /// poolにノーツを返却する処理
    /// </summary>
    /// <param name="note"></param>
    void ReturnNote(int index, GameObject note)
    {
        if (note == null)
        {
            return;
        }

        if (index < 0 || index >= _notePoolArray.Length)
        {
            Debug.LogError($"インデックス'{index}'が範囲外までいっちゃったやで");
            return;
        }

        note.SetActive(false);
        note.transform.SetParent(_parent);
        _notePoolArray[index].Enqueue(note);

        _gc.RemoveNote(note);
    }
}
