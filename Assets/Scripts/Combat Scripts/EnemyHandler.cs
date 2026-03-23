using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public static bool guardsman1;
    public GameObject Guardsman1;

    public static bool ragingtiger1;
    public GameObject RagingTiger1;

    public static bool divinegeneral1;
    public GameObject DivineGeneral1;

    public bool isLevel25;
    public CombatSystem combatSystem;


    public void Update()
    {
        if (guardsman1)
        {
            Guardsman1.SetActive(false);
        }
        if (ragingtiger1)
        {
            RagingTiger1.SetActive(false);
        }
        if (divinegeneral1)
        {
            DivineGeneral1.SetActive(false);
        }

        if (isLevel25)
        {
            combatSystem.isLevel25 = true;
        }
    }
}
