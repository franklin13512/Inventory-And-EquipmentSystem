using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public int Health;
    public TMP_Text HealthText;

    public int Attack, Defense, Agility, Intelligence;
    public TMP_Text AttackText, DefenseText, AgilityText, IntelligenceText;

    public TMP_Text AttackPreText, DefensePreText, AgilityPreText, IntelligencePreText;
    public Image ItemPreImage;

    public HealthManager HManager;

    [SerializeField]
    private GameObject PreviewImagePanel;
    [SerializeField]
    private GameObject PreviewStatsPanel;

    // Start is called before the first frame update
    void Start()
    {
        HManager = GameObject.Find("Canvas").GetComponent<HealthManager>();
        UpdateHealth(HManager.Health);
        UpdateStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStats()
    {
        AttackText.text = Attack.ToString();
        DefenseText.text = Defense.ToString();
        AgilityText.text = Agility.ToString();
        IntelligenceText.text = Intelligence.ToString();
    }

    public void PreviewStats(int Attack, int Defense, int Agility, int Intelligence, Sprite ItemImage)
    {
        AttackPreText.text = Attack.ToString();
        DefensePreText.text = Defense.ToString();
        AgilityPreText.text = Agility.ToString();
        IntelligencePreText.text = Intelligence.ToString();

        ItemPreImage.sprite = ItemImage;

        PreviewImagePanel.SetActive(true);
        PreviewStatsPanel.SetActive(true);
    }

    public void UnPreviewStats()
    {
        PreviewImagePanel.SetActive(false);
        PreviewStatsPanel.SetActive(false);
    }
    

    

    public void UpdateHealth(int Health)
    {
        HealthText.text = Health.ToString() +"/" + HManager.MaxHealth.ToString();
    }
}
