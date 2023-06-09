using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region 싱글톤
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
    // * Enemy 정보
    public int damage = 3; //공격력

    public int ciriticalPer = 0; //크리티컬 확률
    public int ciriticlaDamage = 0; //크리티컬 데미지
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
        if (persent < ciriticalPer) // 크리티컬 확률
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
