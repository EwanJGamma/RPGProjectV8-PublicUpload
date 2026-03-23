using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitThreat;

    public int damage;
    public int recovery;
    public int slashDamage;
    public int slashCost;
    public int mundareDamage;
    public int mundareCost;
    public int guardCost;
    public int ragingClawDamage;
    public int roarSpiritDamage;
    public int maxHP;
    public int maxSP;

    public int HP;
    public int SP;


    public bool TDmg(int damage)
    {
        HP -= damage;

        if (HP <= 0)
            return true;
            else
                return false;
    }

    //Same as previous script for damage, but this works for healing and can be used to simulate the player having higher HP via levelling up
    public bool HDmg(int damage)
    {
        HP += damage;

        if (HP <= 0)
            return true;
        else
            return false;
    }

    public bool SPCost(int cost)
    {
        SP -= cost;

        if (SP <= 0)
            return true;
        else
            return false;
    }  

}
