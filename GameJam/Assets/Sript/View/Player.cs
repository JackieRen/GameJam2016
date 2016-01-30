using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public SpriteRenderer _playerImage = null; 
	public SpriteRenderer _award = null;
    // public SpriteRenderer[] _awardList = null;
    public Sprite _frontSprite = null;
    public Sprite _backSprite = null;
    public Animator _animator = null;
    public float _playerMoveSpeed = 0;
    public bool _isMove = false;
    
    private Vector3 velocity_ = Vector3.zero;
    private Vector3 oldPos_ = Vector3.zero;
    private string _awardName = "";
    
    void Start () {
	   _award.gameObject.SetActive(false);
       oldPos_ = this.transform.position;
	}
    
    void FixedUpdate()
    {
        if(_isMove){
            PlayerMove();
        }
    }
    
    void OnCollisionEnter2D(Collision2D enemy)
    {
        if(enemy.gameObject.tag == "Tree"){
            _award.gameObject.SetActive(false);
        }else if(enemy.gameObject.tag == "Award"){
            _awardName = enemy.gameObject.name;
            if(!_award.gameObject.activeSelf){
                enemy.gameObject.SetActive(false);
                Debug.Log(enemy.gameObject.name);
                _award.sprite = enemy.gameObject.GetComponent<SpriteRenderer>().sprite;
                _award.gameObject.SetActive(true);
            }
        }
    }
    
    private void PlayerMove()
    {
        velocity_ = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized * _playerMoveSpeed;
        this.transform.position = this.transform.position + velocity_ * Time.fixedDeltaTime;
        if(oldPos_.y == this.transform.position.y && oldPos_.x == this.transform.position.x){
            _animator.SetBool("Walk", false);
        }else{
            _animator.SetBool("Walk", true);
        }
        if(oldPos_.y >= this.transform.position.y){
            _playerImage.sprite = _frontSprite;
            oldPos_ = this.transform.position;
        }else{
            _playerImage.sprite = _backSprite;
            oldPos_ = this.transform.position;
        }
        
    }
    
    
}
