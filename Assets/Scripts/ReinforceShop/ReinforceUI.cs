using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ReinforceUI : MonoBehaviour
{
    public Item ReinforceWeaponitem; 
    public Item ReinforcePitchingitem; 
    public Item ingredientJwerlitme;
    public Item ArmoringredientJwerlitme;

    public Image itemImage;
    public Image PitchingitemImage;
    public Image ingredientJwerl;
    public Image ArmoringredientJwerl;

    public EquipmentUI equipmentui;

    public Equipment equipment;

    public TextMeshProUGUI EquipmentName;
    public TextMeshProUGUI JwerlCountText;
    public TextMeshProUGUI ArmorJwerlCountText;
    public TextMeshProUGUI ReinforceresultText;
    public Text GetGoldText;
    public Text SetGoldText;
    public Text PitchingGetGoldText;
    public Text PitchingSetGoldText;
    public Text weapondamageText;
    public Text ciriticalperText;
    public Text ciriticaldamageText;


    public Reinforce[] reinforces;
    public Reinforce reinforce;
    public Reinforce reinforce2;

    public Button SwordReinforceButton;
    public Button SwordReinforceStartButton;
    public Button PitchingReinforceButton;
    public Button PitchingReinforceStartButton;

    public GameObject ReinforceScreen;
    public GameObject ReinforceResult;
    public GameObject PitchingReinforceScreen;

    [SerializeField]
    private GameObject SlotsParent;

    // 슬롯들
    private Slot[] slots;
    private Slot slot;

    public int NeededNumber = 1; //강화할때 필요한 보석 갯수
    public int ArmorNeededNumber = 1; //강화할때 필요한 보석 갯수
    public int JwerlCounts; // 인벤토리 slot에있는 보석갯수
    public int ArmorJwerlCounts; // 인벤토리 slot에있는 보석갯수
    public int ReinforceGold = 100; // 강화할때 필요한 골드
    public int PitchingReinforceGold = 100; // 강화할때 필요한 골드
    public int ReinforceProbability = 90; // 무기강화확률 점점 줄어들예정

    private bool isReinforce = false;
    private bool isReinforceresult = false;
    private bool isPitchingReinforce = false;
    public int weapondamage;
    public int ciriticalper;
    public int ciriticaldamage;
    public int Defence;

    Player player;
    //public Button Exitbutton;

    public AudioClip[] clip;


    // Start is called before the first frame update
    void Start()
    {
        SwordReinforceButton.onClick.AddListener(ReinforceClick);
        PitchingReinforceButton.onClick.AddListener(PitchingReinforceClick);
        ReinforceScreen.SetActive(false);
        PitchingReinforceScreen.SetActive(false);
        slots = SlotsParent.GetComponentsInChildren<Slot>();
        SwordReinforceStartButton.onClick.AddListener(SwordReinforceStart);
        PitchingReinforceStartButton.onClick.AddListener(PitchReinforceStart);
        ReinforceResult.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        reinforces[0].ReinforceImage();
        reinforces[1].PitchReinforceImage();
        ReinforceIngredient();
        ArmorReinfroceIngredient();
        GetGoldText.text = "보유 골드 : " + PlayerManager.Instance.gold;
        SetGoldText.text = "재련 비용 : " + ReinforceGold;
        PitchingGetGoldText.text = "보유 골드 : " + PlayerManager.Instance.gold;
        PitchingSetGoldText.text = "재련 비용 : " + PitchingReinforceGold;


    }
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    private void SetColor2(float _alpha)
    {
        Color color = ingredientJwerl.color;
        color.a = _alpha;
        ingredientJwerl.color = color;
    }
    private void SetColor3(float _alpha)
    {
        Color color = PitchingitemImage.color;
        color.a = _alpha;
        PitchingitemImage.color = color;
    }
    private void SetColor4(float _alpha)
    {
        Color color = ArmoringredientJwerl.color;
        color.a = _alpha;
        ArmoringredientJwerl.color = color;
    }

    public void ReinforceClick()
    {
        if (reinforce.isReinforceWeapon)
        {
            ReinforceScreen.SetActive(true);
            PitchingReinforceScreen.SetActive(false);
            ReinforceWeaponitem = reinforces[0].Weaponitem;
            itemImage.sprite = ReinforceWeaponitem.itemImage;
            SetColor(1);
            EquipmentName.text = ReinforceWeaponitem.itemDesc;

            weapondamageText.text = "무기 공격력 증가 : + " + 5;
            ciriticalperText.text = "크리티컬 퍼센트 증가 : + " + 3;
            ciriticaldamageText.text = "크리티컬 공격력 증가 : + " + 1;
            SoundManager.instance.SFXPlay("ClickSound", clip[0]);
            Debug.Log("111111111111");
        }
    }
    public void PitchingReinforceClick()
    {
        if(reinforce2.isReinforcePitching)
        {
            Debug.Log("2");
            PitchingReinforceScreen.SetActive(true);
            ReinforceScreen.SetActive(false);
            ReinforcePitchingitem = reinforces[1].Pitchingitem;
            PitchingitemImage.sprite = ReinforcePitchingitem.itemImage;
            SetColor3(1);
            EquipmentName.text = ReinforcePitchingitem.itemDesc;
            weapondamageText.text = "";
            ciriticalperText.text = "방어력 증가 : + " + 1;
            ciriticaldamageText.text = "";
            SoundManager.instance.SFXPlay("ClickSound", clip[0]);
        }
    }
    public void ReinforceIngredient()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if(slots[i].item.itemName == "IngredientRed")
                {
                    ingredientJwerlitme = slots[i].item;
                    ingredientJwerl.sprite = slots[i].item.itemImage;
                    JwerlCounts = slots[i].itemCount;
                    JwerlCountText.text = JwerlCounts + (" / ") + NeededNumber;
                    SetColor2(1);
                }
            }
        }
    }
    public void ArmorReinfroceIngredient()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item != null)
            {
                if(slots[i].item.itemName == "IngredientBlue")
                {
                    ArmoringredientJwerlitme = slots[i].item;
                    ArmoringredientJwerl.sprite = slots[i].item.itemImage;
                    ArmorJwerlCounts = slots[i].itemCount;
                    ArmorJwerlCountText.text = ArmorJwerlCounts + (" / ") + ArmorNeededNumber;
                    SetColor4(1);
                }
            }
        }
    }
    public void Clear()
    {
        ingredientJwerlitme = null;
        JwerlCounts = 0;
        ingredientJwerl.sprite = null;
        SetColor2(0);
        

    }
    public void SwordReinforceStart()
    {
        if(PlayerManager.Instance.gold >= ReinforceGold && JwerlCounts >= NeededNumber && ReinforceWeaponitem)
        { // Player가 가지고있는 골드가 ReinforceGold보다 같거나 많아야하고&& 인벤토리에있는 보석이 강화갯수보다 많아야하고 && 무기를 장착하고있다면

            SoundManager.instance.SFXPlay("ClickSound", clip[0]);
            isReinforce = true;
            Debug.Log("무기 강화 중");
            int Probability = Random.Range(0, 100); // 확률
            if(Probability < ReinforceProbability) // ReinfroceProbability == 90을넣어서 현재확률은 90프로 강화성공이라면
            {
                weapondamage += 5;
                ciriticalper += 3;
                ciriticaldamage += 1;
                PlayerManager.Instance.gold -= ReinforceGold; 
                PlayerManager.Instance.WeaponDamage += weapondamage;
                PlayerManager.Instance.ciriticalPer += ciriticalper;
                PlayerManager.Instance.ciriticlaDamage += ciriticaldamage;
                PlayerManager.Instance.WeaponEnforce += 1;
                JwerlCounts -= NeededNumber;
                ReinforceGold += 80;
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].item != null)
                    {
                        if (slots[i].item.itemName == "IngredientRed")
                        {
                            slots[i].SetSlotCount(-NeededNumber);
                        }
                    }
                }
                if (JwerlCounts <= 0)
                {
                    ingredientJwerlitme = null;
                    JwerlCounts = 0;
                    ingredientJwerl.sprite = null;
                    SetColor2(0);
                }

                NeededNumber += 2;

                JwerlCountText.text = 0 + (" / ") + NeededNumber;

                ReinforceProbability -= 10;
                if(ReinforceProbability <= 50)
                {
                    if (ReinforceProbability <= 5)
                    {
                        ReinforceProbability = 5;
                    }
                }
                ReinforceResult.SetActive(true);
                ReinforceresultText.text = "강화 성공!";
                StartCoroutine(ReinforceResultTime());
                Debug.Log("강화성공");
                weapondamage = 0;
                ciriticalper = 0;
                ciriticaldamage = 0;

            }
            else // 강화 실패라면
            {
                //isReinforceresult = true;
                JwerlCounts -= NeededNumber;
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].item != null)
                    {
                        if (slots[i].item.itemName == "IngredientRed")
                        {
                            slots[i].SetSlotCount(-NeededNumber);
                        }
                    }
                }
                if (JwerlCounts <= 0)
                {
                    ingredientJwerlitme = null;
                    JwerlCounts = 0;
                    ingredientJwerl.sprite = null;
                    SetColor2(0);
                }
                JwerlCountText.text = 0 + (" / ") + NeededNumber;
                PlayerManager.Instance.gold -= ReinforceGold;
                Debug.Log("강화 실패" + JwerlCounts);
                ReinforceResult.SetActive(true);
                ReinforceresultText.text = "강화 실패!";
                StartCoroutine(ReinforceResultTime());
                
                weapondamage = 0;
                ciriticalper = 0;
                ciriticaldamage = 0;
            }
        }
        else if(PlayerManager.Instance.gold < ReinforceGold)
        {
            Debug.Log("골드가 부족합니다");
            ReinforceResult.SetActive(true);
            ReinforceresultText.text = "골드가 부족합니다!";
            StartCoroutine(ReinforceResultTime());
        }
        else if (JwerlCounts < NeededNumber)
        {
            Debug.Log("강화 보석이 부족합니다");
            ReinforceResult.SetActive(true);
            ReinforceresultText.text = "강화 보석이 부족합니다!";
            StartCoroutine(ReinforceResultTime());
        }
    }
    IEnumerator ReinforceResultTime()
    {
        yield return new WaitForSeconds(1f);
        ReinforceResult.SetActive(false);

    }
    public void PitchReinforceStart()
    {
        if(PlayerManager.Instance.gold >= PitchingReinforceGold && ArmorJwerlCounts >= ArmorNeededNumber && ReinforcePitchingitem)
        {
            SoundManager.instance.SFXPlay("ClickSound", clip[0]);
            isPitchingReinforce = true;
            Debug.Log("방어구 강화중");
            int Probability = Random.Range(0, 100);
            if(Probability < ReinforceProbability)
            {
                Defence += 1;
                PlayerManager.Instance.gold -= PitchingReinforceGold;
                PlayerManager.Instance.Defence += Defence;
                PlayerManager.Instance.PitchingEnforce += 1;
                ArmorJwerlCounts -= ArmorNeededNumber;
                PitchingReinforceGold += 80;
                
                for (int i = 0; i < slots.Length; i++)
                {
                    if(slots[i].item != null)
                    {
                        if(slots[i].item.itemName == "IngredientBlue")
                        {
                            slots[i].SetSlotCount(-ArmorNeededNumber);
                        }
                    }
                }
                if (ArmorJwerlCounts <= 0)
                {
                    ArmoringredientJwerlitme = null;
                    ArmorJwerlCounts = 0;
                    ArmoringredientJwerl.sprite = null;
                    
                    SetColor4(0);
                }

                ArmorNeededNumber += 2;
                ArmorJwerlCountText.text = ArmorJwerlCounts + (" / ") + ArmorNeededNumber;


                ReinforceProbability -= 10;
                if (ReinforceProbability <= 50)
                {
                    if (ReinforceProbability <= 5)
                    {
                        ReinforceProbability = 5;
                    }
                }
                ReinforceResult.SetActive(true);
                ReinforceresultText.text = "강화 성공!";
                StartCoroutine(ReinforceResultTime());
                Debug.Log("강화성공");
                Defence = 0;
            }
            else
            {
                ArmorJwerlCounts -= ArmorNeededNumber;
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].item != null)
                    {
                        if (slots[i].item.itemName == "IngredientBlue")
                        {
                            slots[i].SetSlotCount(-ArmorNeededNumber);
                        }
                    }
                }
                if (ArmorJwerlCounts <= 0)
                {
                    ArmoringredientJwerlitme = null;
                    ArmorJwerlCounts = 0;
                    ArmoringredientJwerl = null;
                    SetColor4(0);
                }
                ArmorJwerlCountText.text = 0 + (" / ") + ArmorNeededNumber;
                PlayerManager.Instance.gold -= PitchingReinforceGold;
                ReinforceResult.SetActive(true);
                ReinforceresultText.text = "강화 실패!";
                StartCoroutine(ReinforceResultTime());

                Defence = 0;
            }
        }
        else if (PlayerManager.Instance.gold < PitchingReinforceGold)
        {
            Debug.Log("골드가 부족합니다");
            ReinforceResult.SetActive(true);
            ReinforceresultText.text = "골드가 부족합니다!";
            StartCoroutine(ReinforceResultTime());
        }
        else if (JwerlCounts < ArmorNeededNumber)
        {
            Debug.Log("강화 보석이 부족합니다");
            ReinforceResult.SetActive(true);
            ReinforceresultText.text = "강화 보석이 부족합니다!";
            StartCoroutine(ReinforceResultTime());
        }
    }
    void ExitButton()
    {
        

    }
}