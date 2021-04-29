using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaisyBoss : MonoBehaviour
{
    public int numProjectiles;
    public GameObject projectile;
    public int projSpeed;
    public float timeBetween;

    private GameObject enemyHealthBar;
    private EnemyHealthBar ehb;
    public int enemyHealth;
    public int damageTaken;
    public GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnProjectiles());

        enemyHealthBar = GameManager.Instance.GetEnemyHealthBar();
        ehb = enemyHealthBar.GetComponent<EnemyHealthBar>();
        enemyHealthBar.gameObject.SetActive(true);
        foreach (GameObject i in doors) {
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
        if (enemyHealth <=0) {
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
        
            if (collision2D.gameObject.CompareTag("Projectile"))
            {
                DamageEnemy(50);
            }
        
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Projectile"))
        {
            DamageEnemy(50);
        }
    }

    IEnumerator SpawnProjectiles()
    {
        for (; ; )
        {
            for (int i = numProjectiles; i > 0; i--)
            {
                GameObject clone = Instantiate(projectile, transform.position, transform.rotation);
                clone.transform.Rotate(new Vector3(0, 0, (i * (180 / (numProjectiles - 1)) * -1) + (180 / (numProjectiles - 1))));
                clone.GetComponent<Rigidbody2D>().velocity = clone.transform.right * projSpeed;
            }

            yield return new WaitForSeconds(timeBetween);
        }
    }
}
