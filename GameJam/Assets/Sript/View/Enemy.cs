﻿using UnityEngine;
using System.Collections;
using GDGeek;

public class Enemy : MonoBehaviour {
    public Ctrl _ctrl = null;
    public SpriteRenderer _enemyImage = null;
    public Sprite _frontSprite = null;
    public Sprite _backSprite = null;
    public SpriteRenderer _talkBoxSprite = null;
    public GameObject _frontDot = null;
    public GameObject _backDot = null;
    public GameObject _talkBox = null;
    public Animator _animator = null;
    public GameObject _target = null;
    public Follower[] _followerList = null;
    public GameObject[] _roockList = null;
    public Player _player = null;
    public float _enemyMoveTime = 0f;
    public Vector3 _deviateNum = Vector3.zero;
    public bool _isMove = false;
    
    private Vector3 oldPos_ = Vector3.zero;
    private float continuedTime_ = 0;
    private float lifeNum_ = 10;
    
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
            continuedTime_ += 0.1f;
            lifeNum_ -= 0.02f;
        }
        if(this.gameObject.activeSelf && continuedTime_ > 5){
            _talkBox.SetActive(true);
            continuedTime_ = 0;
        }
        if(lifeNum_ <= 0){
            EnemyDeath();
        }
        
    }
    
    void OnCollisionEnter2D(Collision2D enemy)
    {
        if(enemy.gameObject.tag == "Player"){
            if(enemy.gameObject.GetComponent<Player>()._award.sprite.name != null){
                string spriteName = enemy.gameObject.GetComponent<Player>()._award.sprite.name;
                if(_talkBoxSprite.sprite.name == spriteName){
                    _isMove = false;
                    enemy.gameObject.GetComponent<Player>()._award.gameObject.SetActive(false);
                    _ctrl._data._getAwardNum += 1;
                    if(_ctrl._data._getAwardNum == _ctrl._data._nextWaveNum + 1){
                        ++_ctrl._data._nextWaveNum;
                        _ctrl.PlayNext();
                        _ctrl._data._getAwardNum = 0;
                    }
                    for(int i = 0; i < _followerList.Length; ++i){
                        if(!_followerList[i].gameObject.activeSelf){
                            _followerList[i].transform.position = this.transform.position;
                            _followerList[i].gameObject.SetActive(true);
                            _followerList[i]._isMove = true;
                            // _player._playerMoveSpeed += 1;
                            break;
                        }
                    }
                    this.gameObject.SetActive(false);
                }
            }
            
        }
        
    }
    
    public void playDeathAnimation()
    {
        _animator.SetTrigger("death");
    }
    
    private void EnemyMove()
    {
        Move(this.gameObject, _enemyMoveTime, _target.transform.position - _deviateNum);
        if(oldPos_.y > this.transform.position.y){
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
    
    private void EnemyDeath()
    {
        TaskWait tw = new TaskWait(1f);
        TaskManager.PushFront(tw, delegate(){
            _isMove = false;
            _animator.SetBool("Death", true);
        });
        TaskManager.PushBack(tw, delegate(){
            lifeNum_ = 10;
            for(int i = 0; i < _roockList.Length; ++i){
                if(!_roockList[i].activeSelf){
                    _roockList[i].transform.position = this.transform.position;
                    _roockList[i].SetActive(true);
                    break;
                }
            }
            this.gameObject.SetActive(false);
            _animator.SetBool("Death", false);
        });
        TaskManager.Run(tw);
    }
    
}
