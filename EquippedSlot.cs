using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using JetBrains.Annotations;

public class EquippedSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image SlotImage;

    [SerializeField]
    private TMP_Text SlotName;

    [SerializeField]
    ItemType ItemType = new ItemType();

    //设置各部位装备展示图像
    [SerializeField]
    private Image DisplaySpriteBody;
    [SerializeField]
    private Image DisplaySpriteHead;
    [SerializeField]
    private Image DisplaySpriteMainHand;

    private string ItemName;
    private Sprite ItemSprite;
    private string ItemDescription;

    private bool SlotIsFull;

    //调用inventoryManager
    private InventoryManager inventoryManager;

    //设置选择外框及其开启状态的判断信号
    public GameObject SelectedPanel;
    public bool PanelIsSelected;

    //声明空图片
    public Sprite EmptySprite;

    public EquipSOLibrary CEquipSOLibrary;

    //从人物身上卸下装备贴图
    [SerializeField]
    private SpriteRenderer BodyAromorInRealDisplay;
    [SerializeField]
    private SpriteRenderer MainHandInRealDisplay;
    [SerializeField]
    private SpriteRenderer HeadInRealDisplay;

    private void Start()
    {
        inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();
        CEquipSOLibrary = GameObject.Find("Canvas").GetComponent<EquipSOLibrary>();
    }

    public void EquipGear(Sprite ItemSprite, string ItemName, string ItemDescription)
    {
        if(SlotIsFull)
        {
            UnequipGear();
        }

        //更新图片
        this.ItemSprite = ItemSprite;
        this.SlotImage.sprite = ItemSprite;
        SlotName.enabled = false;

        //更新描述
        this.ItemName = ItemName;
        this.ItemDescription = ItemDescription;

        //更新身上显示的装备贴图
        if (ItemType == ItemType.Body)
        {
            this.DisplaySpriteBody.sprite = ItemSprite;
            //Debug.Log("1");
            BodyAromorInRealDisplay.sprite = ItemSprite;
        }else if (ItemType == ItemType.Head)
        {
            this.DisplaySpriteHead.sprite = ItemSprite;
            //Debug.Log("2");
            HeadInRealDisplay.sprite = ItemSprite;
        }
        else if (ItemType == ItemType.MainHand)
        {
            this.DisplaySpriteMainHand.sprite = ItemSprite;
            //Debug.Log("3");
            MainHandInRealDisplay.sprite = ItemSprite;
        }
        else
        {
            Debug.Log("return nothing");
        }

        for (int i = 0; i < CEquipSOLibrary.EquipmentSOs.Length; i++)
        {
            if (CEquipSOLibrary.EquipmentSOs[i].ItemName == ItemName)
            {
                CEquipSOLibrary.EquipmentSOs[i].EquipGearStats();
            }
        }


        SlotIsFull = true;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    void OnLeftClick()
    {
        if (SlotIsFull)
        {
            if (PanelIsSelected && SlotIsFull)
            {
                //Debug.Log(1);
                UnequipGear();
            }
            else
            {
                inventoryManager.DeSelectPanel();
                SelectedPanel.SetActive(true);
                PanelIsSelected = true;

                for (int i = 0; i < CEquipSOLibrary.EquipmentSOs.Length; i++)
                {
                    if (CEquipSOLibrary.EquipmentSOs[i].ItemName == this.ItemName)
                    {
                        CEquipSOLibrary.EquipmentSOs[i].PreviewEquipment();
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

    private void OnRightClick()
    {
        UnequipGear();
    }

    private void UnequipGear()
    {
        if (SlotIsFull && ItemType != ItemType.Consumable && ItemType != ItemType.Crafting && ItemType != ItemType.Collectible && ItemType != ItemType.None && ItemSprite != EmptySprite)
        {
            inventoryManager.AddItem(ItemName, 1, ItemSprite, ItemDescription, ItemType);
        }

        for (int i = 0; i < CEquipSOLibrary.EquipmentSOs.Length; i++)
        {
            if (CEquipSOLibrary.EquipmentSOs[i].ItemName == ItemName)
            {
                CEquipSOLibrary.EquipmentSOs[i].UnequipGearStats();
                //Debug.Log(1);
            }
        }

        this.ItemSprite = EmptySprite;
        this.SlotImage.sprite = EmptySprite;
        this.ItemName = "";
        this.ItemDescription = "";
        SlotName.enabled = true;
        SelectedPanel.SetActive(false);
        PanelIsSelected = false;
        SlotIsFull = false;

        if (ItemType == ItemType.Body)
        {
            this.DisplaySpriteBody.sprite = EmptySprite;
            //Debug.Log("1");
            BodyAromorInRealDisplay.sprite = EmptySprite;
        }
        else if (ItemType == ItemType.Head)
        {
            this.DisplaySpriteHead.sprite = EmptySprite;
            //Debug.Log("2");
            HeadInRealDisplay.sprite = EmptySprite;
        }
        else if (ItemType == ItemType.MainHand)
        {
            this.DisplaySpriteMainHand.sprite = EmptySprite;
            //Debug.Log("3");
            MainHandInRealDisplay.sprite = EmptySprite;
        }
        else
        {
            Debug.Log("return nothing");
        }

        GameObject.Find("StatManager").GetComponent<PlayerStat>().UnPreviewStats();


    }
}
