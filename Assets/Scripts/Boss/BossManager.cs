using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    #region �̱���
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

    // * Boss�� ����
    public int damage = 1;
    public float speed = 1.5f;
    public float maxHp = 200;
    public float currentHp;
    public int ciriticalpersent = 1;
    public int ciriticalDamage = 0;

    // * ���� ������ ���� GameObject
    public GameObject Boss;

    // * ũ��Ƽ���� �߻������� true  �߻����� �ʾ����� false;
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
    public int BossNormalGetDamage() //���� �Ϲݰ���
    {
        int persent = Random.Range(0, 10);
        return damage + persent + PlayerManager.Instance.Defence;
    }
    public int BossSkillGetDamage() // ���� ��ų
    {
        int persent = Random.Range(10, 30);
        return damage + persent + PlayerManager.Instance.Defence;
    }
}
