using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Cursors;
    public UnityEvent DonePlacing;
    public UnityEvent placing;

    void Update()
    {
        var numCursors = FindObjectsByType<Cursor>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        var cursorsEnabled = numCursors.Count(cursor => cursor.isActiveAndEnabled);

        if (numCursors.Length > 0 && cursorsEnabled == 0)
        {
            DonePlacing.Invoke();
        }
        else
        {
            placing.Invoke();
        }
    }
}
