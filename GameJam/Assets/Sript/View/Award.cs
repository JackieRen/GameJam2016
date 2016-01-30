using UnityEngine;
using System.Collections;

public class Award : MonoBehaviour {
    
    public BoxCollider2D _award = null;
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if(enemy.gameObject.tag == "Player"){
            _award.isTrigger = false;
        }else if(enemy.gameObject.tag == "Enemy"){
            _award.isTrigger = true;
        }
    }
    
}
