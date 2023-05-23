using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region �̱���
    private static EnemyManager instance = null;
    public static EnemyManager Instance
    {
        get
        {
            if (instance == null) return null;
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
    // * Enemy ����
    public int damage = 3; //���ݷ�

    public int ciriticalPer = 0; //ũ��Ƽ�� Ȯ��
    public int ciriticlaDamage = 0; //ũ��Ƽ�� ������
    public GameObject enemy;
    public GameObject RangerEnemy;
    public GameObject SkeletonEnemy;

    public bool isCiritical;

    private void Start()
    {
        
    }
    public int GetDamage()
    {
        int persent = Random.Range(0, 100);
        int normalAttack = Random.Range(0, 4);
        int playerdefence = PlayerManager.Instance.Defence;
        if (persent < ciriticalPer) // ũ��Ƽ�� Ȯ��
        {
            isCiritical = true;
            return damage + (damage * ciriticlaDamage / 100);
        }
        else
        {
            isCiritical = false;
            return (damage + normalAttack) / playerdefence;
        }

    }
}
