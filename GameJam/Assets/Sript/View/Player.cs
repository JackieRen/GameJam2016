using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public SpriteRenderer _award = null;
    
    void Start () {
	   _award.gameObject.SetActive(false);
	}
    
    void OnCollisionEnter2D(Collision2D enemy)
    {
        if(enemy.gameObject.tag == "Tree"){
            _award.gameObject.SetActive(false);
        }else if(enemy.gameObject.tag == "Award"){
            enemy.gameObject.SetActive(false);
            _award.sprite = enemy.gameObject.GetComponent<SpriteRenderer>().sprite;
            _award.gameObject.SetActive(true);
        }
    }
    
}
