using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Slider healthSlider;
    public Slider spiritSlider;

    public void SetHUD(Unit unit)
    {

        healthSlider.maxValue = unit.maxHP;
        healthSlider.value = unit.HP;

        spiritSlider.maxValue = unit.maxSP;
        spiritSlider.value = unit.SP;
    }


    public void SetHP(int health)
    {
        healthSlider.value = health;
    }
    public void SetSP(int spirit)
    {
        spiritSlider.value = spirit;
    }
}
