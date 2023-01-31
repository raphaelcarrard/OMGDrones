using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject explosion;

    [SerializeField]
    private float speed, tiltSpeed, touchSpeed;

    [SerializeField]
    private float padding;
    private float maxLeft, maxRight;
    private Vector3 position;

    void Awake()
    {
        position = transform.position;
    }

    void Update(){
        LimitPosition();
        if(Application.platform == RuntimePlatform.Android){
            if(GameController.instance.controls){
                AccelerometerMove();
            }else{
                TouchScreenMove();
            }
        }else if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.LinuxPlayer){
            PlayerMovement();
        }
    }

    void PlayerMovement(){
        float h = Input.GetAxis("Horizontal");
        position.x += h * speed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, maxLeft + padding, maxRight - padding);
        transform.position = position;
    }

    void AccelerometerMove(){
        float x = Input.acceleration.x;
        if(x > 0.1){
            position.x += x * tiltSpeed * Time.deltaTime;
        }else if(x < -0.1){
            position.x += x * tiltSpeed * Time.deltaTime;
        }
        position.x = Mathf.Clamp(position.x, maxLeft + padding, maxRight - padding);
        transform.position = position;
    }

    void TouchScreenMove(){
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            position = transform.position;
            if(touch.position.x < Screen.width / 2 && touch.position.y < Screen.height - touch.position.y * 0.30f){
                MoveLeft();
            }else if(touch.position.x > Screen.width / 2 && touch.position.y < Screen.height - touch.position.y * 0.30f){
                MoveRight();
            }
            if(touch.phase == TouchPhase.Ended){
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            position.x = Mathf.Clamp(position.x, maxLeft + padding, maxRight - padding);
            transform.position = position;
        }
    }

    void MoveRight(){
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(touchSpeed, 0);
    }

    void MoveLeft(){
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-touchSpeed, 0);
    }

    void LimitPosition(){
        Vector3 leftBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        Vector3 rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.transform.position.z));
        maxLeft = leftBound.x;
        maxRight = rightBound.x;
    }
}
