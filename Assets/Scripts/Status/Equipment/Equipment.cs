using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Equipment : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public Item item;
    public int itemCount; // »πµÊ«— æ∆¿Ã≈€¿« ∞πºˆ
    public Image itemImage;
    public bool isEquipWeapon = false;
    public bool isEquipPitching = false;

    public EquipmentToolTip Equipmenttooltip;
    public InventoryUI invenui;

    public AudioClip[] clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    public void AddItem(Item _item)
    {
        item = _item;
        itemImage.sprite = item.itemImage;
        if(item.itemtype == Item.ItemType.EquipmentWeapon)
        {
            SetColor(1);
        }
        else if(item.itemName == "Pitching")
        {
            SetColor(1);
        }
        else
        {
            SetColor(0);
        }
    }
    public void SetSlotCount(int _count)
    {
        itemCount += _count;

        if (itemCount <= 0)
        {
            ClearSlot();
            Debug.Log("quickslot axis");
        }

    }

    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            Equipmenttooltip.ShowToolTip(item, transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != null)
        {
            Equipmenttooltip.HideToolTip();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if(item != null)
            {
                if(item.itemName == "Pitching")
                {
                    Debug.Log("!Itemnull");
                    invenui.AcquireItem(item);
                    SetSlotCount(-1);
                    SoundManager.instance.SFXPlay("EquipmentPitching", clip[0]);
                    isEquipWeapon = false;
                    Equipmenttooltip.HideToolTip();
                    return;
                }
                if(item.itemName == "Sword")
                {
                    Debug.Log("!Itemnull");
                    invenui.AcquireItem(item);
                    SetSlotCount(-1);
                    SoundManager.instance.SFXPlay("EquipmentSword", clip[1]);
                    isEquipWeapon = false;
                    Equipmenttooltip.HideToolTip();
                    return;
                }
                
            }
        }
    }
}
