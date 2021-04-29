using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBoss : MonoBehaviour
{
    private bool faceRight = false;
    private Rigidbody2D rb2d;
    private Vector2 v2;

    public float dogSpeed;
    public float runTime;
    public float waitTime;

    private GameObject enemyHealthBar;
    private EnemyHealthBar ehb;
    private int enemyHealth;
    public int damageTaken;
    public GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(DogMove());

        enemyHealthBar = GameManager.Instance.GetEnemyHealthBar();
        ehb = enemyHealthBar.GetComponent<EnemyHealthBar>();
        enemyHealthBar.gameObject.SetActive(true);
        enemyHealth = ehb.maxHealth;
        Debug.Log(enemyHealth);
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
    private void FixedUpdate()
    {
        rb2d.velocity = v2;
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

        if (collision2D.gameObject.CompareTag("Projectile"))
        {
            DamageEnemy(damageTaken);
        }

    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Projectile"))
        {
            DamageEnemy(damageTaken);
        }
    }
    private void flip()
    {
        faceRight = !faceRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    IEnumerator DogMove()
    {
        for (; ; ) {
            if (faceRight)
            {
                v2 = new Vector2(dogSpeed, 0);
            }
            else
            {
                v2 = new Vector2(-dogSpeed, 0);
            }
            yield return new WaitForSeconds(runTime);

            v2 = new Vector2(0, 0);

            yield return new WaitForSeconds(waitTime);
            flip();
        }
    }
}
