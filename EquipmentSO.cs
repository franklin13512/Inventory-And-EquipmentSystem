using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquipmentSO : ScriptableObject
{
    public string ItemName;
    public int Attack, Defense, Agility, Intelligence;

    [SerializeField]
    private Sprite ItemSprite;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PreviewEquipment()
    {
        GameObject.Find("StatManager").GetComponent<PlayerStat>().PreviewStats(Attack, Defense, Agility, Intelligence, ItemSprite);
    }

    public void EquipGearStats()
    {
        PlayerStat PStat = GameObject.Find("StatManager").GetComponent<PlayerStat>();

        PStat.Attack += Attack;
        PStat.Defense += Defense;
        PStat.Agility += Agility;
        PStat.Intelligence += Intelligence;

        PStat.UpdateStats();
    }

    public void UnequipGearStats() 
    {
        PlayerStat PStat = GameObject.Find("StatManager").GetComponent<PlayerStat>();

        PStat.Attack -= Attack;
        PStat.Defense -= Defense;
        PStat.Agility -= Agility;
        PStat.Intelligence -= Intelligence;

        PStat.UpdateStats();
    }
}
