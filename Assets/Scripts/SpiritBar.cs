using UnityEngine;
using UnityEngine.UI;


public class SpiritBar : MonoBehaviour
{
  public Slider slider; 
  public void SetMaxSpirit(int spirit)
   {
       slider.maxValue = spirit;
       slider.value = spirit;
    }

    public void SetSpirit(int spirit)
   {
       slider.value = spirit;
    }
}
