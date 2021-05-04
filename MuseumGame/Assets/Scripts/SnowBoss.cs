using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBoss : MonoBehaviour
{
    
    public GameObject projectile;
    public int projSpeed;
    public float firerate;
    public float size;

    private GameObject enemyHealthBar;
    private EnemyHealthBar ehb;
    private int enemyHealth;
    public int damageTaken;
    public GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnProjectiles());

        enemyHealthBar = GameManager.Instance.GetEnemyHealthBar();
        ehb = enemyHealthBar.GetComponent<EnemyHealthBar>();
        enemyHealthBar.gameObject.SetActive(true);
        enemyHealth = ehb.maxHealth;
        foreach (GameObject i in doors)
        {
            i.GetComponent<Collider2D>().isTrigger = false;
            i.GetComponent<LoadLevel>().enabled = false;
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DamageEnemy(int damage)
    {
        enemyHealth -= damage;
        ehb.SetHealth(enemyHealth);
        if (enemyHealth <= 0)
        {
            enemyHealthBar.SetActive(false);
            foreach (GameObject i in doors)
            {
                i.GetComponent<Collider2D>().isTrigger = true;
                i.GetComponent<LoadLevel>().enabled = true;
            }
            Destroy(gameObject);
        }

    }
    void OnCollisionEnter2D(Collision2D collision2D)
    {

        if (collision2D.gameObject.CompareTag("Player"))
        {
            DamageEnemy(damageTaken);
        }

    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Player"))
        {
            DamageEnemy(damageTaken);
        }
    }

    IEnumerator SpawnProjectiles()
    {
        for (; ; )
        {

           
                yield return new WaitForSeconds(1 / firerate);

                GameObject clone = Instantiate(projectile, transform.position, transform.rotation);
                clone.transform.localScale = new Vector3(size, size, 0);
                clone.transform.Rotate(new Vector3(0, 0, 180));
                clone.GetComponent<Rigidbody2D>().velocity = clone.transform.right * projSpeed;

            
            
        }
    }
    
}
