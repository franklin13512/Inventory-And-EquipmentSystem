using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public int Health = 1;
    public int MaxHealth = 20;
    public TMP_Text CurrentHealthText;
    public TMP_Text MaxHealthText;
    // Start is called before the first frame update
    void Start()
    {
        MaxHealthText.text = "/" + MaxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealthText.text = Health.ToString();
    }

    public void RecoverHealth(int HealthToRecover)
    {
        Health += HealthToRecover;
        if (Health >= MaxHealth)
        {
            Health = MaxHealth;
           
        }
        CurrentHealthText.text = Health.ToString();
    }
}
