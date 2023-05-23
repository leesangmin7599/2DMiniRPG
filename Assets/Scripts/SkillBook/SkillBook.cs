using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillBook : MonoBehaviour
{
    public Button FireBollButton;
    public TextMeshProUGUI FireBollSkillPointText;
    public Text FireBollNeedPointText;
    public Text FireBollSkillLevel;
    public TextMeshProUGUI FireBollSkillLevelUpText;

    public Button LightningButton;
    public TextMeshProUGUI LightningSkillPointText;
    public Text LightningNeedPointText;
    public Text LightningSkillLevel;
    public TextMeshProUGUI LightningSkillLevelUpText;

    public TextMeshProUGUI SkillPointExitText;
    public AudioClip[] clip;

    // Start is called before the first frame update
    void Start()
    {
        FireBollButton.onClick.AddListener(FireBollReinforce);
        LightningButton.onClick.AddListener(LightningReinforce);
        SkillPointExitText.enabled = false;
        FireBollSkillLevelUpText.enabled = false;
        LightningSkillLevelUpText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        FireBollSkillPointText.text = ("스킬포인트 : " + PlayerManager.Instance.SkillPoint);

        FireBollSkillLevel.text = PlayerManager.Instance.FireBollSkillLevel.ToString();
        LightningSkillLevel.text = PlayerManager.Instance.LightningSkillLevel.ToString();
        FireBollNeedPointText.text = PlayerManager.Instance.MaxSkillPoint.ToString();
        LightningNeedPointText.text = PlayerManager.Instance.LightningMaxSkillPotion.ToString();
    }

    void FireBollReinforce()
    {
        if(PlayerManager.Instance.SkillPoint >= PlayerManager.Instance.MaxSkillPoint)
        {
            PlayerManager.Instance.SkillPoint -= PlayerManager.Instance.MaxSkillPoint;
            PlayerManager.Instance.firebolldamage += 3;
            FireBollSkillLevelUpText.enabled = true;
            FireBollSkillLevelUpText.text = ("Fire Boll 피해량이" + PlayerManager.Instance.firebolldamage + "로 증가합니다!");
            PlayerManager.Instance.MaxSkillPoint++;
            PlayerManager.Instance.FireBollSkillLevel++;
            Debug.Log("FireBollPowerUp");
            SoundManager.instance.SFXPlay("ClickSound", clip[0]);
        }
        else
        {
            Debug.Log("스킬포인트 부족");
            StartCoroutine(FadeOut());
        }
    }
    void LightningReinforce()
    {
        if (PlayerManager.Instance.SkillPoint >= PlayerManager.Instance.MaxSkillPoint)
        {
            PlayerManager.Instance.SkillPoint -= PlayerManager.Instance.LightningMaxSkillPotion;
            PlayerManager.Instance.Lightningdamage += 3;
            LightningSkillLevelUpText.enabled = true;
            LightningSkillLevelUpText.text = ("Lightning 피해량이" + PlayerManager.Instance.Lightningdamage + "로 증가합니다!");
            PlayerManager.Instance.LightningMaxSkillPotion++;
            PlayerManager.Instance.LightningSkillLevel++;
            Debug.Log("LightningPowerup");
            SoundManager.instance.SFXPlay("ClickSound", clip[0]);
        }
        else
        {
            Debug.Log("스킬포인트 부족");
            StartCoroutine(FadeOut());
        }
    }
    IEnumerator FadeOut() // 화면 점점 밝게
    {
        SkillPointExitText.enabled = true;
        float faedCount = 1;
        while (faedCount >= 0f)
        {
            faedCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            SkillPointExitText.color = new Color(255, 0, 0, faedCount);
        }
        yield return new WaitForSeconds(0.2f);
        SkillPointExitText.enabled = false;
    }
}
