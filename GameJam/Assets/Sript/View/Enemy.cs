using UnityEngine;
using System.Collections;
using GDGeek;

public class Enemy : MonoBehaviour {

	public Ctrl _ctrl = null;
    public SpriteRenderer _enemyImage = null;
    public Sprite _frontSprite = null;
    public Sprite _backSprite = null;
    public GameObject _frontDot = null;
    public GameObject _backDot = null;
    public GameObject _talkBox = null;
    public Animator _animator = null;
    public GameObject _player = null;
    public float _enemyMoveTime = 0f;
    public bool _isMove = false;
    
    private Vector3 oldPos_ = Vector3.zero;
    
    void Start()
    {
        _backDot.SetActive(false);
        _talkBox.SetActive(false);
        oldPos_ = this.transform.position;
    }
    
    void FixedUpdate()
    {
        if(_isMove){
            EnemyMove();
        }
    }
    
    void OnCollisionEnter2D(Collision2D enemy)
    {
        if(enemy.gameObject.tag == "Player"){
            Debug.Log("Touch Player");
             _ctrl.fsmPost("end");
        }
        
    }
    
    public void playDeathAnimation()
    {
        _animator.SetTrigger("death");
    }
    
    private void EnemyMove()
    {
        Move(this.gameObject, _enemyMoveTime, _player.transform.position);
        // if(oldPos_.y == this.transform.position.y && oldPos_.x == this.transform.position.x){
        //     _animator.SetBool("Walk", false);
        // }else{
        //     _animator.SetBool("Walk", true);
        // }
        if((double)oldPos_.y > (double)this.transform.position.y){
            _enemyImage.sprite = _frontSprite;
            _frontDot.SetActive(true);
            _backDot.SetActive(false);
            oldPos_ = this.transform.position;
        }else{
            _enemyImage.sprite = _backSprite;
            _frontDot.SetActive(false);
            _backDot.SetActive(true);
            oldPos_ = this.transform.position;
        }
    }
    
    private void Move(GameObject ob, float time, Vector3 position)
    {
        TweenLocalPosition comp = Tween.Begin<TweenLocalPosition>(ob, time);
        comp.from = comp.position;
        comp.to = position;
        if (_enemyMoveTime <= 0f)
        {
            comp.Sample(1f, true);
            comp.enabled = false;
        }
    }
    
}
