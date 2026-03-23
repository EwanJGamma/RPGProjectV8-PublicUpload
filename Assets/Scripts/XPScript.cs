using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI xpText;
    [SerializeField] Image LvlSlider;
    [SerializeField] public int Level;
    [SerializeField] public int Experience;
    [SerializeField] int targetXP = 100;
    [SerializeField] int targetXPIncrease = 100;
    int currentLevel;
    int currentXP;

    private void Start()
    {
        currentLevel = 1;
        currentXP = 0;
    }

    public void AddExperience(int amount)
    {
        currentXP += amount;
        CheckLevelUp();
    }

    public void CheckLevelUp()
    {
        while (currentXP >= targetXP)
            currentLevel++;
        currentXP -= targetXP;
        targetXP += targetXPIncrease;
    }
}