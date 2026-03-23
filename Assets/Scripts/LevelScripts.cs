using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScripts : MonoBehaviour

{
    public GameObject playerCombatPrefab;
    public GameObject Player;
    public CombatSystem combatSystem;
    public EnemyHandler overworldManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            overworldManager.isLevel25 = true;

        }
    }
}
