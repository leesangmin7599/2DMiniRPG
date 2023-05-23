using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    // 보스방 : 정지 - 텔포 - 공격 - 스킬 - 정지 - 텔포 //  - 공격 - 스킬 - 정지 - 텔포 
    Boss_State state = Boss_State.IDLE;

    Animator anim;
    Rigidbody2D rid;
    GameObject player;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsulecollider2d;

    public bool isAttacking = false;
    public bool isCast = false;
    public bool isTeleport = false;
    public float CastSkilleCoolTime = 1f;
    public float AttackCoolTime = 1f;
    public bool isDead = false;

    public GameObject BossSkill;
    public GameObject BossTeleportEffect;
    public GameObject BossHitBox;
    public GameObject damageText;

    public AudioClip[] clip;

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rid = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsulecollider2d = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case Boss_State.IDLE:
                Idle();
                Debug.Log("BossIdle");
                break;
            case Boss_State.MOVE:
                Move();
                Debug.Log("Bossmove");
                break;
            case Boss_State.ATTACK:
                Attack();
                Debug.Log("BossATTACK");
                break;
            case Boss_State.CAST:
                Cast();
                Debug.Log("BossCAST");
                break;
            case Boss_State.TELEPORT:
                Teleport();
                Debug.Log("BossTELEPORT");
                break;
            default:
                break;
        }
        StartCoroutine(BossCastCoolTimePlus());
        StartCoroutine(AttackTimePlus());
        
    }
    IEnumerator BossCastCoolTimePlus()
    {
        yield return new WaitForSeconds(0.5f);
        CastSkilleCoolTime += 0.0002f;
        if (CastSkilleCoolTime >= 1)
        {
            CastSkilleCoolTime = 1f;
        }
    }
    IEnumerator AttackTimePlus()
    {
        yield return new WaitForSeconds(0.5f);
        AttackCoolTime += 0.01f;
        if (AttackCoolTime >= 1)
        {
            AttackCoolTime = 1f;
        }
    }
    private void Idle()
    {
        float dis = Vector3.Distance(player.transform.position, transform.position);
        Debug.Log("Idle");
       
        if(dis > 5f && dis < 15f)
        {
            state = Boss_State.TELEPORT;
        }
    }

    public void Move()
    {
        if(!isAttacking)
        {
            float dis = Vector3.Distance(player.transform.position, transform.position);
            anim.SetFloat("Speed", 3);
            if (player.transform.position.x - transform.position.x < 0) // 플레이어가 보스 왼쪽에있을때
            {
                transform.localScale = new Vector3(10f, 10f, 1f);
                transform.Translate(Vector3.left * BossManager.Instance.speed * Time.deltaTime);
                Debug.Log("LeftMove");
            }
            else
            {
                transform.localScale = new Vector3(-10f, 10f, 1f);
                transform.Translate(Vector3.right * BossManager.Instance.speed * Time.deltaTime);
                Debug.Log("RightMove");
            }
            if (dis < 5f)
            {
                anim.SetFloat("Speed", 0);
                state = Boss_State.ATTACK;
            }
            if (dis > 10f && dis < 20f)
            {
                anim.SetFloat("Speed", 0);
                state = Boss_State.CAST;
            }
            if(dis > 30f)
            {
                state = Boss_State.IDLE;
            }
            if(dis > 20f && dis < 30f)
            {
                state = Boss_State.TELEPORT;
            }
        }
    }
    private void Attack()
    {
        if(!isAttacking && AttackCoolTime == 1f)
        {
            AttackCoolTime = 0;
            isAttacking = true;
            Debug.Log("Attack");
            BossManager.Instance.speed = 0f;
            anim.SetBool("Attack", true);
            SoundManager.instance.SFXPlay("BossAttack", clip[1]);
            StartCoroutine(AttackInEnd());
        }
        float dis = Vector3.Distance(player.transform.position, transform.position);
        isTeleport = false;
        if (dis > 4f && dis < 10f)
        {
            StartCoroutine(AttackEnd());
            BossManager.Instance.speed = 1.5f;
            anim.SetBool("Attack", false);
            state = Boss_State.CAST;
        }
    }
    IEnumerator AttackInEnd()
    {
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("Attack", false);
        isAttacking = false;
        StartCoroutine(AttackInEnd());
    }
    IEnumerator AttackEnd()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }

    // * BossHitBox Amiation set active
    public void MeleeAttack()
    {
        StartCoroutine(BossHitBoxEnd());
    }
    IEnumerator BossHitBoxEnd()
    {
        BossHitBox.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        BossHitBox.SetActive(false);
    }
    private void Cast()
    {
        if(!isCast && CastSkilleCoolTime == 1f)
        {
            isCast = true;
            CastSkilleCoolTime = 0f;
            anim.SetTrigger("Cast");
            SoundManager.instance.SFXPlay("BossSpell", clip[2]);
            StartCoroutine(BossSkillCreate());
            BossManager.Instance.speed = 0;
            StartCoroutine(CastEnd());
            if (player.transform.position.x - transform.position.x < 0) // 플레이어가 보스 왼쪽에있을때
            {
                transform.localScale = new Vector3(10f, 10f, 1f);
                Debug.Log("LeftCast");
            }
            else
            {
                transform.localScale = new Vector3(-10f, 10f, 1f);
                Debug.Log("RightCast");
            }
            float dis = Vector3.Distance(player.transform.position, transform.position);
            if (dis < 8f)
            {
                isCast = false;
                state = Boss_State.ATTACK;
            }
            if (dis > 15f)
            {
                state = Boss_State.IDLE;
            }
            Debug.Log("Cast");
        }
        
    }
    IEnumerator BossCastEndMove()
    {
        yield return new WaitForSeconds(1.1f);
        BossManager.Instance.speed = 1.5f;
    }
    IEnumerator BossSkillCreate()
    {
        yield return new WaitForSeconds(1f);
        if(GameObject.Find("Player").GetComponent<Player>().isJumping == true)
        {
            if(player.transform.position.x - transform.position.x > 0 && !isTeleport) // 플레이어가 보스 오른쪽에있을때
            {
                Instantiate(BossSkill, player.transform.position + new Vector3(3f, -1.5f, 0), Quaternion.identity);
                Instantiate(BossSkill, player.transform.position + new Vector3(-2f, -1.5f, 0), Quaternion.identity);
                Instantiate(BossSkill, player.transform.position + new Vector3(7f, -1.5f, 0), Quaternion.identity);
                Instantiate(BossSkill, player.transform.position + new Vector3(11f, -1.5f, 0), Quaternion.identity);
                Instantiate(BossSkill, player.transform.position + new Vector3(-7f, -1.5f, 0), Quaternion.identity);
                Debug.Log("rightfalse");
            }
            if(player.transform.position.x - transform.position.x < 0 && !isTeleport)
            {
                Debug.Log("ddd");
                Instantiate(BossSkill, player.transform.position + new Vector3(3f, -1.5f, 0), Quaternion.identity);
                Instantiate(BossSkill, player.transform.position + new Vector3(-2f, -1.5f, 0), Quaternion.identity);
                Instantiate(BossSkill, player.transform.position + new Vector3(7f, -1.5f, 0), Quaternion.identity);
                Instantiate(BossSkill, player.transform.position + new Vector3(11f, -1.5f, 0), Quaternion.identity);
                Instantiate(BossSkill, player.transform.position + new Vector3(-7f, -1.5f, 0), Quaternion.identity);
                Debug.Log("leftfalse");
            }
        }
        else if(GameObject.Find("Player").GetComponent<Player>().isJumping == false && !isTeleport)
        {
            Instantiate(BossSkill, player.transform.position + new Vector3(3f, -3.5f, 0), Quaternion.identity);
            Instantiate(BossSkill, player.transform.position + new Vector3(5f, -3.5f, 0), Quaternion.identity);
            Instantiate(BossSkill, player.transform.position + new Vector3(9f, -3.5f, 0), Quaternion.identity);
            Instantiate(BossSkill, player.transform.position + new Vector3(-1f, -3.5f, 0), Quaternion.identity);
            Instantiate(BossSkill, player.transform.position + new Vector3(-5f, -3.5f, 0), Quaternion.identity);
            Debug.Log("truejump");
        }
        
    }
    IEnumerator CastEnd()
    {
        yield return new WaitForSeconds(1f);
        isCast = false;  
    }
    void Teleport()
    {
        if(!isTeleport)
        {
            isTeleport = true;
            SoundManager.instance.SFXPlay("BossTeleport", clip[0]);
            Instantiate(BossTeleportEffect, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            spriteRenderer.enabled = false;
            capsulecollider2d.enabled = false;
            StartCoroutine(BossTeleport());
        }
        
        Debug.Log("Teleport");
        float dis = Vector3.Distance(player.transform.position, transform.position);
        if(dis < 4f)
        {
            state = Boss_State.ATTACK;
        }
    }
    IEnumerator BossTeleport()
    {
        yield return new WaitForSeconds(1f);
        spriteRenderer.enabled = true;
        capsulecollider2d.enabled = true;
        transform.position = player.transform.position + new Vector3(1f, 0f, 0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
        {
            return;
        }
        if (collision.CompareTag("PlayerHitBox") && BossManager.Instance.currentHp >= 0)
        {
            float otherX = collision.transform.parent.position.x;
            float myX = transform.position.x;
            Vector3 nuckBackPos = new Vector3(myX - otherX, 0, 0);
            GetComponent<Rigidbody2D>().AddForce(nuckBackPos.normalized * 20);

            int tempDamage = PlayerManager.Instance.GetDamage();
            BossManager.Instance.currentHp -= tempDamage;

            anim.SetTrigger("Hit");

            GameObject obj = Instantiate(damageText, collision.transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            obj.GetComponent<DamageText>().damage = tempDamage;


            if (BossManager.Instance.currentHp <= 0)
            {
                Die();
            }
            Debug.Log("PlayerHitBox");
        }
        if (collision.CompareTag("Explosion") && BossManager.Instance.currentHp >= 0)
        {
            
            int tempDamage = PlayerManager.Instance.ExplosionDamage();
            BossManager.Instance.currentHp -= tempDamage;

            anim.SetTrigger("Hit");

            GameObject obj = Instantiate(damageText, collision.transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            obj.GetComponent<DamageText>().damage = tempDamage;

            Destroy(collision.gameObject);
            if(BossManager.Instance.currentHp <= 0)
            {
                Die();
            }
            Debug.Log("Explosion");
        }
        if(collision.CompareTag("LightAttack") && BossManager.Instance.currentHp >= 0)
        {
            int tempDamage = PlayerManager.Instance.LightningDamage();
            BossManager.Instance.currentHp -= tempDamage;
            anim.SetTrigger("Hit");
            GameObject obj = Instantiate(damageText, collision.transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
            obj.GetComponent<DamageText>().damage = tempDamage;
            if (BossManager.Instance.currentHp <= 0)
            {
                Die();
            }
        }
        
    }
    void Die()
    {
        isDead = true;
        BossManager.Instance.speed = 0;
        Debug.Log("dIE");
        player.GetComponent<Player>().BossHpGameObject.SetActive(false);
        PlayerManager.Instance.gold += 1000;
        Destroy(gameObject);
    }
    IEnumerator DestroyBoss()
    {
        yield return new WaitForSeconds(5f);
    }
}
