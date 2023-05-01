using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float speed = 1f;
    public float distance = 8f;
    float distancePassed;
    public List<Sprite> sprites;
    SpriteRenderer currentSprite;

    void Start() {
        currentSprite = GetComponent<SpriteRenderer>();
    }

    void Update() {
        switch(Mathf.Floor(distancePassed/(distance / 4))) {
            case 0: 
                transform.position -= transform.up * speed * Time.deltaTime;
                currentSprite.sprite = sprites[0];
                break;
            case 1: 
                transform.position += transform.right * speed * Time.deltaTime;
                currentSprite.sprite = sprites[1];
                break;
            case 2: 
                transform.position += transform.up * speed * Time.deltaTime;
                currentSprite.sprite = sprites[2];
                break;
            case 3: 
                transform.position -= transform.right * speed * Time.deltaTime;
                currentSprite.sprite = sprites[3];
                break;
        }
        distancePassed += speed * Time.deltaTime;
        if(distancePassed >= distance) {
            distancePassed = 0;
        }
    }
}
