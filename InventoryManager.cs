using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{

    public GameObject InventoryMenu;
    private bool InventoryIsOpen;
    public ItemSlot[] ItemSlots;
    public string ItemDescription;

    public ItemSO[] Items;
    // Start is called before the first frame update
    void Start()
    {
        InventoryIsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Bag") && !InventoryIsOpen)
        {
            InventoryMenu.SetActive(true);
            InventoryIsOpen = true;
            Time.timeScale = 0;
        }
        else if (Input.GetButtonDown("Bag") && InventoryIsOpen)
        {
            InventoryMenu.SetActive(false);
            InventoryIsOpen = false;
            Time.timeScale = 1;
        }

    }

    public int AddItem(string ItemName, int ItemQuantity, Sprite ItemSprite, string ItemDescription)
    {
        //Debug.Log("Name is " + ItemName + " || Quantity is " + ItemQuantity + " || Sprite is " + ItemSprite);
        for (int i = 0; i < ItemSlots.Length; i++)
        {

            if (ItemSlots[i].SlotIsFull == false && ItemSlots[i].ItemName == ItemName || ItemSlots[i].ItemQuantity == 0)
            {
                int LeftItems = ItemSlots[i].AddItem(ItemName, ItemQuantity, ItemSprite, ItemDescription);
                if (LeftItems > 0)
                {
                    AddItem(ItemName, LeftItems, ItemSprite, ItemDescription);
                }
                return LeftItems;
            }
            
        }
        return ItemQuantity;
    }

    public void DeSelectPanel()
    {
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            ItemSlots[i].SelectedPanel.SetActive(false);
            ItemSlots[i].PanelIsSelected = false;
        }
    }

    public bool UseItems(string ItemName)
    {
        for(int i = 0; i < Items.Length; i++)
        {
            if (Items[i].name == ItemName)
            {
                bool IAmNotFull = Items[i].UseItem();
                //Debug.Log("InventoryManager Normal");
                return IAmNotFull;
            }
        }
        return false;
    }

    public void DropStuff(string ItemName)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].name == ItemName)
            {
                Vector3 V3 = GameObject.FindWithTag("Player").transform.position + new Vector3(1f ,0f ,0f );
                Instantiate(Items[i].ItemPrefab, V3, GameObject.FindWithTag("Player").transform.rotation);
            }
        }
    }
}
