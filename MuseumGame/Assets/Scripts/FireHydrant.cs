using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrant : MonoBehaviour
{
    
    public GameObject projectile;
    public int projSpeed;
    public float timeBetween;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(SpawnProjectiles());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnProjectiles()
    {
        for (; ; )
        {
            animator.SetBool("Firing", true);
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < 3; i++)
            {
                GameObject clone = Instantiate(projectile, transform.position, transform.rotation);
                clone.transform.Rotate(new Vector3(0, 0, i * 90));
                clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y,.5f);
                clone.GetComponent<Rigidbody2D>().velocity = clone.transform.right * projSpeed;
            }
            
            animator.SetBool("Firing", false);

            yield return new WaitForSeconds(timeBetween);
        }
    }
}
