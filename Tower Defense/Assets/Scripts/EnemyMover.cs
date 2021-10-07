using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Tile> path = new List<Tile>();
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
            Tile tile = child.GetComponent<Tile>();

            if (tile != null){
                path.Add(tile);
            }
        }
    }

    void ReturnToStart(){
        transform.position = path[0].transform.position;
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
    IEnumerator FollowPath()
    {

        foreach (Tile tile in path)
        {
            Vector3 startPosition = gameObject.transform.position;
            Vector3 endPosition = tile.transform.position;
            float travelPercent = 0f;
            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * enemySpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }

        }

        FinishPath();

    }

    
}
