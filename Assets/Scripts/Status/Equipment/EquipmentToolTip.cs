using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentToolTip : MonoBehaviour
{
    [SerializeField]
    private GameObject Equipmenttooltip;

    [SerializeField]
    private Text text_itemname;
    [SerializeField]
    private Text text_itemDesc;
    [SerializeField]
    private Text text_itemEnforce;
    [SerializeField]
    private Text text_itemAttack;
    [SerializeField]
    private Text text_itemCiriper;
    [SerializeField]
    private Text text_itemCiriDam;

    //public EquipmentUI equipmentui;
    public Equipment equipment;
    public Image itemimage;

    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        Equipmenttooltip.SetActive(true);

        Equipmenttooltip.transform.position = _pos;
        text_itemname.text = _item.itemName;
        text_itemDesc.text = _item.itemDesc;
        itemimage.sprite = _item.itemImage;
        
        text_itemAttack.text = "무기 공격력 : + " + PlayerManager.Instance.WeaponDamage;
        text_itemCiriper.text = "크리티컬 퍼센트 : + " + PlayerManager.Instance.ciriticalPer;
        text_itemCiriDam.text = "크리티컬 데미지 : + " + PlayerManager.Instance.ciriticlaDamage;
        if (_item.itemtype == Item.ItemType.EquipmentWeapon)
        {
            text_itemAttack.text = "무기 공격력 : + " + PlayerManager.Instance.WeaponDamage;
            text_itemCiriper.text = "크리티컬 퍼센트 : + " + PlayerManager.Instance.ciriticalPer;
            text_itemCiriDam.text = "크리티컬 데미지 : + " + PlayerManager.Instance.ciriticlaDamage;
            text_itemEnforce.text = ("(+ " + PlayerManager.Instance.WeaponEnforce + ")");
        }
        if (_item.itemtype == Item.ItemType.EquipmentPitching)
        {
            text_itemname.fontSize = 40;
            text_itemDesc.fontSize = 20;
            text_itemAttack.text = "방어력 : + " + PlayerManager.Instance.Defence;
            text_itemCiriper.text = "";
            text_itemCiriDam.text = "";
            text_itemEnforce.text = ("(+ " + PlayerManager.Instance.PitchingEnforce + ")");
        }

    }

    public void HideToolTip()
    {
        Equipmenttooltip.SetActive(false);
    }
}
