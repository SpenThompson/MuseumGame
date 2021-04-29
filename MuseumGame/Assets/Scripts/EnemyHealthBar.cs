using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthBar;
    public int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
 
}
