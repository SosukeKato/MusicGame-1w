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
        SpawnNote();
    }

    /// <summary>
    /// ÉmÅ[ÉcÇê∂ê¨(Active)Ç∑ÇÈèàóù
    /// </summary>
    public void SpawnNote()
    {
        GameObject note = _np.GetNote(_notePoolIndex);

        note.transform.position = _spawnPoint.position;
        note.transform.rotation = Quaternion.identity;
    }
}
