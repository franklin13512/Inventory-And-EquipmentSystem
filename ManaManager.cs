using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaManager : MonoBehaviour
{
    public int Mana = 1;
    public int MaxMana = 50;
    public TMP_Text CurrentManaText;
    public TMP_Text MaxManaText;
    // Start is called before the first frame update
    void Start()
    {
        MaxManaText.text = "/" + MaxMana.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentManaText.text = Mana.ToString();
    }

    public void RecoverMana(int ManaToRecover)
    {
        Mana += ManaToRecover;
        if (Mana >= MaxMana)
        {
            Mana = MaxMana;

        }
        CurrentManaText.text = Mana.ToString();
    }
}
