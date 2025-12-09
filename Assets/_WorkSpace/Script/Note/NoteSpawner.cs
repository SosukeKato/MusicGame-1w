using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    NotePool _np;

    [SerializeField]
    Transform _spawnPoint;
    [SerializeField]
    public int _notePoolIndex;

    void Start()
    {
        _np = FindAnyObjectByType<NotePool>();
        SpawnNote(_notePoolIndex);
    }

    /// <summary>
    /// ÉmÅ[ÉcÇê∂ê¨(Active)Ç∑ÇÈèàóù
    /// </summary>
    public void SpawnNote(int index)
    {
        GameObject note = _np.GetNote(index);

        note.transform.position = _spawnPoint.position;
        note.transform.rotation = Quaternion.identity;
    }
}
