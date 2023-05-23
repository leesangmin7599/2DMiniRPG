using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    #region 싱글톤
    private static BossManager instance = null;
    public static BossManager Instance
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

    // * Boss의 정보
    public int damage = 1;
    public float speed = 1.5f;
    public float maxHp = 200;
    public float currentHp;
    public int ciriticalpersent = 1;
    public int ciriticalDamage = 0;

    // * 보스 생성을 위한 GameObject
    public GameObject Boss;

    // * 크리티컬이 발생했을때 true  발생하지 않았을때 false;
    public bool isCiritical;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int BossNormalGetDamage() //보스 일반공격
    {
        int persent = Random.Range(0, 10);
        return damage + persent + PlayerManager.Instance.Defence;
    }
    public int BossSkillGetDamage() // 보스 스킬
    {
        int persent = Random.Range(10, 30);
        return damage + persent + PlayerManager.Instance.Defence;
    }
}
