using UnityEngine;
using System.Collections;
using GDGeek;

public class Follower : MonoBehaviour {

    public GameObject _player = null;
    public Animator _animator = null;
	public float _moveTime = 0;
    
    private bool isMove_ = false;
    
    public bool IsMove{
        get{return isMove_;}
    }
    
	void FixedUpdate()
    {
	   if(isMove_){
           FollowerMove();
       }
	}
    
    public void playDeathAnimation()
    {
        _animator.SetTrigger("death");
    }
    
    private void FollowerMove()
    {
        Vector3 pos = _player.transform.position + Vector3.one;
        Move(this.gameObject, _moveTime, pos);
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
    
}
