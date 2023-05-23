using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Reinforce : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item Weaponitem;
    public Item Pitchingitem;
    public Image itemImage;
    public Image PitchingitemImage;
    public EquipmentUI equipmentui;
    public Equipment equpment;
    

    public TextMeshProUGUI EquipmentName;
    public TextMeshProUGUI PitchingEquipmentName;

    public bool isReinforceWeapon = false;
    public bool isReinforcePitching = false;

   // public ReinforceLeftEquipmentToolTip Reinforceleftequipmenttooltip;
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
    private void SetColor2(float _alpha)
    {
        Color color = PitchingitemImage.color;
        color.a = _alpha;
        PitchingitemImage.color = color;
    }
    public void ReinforceImage()
    {
        if (equipmentui.equipments[9].isEquipWeapon == true)
        {
            isReinforceWeapon = true;
            Weaponitem = equipmentui.equipments[9].item;
            itemImage.sprite = Weaponitem.itemImage;
            EquipmentName.text = Weaponitem.itemDesc;
            SetColor(1);
        }
    }
    public void PitchReinforceImage()
    {
        if (equipmentui.equipments[0].isEquipPitching == true)
        {
            isReinforcePitching = true;
            Pitchingitem = equipmentui.equipments[0].item;
            PitchingitemImage.sprite = Pitchingitem.itemImage;
            PitchingEquipmentName.text = Pitchingitem.itemDesc;
            SetColor2(1);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
