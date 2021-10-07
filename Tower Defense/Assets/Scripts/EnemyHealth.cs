using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))] 
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 4;
    [Tooltip("Adds amount of hitpoints when enemy dies")]
    [SerializeField] int difficultyRamp = 1;
    int currentHealth = 0;
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
            maxHitPoints += difficultyRamp;
        }
    }
}
