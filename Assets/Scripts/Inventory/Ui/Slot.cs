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

    public Item item; // ȹ���� ������
    public int itemCount; // ȹ���� �������� ����
    public Image itemImage; //�������� �̹���
    public Vector3 originPos;

    private Slot[] slots;
    [SerializeField]
    private GameObject SlotsParent;

    // �ʿ��� ������Ʈ
    [SerializeField]
    private Text text_Count; // ȹ���� �������� ������ ��Ÿ�������� ���� ����
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
    // ������ ȹ��
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item; // ȹ���� �������� _item�� �־��ְ�
        itemCount = _count; // ȹ���� �������� ���������� _count�� �־��ְ�
        itemImage.sprite = item.itemImage; // ������ �̹����� Item.cs�� ����� Iamge �� �־��ְ�
        SetColor(1);
        if (item.itemtype != Item.ItemType.EquipmentWeapon) //ȹ���� �������� Ÿ���� ��� �ƴҰ�쿡
        {
            CountImage.SetActive(true); // �������� Ȯ���ߴٸ� ������� �������� ������ ��Ÿ���� image�� true�� ��Ÿ����
            text_Count.text = itemCount.ToString(); // �װ�����ŭ ToString�� ������ ��Ÿ����
        }
        if (item.itemtype == Item.ItemType.EquipmentWeapon) /// �����
        {
            text_Count.text = "0";
            CountImage.SetActive(false);
            Debug.Log("Equipment");
        }
        else if (item.itemtype != Item.ItemType.EquipmentPitching)
        {
            CountImage.SetActive(true); // �������� Ȯ���ߴٸ� ������� �������� ������ ��Ÿ���� image�� true�� ��Ÿ����
            text_Count.text = itemCount.ToString(); // �װ�����ŭ ToString�� ������ ��Ÿ����
        }
        else if (item.itemtype == Item.ItemType.EquipmentPitching) /// �����
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

    // �ش� ������ ������ ���� ������Ʈ(������ ���� ����)
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
            if(item != null) // �������� ���Կ� �ִٸ�
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
                    Debug.Log("HP�������� ����߽��ϴ�");
                    PlayerManager.Instance.currentHp += 30;
                    SetSlotCount(-1);
                    QuickSlot.instance.SetSlotCount(-1);
                    slottooltip.HideToolTip();
                    //quickslot.GetComponent<QuickSlot>().SetSlotCount(-1);
                    return;
                }
                if(item.itemName == "Mana")
                {
                    Debug.Log("MP�������� ����߽��ϴ�");
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
    public void OnEndDrag(PointerEventData eventData) //�巡�װ� ��𿡼��� �������� ȣ��
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
        Debug.Log("OnEndDrag");


    }
    public void OnDrop(PointerEventData eventData) // �巡�װ� �ٸ� ���Կ��� �������� ȣ��
    {
        if(DragSlot.instance.dragSlot != null) //�κ��丮�� ���� �������� ���
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

    // ���콺�� �κ��丮 ���Կ� ���� �߻�
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

    // ���콺�� �κ��丣 ���Կ��� �������ö� �߻�
    public void OnPointerExit(PointerEventData eventData)
    {
        if(item != null)
        {
            slottooltip.HideToolTip();
            Debug.Log("OnPointerExit");
        }
        
    }
}
