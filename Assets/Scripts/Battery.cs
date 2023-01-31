using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    
    private int battery = 25;
    private Rigidbody2D rigidBody;

    void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0, -5);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            GameplayController.instance.BatteryCharge(battery);
            Destroy(gameObject);
        }
    }
}
