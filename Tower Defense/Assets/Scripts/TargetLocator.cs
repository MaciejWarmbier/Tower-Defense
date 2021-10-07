using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform upperBallista;
    [SerializeField] float towerRange = 15f;
    [SerializeField] ParticleSystem projectileParticles;
    Transform target;
        void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget(){
        
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistatnce = Mathf.Infinity;

        foreach(Enemy enemy in enemies){
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if(targetDistance < maxDistatnce){
                closestTarget = enemy.transform;
                maxDistatnce = targetDistance;
            }
        }

        target = closestTarget;
        
    }
    void AimWeapon(){
        if(target != null){
        float targetDistance = Vector3.Distance(transform.position, target.position);
        
        upperBallista.LookAt(target.position);
        if(targetDistance <= towerRange){
            Attack(true);
        }
        else {
            Attack(false);
        }
        }
        
    }

    void Attack(bool isActive){
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }
}
