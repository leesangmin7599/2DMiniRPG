using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject player;
    public GameObject hitBox;
    public GameObject collisionbox;
    public GameObject effect; //점프이펙트
    public GameObject RightJumpeffect; //점프이펙트
    public GameObject LeftJumpeffect; //점프이펙트
    public GameObject RightExplosioneffect; // 스킬
    public GameObject LeftExplosioneffect; // 스킬
    public GameObject damageText;
    public GameObject InventoryPanel; // 인벤토리 실행시킬 GameObject
    public GameObject StatusGameObject; // 상태창을 실행시킬 GameObject
    public GameObject SkillBookGameObject;
    public GameObject ReinforceGameObject;
    public GameObject ShopGameObject;
    public GameObject TextGameObject;
    public GameObject SettingObject;
    public GameObject LightingEffect;
    public GameObject RightLightingEffect;
    public GameObject PlayerDieGameObject;
    public GameObject BossHpGameObject;
    public GameObject BossDieGoldObject;
    Vector3 destination;
    public Image JumpImage;
    public Image healthImage;
    public Image BossHealthImage;
    public Image ManaImage;
    public Image ExplosionImage;
    public Image SildImage;
    public Image LightningImage;
    public Image LeftTophealthImage;
    public Image LeftTopManaImage;
    public Image LeftTopStaminaImage;
    public InventoryUI inven;
    public QuickSlotUI quickslotui;
    public Text LevelText;
    public GameObject Fadeinout;
    public GameObject PlayerDiePanelGameObject;
    public Image PanelImage;
    public Image PlayerDiePanelImage;
    GameObject Cameratest;



    public int comboCount = 0;
    public int JumpcomboCount = 0;
    public float maxComboTime = 1.5f;
    public float comboTimer = 0f;
    public bool isAttacking = false;
    public bool isJumping;
    public int jumpCount = 2;
    public float dodgeDuration = 0.5f; // 회피 무적 지속 시간
    public bool isDodging = false; // 회피 중인지 여부를 저장하는 변수
    public bool isCast = false;
    private float dodgeStartTime; // 회피 시작 시간을 저장하는 변수
    public float JumpcoolTime = 1f;
    public float maxJumpcoolTime = 1f;
    public float SildcoolTime = 1f;
    public float maxSildcoolTime = 1f;
    public float ExplosioncoolTime = 1f;
    public float maxExplosioncoolTime = 1f;
    public float LightningAttack = 1f;
    public float maxLightningCoolTime = 1f;
    public bool isDead = false;
    public bool ismove = false;
    public bool activeInventory = false;
    public bool activeStatus = false;
    public bool activeSkillbook = false;
    public bool activeSetting = false;
    public bool isReinforceNPC = false;
    public bool isShopNPC = false;
    public bool isCityportal = false;
    public bool isTutorialInCityGoPortals = false;
    public bool isCityinFirstFieldGoPortal = false;
    public bool isFirstFieldInCityGoPortal = false;
    public bool isFirstFieldInSecondFieldGoPortal = false;
    public bool isSecondFieldInFirstFieldGoPortal = false;
    public bool isSecondFieldBossFieldGoPortal = false;
    public bool isBossFieldInSecondFieldGoPortal = false;
    public bool isBossFieldInCityGoPortal = false;
    public bool isCine = false;
    public bool isLightning = false;
    public bool isscreen = false;


    SpriteRenderer spriteRender;
    CapsuleCollider2D cap2D;
    Animator anim;
    Rigidbody2D rid;
    Transform postr;
    GameObject Boss;
    Item item;
    public AudioClip[] clip;

    Player_State state = Player_State.IDLE;

    public Button ExitSetting;
    public Button PlayerResurrectionButton;
    public Button BossDieGetGoldButton;
    public Button ExitShopButton;
    public Button ExitReinforceButton;
    public GameObject MapNameObject;
    public Image MapNameTopImage;
    public Image MapNameUnderImage;
    public Text MapNameText;
    public Text ItemName;
    public GetItemScrollView getitemscrollview;
    ReinforceUI reinfroceui;

    // Start is called before the first frame update
    void Start()
    {
        //item = GetComponent<Item>();
        Fadeinout.SetActive(true);
        StartCoroutine(StartFadeOut());
        anim = GetComponent<Animator>();
        rid = GetComponent<Rigidbody2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        cap2D = GetComponent<CapsuleCollider2D>();
        reinfroceui = FindObjectOfType<ReinforceUI>();
        hitBox.SetActive(false);
        isAttacking = false;
        jumpCount = 0;
        Boss = GameObject.FindGameObjectWithTag("Boss");
        StartCoroutine(PlusHealth());
        StartCoroutine(PlusMana());
        StartCoroutine(PlusStamina());
        JumpImage.enabled = false;
        InventoryPanel.SetActive(activeInventory);
        StatusGameObject.SetActive(activeStatus);
        SkillBookGameObject.SetActive(activeSkillbook);
        SettingObject.SetActive(activeSetting);
        ReinforceGameObject.SetActive(false);
        ShopGameObject.SetActive(false);
        TextGameObject.SetActive(false);
        BossHpGameObject.SetActive(false);
        BossDieGoldObject.SetActive(false);
        MapNameObject.SetActive(false);
        PlayerResurrectionButton.onClick.AddListener(Playerresurrectionbutton);
        //GKey.SetActive(false);
        //FirFieldInCityPortalGKey.SetActive(false);
        //FirFieldInSecPortalGKey.SetActive(false);
        //Fadeinout.SetActive(false);
        DontDestroyOnLoad(gameObject);
        Cameratest = GameObject.Find("Camera");
        //BossDieGetGold();
        BossDieGetGoldButton.onClick.AddListener(Bossdiegetgoldbutton);
        ExitShopButton.onClick.AddListener(ShopExitButton);
        ExitReinforceButton.onClick.AddListener(ReinforceExitButton);

        if(SceneManager.GetActiveScene().name =="TutorialMap")
        {
            Debug.Log("Tutor");
            MapNameText.text = "튜토리얼 지역";
            StartCoroutine(MapnameFadeOut());
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        ItemName.enabled = false;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "City")
        {
            Debug.Log("city");
            StartCoroutine(MapnameFadeOut());
            MapNameText.text = "마을";
        }
        if(scene.name == "FirstField")
        {
            Debug.Log("FirstField");
            StartCoroutine(MapnameFadeOut());
            MapNameText.text = "첫번째 사냥터";
        }
        if (scene.name == "SceondField")
        {
            Debug.Log("SceondField");
            StartCoroutine(MapnameFadeOut());
            MapNameText.text = "두번째 사냥터";
        }
        if (scene.name == "BossField")
        {
            Debug.Log("BossField");
            StartCoroutine(MapnameFadeOut());
            MapNameText.text = "보스 지역";
        }

    }
    private void SceneChange()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("LoadScene");
            SceneManager.LoadScene("City");
            //DontDestroyOnLoad(Character);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene("Firstmap");
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case Player_State.IDLE:
                Idle();
                Debug.Log("Idle");
                break;
            case Player_State.MOVE:
                Move();
                Debug.Log("MOVE");
                break;
            case Player_State.ATTACK:
                Attack();
                Debug.Log("ATTACK");
                break;
            case Player_State.DIE:
                break;
            case Player_State.Sild:
                Sild();
                Debug.Log("Sild");
                break;
            case Player_State.Cast:
                Cast();
                Debug.Log("Cast");
                break;
            default:
                break;
        }
        Jump();
        LightingAttack();
        InventoryOnOff();
        StatusOnOff();
        SkillBookOnOff();
        StartCoroutine(JumpCoolTimePlus());
        StartCoroutine(SildCoolTimePlus());
        StartCoroutine(ExplosionCoolTimePlus());
        StartCoroutine(LightningPlus());
        JumpImage.fillAmount = JumpcoolTime / maxJumpcoolTime;
        ExplosionImage.fillAmount = ExplosioncoolTime / maxExplosioncoolTime;
        SildImage.fillAmount = SildcoolTime / maxSildcoolTime;
        LightningImage.fillAmount = LightningAttack / maxLightningCoolTime;
        healthImage.fillAmount = PlayerManager.Instance.currentHp / PlayerManager.Instance.maxHp;
        BossHealthImage.fillAmount = BossManager.Instance.currentHp / BossManager.Instance.maxHp;
        ManaImage.fillAmount = PlayerManager.Instance.currentMana / PlayerManager.Instance.MaxMana;
        LeftTophealthImage.fillAmount = PlayerManager.Instance.currentHp / PlayerManager.Instance.maxHp;
        LeftTopManaImage.fillAmount = PlayerManager.Instance.currentMana / PlayerManager.Instance.MaxMana;
        LeftTopStaminaImage.fillAmount = PlayerManager.Instance.currentStamina / PlayerManager.Instance.MaxStamina;

        SpeedMove();
        LevelUp();
        SceneChange();
        ReinforceNPCOnOff();
        ShopNPCOnOff();
        SettinOnOff();
        CityGoPortal();
        CityInFirstFieldGoPortal();
        FirstFieldInCityGoPortal();
        FirstFieldInSecondFieldGoPortal();
        SecondFieldInFirstFieldGoPortal();
        SecondFieldInBossFieldGoPortal();
        BossFieldInSecondFieldGoPortal();
        BossFieldInCityportal();
        BossHp();
        LevelText.text = PlayerManager.Instance.Level.ToString();

        
        ExitSetting.onClick.AddListener(ExitSettingbutton);
        

    }
    IEnumerator LightningPlus()
    {
        yield return new WaitForSeconds(0.5f);
        LightningAttack += 0.0002f;
        if (LightningAttack >= 1)
        {
            LightningAttack = 1f;
        }
    }
    IEnumerator PlusHealth()
    {
        while (true) // true일때 중괄호 실행
        {
            yield return new WaitForSeconds(1f);
            if (!isDead)
            {
                PlayerManager.Instance.currentHp += 1f;
                if (PlayerManager.Instance.currentHp >= PlayerManager.Instance.maxHp)
                {
                    PlayerManager.Instance.currentHp = PlayerManager.Instance.maxHp;
                }
            }
        }
    }
    IEnumerator PlusMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            PlayerManager.Instance.currentMana += 1f;
            if (PlayerManager.Instance.currentMana >= PlayerManager.Instance.MaxMana)
            {
                PlayerManager.Instance.currentMana = PlayerManager.Instance.MaxMana;
            }
        }
    }
    IEnumerator PlusStamina()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            PlayerManager.Instance.currentStamina += 1f;
            if (PlayerManager.Instance.currentStamina >= PlayerManager.Instance.MaxStamina)
            {
                PlayerManager.Instance.currentStamina = PlayerManager.Instance.MaxStamina;
            }
        }
    }
    IEnumerator ExplosionCoolTimePlus()
    {
        yield return new WaitForSeconds(0.5f);
        ExplosioncoolTime += 0.0003f;
        if (ExplosioncoolTime >= 1)
        {
            ExplosioncoolTime = 1f;
        }
    }
    private void Cast()
    {
        if (PlayerManager.Instance.currentMana >= 30 && ExplosioncoolTime >= 1)
        {
            PlayerManager.Instance.currentMana -= 30;
            SoundManager.instance.SFXPlay("Cast", clip[5]);
            ExplosioncoolTime = 0;
            isCast = true;
            anim.SetTrigger("Cast");
            if (spriteRender.flipX == false)
            {
                Instantiate(RightExplosioneffect, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            if (spriteRender.flipX == true)
            {
                Instantiate(LeftExplosioneffect, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            }
        }
        StartCoroutine(EndCasting());
        state = Player_State.IDLE;
    }
    IEnumerator EndCasting() // 위에있는 Cast에 있는 isCast를 0.4f뒤에 false로 만들어주기위해 만든 코루틴
    {
        yield return new WaitForSeconds(0.4f);
        isCast = false;
    }
    private void Sild()
    {
        float h = Input.GetAxisRaw("Horizontal");
        PlayerManager.Instance.speed = 6f;
        transform.Translate(Vector3.right * h * PlayerManager.Instance.speed * Time.deltaTime);
        if (SildcoolTime >= 1)
        {
            SoundManager.instance.SFXPlay2("Sild", clip[4]);
            Debug.Log(PlayerManager.Instance.speed);
            SildcoolTime = 0;

            isDodging = true;
            anim.SetTrigger("Sild");
            StartCoroutine(EndDodging());
        }
        //스타트코르틴으로

    }
    IEnumerator SildCoolTimePlus()
    {
        yield return new WaitForSeconds(0.5f);
        SildcoolTime += 0.0004f;
        if (SildcoolTime >= 1)
        {
            SildcoolTime = 1f;
        }
    }
    IEnumerator EndDodging() // 위에있는 Sild에 있는 isDodging 0.4f뒤에 false로 만들어주기위해 만든 코루틴
    {
        yield return new WaitForSeconds(0.8f);
        PlayerManager.Instance.speed = 3f;
        isDodging = false;
        state = Player_State.IDLE;
    }
    private void Idle()
    {
        PlayerManager.Instance.speed = 0;
        ismove = false;
        anim.SetFloat("Speed", 0);
        if (Input.GetButton("Horizontal"))
        {
            state = Player_State.MOVE;
        }
        if (!isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                state = Player_State.ATTACK;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && SildcoolTime >= 1f)
        {
            state = Player_State.Sild;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            state = Player_State.Cast;
        }
    }
    void SpeedMove()
    {
        if (isJumping && !isAttacking && !isCast && !isDead && PlayerManager.Instance.currentStamina >= 0 && Input.GetKey(KeyCode.LeftShift) && !isLightning)
        {
            float h = Input.GetAxisRaw("Horizontal");
            PlayerManager.Instance.speed = 6f;
            transform.Translate(Vector3.right * h * PlayerManager.Instance.speed * Time.deltaTime);
            PlayerManager.Instance.currentStamina -= 0.1f;

        }
    }
    void LightingAttack()
    {
        if(!isAttacking && !isCast && !isDead && isJumping && PlayerManager.Instance.currentMana >= 30 && Input.GetKeyDown(KeyCode.X) && LightningAttack >= 1f)
        {
            Debug.Log("LightingAttack");
            SoundManager.instance.SFXPlay2("Lightningsound", clip[8]);
            isLightning = true;
            isDodging = true;
            LightningAttack = 0;
            PlayerManager.Instance.currentMana -= 30;
            if (spriteRender.flipX == false)
            {
                spriteRender.enabled = false;
                Instantiate(LightingEffect, transform.position + new Vector3(2f, -0.5f, 0), Quaternion.identity);
                StartCoroutine(Lightningtrue());

            }
            if (spriteRender.flipX == true)
            {
                spriteRender.enabled = false;
                Instantiate(RightLightingEffect, transform.position + new Vector3(-2f, -0.5f, 0), Quaternion.identity);
                StartCoroutine(RightLingning());
            }
        }    
    }
    IEnumerator Lightningtrue()
    {
        yield return new WaitForSeconds(1f);
        isDodging = false;
        isLightning = false;
        spriteRender.enabled = true;
        this.transform.position = transform.position + new Vector3(5f, 0, 0);
    }
    IEnumerator RightLingning()
    {
        yield return new WaitForSeconds(1f);
        isDodging = false;
        isLightning = false;
        spriteRender.enabled = true;
        this.transform.position = transform.position + new Vector3(-5f, 0, 0);
    }
    private void Move()
    {
        if (!isAttacking && !isCast && !isDead && !isLightning)
        {
            ismove = true;
            float h = Input.GetAxisRaw("Horizontal");
            anim.SetFloat("Speed", 3);
            PlayerManager.Instance.speed = 5f;
            transform.Translate(Vector3.right * h * PlayerManager.Instance.speed * Time.deltaTime);
            if (h < 0) // 음수라면 왼쪽으로 이동했다면
            {
                spriteRender.flipX = true;
                hitBox.transform.position = transform.position + new Vector3(-0.7f, 0f, 0);
                collisionbox.transform.position = transform.position + new Vector3(-0.7f, 0f, 0);
            }
            else if (h > 0)
            {
                spriteRender.flipX = false;
                hitBox.transform.position = transform.position + new Vector3(0.7f, 0f, 0);
                collisionbox.transform.position = transform.position + new Vector3(0.7f, 0f, 0);
            }
            else
            {
                state = Player_State.IDLE;
            }
            if (Input.GetKeyDown(KeyCode.LeftControl) && !isAttacking)
            {
                state = Player_State.ATTACK;
            }
            if (!isJumping)
            {
                anim.SetFloat("Speed", 0);
            }
            if (Input.GetKeyDown(KeyCode.Space) && !isAttacking && isJumping && SildcoolTime >= 1f)
            {
                state = Player_State.Sild;
                //PlayerManager.Instance.speed = 3.5f;
            }
            if (Input.GetKeyDown(KeyCode.Z) && !isAttacking && isJumping)
            {
                state = Player_State.Cast;
            }
        }
    }

    void Attack()
    {
        if (isJumping && !isLightning) // 점프중이지 않을때
        {
            switch (comboCount) // 바닥에서의 공격 구현
            {
                case 0:
                    isAttacking = true;
                    anim.SetTrigger("Attack");
                    comboCount++;
                    StartCoroutine(Attacksound());
                    StartCoroutine(AttackEnd());
                    break;
                case 1:
                    isAttacking = true;
                    anim.SetTrigger("Attack2");
                    comboCount++;
                    StartCoroutine(Attacksound1());
                    StartCoroutine(AttackEnd());
                    break;
                case 2:
                    isAttacking = true;
                    anim.SetTrigger("Attack3");
                    comboCount = 0;
                    comboTimer = 0;
                    StartCoroutine(Attacksound2());
                    StartCoroutine(AttackEnd());
                    break;
                default:
                    break;
            }
        }
        if (!isJumping && !isLightning) // 점프중일때 
        {
            float h = Input.GetAxisRaw("Horizontal");
            if (h < 0 || h > 0) // 이동키 를 입력하면
            {
                switch (JumpcomboCount) // 공중공격 구현
                {
                    case 0:
                        isAttacking = true;
                        anim.SetTrigger("JumpAttack");
                        JumpcomboCount++;
                        StartCoroutine(Attacksound1());
                        StartCoroutine(AttackEnd());
                        break;
                    case 1:
                        isAttacking = true;
                        anim.SetTrigger("JumpAttack2");
                        JumpcomboCount = 0;
                        comboTimer = 0;
                        StartCoroutine(Attacksound2());
                        StartCoroutine(AttackEnd());
                        break;
                    default:
                        break;
                }
            }

        }

        state = Player_State.IDLE;
        if (comboCount > 0)
        {
            comboTimer += Time.deltaTime;
            if (comboTimer >= 2.5f)
            {
                ResetCombo();
            }
        }
    }
    IEnumerator Attacksound()
    {
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay("Attack", clip[0]);
    }
    IEnumerator Attacksound1()
    {
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay("Attack", clip[1]);
    }
    IEnumerator Attacksound2()
    {
        yield return new WaitForSeconds(0.1f);
        SoundManager.instance.SFXPlay("Attack", clip[2]);
    }
    void ResetCombo()
    {
        comboCount = 0;
        comboTimer = 0f;
    }
    IEnumerator AttackEnd() // 0.4초뒤에 isAttacking을 false로 만들어주기위해 만든 코루틴
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }
    public void OnAttackStart() // * AnimationEvent
    {
        hitBox.SetActive(true);
    }
    public void OnAttackEnd() // * AnimationEvent
    {
        hitBox.SetActive(false);
    }
    private void Jump()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (jumpCount > 0)
            {
                if (jumpCount == 2)
                {
                    anim.SetBool("Jump", false);
                    rid.AddForce(new Vector3(0, 300, 0));
                    Instantiate(effect, transform.position + new Vector3(0,  -0.8f, 0), Quaternion.identity);
                    SoundManager.instance.SFXPlay("Jump", clip[3]);
                    isJumping = false;
                }
                else
                {
                    if (JumpcoolTime >= 1)
                    {
                        JumpcoolTime = 0;
                        anim.SetBool("Jump", false);
                        rid.AddForce(new Vector3(0, 300, 0));
                        rid.velocity = new Vector2(horizontalInput * 1, rid.velocity.y);
                        SoundManager.instance.SFXPlay("Jump", clip[3]);
                        isJumping = false;
                        JumpImage.enabled = true;
                        StartCoroutine(JumpImageOnOff());
                        if (horizontalInput > 0)
                        {
                            Instantiate(RightJumpeffect, transform.position + new Vector3(-0.1f, 0, 0), Quaternion.Euler(new Vector3(0, 0, -90f)));
                        }
                        if (horizontalInput < 0)
                        {
                            Instantiate(LeftJumpeffect, transform.position + new Vector3(0.1f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 90f)));
                        }
                    }
                }
                jumpCount--;
            }
        }
    }
    IEnumerator JumpImageOnOff()
    {
        yield return new WaitForSeconds(2f);
        JumpImage.enabled = false;
    }
    IEnumerator JumpCoolTimePlus()
    {
        yield return new WaitForSeconds(0.5f);
        JumpcoolTime += 0.002f;
        if (JumpcoolTime >= 1)
        {
            JumpcoolTime = 1f;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 2;
            JumpcomboCount = 0;
            isJumping = true;
            anim.SetBool("Jump", true);
        }
        if (collision.gameObject.CompareTag("PotionItem"))
        {
            Debug.Log("POTION");
            SoundManager.instance.SFXPlay("Jump", clip[6]);
            inven.AcquireItem(collision.gameObject.GetComponent<ItemPickUp>().item);
            quickslotui.AcquireItem(collision.gameObject.GetComponent<ItemPickUp>().item);
            getitemscrollview.AddShowItem(collision.gameObject.GetComponent<ItemPickUp>().item);
            Destroy(collision.gameObject);
           // StartCoroutine(ItemNameFadeOut());
        }
        if (collision.gameObject.CompareTag("Equipment"))
        {
            Debug.Log("Equipment");
            SoundManager.instance.SFXPlay("Jump", clip[6]);
            inven.AcquireItem(collision.gameObject.GetComponent<ItemPickUp>().item);
            getitemscrollview.AddShowItem(collision.gameObject.GetComponent<ItemPickUp>().item);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Jwerl"))
        {
            Debug.Log("Jwerl");
            SoundManager.instance.SFXPlay("Jump", clip[6]);
            inven.AcquireItem(collision.gameObject.GetComponent<ItemPickUp>().item);
            getitemscrollview.AddShowItem(collision.gameObject.GetComponent<ItemPickUp>().item);
            Destroy(collision.gameObject);
        }
    }
    IEnumerator ItemNameDie()
    {
        float feadCount = 1;
        while (feadCount >= 0f)
        {
            feadCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            ItemName.color = new Color(255, 255, 255, feadCount);
        }
        yield return new WaitForSeconds(0.2f);
        ItemName.enabled = false;
    }

    IEnumerator ItemNameFadeOut() // 화면 점점 밝게
    {
        
        
        float faedCount = 1;
        while (faedCount >= 0f)
        {
            faedCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            ItemName.color = new Color(255, 255, 255, faedCount);
        }
        yield return new WaitForSeconds(0.2f);
        ItemName.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("EnemyHitBox") && !isDodging && PlayerManager.Instance.currentHp >= 0 && !isDead)
        {
            anim.SetTrigger("Hit");
            float otherX = collision.transform.parent.position.x;
            float myX = transform.position.x;
            Vector3 nuckBackPos = new Vector3(myX - otherX, 0, 0);
            GetComponent<Rigidbody2D>().AddForce(nuckBackPos.normalized * 40);

            int tempDamage = EnemyManager.Instance.GetDamage();
            PlayerManager.Instance.currentHp -= tempDamage;

            GameObject obj = Instantiate(damageText, collision.transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            obj.GetComponent<PlayerInDamageText>().damage = tempDamage;
            if (PlayerManager.Instance.currentHp <= 0)
            {
                PlayerManager.Instance.currentHp = 0;
                //anim.ResetTrigger("Hit");
                Die();
            }
            if (isDead)
            {
                return;
            }
        }
        if (collision.CompareTag("Arrow") && !isDodging && PlayerManager.Instance.currentHp >= 0 && !isDead)
        {
            int tempDamage = EnemyManager.Instance.GetDamage();
            PlayerManager.Instance.currentHp -= tempDamage;

            GameObject obj = Instantiate(damageText, collision.transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            obj.GetComponent<PlayerInDamageText>().damage = tempDamage;

            Destroy(collision.gameObject);
            if (PlayerManager.Instance.currentHp <= 0)
            {
                PlayerManager.Instance.currentHp = 0;
                Die();
            }
        }
        if (collision.CompareTag("BossSkill") && !isDodging && PlayerManager.Instance.currentHp >= 0 && !isDead)
        {
            int tempDamage = BossManager.Instance.BossSkillGetDamage();
            PlayerManager.Instance.currentHp -= tempDamage;

            GameObject obj = Instantiate(damageText, transform.position + new Vector3(0f, 1f, 0), Quaternion.identity);
            obj.GetComponent<PlayerInDamageText>().damage = tempDamage;

            if (PlayerManager.Instance.currentHp <= 0)
            {
                PlayerManager.Instance.currentHp = 0;
                Die();
            }
        }
        if (collision.CompareTag("BossHitBox") && !isDead && PlayerManager.Instance.currentHp >= 0 && !isDead)
        {
            float otherX = collision.transform.parent.position.x;
            float myX = transform.position.x;
            Vector3 nuckBackPos = new Vector3(myX - otherX, 0, 0);
            GetComponent<Rigidbody2D>().AddForce(nuckBackPos.normalized * 40);

            int tempDamage = BossManager.Instance.BossNormalGetDamage();
            PlayerManager.Instance.currentHp -= tempDamage;

            GameObject obj = Instantiate(damageText, transform.position + new Vector3(0f, 1f, 0), Quaternion.identity);
            obj.GetComponent<PlayerInDamageText>().damage = tempDamage;

            if (PlayerManager.Instance.currentHp <= 0)
            {
                PlayerManager.Instance.currentHp = 0;
                Die();
                
            }
        }
        if (collision.CompareTag("ReinforceNPC"))
        {
            isReinforceNPC = true;
            TextGameObject.SetActive(true);
        }
        if (collision.CompareTag("ShopNPC"))
        {
            isShopNPC = true;
            TextGameObject.SetActive(true);
        }
        if(collision.CompareTag("TutorialInCityGoPortal"))
        {
            Debug.Log("TutorialInCityGoPortal");
            isTutorialInCityGoPortals = true;
            TextGameObject.SetActive(true);
        }
        if(collision.CompareTag("CityInFirstFieldGoPortal"))
        {
            Debug.Log("CityInFirstFieldGoPortal");
            isCityinFirstFieldGoPortal = true;
            TextGameObject.SetActive(true);
        }
        if(collision.CompareTag("FirstFieldInCityGoPortal"))
        {
            Debug.Log("FirstFieldInCityGoPortal");
            isFirstFieldInCityGoPortal = true;
            TextGameObject.SetActive(true);
        }
        if (collision.CompareTag("FirstFieldInSecondFieldGoPortal"))
        {
            Debug.Log("FirstFieldInSecondFieldGoPortal");
            isFirstFieldInSecondFieldGoPortal = true;
            TextGameObject.SetActive(true);
        }
        if (collision.CompareTag("SecondFieldInFirstFieldGoPortal"))
        {
            Debug.Log("SecondFieldInFirstFieldGoPortal");
            isSecondFieldInFirstFieldGoPortal = true;
            TextGameObject.SetActive(true);
        }
        if(collision.CompareTag("SecondFieldInBoosFieldGoPortal"))
        {
            Debug.Log("SecondFieldInBoosFieldGoPortal");
            isSecondFieldBossFieldGoPortal = true;
            TextGameObject.SetActive(true);
        }
        if(collision.CompareTag("BossFieldInSecondFieldGoPortal"))
        {
            Debug.Log("BossFieldInSecondFieldGoPortal");
            isBossFieldInSecondFieldGoPortal = true;
            TextGameObject.SetActive(true);
        }
        if(collision.CompareTag("BossFieldInCityPortal"))
        {
            Debug.Log("BossFieldInCityPortal");
            isBossFieldInCityGoPortal = true;
            TextGameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ReinforceNPC"))
        {
            isReinforceNPC = false;
            ReinforceGameObject.SetActive(false);
            TextGameObject.SetActive(false);
        }
        if (collision.CompareTag("ShopNPC"))
        {
            isShopNPC = false;
            ShopGameObject.SetActive(false);
            TextGameObject.SetActive(false);
        }
        if (collision.CompareTag("TutorialInCityGoPortal"))
        {
            Debug.Log("TutorialInCityGoPortal");
            isTutorialInCityGoPortals = false;
            TextGameObject.SetActive(false);
        }
        if (collision.CompareTag("CityInFirstFieldGoPortal"))
        {
            Debug.Log("CityInFirstFieldGoPortal");
            isCityinFirstFieldGoPortal = false;
            TextGameObject.SetActive(false);
        }
        if (collision.CompareTag("FirstFieldInCityGoPortal"))
        {
            Debug.Log("FirstFieldInCityGoPortal");
            isFirstFieldInCityGoPortal = false;
            TextGameObject.SetActive(false);
        }
        if (collision.CompareTag("FirstFieldInSecondFieldGoPortal"))
        {
            Debug.Log("FirstFieldInSecondFieldGoPortal");
            isFirstFieldInSecondFieldGoPortal = false;
            TextGameObject.SetActive(false);
        }
        if(collision.CompareTag("SecondFieldInFirstFieldGoPortal"))
        {
            Debug.Log("SecondFieldInFirstFieldGoPortal");
            isSecondFieldInFirstFieldGoPortal = false;
            TextGameObject.SetActive(false);
        }
        if (collision.CompareTag("SecondFieldInBoosFieldGoPortal"))
        {
            Debug.Log("SecondFieldInBoosFieldGoPortal");
            isSecondFieldBossFieldGoPortal = false;
            TextGameObject.SetActive(false);
        }
        if (collision.CompareTag("BossFieldInSecondFieldGoPortal"))
        {
            Debug.Log("BossFieldInSecondFieldGoPortal");
            isBossFieldInSecondFieldGoPortal = false;
            TextGameObject.SetActive(false);
        }
        if (collision.CompareTag("BossFieldInCityPortal"))
        {
            Debug.Log("BossFieldInCityPortal");
            isBossFieldInCityGoPortal = false;
            TextGameObject.SetActive(true);
        }
    }
    void Die()
    {
        Debug.Log("Die");
        isDead = true;
        PlayerManager.Instance.speed = 0;
        this.gameObject.layer = 14;
        anim.SetTrigger("Death");
        anim.SetTrigger("HitDie");
        //StartCoroutine(DestroyPlayer());
        StartCoroutine(PlayerdieFadeIn());
        PlayerDieGameObject.SetActive(true);
        BossHpGameObject.SetActive(false);
    }
    IEnumerator DestroyPlayer()
    {
        yield return new WaitForSeconds(2f);
        //SceneManager.LoadScene("City");
        //this.transform.position = new Vector3(0f, -3.8f, 0);
        //anim.SetTrigger("DeathEnd");
        //state = Player_State.IDLE;
        //isDead = false;
        //PlayerManager.Instance.speed = 5;
        //this.gameObject.layer = 3;
    }
    public void InventoryOnOff()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            SoundManager.instance.SFXPlay("UI", clip[7]);
            InventoryPanel.SetActive(activeInventory);
        }
    }
    public void StatusOnOff()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Status");
            SoundManager.instance.SFXPlay("UI", clip[7]);
            activeStatus = !activeStatus;
            if (activeStatus)
            {
                StatusGameObject.SetActive(true);
            }
            else
            {
                StatusGameObject.SetActive(false);
            }
        }
    }
    public void SkillBookOnOff()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            activeSkillbook = !activeSkillbook;
            SoundManager.instance.SFXPlay("UI", clip[7]);
            SkillBookGameObject.SetActive(activeSkillbook);

        }
    }
    public void SettinOnOff()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            activeSetting = !activeSetting;
            SoundManager.instance.SFXPlay("UI", clip[7]);
            SettingObject.SetActive(activeSetting);
        }
    }
    public void ExitSettingbutton()
    {
        SettingObject.SetActive(!activeSetting);
    }
    public void LevelUp()
    {
        if (PlayerManager.Instance.LevelExperience >= PlayerManager.Instance.MaxLevelExperience)
        {
            LevelUpStat();
        }
    }
    public void LevelUpStat()
    {
        PlayerManager.Instance.Level++; // 플레이어 레벨이 오른다면
        PlayerManager.Instance.LevelExperience = 0;
        PlayerManager.Instance.MaxLevelExperience += 10;
        PlayerManager.Instance.SkillPoint++;
        PlayerManager.Instance.damage += 3;
        PlayerManager.Instance.ciriticalPer += 1;
        PlayerManager.Instance.ciriticlaDamage += 0.5f;
        PlayerManager.Instance.maxHp += 10;
        PlayerManager.Instance.MaxMana += 10;
        PlayerManager.Instance.MaxStamina += 10;
    }
    IEnumerator FadeIn() // 화면 점점 어둡게
    {
        Fadeinout.SetActive(true);
        float faedCount = 0;
        while(faedCount <= 1.0f)
        {
            faedCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            PanelImage.color = new Color(0, 0, 0, faedCount);
        }
    }
    IEnumerator FadeOut() // 화면 점점 밝게
    {
        float faedCount = 1;
        while (faedCount >= 0f)
        {
            faedCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            PanelImage.color = new Color(0, 0, 0, faedCount);
        }
        yield return new WaitForSeconds(0.2f);
        Fadeinout.SetActive(false);
    }
    IEnumerator StartFadeOut()
    {
        yield return new WaitForSeconds(1.5f);
        float faedCount = 1;
        while (faedCount >= 0f)
        {
            faedCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            PanelImage.color = new Color(0, 0, 0, faedCount);
        }
        yield return new WaitForSeconds(0.2f);
        Fadeinout.SetActive(false);
    }
    IEnumerator MapnameFadeOut()
    {
        yield return new WaitForSeconds(3f);
        MapNameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        float faedCount = 1;
        while (faedCount >= 0f)
        {
            faedCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            MapNameTopImage.color = new Color(255, 255, 255, faedCount);
            MapNameUnderImage.color = new Color(255, 255, 255, faedCount);
            MapNameText.color = new Color(255, 255, 255, faedCount);
        }
        yield return new WaitForSeconds(0.5f);
        MapNameObject.SetActive(false);
    }
    IEnumerator CitynameFadeOut()
    {
        yield return new WaitForSeconds(14f);
        MapNameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        float faedCount = 1;
        while (faedCount >= 0f)
        {
            faedCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            MapNameTopImage.color = new Color(255, 255, 255, faedCount);
            MapNameUnderImage.color = new Color(255, 255, 255, faedCount);
            MapNameText.color = new Color(255, 255, 255, faedCount);
        }
        yield return new WaitForSeconds(0.5f);
        MapNameObject.SetActive(false);
    }

    IEnumerator PlayerdieFadeIn()
    {
        PlayerDiePanelGameObject.SetActive(true);
        float faedCound = 0;
        while(faedCound <= 0.5f)
        {
            faedCound += 0.01f;
            yield return new WaitForSeconds(0.01f);
            PlayerDiePanelImage.color = new Color(0, 0, 0, faedCound);
        }
    }
    IEnumerator PlayerdieFadeOut()
    {
        float faedCount = 0.5f;
        while(faedCount >= 0)
        {
            faedCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            PlayerDiePanelImage.color = new Color(0, 0, 0, faedCount);
        }
    }
    public void ReinforceNPCOnOff()
    {
        if (Input.GetKeyDown(KeyCode.G) && isReinforceNPC)
        {
            SoundManager.instance.SFXPlay("CilckSound", clip[7]);
            ReinforceGameObject.SetActive(true);
            InventoryPanel.SetActive(false);
            StatusGameObject.SetActive(false);
            SkillBookGameObject.SetActive(false);
            Debug.Log("OUTNPCCONTECT");
        }
    }
    public void ShopNPCOnOff()
    {
        if (Input.GetKeyDown(KeyCode.G) && isShopNPC)
        {
            SoundManager.instance.SFXPlay("CilckSound", clip[7]);
            ShopGameObject.SetActive(true);
            InventoryPanel.SetActive(false);
            StatusGameObject.SetActive(false);
            SkillBookGameObject.SetActive(false);
            Debug.Log("OUTNPCCONTECT");
        }
    }
    public void CityGoPortal()
    {
        if (Input.GetKey(KeyCode.G) && isTutorialInCityGoPortals)
        {
            StartCoroutine(FadeIn());
            StartCoroutine(TutorialInCitygo());
            
        }
    }
    IEnumerator TutorialInCitygo()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("City"); // 이거 나중에 시티로 고치기
        //player.SetActive(false);
        this.transform.position = new Vector3(0f, -3.8f, 0);
        TextGameObject.SetActive(false);
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(0.3f);
        player.SetActive(false);
        yield return new WaitForSeconds(13.3f);
        player.SetActive(true);
    }
    public void CityInFirstFieldGoPortal()
    {
        if(Input.GetKey(KeyCode.G) && isCityinFirstFieldGoPortal)
        {
            StartCoroutine(FadeIn());
            StartCoroutine(CityInFirstFieldgo());
        }
    }
    IEnumerator CityInFirstFieldgo()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("FirstField");
        this.transform.position = new Vector3(0, -3.3f, 0);
        isCityinFirstFieldGoPortal = false;
        TextGameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeOut());
    }
    public void FirstFieldInCityGoPortal()
    {
        if(Input.GetKey(KeyCode.G) && isFirstFieldInCityGoPortal)
        {
            StartCoroutine(FadeIn());
            StartCoroutine(FirstFieldInCitygo());
        }
    }
    IEnumerator FirstFieldInCitygo()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("City");
        this.transform.position = new Vector3(110f, -3.8f, 0);
        isFirstFieldInCityGoPortal = false;
        TextGameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeOut());

    }
    public void FirstFieldInSecondFieldGoPortal()
    {
        if(Input.GetKey(KeyCode.G) && isFirstFieldInSecondFieldGoPortal)
        {
            StartCoroutine(FadeIn());
            StartCoroutine(FirstFieldInSecondFieldGo());
        }
    }
    IEnumerator FirstFieldInSecondFieldGo()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("SceondField");
        this.transform.position = new Vector3(0, -3.3f, 0);
        isFirstFieldInSecondFieldGoPortal = false;
        TextGameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeOut());
    }
    public void SecondFieldInFirstFieldGoPortal()
    {
        if(Input.GetKey(KeyCode.G) && isSecondFieldInFirstFieldGoPortal)
        {
            StartCoroutine(FadeIn());
            StartCoroutine(SecondFieldInFirstFieldGo());
        }
    }
    IEnumerator SecondFieldInFirstFieldGo()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("FirstField");
        this.transform.position = new Vector3(145f, -3.3f, 0);
        isSecondFieldInFirstFieldGoPortal = false;
        TextGameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeOut());

    }
    public void SecondFieldInBossFieldGoPortal()
    {
        if(Input.GetKey(KeyCode.G) && isSecondFieldBossFieldGoPortal)
        {
            StartCoroutine(FadeIn());
            StartCoroutine(SecondFieldInBossFielddGo());
        }
    }
    IEnumerator SecondFieldInBossFielddGo()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("BossField");
        this.transform.position = new Vector3(0, -3.3f, 0);
        isSecondFieldBossFieldGoPortal = false;
        TextGameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeOut());
    }
    public void BossFieldInSecondFieldGoPortal()
    {
        if(Input.GetKey(KeyCode.G) && isBossFieldInSecondFieldGoPortal)
        {
            StartCoroutine(FadeIn());
            StartCoroutine(BossFieldInSecondFieldGo());
        }
    }
    IEnumerator BossFieldInSecondFieldGo()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("SceondField");
        this.transform.position = new Vector3(145f, -3.3f, 0);
        isBossFieldInSecondFieldGoPortal = false;
        TextGameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeOut());
    }
    public void BossFieldInCityportal()
    {
        if(Input.GetKey(KeyCode.G) && isBossFieldInCityGoPortal)
        {
            StartCoroutine(FadeIn());
            StartCoroutine(BossfieldIncityportal());
        }
    }
    IEnumerator BossfieldIncityportal()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("City");
        this.transform.position = new Vector3(110f, -3.8f, 0);
        isBossFieldInCityGoPortal = false;
        TextGameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeOut());
    }
    void Playerresurrectionbutton()
    {
        StartCoroutine(Resurrection());
        Debug.Log("ㅇ");
    }
    IEnumerator Resurrection()
    {
        PlayerDiePanelGameObject.SetActive(false);
        PlayerDieGameObject.SetActive(false);
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("City");
        this.transform.position = new Vector3(0f, -3.8f, 0);
        anim.SetTrigger("DeathEnd");
        state = Player_State.IDLE;
        isDead = false;
        PlayerManager.Instance.speed = 5;
        PlayerManager.Instance.currentHp = PlayerManager.Instance.maxHp;
        this.gameObject.layer = 3;
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeOut());
        isDead = false;
    }
    void BossHp()
    {
        if(SceneManager.GetActiveScene().name == "BossField")
        {
            GameObject Boss = GameObject.FindGameObjectWithTag("Boss");
            if (Boss != null)
            {
                float dis = Vector3.Distance(Boss.transform.position, transform.position);
                if(dis < 15f)
                {
                    BossHpGameObject.SetActive(true);
                }
                if(PlayerManager.Instance.currentHp <= 0)
                {
                    BossHpGameObject.SetActive(false);
                }
            }
            
        }
            
    }
    void Bossdiegetgoldbutton()
    {
        BossDieGoldObject.SetActive(false);
        BossHpGameObject.SetActive(false);
    }
    void ShopExitButton()
    {
        ShopGameObject.SetActive(false);
    }
    void ReinforceExitButton()
    {
        ReinforceGameObject.SetActive(false);
        if(reinfroceui != null)
        {
            reinfroceui.ReinforceScreen.SetActive(false);
            reinfroceui.PitchingReinforceScreen.SetActive(false);
            reinfroceui.weapondamageText.text = "";
            reinfroceui.ciriticalperText.text = "";
            reinfroceui.ciriticaldamageText.text = "";
        }
    }

}

