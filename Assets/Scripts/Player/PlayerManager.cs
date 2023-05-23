using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region 싱글톤

    private static PlayerManager instance = null;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    // * 플레이어 정보
    public int damage = 5; //공격력
    public int Defence = 1;
    public int firebolldamage = 5;
    public int Lightningdamage = 5;
    public float currentHp = 100; //현재 체력
    public float maxHp = 100; //최대 체력
    public int ciriticalPer = 1; //크리티컬 확률
    public float ciriticlaDamage = 1.0f; //크리티컬 데미지
    public float speed = 1; // 플레이어의 이동속도
    public float MaxMana = 100; // 플레이어의 최대마나
    public float currentMana = 100; // 플레이어의 현재 마나
    public float MaxStamina = 100;
    public float currentStamina = 100;
    public int Level = 1;
    public int LevelExperience = 0;
    public int MaxLevelExperience = 100;
    public int SkillPoint = 10;
    public int MaxSkillPoint = 1;
    public int FireBollSkillLevel = 1;
    public int LightningSkillLevel = 1;
    public int LightningSkillPoint = 1;
    public int LightningMaxSkillPotion = 1;
    public bool isCiritical;

    // * PlayerAttackDamage
    public int AttackDamage;


    // * 골드
    public int gold = 1000;

    // * 무기
    public int WeaponDamage = 3;
    public int WeaponEnforce = 0; // 무기가 몇강인지
    public int PitchingEnforce = 0; // 무기가 몇강인지

    private void Update()
    {
        AttackDamage = damage + WeaponDamage;
    }
    public int GetDamage()
    {
        int persent = Random.Range(0, 100); //100퍼 확률
        int persentdamage = Random.Range(0, 10);
        int normalAttack = Random.Range(0, 4);
        if (persent < ciriticalPer) //크리티컬 확률
        {
            isCiritical = true;
            return AttackDamage + persentdamage * 2 + (AttackDamage * (int)ciriticlaDamage / 100);
        }
        else //일반 공격
        {
            isCiritical = false;
            return AttackDamage + normalAttack;
        }
    }
   
    public int ExplosionDamage()
    {
        int persent = Random.Range(0, 100);
        int persentdamage = Random.Range(0, 10);
        int ExplosionDamage = Random.Range(20, 30);
        if (persent < ciriticalPer)
        {
            isCiritical = true;
            return ExplosionDamage + persentdamage * 3 + (damage * (int)ciriticlaDamage / 100);
        }
        else
        {
            isCiritical = false;
            return firebolldamage + ExplosionDamage;
        }
    }
    public int LightningDamage()
    {
        int persent = Random.Range(0, 100);
        int persentDamage = Random.Range(0, 10);
        int LightningDamage = Random.Range(20, 30);
        if(persent < ciriticalPer)
        {
            isCiritical = true;
            return LightningDamage + persentDamage * 3 + (damage * (int)ciriticlaDamage / 100);
        }
        else
        {
            isCiritical = false;
            return Lightningdamage + LightningDamage;
        }
        
    }
}
