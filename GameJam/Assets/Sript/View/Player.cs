using UnityEngine;
using System.Collections;
using GDGeek;

public class Player : MonoBehaviour {
    public Ctrl _ctrl = null;
    public SpriteRenderer _playerImage = null; 
	public SpriteRenderer _award = null;
    public Sprite _frontSprite = null;
    public Sprite _backSprite = null;
    public Animator _animator = null;
    public Animator _treeAnimator = null;
    public Tree[] _treeList = null;
    public float _playerMoveSpeed = 0;
    public bool _isMove = true;
    public int _getAwardNum = 0;
    
    private Vector3 velocity_ = Vector3.zero;
    private Vector3 oldMousePos_ = Vector3.zero;
    private float mousePosX = 0;
    private float mousePosY = 0;
    private string awardName_ = "";
    
    void Start () {
	   _award.gameObject.SetActive(false);
       this.transform.position = new Vector3(0, -3, 0);
       _getAwardNum = 0;
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
            if(_award.gameObject.activeSelf){
                _award.gameObject.SetActive(false);
                _treeAnimator.SetBool("Clear", true);
                for(int i = 0; i < _treeList.Length; ++i){
                    if(_treeList[i].gameObject.activeSelf){
                        _treeList[i]._treeLifeNum += 10;
                        break;
                    }
                }
                _ctrl._data._enemyDeathNum += 1;
                if(_ctrl._data._enemyDeathNum == _ctrl._data._nextWaveNum){
                    _ctrl._data._nextWaveNum += 1;
                    _ctrl.PlayNext();
                    _ctrl._data._enemyDeathNum = 0;
                }
            }
        }else if(enemy.gameObject.tag == "Award"){
            if(!_award.gameObject.activeSelf){
                awardName_ = enemy.gameObject.GetComponent<SpriteRenderer>().sprite.name;
                enemy.gameObject.SetActive(false);
                _award.sprite = enemy.gameObject.GetComponent<SpriteRenderer>().sprite;
                _award.gameObject.SetActive(true);
            }
        }
    }
    
    private void PlayerMove()
    {
        velocity_ = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized * _playerMoveSpeed;
        this.transform.position = this.transform.position + velocity_ * Time.fixedDeltaTime;
        // mousePosX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        // mousePosY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        // if(oldMousePos_.x > mousePosX && oldMousePos_.y > mousePosY){
        //     velocity_ = new Vector3((this.transform.position.x - _playerMoveSpeed), (this.transform.position.y - _playerMoveSpeed), 0);
        //     _playerImage.sprite = _frontSprite;
        //     // this.transform.position = velocity_;
        //     Move(this.gameObject, 0.01f, velocity_);
        //     oldMousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // }else if(oldMousePos_.x < mousePosX && oldMousePos_.y > mousePosY){
        //     velocity_ = new Vector3((this.transform.position.x + _playerMoveSpeed), (this.transform.position.y - _playerMoveSpeed), 0);
        //     _playerImage.sprite = _backSprite;
        //     // this.transform.position = velocity_;
        //     Move(this.gameObject, 0.01f, velocity_);
        //     oldMousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // }else if(oldMousePos_.x > mousePosX && oldMousePos_.y < mousePosY){
        //     velocity_ = new Vector3((this.transform.position.x - _playerMoveSpeed), (this.transform.position.y + _playerMoveSpeed), 0);
        //     _playerImage.sprite = _frontSprite;
        //     // this.transform.position = velocity_;
        //     Move(this.gameObject, 0.01f, velocity_);
        //     oldMousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // }else if(oldMousePos_.x < mousePosX && oldMousePos_.y < mousePosY){
        //     velocity_ = new Vector3((this.transform.position.x + _playerMoveSpeed), (this.transform.position.y + _playerMoveSpeed), 0);
        //     _playerImage.sprite = _backSprite;
        //     // this.transform.position = velocity_;
        //     Move(this.gameObject, 0.01f, velocity_);
        //     oldMousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // }else if(oldMousePos_.x > mousePosX && oldMousePos_.y == mousePosY){
        //     velocity_ = new Vector3((this.transform.position.x - _playerMoveSpeed), this.transform.position.y, 0);
        //     _playerImage.sprite = _frontSprite;
        //     // this.transform.position = velocity_;
        //     Move(this.gameObject, 0.01f, velocity_);
        //     oldMousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // }else if(oldMousePos_.x < mousePosX && oldMousePos_.y == mousePosY){
        //     velocity_ = new Vector3((this.transform.position.x + _playerMoveSpeed), this.transform.position.y, 0);
        //     _playerImage.sprite = _backSprite;
        //     // this.transform.position = velocity_;
        //     Move(this.gameObject, 0.01f, velocity_);
        //     oldMousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // }else if(oldMousePos_.x == mousePosX && oldMousePos_.y > mousePosY){
        //     velocity_ = new Vector3(this.transform.position.x, (this.transform.position.y - _playerMoveSpeed), 0);
        //     // this.transform.position = velocity_;
        //     Move(this.gameObject, 0.01f, velocity_);
        //     oldMousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // }else if(oldMousePos_.x == mousePosX && oldMousePos_.y < mousePosY){
        //     velocity_ = new Vector3(this.transform.position.x, (this.transform.position.y + _playerMoveSpeed), 0);
        //     // this.transform.position = velocity_;
        //     Move(this.gameObject, 0.01f, velocity_);
        //     oldMousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // }else{
        //     velocity_ = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        //     this.transform.position = velocity_;
        //     // Move(this.gameObject, 0.01f, velocity_);
        //     oldMousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // }
        
    }
    
    private void Move(GameObject ob, float time, Vector3 position)
    {
        TweenLocalPosition comp = Tween.Begin<TweenLocalPosition>(ob, time);
        comp.from = comp.position;
        comp.to = position;
    }
    
    
}
