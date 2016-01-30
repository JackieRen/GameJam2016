using UnityEngine;
using System.Collections;

public class TreeBoxCollider : MonoBehaviour {
    
    public SpriteRenderer _tree = null;
    public int _sortNum = 0;
    
	void Start () {
	
	}
	
	void Update () {
	
	}
    
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if(enemy.gameObject.tag == "Player"){
            _tree.sortingOrder = _sortNum;
        }else if(enemy.gameObject.tag == "Enemy"){
            _tree.sortingOrder = _sortNum;
        }
    }
    
}
