using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    #region ΩÃ±€≈Ê

    private static Gold instance = null;
    public static Gold Instance
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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    public GameObject goldText;
    public int rnd;

    public AudioClip[] clip;
    // Start is called before the first frame update
    void Start() 
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-50, 50), Random.Range(100, 100)));
        rnd = Random.Range(1, 30);
    }
    private void OnEnable()
    {
        StartCoroutine(goldEnd());
    }
    IEnumerator goldEnd() 
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.Instance.gold += rnd;
            SoundManager.instance.SFXPlay("GoldSound", clip[0]);
            Instantiate(goldText, collision.gameObject.transform.position, Quaternion.identity);
            Debug.Log("coin" + PlayerManager.Instance.gold);
            Destroy(gameObject);
        }
    }
}
