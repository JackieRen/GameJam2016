using UnityEngine;
using System.Collections;

public class Prop : MonoBehaviour {
	public BoxCollider2D _prop = null;
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if(enemy.gameObject.tag == "Player"){
            _prop.isTrigger = false;
        }else if(enemy.gameObject.tag == "Enemy"){
            _prop.isTrigger = true;
        }
    }
    
}
