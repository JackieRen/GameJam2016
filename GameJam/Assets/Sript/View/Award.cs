using UnityEngine;
using System.Collections;

public class Award : MonoBehaviour {
    public SpriteRenderer _awardSprite = null;
    public SpriteRenderer _image = null;
    public CircleCollider2D _awardBoxCollider = null;
    
    void Awake()
    {
        _awardSprite.sprite = _image.sprite;
    }
    
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if(enemy.gameObject.tag == "Player"){
            _awardBoxCollider.isTrigger = false;
        }else if(enemy.gameObject.tag == "Enemy"){
            _awardBoxCollider.isTrigger = true;
        }
    }
    
}
