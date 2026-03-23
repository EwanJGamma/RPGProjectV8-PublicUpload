using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public int sceneBuildIndex;

    //Sets the Enemies to be public so we can find them and tell the combat system which ones it needs to spawn in
    public GameObject Guardsman;
    public GameObject RagingTiger;
    public GameObject DivineGeneral;
    public GameObject CombatSystem;

    //Sets the enemy name
    public static string enemyName;

    //Gives the enemy a unique ID to call
    public static string enemyUniqueName;
    public string enemyUniqueNo;
    
    //Screen Change when Enemy collides with Player
    //Takes you to combat scene

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {  
            print("Player has entered combat");
            enemyName = gameObject.name;
            enemyUniqueName = enemyUniqueNo;
            SceneManager.LoadScene(sceneBuildIndex);
        }

    }



}
 