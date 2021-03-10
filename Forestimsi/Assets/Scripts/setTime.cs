using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setTime : MonoBehaviour
{
    public Transform LoadingBar;
    public Transform testIndicator;
    [SerializeField] public static float currentAmount;

   /*zamanın ayarlanması*/
    public void AdjustSpeed(float newValue)
    {
        currentAmount = newValue;
        if (currentAmount < 0)
        {
            currentAmount = 0;
        }
        if (currentAmount < 7200)
        {
            testIndicator.GetComponent<Text>().text = ((int)(currentAmount/60)).ToString() + " minut";

        }

        LoadingBar.GetComponent<Image>().fillAmount = currentAmount /7200;
    }
}
