using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    //装备栏
    [SerializeField]
    private EquippedSlot HeadSlot, ShirtSlot, BodySlot, LegsSlot, MainHandSlot, OffHandSlot, RelicSlot, FeetSlot;


    public string ItemName;
    public int ItemQuantity;
    public Sprite ItemSprite;
    public bool SlotIsFull;
    public ItemType ItemType;
    [SerializeField]
    [TextArea]
    private string ItemDescription;

    //[SerializeField]
    //private TMP_Text ItemText;
    //[SerializeField]
    //private TMP_Text ItemNameText;

    [SerializeField]
    private Image ItemImage;

    public GameObject SelectedPanel;
    public bool PanelIsSelected;

    private InventoryManager inventoryManager;

    [SerializeField]
    private Sprite EmptyImage;

    [SerializeField]
    private EquipSOLibrary EquipmentLibrary;

    ////在人物身上显示装备（非装备界面）
    //[SerializeField]
    //private SpriteRenderer BodyAromorInRealDisplay;
    //[SerializeField]
    //private SpriteRenderer MainHandInRealDisplay;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();
        EquipmentLibrary = GameObject.Find("Canvas").GetComponent<EquipSOLibrary>();
        PanelIsSelected = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int AddItem(string ItemName, int ItemQuantity, Sprite ItemSprite, string ItemDescription, ItemType ItemType)
    {
        //判断该槽位是否已满
        if (SlotIsFull == true)
        {
            return ItemQuantity;
        }

        this.ItemName = ItemName;
        this.ItemSprite = ItemSprite;
        this.ItemDescription = ItemDescription;
        this.ItemType = ItemType;

        ItemImage.sprite = ItemSprite;

        this.ItemQuantity = 1;
        SlotIsFull = true;
        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GetLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            GetRightClick();
        }
    }

    private void GetLeftClick()
    {
        if(SlotIsFull)
        {
            if (PanelIsSelected)
            {
                EquipGear();
            }
            else
            {

                inventoryManager.DeSelectPanel();
                SelectedPanel.SetActive(true);
                PanelIsSelected = true;

                //ItemNameText.text = ItemName;
                for (int i = 0; i < EquipmentLibrary.EquipmentSOs.Length; i++)
                {
                    if (EquipmentLibrary.EquipmentSOs[i].ItemName == this.ItemName)
                    {
                        EquipmentLibrary.EquipmentSOs[i].PreviewEquipment();
                    }
                }
            }
        }
        else
        {
            inventoryManager.DeSelectPanel();
            SelectedPanel.SetActive(true);
            PanelIsSelected = true;

            GameObject.Find("StatManager").GetComponent<PlayerStat>().UnPreviewStats();
        }

    }

    private void EquipGear()
    {
        if(ItemType == ItemType.Head)
        {
            HeadSlot.EquipGear(ItemSprite, ItemName, ItemDescription);
        }
        if (ItemType == ItemType.Shirt)
        {
            ShirtSlot.EquipGear(ItemSprite, ItemName, ItemDescription);
        }
        if (ItemType == ItemType.Body)
        {
            BodySlot.EquipGear(ItemSprite, ItemName, ItemDescription);
            //BodyAromorInRealDisplay.sprite = ItemSprite;
        }
        if (ItemType == ItemType.Legs)
        {
            LegsSlot.EquipGear(ItemSprite, ItemName, ItemDescription);
        }
        if (ItemType == ItemType.MainHand)
        {
            MainHandSlot.EquipGear(ItemSprite, ItemName, ItemDescription);
            //MainHandInRealDisplay.sprite = ItemSprite;
        }
        if (ItemType == ItemType.OffHand)
        {
            OffHandSlot.EquipGear(ItemSprite, ItemName, ItemDescription);
        }
        if (ItemType == ItemType.Relic)
        {
            RelicSlot.EquipGear(ItemSprite, ItemName, ItemDescription);
        }
        if (ItemType == ItemType.Feet)
        {
            FeetSlot.EquipGear(ItemSprite, ItemName, ItemDescription);
        }

        ItemQuantity -= 1;
        SlotIsFull = false;
        if (ItemQuantity <= 0)
        {
            Empty();
        }

    }

    public void Empty()
    {
        //Debug.Log(2);
        ItemQuantity = 0;
        //ItemText.text = "";
        //ItemText.enabled = false;
        ItemName = "";
        ItemType = ItemType.None;
        ItemSprite = EmptyImage;
        ItemDescription = "";
        ItemImage.sprite = EmptyImage;

        //ItemNameText.text = "";

        GameObject.Find("StatManager").GetComponent<PlayerStat>().UnPreviewStats();
    }

    private void GetRightClick()
    {
        inventoryManager.DropStuff(this.ItemName);

        ItemQuantity -= 1;
        //ItemText.text = ItemQuantity.ToString();
        SlotIsFull = false;
        if (ItemQuantity <= 0)
        {
            Empty();
        }
    }
}
