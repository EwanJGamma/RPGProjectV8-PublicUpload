using UnityEngine;

public class EnemyDestroyed : MonoBehaviour
{
   
    public bool isDefeated;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (isDefeated == true)
        {


            Debug.Log("Destroyed");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
   
}
