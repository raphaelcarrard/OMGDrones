using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySpawner : MonoBehaviour
{

    public GameObject battery;
    private float maxLeft, maxRight, maxTop;
    private float time;
    private int delay;

    void Awake(){
        time = 0f;
        delay = Random.Range(15, 30);
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
                delay = Random.Range(15, 30);
                Instantiate(battery, new Vector3(Random.Range (maxLeft + 1f, maxRight - 1f), maxTop, transform.position.z), Quaternion.Euler(new Vector3(0, 0, -90)));
            }
        }
    }

    void LimitBoundary(){
        Vector3 leftBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z + 10));
        Vector3 rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.transform.position.z + 10));
        Vector3 topBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.transform.position.z + 10));
        maxLeft = leftBound.x;
        maxRight = rightBound.x;
        maxTop = topBound.y;
    }
}
