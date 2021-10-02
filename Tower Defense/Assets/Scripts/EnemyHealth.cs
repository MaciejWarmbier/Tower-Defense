using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    [SerializeField] int currentHealth = 0;
    Enemy enemy;

    private void Start() {
        enemy = GetComponent<Enemy>();
    }
    void OnEnable()
    {
        currentHealth = maxHitPoints;
    }

    void OnParticleCollision(GameObject other) {
        ProcessHit();
    }

    void ProcessHit(){
        currentHealth -= 1;

        if(currentHealth <= 0 ){
            gameObject.SetActive(false);
            enemy.RewardGold();
        }
    }
}
