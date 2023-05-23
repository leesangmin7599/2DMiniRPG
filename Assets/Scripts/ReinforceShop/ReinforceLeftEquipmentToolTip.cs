using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReinforceLeftEquipmentToolTip : MonoBehaviour
{
    [SerializeField]
    private GameObject ReinforceLeftEquipmentTooltip;

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



    public Equipment equipment;
    public Image itemimage;
    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        ReinforceLeftEquipmentTooltip.SetActive(true);

        ReinforceLeftEquipmentTooltip.transform.position = _pos;
        if (_item.itemtype == Item.ItemType.EquipmentPitching)
        {
            text_itemname.fontSize = 40;
            text_itemDesc.fontSize = 20;
            text_itemname.text = equipment.item.itemName;
            text_itemDesc.text = equipment.item.itemDesc;
            itemimage.sprite = equipment.item.itemImage;
            text_itemDesc.text = ("(+ " + PlayerManager.Instance.PitchingEnforce + ")");
            text_itemname.fontSize = 40;
            text_itemDesc.fontSize = 20;
            text_itemAttack.text = "¹æ¾î·Â : + " + PlayerManager.Instance.Defence;
            text_itemCiriper.text = "";
            text_itemCiriDam.text = "";
        }
    }

    public void HideToolTip()
    {
        ReinforceLeftEquipmentTooltip.SetActive(false);
    }
}
