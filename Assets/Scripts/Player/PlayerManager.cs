using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region �̱���

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
    // * �÷��̾� ����
    public int damage = 5; //���ݷ�
    public int Defence = 1;
    public int firebolldamage = 5;
    public int Lightningdamage = 5;
    public float currentHp = 100; //���� ü��
    public float maxHp = 100; //�ִ� ü��
    public int ciriticalPer = 1; //ũ��Ƽ�� Ȯ��
    public float ciriticlaDamage = 1.0f; //ũ��Ƽ�� ������
    public float speed = 1; // �÷��̾��� �̵��ӵ�
    public float MaxMana = 100; // �÷��̾��� �ִ븶��
    public float currentMana = 100; // �÷��̾��� ���� ����
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


    // * ���
    public int gold = 1000;

    // * ����
    public int WeaponDamage = 3;
    public int WeaponEnforce = 0; // ���Ⱑ �����
    public int PitchingEnforce = 0; // ���Ⱑ �����

    private void Update()
    {
        AttackDamage = damage + WeaponDamage;
    }
    public int GetDamage()
    {
        int persent = Random.Range(0, 100); //100�� Ȯ��
        int persentdamage = Random.Range(0, 10);
        int normalAttack = Random.Range(0, 4);
        if (persent < ciriticalPer) //ũ��Ƽ�� Ȯ��
        {
            isCiritical = true;
            return AttackDamage + persentdamage * 2 + (AttackDamage * (int)ciriticlaDamage / 100);
        }
        else //�Ϲ� ����
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
