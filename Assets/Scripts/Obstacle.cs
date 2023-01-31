using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    
    public float speed;
    private Rigidbody2D ridigBody;

    void Awake(){
        ridigBody = GetComponent<Rigidbody2D>();
        ridigBody.velocity = new Vector2(0, -speed);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            GameplayController.instance.PlayerDied(collider.gameObject);
        }
    }
}
