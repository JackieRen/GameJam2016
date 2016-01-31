using UnityEngine;
using System.Collections;

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
    public bool _isMove = false;
    public int _getAwardNum = 0;
    
    private Vector3 velocity_ = Vector3.zero;
    private Vector3 oldPos_ = Vector3.zero;
    private string awardName_ = "";
    
    void Start () {
	   _award.gameObject.SetActive(false);
       oldPos_ = this.transform.position;
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
                ++_getAwardNum;
                if(_getAwardNum == _ctrl._data._awardDataList[_ctrl.nextWaveNum_]){
                    ++_ctrl.nextWaveNum_;
                    _ctrl.PlayNext();
                    _getAwardNum = 0;
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
