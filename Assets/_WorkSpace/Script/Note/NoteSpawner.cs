using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    NotePool _np;

    [SerializeField]
    Transform _spawnPoint;
    void Start()
    {
        _np = FindAnyObjectByType<NotePool>();
        SpawnNote();
    }

    /// <summary>
    /// ƒm[ƒc‚ğ¶¬(Active)‚·‚éˆ—
    /// </summary>
    void SpawnNote()
    {
        GameObject note = _np.GetNote();

        note.transform.position = _spawnPoint.position;
        note.transform.rotation = Quaternion.identity;
    }
}
