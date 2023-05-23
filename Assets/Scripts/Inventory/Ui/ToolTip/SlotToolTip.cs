using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField]
    private GameObject slottooltip;

    [SerializeField]
    private Text text_itemname;
    [SerializeField]
    private Text text_itemDesc;
    [SerializeField]
    private Text text_itemhowtoused;
    [SerializeField]
    private Text text_itemExplanation;
    [SerializeField]
    private Text text_itemCount;
    [SerializeField]
    private Text text_itemAttack;
    [SerializeField]
    private Text text_itemCiriper;
    [SerializeField]
    private Text text_itemCiriDam;

    public Image itemimage;
    [SerializeField]
    private GameObject SlotsParent;
    public Slot[] slots;

    void Start()
    {
        slots = SlotsParent.GetComponentsInChildren<Slot>();
    }
    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        slottooltip.SetActive(true);
        slottooltip.transform.position = _pos;
        text_itemname.text = _item.itemName;
        text_itemDesc.text = _item.itemDesc;
        itemimage.sprite = _item.itemImage;
        text_itemExplanation.text = _item.itemExplanation;
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item != null)
            {
                if(slots[i].item.itemName == _item.itemName)
                {
                    text_itemCount.text = "[X" + slots[i].itemCount.ToString() + "]";
                }
                
            }
        }
        if (_item.itemtype == Item.ItemType.EquipmentWeapon)
        {
            text_itemhowtoused.text = "우클릭 - 장착";
            text_itemAttack.text = "무기 공격력 : + " + PlayerManager.Instance.WeaponDamage;
            text_itemCiriper.text = "크리티컬 퍼센트 : + " + PlayerManager.Instance.ciriticalPer;
            text_itemCiriDam.text = "크리티컬 데미지 : + " + PlayerManager.Instance.ciriticlaDamage;
        }
        if(_item.itemtype == Item.ItemType.EquipmentPitching)
        {
            text_itemhowtoused.text = "우클릭 - 장착";
            text_itemAttack.text = "방어력 : + " + PlayerManager.Instance.Defence;
            text_itemCiriper.text = "";
            text_itemCiriDam.text = "";
        }
        if(_item.itemtype == Item.ItemType.Ingredient)
        {
            text_itemAttack.text = _item.itemExplanation;
            text_itemhowtoused.text = "";
            text_itemCiriper.text = "";
            text_itemCiriDam.text = "";
        }

        else if (_item.itemtype == Item.ItemType.Used)
            text_itemhowtoused.text = "우클릭 - 먹기";
        else
            text_itemhowtoused.text = " ";
    }

    public void HideToolTip()
    {
        slottooltip.SetActive(false);
    }
}
