using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialEnemy : MonoBehaviour
{
    public float currentHp; //현재 체력
    public float maxHp = 68; //최대 체력
    public GameObject damageText;
    public Image healthImage;
    public Image BackhealthImage;
    public GameObject LeftBlood;
    public GameObject RightBlood;

    Animator anim;

    public bool isDead = false;
    public AudioClip[] clip;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        healthImage.fillAmount = currentHp / maxHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerHitBox") && currentHp >= 0)
        {
            int tempDamage = PlayerManager.Instance.GetDamage();
            currentHp -= tempDamage;
            GameObject obj = Instantiate(damageText, collision.transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            obj.GetComponent<DamageText>().damage = tempDamage;
            if (player.transform.position.x - transform.position.x < 0) // player가 왼쪽에 있을때
            {
                Instantiate(RightBlood, transform.position, Quaternion.identity);
            }
            if (player.transform.position.x - transform.position.x > 0) // player가 오른쪽에 있을때
            {
                Instantiate(LeftBlood, transform.position, Quaternion.identity);
            }
            SoundManager.instance.SFXPlay("EnemyHit", clip[0]);

            if (currentHp <= 0)
            {
                anim.SetTrigger("Die");
                Debug.Log("Die");
                isDead = true;
                StartCoroutine(Die());
            }
            if (isDead)
            {
                return;
            }
        }
        if(collision.gameObject.CompareTag("Explosion") && currentHp >= 0)
        {
            int tempDamage = PlayerManager.Instance.ExplosionDamage();
            currentHp -= tempDamage;
            GameObject obj = Instantiate(damageText, collision.transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            obj.GetComponent<DamageText>().damage = tempDamage;
            if (player.transform.position.x - transform.position.x < 0) // player가 왼쪽에 있을때
            {
                Instantiate(RightBlood, transform.position, Quaternion.identity);
            }
            if (player.transform.position.x - transform.position.x > 0) // player가 오른쪽에 있을때
            {
                Instantiate(LeftBlood, transform.position, Quaternion.identity);
            }
            SoundManager.instance.SFXPlay("EnemyHit", clip[0]);
            Destroy(collision.gameObject);
            if (currentHp <= 0)
            {
                anim.SetTrigger("Die");
                isDead = true;
                StartCoroutine(Die());
            }
            if (isDead)
            {
                return;
            }
        }
        if(collision.gameObject.CompareTag("LightAttack") && currentHp >= 0)
        {
            int tempDamage = PlayerManager.Instance.LightningDamage();
            currentHp -= tempDamage;
            GameObject obj = Instantiate(damageText, collision.transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            obj.GetComponent<DamageText>().damage = tempDamage;
            if (player.transform.position.x - transform.position.x < 0) // player가 왼쪽에 있을때
            {
                Instantiate(RightBlood, transform.position, Quaternion.identity);
            }
            if (player.transform.position.x - transform.position.x > 0) // player가 오른쪽에 있을때
            {
                Instantiate(LeftBlood, transform.position, Quaternion.identity);
            }
            SoundManager.instance.SFXPlay("EnemyHit", clip[0]);
            if (currentHp <= 0)
            {
                anim.SetTrigger("Die");
                isDead = true;
                StartCoroutine(Die());
            }
            if(isDead)
            {
                return;
            }
        }
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
