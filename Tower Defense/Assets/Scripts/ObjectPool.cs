using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] [Range(0.1f,30f)] float spawnTime = 1f;
    [SerializeField] [Range(0,50)]int poolSize = 5;
    [SerializeField] GameObject enemy;

    GameObject[] pool;

    void Awake() {
       PopulatePool(); 
    }
    void Start()
    {
        StartCoroutine(CreateEnemy());
    }

    void PopulatePool(){
        pool = new GameObject[poolSize];

        for(int i = 0; i< pool.Length ; i++){
            pool[i] = Instantiate(enemy,transform);
            pool[i].SetActive(false);
        }
    }

    void EnableObjectInPool(){
        for(int i=0;i<pool.Length;i++){
            if(pool[i].activeInHierarchy == false){
                pool[i].SetActive(true);
                return;
            }
        }
    }

    IEnumerator CreateEnemy(){
        while(true)
        {
            EnableObjectInPool();
        yield return new WaitForSeconds(spawnTime);
        }
    }
    
}
