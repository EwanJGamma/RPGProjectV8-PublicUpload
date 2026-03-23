using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatus", menuName = "Scriptable Objects/CharacterStatus")]
public class CharacterStatus : ScriptableObject
{
    public string charName = "name";
    public float[] position = new float[2];
    public GameObject characterGameObject;
    public int level = 1;
    public float maxHealth = 100;
    public float maxSpirit = 100;
    public float health = 100;
    public float spirit = 100;

}
