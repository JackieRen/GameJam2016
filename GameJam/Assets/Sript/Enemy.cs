using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnCollisionEnter2D(Collision2D enemy)
    {
        if(enemy.gameObject.tag == "Player"){
            Debug.Log("Touch Player");
        }
    }
    
}
