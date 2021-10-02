using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0,5f)] float enemySpeed = 1f;
    Enemy enemy;
    void OnEnable()
    { 
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void Start() {
        enemy = GetComponent<Enemy>();
    }
    void FindPath(){

        path.Clear();

        GameObject pathFolder = GameObject.FindGameObjectWithTag("Path");
        foreach(Transform child in pathFolder.transform){
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if (waypoint != null){
                path.Add(waypoint);
            }
        }
    }

    void ReturnToStart(){
        transform.position = path[0].transform.position;
    }

    IEnumerator FollowPath(){

        foreach (Waypoint waypoint in path){
            Vector3 startPosition = gameObject.transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;
            transform.LookAt(endPosition);

            while(travelPercent < 1f){
                travelPercent += Time.deltaTime * enemySpeed;
                transform.position = Vector3.Lerp(startPosition,endPosition,travelPercent);
                yield return new WaitForEndOfFrame();
            }
            
        }

        enemy.StealGold();
        gameObject.SetActive(false);
        
    }
}
