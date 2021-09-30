using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] float waitTime = 1f;
    void Start()
    {
       StartCoroutine(FollowPath());
    }


    IEnumerator FollowPath(){
        foreach (Waypoint waypoint in path){
            gameObject.transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
}
