using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpawnItems : MonoBehaviour
{
    public GameObject ButtonPrefab;
    public List<LevelObject> levelObjects;

    public void Spawn()
    {
        foreach (var item in FindObjectsByType<Cursor>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            item.gameObject.SetActive(true);
        }

        List<LevelObject> shuffleObjects = levelObjects.OrderBy(x => Random.Range(-1f,1f)).ToList();
        int playercount = FindObjectsByType<PlayerInput>(FindObjectsSortMode.None).Length;

        List<LevelObject> selectedObjects = shuffleObjects.Take(playercount).ToList();

        foreach (LevelObject obj in selectedObjects)
        {
            var button = Instantiate(ButtonPrefab, transform).GetComponent<EventTrigger>();

            button.GetComponent<Image>().sprite = obj.icon;
            button.triggers.First(item => item.eventID == EventTriggerType.PointerClick).callback.AddListener(data => SpawmGameObject(data, obj.prefab,button.gameObject));
        }

        gameObject.SetActive(true);
    }

    public void SpawmGameObject(BaseEventData data, GameObject obj, GameObject button)
    {
        int playerIndex = (data as PointerEventData).pointerId;

        var newObject = Instantiate(obj, Vector3.zero, Quaternion.identity);
        newObject.GetComponent<PlaceAbleObject>().playerIndex = playerIndex;

        Destroy(button);
    }

    
    private void Update()
    {
        if (GetComponentsInChildren<Button>().Length==0)
        {
         //   gameObject.SetActive(false);
        }
    }

}
