using UnityEngine;
using System.Collections;
using GDGeek;

public class Follower : MonoBehaviour {

    public GameObject _target = null;
    public Animator _animator = null;
    public SpriteRenderer _enemyImage = null;
    public Sprite _frontSprite = null;
    public Sprite _backSprite = null;
    public Player _player = null;
	public float _moveTime = 0;
    public bool _isMove = false;
    
    private Vector3 oldPos_ = Vector3.zero;
    private float continuedTime_ = 0;
    
    void Start()
    {
        oldPos_ = this.transform.position;
    }
    
	void FixedUpdate()
    {
	   if(_isMove){
           FollowerMove();
           continuedTime_ += 0.01f;
           if(continuedTime_ >= 10){
               FollowerDeath();
           }
       }
	}
    
    public void StateOver(){
        this.gameObject.SetActive(false);
        continuedTime_ = 0;
        _animator.SetBool("Death", false);
        _player._playerMoveSpeed -= 1;
    }
    
    private void FollowerMove()
    {
        Vector3 pos = _target.transform.position - new Vector3(0.8f, 0.8f, 0);
        Move(this.gameObject, _moveTime, pos);
        if(oldPos_.y > this.transform.position.y){
            _enemyImage.sprite = _frontSprite;
            oldPos_ = this.transform.position;
        }else{
            _enemyImage.sprite = _backSprite;
            oldPos_ = this.transform.position;
        }
    }
    
    private void Move(GameObject ob, float time, Vector3 position)
    {
        TweenLocalPosition comp = Tween.Begin<TweenLocalPosition>(ob, time);
        comp.from = comp.position;
        comp.to = position;
        if (_moveTime <= 0f)
        {
            comp.Sample(1f, true);
            comp.enabled = false;
        }
    }
    
    private void FollowerDeath()
    {
        _isMove = false;
        _animator.SetBool("Death", true);
    }
    
}
