using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{


    public GameObject quickslot;
    public GameObject quickslot2;
    public GameObject slot;
    public GameObject slot2;
    public EquipmentUI equipmentui;

    private SlotToolTip slottooltip;

    public Item item; // 획득한 아이템
    public int itemCount; // 획득한 아이템의 갯수
    public Image itemImage; //아이템의 이미지
    public Vector3 originPos;

    private Slot[] slots;
    [SerializeField]
    private GameObject SlotsParent;

    // 필요한 컴포넌트
    [SerializeField]
    private Text text_Count; // 획득한 아이템의 갯수를 나타내기위해 만든 변수
    [SerializeField]
    private GameObject CountImage;

    public AudioClip[] clip;
    private void Start()
    {
        originPos = transform.position;
        quickslot = GameObject.Find("QuickSlot");
        quickslot2 = GameObject.Find("QuickSlot2");
        slot = GameObject.Find("Slot");
        slot2 = GameObject.Find("Slot (1)");
        slottooltip = FindObjectOfType<SlotToolTip>();
        slottooltip.enabled = false;
        slots = SlotsParent.GetComponentsInChildren<Slot>();
    }
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    // 아이템 획득
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item; // 획득한 아이템의 _item을 넣어주고
        itemCount = _count; // 획득한 아이템의 갯수에따라 _count를 넣어주고
        itemImage.sprite = item.itemImage; // 아이템 이미지는 Item.cs에 저장된 Iamge 를 넣어주고
        SetColor(1);
        if (item.itemtype != Item.ItemType.EquipmentWeapon) //획득한 아이템의 타입이 장비가 아닐경우에
        {
            CountImage.SetActive(true); // 아이템을 확득했다면 만들어준 아이템의 갯수를 나타내는 image를 true로 나타내고
            text_Count.text = itemCount.ToString(); // 그갯수만큼 ToString로 갯수를 나타내기
        }
        if (item.itemtype == Item.ItemType.EquipmentWeapon) /// 장비라면
        {
            text_Count.text = "0";
            CountImage.SetActive(false);
            Debug.Log("Equipment");
        }
        else if (item.itemtype != Item.ItemType.EquipmentPitching)
        {
            CountImage.SetActive(true); // 아이템을 확득했다면 만들어준 아이템의 갯수를 나타내는 image를 true로 나타내고
            text_Count.text = itemCount.ToString(); // 그갯수만큼 ToString로 갯수를 나타내기
        }
        else if (item.itemtype == Item.ItemType.EquipmentPitching) /// 장비라면
        {
            text_Count.text = "0";
            CountImage.SetActive(false);
            Debug.Log("Head");
        }
        else if (item.itemName == "IngredientRed")
        {
            Debug.Log("ingeridne");
        }
        
    }

    // 해당 슬롯의 아이템 갯수 업데이트(아이템 갯수 조정)
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

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
        text_Count.text = "0";
        CountImage.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(item != null) // 아이템이 슬롯에 있다면
            {
                if (item.itemName == "Pitching")
                {
                    Debug.Log("gd");
                    equipmentui.AcquireItem2(item);
                    equipmentui.equipments[0].isEquipPitching = true;
                    SetSlotCount(-1);
                    SoundManager.instance.SFXPlay("EquipmentPitching", clip[0]);
                    slottooltip.HideToolTip();
                    return;
                }
                if (item.itemName == "HP")
                {
                    Debug.Log("HP아이템을 사용했습니다");
                    PlayerManager.Instance.currentHp += 30;
                    SetSlotCount(-1);
                    QuickSlot.instance.SetSlotCount(-1);
                    slottooltip.HideToolTip();
                    //quickslot.GetComponent<QuickSlot>().SetSlotCount(-1);
                    return;
                }
                if(item.itemName == "Mana")
                {
                    Debug.Log("MP아이템을 사용했습니다");
                    PlayerManager.Instance.currentMana += 30;
                    SetSlotCount(-1);
                    slottooltip.HideToolTip();
                    quickslot2.GetComponent<QuickSlot>().SetSlotCount(-1);
                    return;
                }
                if(item.itemName == "IngredientRed")
                {
                    return;
                }
                if(item.itemName == "Sword")
                {
                    equipmentui.AcquireItem(item);
                    equipmentui.equipments[9].isEquipWeapon = true;
                    SetSlotCount(-1);
                    SoundManager.instance.SFXPlay("EquipmentSword", clip[1]);
                    slottooltip.HideToolTip();
                    return;
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);
            DragSlot.instance.transform.position = eventData.position;
            Debug.Log("OnBeginDrag");
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
            Debug.Log("OnDrag");
        }
            
    }
    public void OnEndDrag(PointerEventData eventData) //드래그가 어디에서든 끝났을때 호출
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
        Debug.Log("OnEndDrag");


    }
    public void OnDrop(PointerEventData eventData) // 드래그가 다른 슬롯에서 끝났을때 호출
    {
        if(DragSlot.instance.dragSlot != null) //인벤토리에 뭔가 있을때만 사용
        {
            ChangeSlot();
            Debug.Log("OnDrop");
        }
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if(_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }

    // 마우스가 인벤토리 슬롯에 들어갈때 발생
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item != null)
        {
            slottooltip.ShowToolTip(item, transform.position);
            Debug.Log("OnPointerEnter");
            SoundManager.instance.SFXPlay("ClickSound", clip[2]);
        }
        else
        {
            SoundManager.instance.SFXPlay("ClickSound", clip[2]);

        }
        
    }

    // 마우스가 인벤토르 슬롯에서 빠져나올때 발생
    public void OnPointerExit(PointerEventData eventData)
    {
        if(item != null)
        {
            slottooltip.HideToolTip();
            Debug.Log("OnPointerExit");
        }
        
    }
}
