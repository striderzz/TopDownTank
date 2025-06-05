using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    private EnemyTankAI enemyTankAI;
    public GameObject turrent;

    private void Start()
    {
        currentHealth = maxHealth;
        enemyTankAI = GetComponent<EnemyTankAI>();
    }
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth < 0)
        {
            enemyTankAI.enabled = false;
            Destroy(this.gameObject);
        }
    }
}
