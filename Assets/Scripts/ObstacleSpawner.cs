using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [HideInInspector]
    public float delay;
    public GameObject obstacle;
    private float maxLeft, maxRight, maxTop;
    private float time;

    void Awake(){
        time = 0f;
    }

    void Update()
    {
        LimitBoundary();
        SpawnTime();
    }

    void SpawnTime(){
        if(GameplayController.instance.gameInProgress){
            time += Time.deltaTime;
            if(time >= delay){
                time = 0;
                Instantiate(obstacle, new Vector3(Random.Range(maxLeft + 0.7f, maxRight - 0.7f), maxTop, transform.position.z), Quaternion.identity);
            }
        }
    }

    void LimitBoundary(){
        Vector3 leftBound = Camera.main.ViewportToWorldPoint(new Vector3(0,0,Camera.main.transform.position.z + 10));
        Vector3 rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1,0,Camera.main.transform.position.z + 10));
        Vector3 topBound = Camera.main.ViewportToWorldPoint(new Vector3(0,1,Camera.main.transform.position.z + 10));
        maxLeft = leftBound.x;
        maxRight = rightBound.x;
        maxTop = topBound.y;
    }
}
