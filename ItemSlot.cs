using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string ItemName;
    public int ItemQuantity;
    public Sprite ItemSprite;
    public bool SlotIsFull;
    public ItemType ItemType;
    [SerializeField]
    [TextArea]
    private string ItemDescription;

    [SerializeField]
    private TMP_Text ItemText;
    [SerializeField]
    private TMP_Text ItemDescriptionText;
    [SerializeField]
    private TMP_Text ItemNameText;
    public Image ItemDescriptionImage;

    [SerializeField]
    private Image ItemImage;

    public GameObject SelectedPanel;
    public bool PanelIsSelected;

    private InventoryManager inventoryManager;

    [SerializeField]
    private Sprite EmptyImage;

    public int MaxOfItems;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("Canvas").GetComponent<InventoryManager>();
        PanelIsSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int AddItem(string ItemName, int ItemQuantity, Sprite ItemSprite, string ItemDescription, ItemType ItemType)
    {
        //�жϸò�λ�Ƿ�����
        if (SlotIsFull == true)
        {
            return ItemQuantity;
        }

        this.ItemName = ItemName;
        this.ItemSprite = ItemSprite;
        this.ItemDescription = ItemDescription;
        this.ItemType = ItemType;

        ItemImage.sprite = ItemSprite;

        this.ItemQuantity += ItemQuantity;
        //���ò�λ��Ʒ�����ﵽ����
        if (this.ItemQuantity >= MaxOfItems)
        {
            this.ItemQuantity = MaxOfItems;
            ItemText.text = MaxOfItems.ToString();
            ItemText.enabled = true;
            SlotIsFull = true;
            //�������ֵʱ��������ǲ���
            int ExtraItems = this.ItemQuantity - MaxOfItems;
            return ExtraItems;
        }

        ItemText.text = this.ItemQuantity.ToString();
        ItemText.enabled = true;
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
        if (PanelIsSelected)
        {
            bool IAmNotFull = inventoryManager.UseItems(ItemName);
            //Debug.Log("ItemSlot Normal");
            if (IAmNotFull)
            {
                ItemQuantity -= 1;
                ItemText.text = ItemQuantity.ToString();
                SlotIsFull = false;
                if (ItemQuantity <= 0)
                {
                    Empty();
                }
            }
        }
        else
        {

            inventoryManager.DeSelectPanel();
            SelectedPanel.SetActive(true);
            PanelIsSelected = true;

            ItemDescriptionText.text = ItemDescription;
            ItemNameText.text = ItemName;
            ItemDescriptionImage.sprite = ItemSprite;

            if (ItemDescriptionImage.sprite == null)
            {
                ItemDescriptionImage.sprite = EmptyImage;
            }
        }

    }

    public void Empty()
    {
        ItemQuantity = 0;
        ItemText.text = "";
        ItemText.enabled = false;
        ItemName = "";
        ItemSprite = EmptyImage;
        ItemDescription = "";
        ItemImage.sprite = EmptyImage;

        ItemDescriptionImage.sprite = EmptyImage;
        ItemDescriptionText.text = "";
        ItemNameText.text = "";
    }

    private void GetRightClick()
    {
        ////���õ�����item�Ľű�����
        //GameObject DropStuff = new GameObject(ItemName);
        //Item DropItem = DropStuff.AddComponent<Item>();
        //DropItem.ItemName = ItemName;
        //DropItem.ItemSprite = ItemSprite;
        //DropItem.ItemQuanity = 1;
        //DropItem.ItemDescription = ItemDescription;

        ////���SpriteRenderer���
        //SpriteRenderer SR = DropStuff.AddComponent<SpriteRenderer>();
        //SR.sprite = ItemSprite;
        //SR.sortingOrder = 1;
        //SR.sortingLayerName = "Ground";

        ////�����ײ�������
        //DropStuff.AddComponent<BoxCollider2D>();
        //DropStuff.AddComponent<Rigidbody2D>();

        ////�������ɾ������С
        //DropStuff.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(1f, 0f, 0f);
        //DropStuff.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        inventoryManager.DropStuff(this.ItemName);

        ItemQuantity -= 1;
        ItemText.text = ItemQuantity.ToString();
        SlotIsFull = false;
        if (ItemQuantity <= 0)
        {
            Empty();
        }
    }
}
