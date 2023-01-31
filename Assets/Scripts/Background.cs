using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    [HideInInspector]
    public float speed;

    private Vector2 offset = Vector2.zero;
    private Material mat;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        offset = mat.GetTextureOffset("_MainTex");
        InitializeBackground();
    }

    // Update is called once per frame
    void Update()
    {
        offset.y += speed * Time.deltaTime;
        mat.SetTextureOffset("_MainTex", offset);
    }

    void InitializeBackground(){
        float height = Camera.main.orthographicSize * 2f;
        float width = height * Screen.width / Screen.height;
        transform.localScale = new Vector3(width, height, transform.position.z);
    }
}
