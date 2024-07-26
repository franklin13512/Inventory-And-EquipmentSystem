using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string ItemName;
    public GameObject ItemPrefab;

    public enum StatToChange
    {
        Nothing,
        Health,
        Stamina,
        Mana
    };

    public enum AttributeToChange
    {
        Nothing,
        Intelligence,
        Agility,
        Socialize
    };

    public StatToChange StatChange = new StatToChange();
    public int AmountOfStatToChange;
    public AttributeToChange AttributeChange = new AttributeToChange();
    public int AmountOfAttributeToChange;

    public bool UseItem()
    {
        if (StatChange == StatToChange.Health)
        {
            HealthManager HealthMNG = GameObject.Find("Canvas").GetComponent<HealthManager>();
            if (HealthMNG.Health >= HealthMNG.MaxHealth)
            {
                return false;
            }
            else
            {
                HealthMNG.RecoverHealth(AmountOfStatToChange);
                //Debug.Log("SO normal");
                return true;
            }
        }
        if (StatChange == StatToChange.Mana)
        {
            ManaManager ManaMNG = GameObject.Find("Canvas").GetComponent<ManaManager>();
            if (ManaMNG.Mana == ManaMNG.MaxMana)
            {
                return false;
            }
            else
            {
                ManaMNG.RecoverMana(AmountOfStatToChange);
                //Debug.Log("SO normal");
                return true;
            }
        }
        return false;
    }
}
