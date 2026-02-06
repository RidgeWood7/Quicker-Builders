using UnityEngine;

[CreateAssetMenu(fileName = "LevelObject", menuName = "Scriptable Objects/LevelObject")]
public class LevelObject : ScriptableObject
{
    public enum Category
    {
        block,
        hazard,
        collectible,
        destruction,
        special
    }

    public Category category;
    public GameObject prefab;
    public Sprite icon;


}
