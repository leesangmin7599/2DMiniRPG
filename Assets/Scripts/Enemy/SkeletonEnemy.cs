using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonEnemy : MonoBehaviour
{
    Enemy_State state = Enemy_State.MOVE;

    public float speed = 1;
    public float currentHp; //현재 체력
    public float maxHp = 68; //최대 체력
    float rnd;
    float spawngoldrnd;
    public GameObject damageText;
    public GameObject gold;
    public GameObject HPPotion;
    public GameObject MPPotion;
    public GameObject Red;
    public GameObject Blue;
    public Image healthImage;
    public Image BackhealthImage;
    public GameObject EnemyHitBox;
    public GameObject LeftBlood;
    public GameObject RightBlood;
    public bool moveleft;
    public bool isDead = false;
    public float AttackCollTime = 1f;

    Animator anim;
    GameObject player;
    Rigidbody2D rb;
    CapsuleCollider2D CapsuleCol;
    SpriteRenderer spriteRen;

    public AudioClip[] clip;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        anim = GetComponent<Animator>();
        CapsuleCol = GetComponent<CapsuleCollider2D>();
        spriteRen = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        rnd = UnityEngine.Random.Range(0, 2);
        spawngoldrnd = UnityEngine.Random.Range(10, 20);
        healthImage.enabled = false;
        BackhealthImage.enabled = false;
        EnemyHitBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case Enemy_State.MOVE:
                Move();
                break;
            case Enemy_State.ATTACK:
                Attack();
                break;
            default:
                break;
        }
        healthImage.fillAmount = currentHp / maxHp;
        StartCoroutine(PlusAttackCollTime());
    }
    IEnumerator PlusAttackCollTime()
    {
        yield return new WaitForSeconds(0.3f);
        AttackCollTime += 0.005f;
        if (AttackCollTime >= 1)
        {
            AttackCollTime = 1f;
        }
    }
    public virtual void Move()
    {
        if(!isDead)
        {

            transform.Translate(Vector3.left * speed * Time.deltaTime);

            // 왼쪽 끝으로 갔다면(왼쪽땅과 충돌중이 아니라면)
            if (!IsGroundLeft())
            {
                transform.localScale = new Vector3(-3f, 3f, 3f);
                speed = 1;
                moveleft = true;
            }
            // 오른쪽 끝으로 갔다면(오른쪽 땅과 충돌중이 아니라면)
            if (!IsGroundRight())
            {
                transform.localScale = new Vector3(3f, 3f, 3f);
                speed = -1;
                moveleft = false;
            }
            if (Vector3.Distance(player.transform.position, transform.position) < 1.5f)
            {
                state = Enemy_State.ATTACK; //공격상태로 변경
                if (player.transform.position.x - transform.position.x < 0) // player가 왼쪽에 있을때
                {
                    transform.localScale = new Vector3(-3f, 3f, 3f);
                }
                if (player.transform.position.x - transform.position.x > 0) // player가 오른쪽에 있을때
                {
                    transform.localScale = new Vector3(3f, 3f, 3f);
                }
            }
        }
        
    }
    bool IsGroundRight() // 캐릭터의 오른쪽 아래에 box를 만들어줌
    {
        return Physics2D.OverlapBox(transform.position + new Vector3(-1f, -1.7f), new Vector2(0.2f, 0.2f), LayerMask.GetMask("Ground"));
    }

    bool IsGroundLeft()// 캐릭터의 왼쪽 아래에 box를 만들어줌
    {
        return Physics2D.OverlapBox(transform.position + new Vector3(1f, -1.7f), new Vector2(0.2f, 0.2f), LayerMask.GetMask("Ground"));
    }
    private void OnDrawGizmos() // 위에 IsGroundLeft랑 Right그림을 그려줌
    {
        // right
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + new Vector3(-1f, -1.7f), new Vector3(0.1f, 0.1f));

        //left
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position + new Vector3(1f, -1.7f), new Vector3(0.1f, 0.1f));
        //DrawCube (위치, 크기) : 큐브모양 기즈모를 그려줌
    }
    public virtual void Attack()
    {
        if (AttackCollTime >= 1f && !isDead)
        {
            anim.SetBool("Attack", true);
            AttackCollTime = 0;
            
            StartCoroutine(AttackInEnd());
        }
        float dis = Vector3.Distance(player.transform.position, transform.position);
        if (dis > 1.5f)
        {
            anim.SetBool("Attack", false);
            state = Enemy_State.MOVE;
            if (player.transform.position.x - transform.position.x < 0 && !moveleft)  // player가 왼쪽에 있을때
            {
                transform.localScale = new Vector3(3f, 3f, 3f);
                Debug.Log("right");
            }
            if (player.transform.position.x - transform.position.x > 0 && moveleft) // player가 왼쪽에 있을때
            {
                transform.localScale = new Vector3(-3f, 3f, 3f);
                Debug.Log("left");
            }
        }

    }
    IEnumerator SwingSound()
    {
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay("EnemySwing", clip[0]);
    }
    IEnumerator AttackInEnd()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Attack", false);
    }
    public void MeleeAttack()
    {
        StartCoroutine(StartAttack());
        StartCoroutine(SwingSound());
    }
    IEnumerator StartAttack()
    {
        EnemyHitBox.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        EnemyHitBox.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitBox") && currentHp >= 0)
        {
            Debug.Log("tihg");
            anim.SetBool("Attack", false);
            speed = 0;
            anim.SetTrigger("Hit");
            StartCoroutine(HitEnd());
            SoundManager.instance.SFXPlay("EnemyHit", clip[1]);
            float otherX = collision.transform.parent.position.x;
            float myX = transform.position.x;
            Vector3 nuckBackPos = new Vector3(myX - otherX, 0, 0);
            //GetComponent<Rigidbody2D>().AddForce(nuckBackPos.normalized * 20);

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
            healthImage.enabled = true;
            BackhealthImage.enabled = true;
            if (currentHp <= 0)
            {
                int NormalAttackrndspawn = UnityEngine.Random.Range(0, 3);
                if (NormalAttackrndspawn == 0)
                {
                    Instantiate(gold, collision.transform.position, Quaternion.identity);
                }
                if (NormalAttackrndspawn == 1) // 나중에 포션 만들어주기
                {
                    Instantiate(HPPotion, collision.transform.position, Quaternion.identity);
                }
                if (NormalAttackrndspawn == 2) // 나중에 포션 만들어주기
                {
                    Instantiate(MPPotion, collision.transform.position, Quaternion.identity);
                }
                if (NormalAttackrndspawn == 3) // 나중에 포션 만들어주기
                {
                    Instantiate(Red, collision.transform.position, Quaternion.identity);
                }
                if (NormalAttackrndspawn == 4) // 나중에 포션 만들어주기
                {
                    Instantiate(Blue, collision.transform.position, Quaternion.identity);
                }
                PlayerManager.Instance.LevelExperience += 10;
                Die();
            }
        }

        if (collision.CompareTag("Explosion") && currentHp >= 0)
        {
            speed = 0;
            anim.SetTrigger("Hit");
            StartCoroutine(HitEnd());
            SoundManager.instance.SFXPlay("EnemyHit", clip[1]);
            Destroy(collision.gameObject);
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
            healthImage.enabled = true;
            BackhealthImage.enabled = true;
            if (currentHp <= 0)
            {
                int ExplosionAttackrndspawn = UnityEngine.Random.Range(0, 3);
                if (ExplosionAttackrndspawn == 0)
                {
                    Instantiate(gold, collision.transform.position, Quaternion.identity);
                }
                if (ExplosionAttackrndspawn == 1) // 나중에 포션 만들어주기
                {
                    Instantiate(HPPotion, collision.transform.position, Quaternion.identity);
                }
                if (ExplosionAttackrndspawn == 2) // 나중에 포션 만들어주기
                {
                    Instantiate(MPPotion, collision.transform.position, Quaternion.identity);
                }
                if (ExplosionAttackrndspawn == 3) // 나중에 포션 만들어주기
                {
                    Instantiate(Red, collision.transform.position, Quaternion.identity);
                }
                if (ExplosionAttackrndspawn == 4) // 나중에 포션 만들어주기
                {
                    Instantiate(Blue, collision.transform.position, Quaternion.identity);
                }
                PlayerManager.Instance.LevelExperience += 10;
                Die();
            }
            if (isDead)
            {
                return;
            }
        }
        if(collision.CompareTag("LightAttack"))
        {
            speed = 0;
            anim.SetTrigger("Hit");
            SoundManager.instance.SFXPlay("EnemyHit", clip[1]);
            int Damage = PlayerManager.Instance.LightningDamage();
            currentHp -= Damage;
            GameObject gameobject = Instantiate(damageText, collision.transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            gameobject.GetComponent<DamageText>().damage = Damage;
            if (player.transform.position.x - transform.position.x < 0) // player가 왼쪽에 있을때
            {
                Instantiate(RightBlood, transform.position, Quaternion.identity);
            }
            if (player.transform.position.x - transform.position.x > 0) // player가 오른쪽에 있을때
            {
                Instantiate(LeftBlood, transform.position, Quaternion.identity);
            }
            healthImage.enabled = true;
            BackhealthImage.enabled = true;
            StartCoroutine(Hitfltr());
            if(currentHp <= 0)
            {
                int ExplosionAttackrndspawn = UnityEngine.Random.Range(0, 3);
                if (ExplosionAttackrndspawn == 0)
                {
                    Instantiate(gold, collision.transform.position, Quaternion.identity);
                }
                if (ExplosionAttackrndspawn == 1) // 나중에 포션 만들어주기
                {
                    Instantiate(HPPotion, collision.transform.position, Quaternion.identity);
                }
                if (ExplosionAttackrndspawn == 2) // 나중에 포션 만들어주기
                {
                    Instantiate(MPPotion, collision.transform.position, Quaternion.identity);
                }
                if (ExplosionAttackrndspawn == 3) // 나중에 포션 만들어주기
                {
                    Instantiate(Red, collision.transform.position, Quaternion.identity);
                }
                if (ExplosionAttackrndspawn == 4) // 나중에 포션 만들어주기
                {
                    Instantiate(Blue, collision.transform.position, Quaternion.identity);
                }
                PlayerManager.Instance.LevelExperience += 10;
                Die();
            }
            if(isDead)
            {
                return;
            }
        }
    }
    IEnumerator Hitfltr()
    {
        yield return new WaitForSeconds(0.1f);
        if (player.transform.position.x - transform.position.x < 0) // player가 왼쪽에 있을때
        {
            speed = 1;
        }
        if (player.transform.position.x - transform.position.x > 0) // player가 오른쪽에 있을때
        {
            speed = -1;
        }
    }
    void Die()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        healthImage.enabled = false;
        BackhealthImage.enabled = false;
        isDead = true;
        speed = 0;
        Debug.Log("dIE");
        this.gameObject.layer = 9;
        anim.SetTrigger("Death");
        StartCoroutine(DestroyEnemy());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground2"))
        {
            Debug.Log("gd");
            if (speed == -1)
            {
                transform.localScale = new Vector3(-3f, 3f, 3f);
                speed = 1;
            }
            else
            {
                transform.localScale = new Vector3(3f, 3f, 3f);
                speed = -1;
            }

        }
    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(0.52f);
        CapsuleCol.enabled = false; // 콜라이더 비활성화
        spriteRen.enabled = false; // 스프라이트 활성화
        yield return new WaitForSeconds(7f);
        CapsuleCol.enabled = true; // 콜라이더 비활성화
        spriteRen.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        healthImage.enabled = false;
        BackhealthImage.enabled = false;
        currentHp = maxHp;
        speed = 1;
        transform.localScale = new Vector3(-3f, 3f, 3f);
        isDead = false;
    }
    IEnumerator HitEnd()
    {

        yield return new WaitForSeconds(0.2f);

        if (!moveleft && currentHp > 1)
        {
            speed = -1;
        }
        else if (moveleft && currentHp > 1)
        {
            speed = 1;
        }
    }

}
