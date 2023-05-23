using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {       
        if(SceneManager.GetActiveScene().name == "FirstField")
        {
            GameObject enemy1 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(11f, -6.49f, 0), Quaternion.identity);
            GameObject enemy2 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(20f, -6.49f, 0), Quaternion.identity);
            GameObject enemy3 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(25f, -6.49f, 0), Quaternion.identity);
            GameObject enemy4 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(13.45f, -6.49f, 0), Quaternion.identity);
            GameObject enemy5 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(28.7f, -0.45f, 0), Quaternion.identity);
            GameObject enemy6 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(16.3f, 4.01f, 0), Quaternion.identity);
            GameObject enemy7 = Instantiate(EnemyManager.Instance.SkeletonEnemy, transform.position + new Vector3(19f, 0.14f, 0), Quaternion.identity);
            GameObject enemy8 = Instantiate(EnemyManager.Instance.SkeletonEnemy, transform.position + new Vector3(26f, 0.14f, 0), Quaternion.identity);
            GameObject enemy9 = Instantiate(EnemyManager.Instance.SkeletonEnemy, transform.position + new Vector3(21.06f, 4.67f, 0), Quaternion.identity);
            GameObject enemy10 = Instantiate(EnemyManager.Instance.SkeletonEnemy, transform.position + new Vector3(26.06f, 4.67f, 0), Quaternion.identity);
            GameObject enemy11 = Instantiate(EnemyManager.Instance.SkeletonEnemy, transform.position + new Vector3(17.95f, 10.15f, 0), Quaternion.identity);
            GameObject enemy12 = Instantiate(EnemyManager.Instance.SkeletonEnemy, transform.position + new Vector3(23.42f, 10.15f, 0), Quaternion.identity);
            GameObject enemy13 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(45f, -4f, 0), Quaternion.identity);
            GameObject enemy14 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(63f, -4f, 0), Quaternion.identity);
            GameObject enemy15 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(64f, 4.5f, 0), Quaternion.identity);
            GameObject enemy16 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(76f, -6.5f, 0), Quaternion.identity);
            GameObject enemy17 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(92f, -6.5f, 0), Quaternion.identity);
            GameObject enemy18 = Instantiate(EnemyManager.Instance.SkeletonEnemy, transform.position + new Vector3(48f, 1.6f, 0), Quaternion.identity);
            GameObject enemy19 = Instantiate(EnemyManager.Instance.SkeletonEnemy, transform.position + new Vector3(57f, 1.6f, 0), Quaternion.identity);
            GameObject enemy20 = Instantiate(EnemyManager.Instance.SkeletonEnemy, transform.position + new Vector3(73f, 5.1f, 0), Quaternion.identity);
            GameObject enemy21 = Instantiate(EnemyManager.Instance.SkeletonEnemy, transform.position + new Vector3(84.9f, 3.1f, 0), Quaternion.identity);
            GameObject enemy22 = Instantiate(EnemyManager.Instance.SkeletonEnemy, transform.position + new Vector3(97.6f, 5.6f, 0), Quaternion.identity);
            GameObject Parentobj = new GameObject("EnemyParent");
            enemy1.transform.SetParent(Parentobj.transform);
            enemy2.transform.SetParent(Parentobj.transform);
            enemy3.transform.SetParent(Parentobj.transform);
            enemy4.transform.SetParent(Parentobj.transform);
            enemy5.transform.SetParent(Parentobj.transform);
            enemy6.transform.SetParent(Parentobj.transform);
            enemy7.transform.SetParent(Parentobj.transform);
            enemy8.transform.SetParent(Parentobj.transform);
            enemy9.transform.SetParent(Parentobj.transform);
            enemy10.transform.SetParent(Parentobj.transform);
            enemy11.transform.SetParent(Parentobj.transform);
            enemy12.transform.SetParent(Parentobj.transform);
            enemy13.transform.SetParent(Parentobj.transform);
            enemy14.transform.SetParent(Parentobj.transform);
            enemy15.transform.SetParent(Parentobj.transform);
            enemy16.transform.SetParent(Parentobj.transform);
            enemy17.transform.SetParent(Parentobj.transform);
            enemy18.transform.SetParent(Parentobj.transform);
            enemy19.transform.SetParent(Parentobj.transform);
            enemy20.transform.SetParent(Parentobj.transform);
            enemy21.transform.SetParent(Parentobj.transform);
            enemy22.transform.SetParent(Parentobj.transform);
        }
        if (SceneManager.GetActiveScene().name == "SceondField")
        {
            GameObject enemy1 = Instantiate(EnemyManager.Instance.RangerEnemy, transform.position + new Vector3(39f, -2.9f, 0), Quaternion.identity);
            GameObject enemy2 = Instantiate(EnemyManager.Instance.RangerEnemy, transform.position + new Vector3(54f, 1.6f, 0), Quaternion.identity);
            GameObject enemy3 = Instantiate(EnemyManager.Instance.RangerEnemy, transform.position + new Vector3(66f, 1.6f, 0), Quaternion.identity);
            GameObject enemy4 = Instantiate(EnemyManager.Instance.RangerEnemy, transform.position + new Vector3(86f, 5.6f, 0), Quaternion.identity);
            GameObject enemy5 = Instantiate(EnemyManager.Instance.RangerEnemy, transform.position + new Vector3(95f, 2.6f, 0), Quaternion.identity);
            GameObject enemy6 = Instantiate(EnemyManager.Instance.RangerEnemy, transform.position + new Vector3(108f, 2.6f, 0), Quaternion.identity);
            GameObject enemy7 = Instantiate(EnemyManager.Instance.RangerEnemy, transform.position + new Vector3(118f, 2.6f, 0), Quaternion.identity);
            GameObject enemy8 = Instantiate(EnemyManager.Instance.RangerEnemy, transform.position + new Vector3(131f, 2.6f, 0), Quaternion.identity);
            GameObject enemy9 = Instantiate(EnemyManager.Instance.RangerEnemy, transform.position + new Vector3(124f, -2.8f, 0), Quaternion.identity);
            GameObject enemy10 = Instantiate(EnemyManager.Instance.RangerEnemy, transform.position + new Vector3(111f, -2.8f, 0), Quaternion.identity);
            GameObject enemy11 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(13f, -5.58f, 0), Quaternion.identity);
            GameObject enemy12 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(20f, -5.58f, 0), Quaternion.identity);
            GameObject enemy13 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(32f, -5.58f, 0), Quaternion.identity);
            GameObject enemy14 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(46f, -3.4f, 0), Quaternion.identity);
            GameObject enemy15 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(73f, 5.15f, 0), Quaternion.identity);
            GameObject enemy16 = Instantiate(EnemyManager.Instance.enemy, transform.position + new Vector3(102f, -3.33f, 0), Quaternion.identity);
            GameObject Parentobj = new GameObject("EnemyParent");
            enemy1.transform.SetParent(Parentobj.transform);
            enemy2.transform.SetParent(Parentobj.transform);
            enemy3.transform.SetParent(Parentobj.transform);
            enemy4.transform.SetParent(Parentobj.transform);
            enemy5.transform.SetParent(Parentobj.transform);
            enemy6.transform.SetParent(Parentobj.transform);
            enemy7.transform.SetParent(Parentobj.transform);
            enemy8.transform.SetParent(Parentobj.transform);
            enemy9.transform.SetParent(Parentobj.transform);
            enemy10.transform.SetParent(Parentobj.transform);
            enemy11.transform.SetParent(Parentobj.transform);
            enemy12.transform.SetParent(Parentobj.transform);
            enemy13.transform.SetParent(Parentobj.transform);
            enemy14.transform.SetParent(Parentobj.transform);
            enemy15.transform.SetParent(Parentobj.transform);
            enemy16.transform.SetParent(Parentobj.transform);
        }
        if(SceneManager.GetActiveScene().name == "BossField")
        {
            GameObject Boss = Instantiate(BossManager.Instance.Boss, transform.position + new Vector3(48f, -3.8f, 0), Quaternion.identity);
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
