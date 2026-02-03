using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpawnItems : MonoBehaviour
{
    public GameObject ButtonPrefab;
    public List<LevelObject> levelObjects;

    public void Spawn()
    {
        List<LevelObject> shuffleObjects = levelObjects.OrderBy(x => Random.Range(-1f,1f)).ToList();
        int playercount = FindObjectsByType<PlayerInput>(FindObjectsSortMode.None).Length;

        List<LevelObject> selectedObjects = shuffleObjects.Take(playercount).ToList();

        foreach (LevelObject obj in selectedObjects)
        {
            Button button = Instantiate(ButtonPrefab, transform).GetComponent<Button>();
            button.image.sprite = obj.icon;
            button.onClick.AddListener(() => SpawmGameObject(obj.prefab,button.gameObject));
        }
    }

    public void SpawmGameObject(GameObject obj,GameObject button)
    {
        Instantiate(obj, Vector3.zero, Quaternion.identity);
        Destroy(button);
    }

    public void Awake()
    {
        Spawn();
    }
    private void Update()
    {
        if (GetComponentsInChildren<Button>().Length==0)
        {
            Destroy(gameObject);
        }
    }

}
