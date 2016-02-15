using UnityEngine;
using System.Collections;
using GDGeek;

public class Tree : MonoBehaviour {
    public Ctrl _ctrl = null;
	public Animator _animator = null;
    public GameObject _nextTree = null;
    public GameObject _deathTree = null;
    public float _treeLifeNum = 20;
    
    void FixedUpdate()
    {
        // _treeLifeNum -= 0.02f;
        if(_treeLifeNum <= 0){
            DeathTree();
        }else if(_treeLifeNum >= 50 && _nextTree != null){
            _nextTree.SetActive(true);
            this.gameObject.SetActive(false);
            _treeLifeNum = 20;
        }
    }
    
    public void AwardStateOver(){
        _animator.SetBool("Clear", false);
    }
    
    private void DeathTree(){
        TaskWait tw = new TaskWait(2f);
        TaskManager.PushFront(tw, delegate(){
            this.gameObject.SetActive(false);
            _deathTree.gameObject.SetActive(true);
        });
        TaskManager.PushBack(tw, delegate(){
            _ctrl.fsmPost("end");
        });
        TaskManager.Run(tw);
    }
    
}
