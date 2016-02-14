using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GDGeek;

public class Ctrl : MonoBehaviour {
    public View _view = null;
    public GameData _data = null;
    public Tree[] _treeList = null;
    public Animator _beginAnimator = null;
    public Animator _endAnimator = null;
    public Animator _black = null;

    private FSM fsm_ = new FSM();
    private List<GameObject> awardList_ = new List<GameObject>();
    private List<GameObject> enemyList_ = new List<GameObject>();
    private List<GameObject> followerList_ = new List<GameObject>();
    private List<GameObject> rockList_ = new List<GameObject>();

    void Start()
    {
        fsm_.addState("begin", BeginState());
        fsm_.addState("play", PlayState());
        fsm_.addState("end", EndState());
        fsm_.addState("again", AgainState());
        fsm_.init("begin");
    }
    
    public void fsmPost(string msg)
    {
        fsm_.post(msg);
    }
    
    public void follower(GameObject go)
    {
        
    }
    
    private State BeginState()
    {
        StateWithEventMap state = new StateWithEventMap();
        state.onStart += delegate{
            _view._beginPanel.SetActive(true);
            _view._playPanel.SetActive(false);
            _view._endPanel.SetActive(false);
            _beginAnimator.SetBool("Go", true);
        };
        state.onOver += delegate{
            TaskWait tw = new TaskWait(2f);
            TaskManager.PushFront(tw, delegate(){
                _black.SetBool("On", true);
            });
            TaskManager.PushBack(tw, delegate(){
                _view._beginPanel.SetActive(false);
                _view._playPanel.SetActive(true);
            });
            TaskManager.Run(tw);
        };
        state.addEvent("begin", "play");
        return state;
    }
    
    private State PlayState()
    {
        StateWithEventMap state = new StateWithEventMap();
        state.onStart += delegate{
            TaskWait tw = new TaskWait(2f);
            TaskManager.PushFront(tw, delegate(){
                PlayPanelInit();
            });
            TaskManager.PushBack(tw, delegate(){
                _black.SetBool("On", false);
            });
            TaskManager.Run(tw);
        };
        state.onOver += delegate{
            
        };
        state.addEvent("end", "end");
        return state;
    }
    
    private State EndState()
    {
        StateWithEventMap state = new StateWithEventMap();
        state.onStart += delegate{
            TaskWait tw = new TaskWait(1.5f);
            TaskManager.PushBack(tw, delegate(){
                _view._endPanel.SetActive(true);
                for(int i = 0; i < awardList_.Count; ++i){
                    DestroyImmediate(awardList_[i]);
                }
                for(int i = 0; i < enemyList_.Count; ++i){
                    DestroyImmediate(enemyList_[i]);
                }
                for(int i = 0; i < followerList_.Count; ++i){
                    DestroyImmediate(followerList_[i]);
                }
                for(int i = 0; i < rockList_.Count; ++i){
                    DestroyImmediate(rockList_[i]);
                }
                enemyList_.Clear();
                followerList_.Clear();
                rockList_.Clear();
                _endAnimator.SetBool("Play", true);
            });
            TaskManager.Run(tw);
        };
        state.onOver += delegate{
            TaskWait tw = new TaskWait(1f);
            TaskManager.PushFront(tw, delegate(){
                _endAnimator.GetComponent<CanvasGroup>().alpha = 0;
                _endAnimator.SetBool("Play", false);
            });
            TaskManager.PushBack(tw, delegate(){
                _view._endPanel.SetActive(false);
                _endAnimator.GetComponent<CanvasGroup>().alpha = 1;
            });
            TaskManager.Run(tw);
        };
        state.addEvent("again", "again");
        return state;
    }
    
    private State AgainState()
    {
        StateWithEventMap state = new StateWithEventMap();
        state.onStart += delegate{
            _view._beginPanel.SetActive(true);
            _view._playPanel.SetActive(false);
            _view._endPanel.SetActive(false);
            _beginAnimator.SetBool("Again", true);
        };
        state.onOver += delegate{
            TaskWait tw = new TaskWait(2f);
            TaskManager.PushFront(tw, delegate(){
                _black.SetBool("On", true);
            });
            TaskManager.PushBack(tw, delegate(){
                _view._beginPanel.SetActive(false);
                _view._playPanel.SetActive(true);
            });
            TaskManager.Run(tw);
        };
        state.addEvent("begin", "play");
        return state;
    }
    
    private void PlayPanelInit()
    {
        _data._enemyDeathNum = 0;
        _data._nextWaveNum = 0;
        _view._player.transform.position = new Vector3(0, -3, 0);
        int awardSpriteNo = _data._waveData[0]._awardSpriteNo[0];
        int awardPosNO = _data._waveData[0]._awardPosNo[0];
        Vector3 awardPos = _data._showAwardPos[awardPosNO];
        GameObject go = Instantiate(_view._award);
        go.transform.position = awardPos;
        go.transform.localScale = _view._award.transform.localScale;
        go.transform.parent = _view._award.transform.parent;
        go.GetComponent<Award>()._image.sprite = _data._awardSpriteList[awardSpriteNo];
        go.SetActive(true);
        for(int i = 0; i < _treeList.Length; ++i){
            _treeList[i]._treeLifeNum = 20;
            _treeList[i].gameObject.SetActive(false);
            _treeList[i]._deathTree.gameObject.SetActive(false);
        }
        _treeList[0].gameObject.SetActive(true);
    }
    
    public void PlayNext()
    {
        awardList_.Clear();
        enemyList_.Clear();
        int awardNum = _data._waveData[_data._nextWaveNum]._awardSpriteNo.Length;
        int enemyNum = _data._waveData[_data._nextWaveNum]._enemyPosNum.Length;
        for(int i = 0; i < awardNum; ++i){
            int awardSpriteNo = _data._waveData[_data._nextWaveNum]._awardSpriteNo[i];
            int awardPosNo = _data._waveData[_data._nextWaveNum]._awardPosNo[i];
            Vector3 awardPos = _data._showAwardPos[awardPosNo - 1];
            GameObject go = Instantiate(_view._award);
            go.transform.position = awardPos;
            go.transform.localScale = _view._award.transform.localScale;
            go.transform.parent = _view._award.transform.parent;
            go.GetComponent<Award>()._image.sprite = _data._awardSpriteList[awardSpriteNo - 1];
            go.SetActive(true);
            awardList_.Add(go);
        }
        for(int i = 0; i < enemyNum; ++i){
            int awardPosNO = _data._waveData[_data._nextWaveNum]._awardPosNo[i];
            Vector3 enemyPos = _data._showAwardPos[awardPosNO - 1];
            GameObject go = Instantiate(_view._enemy);
            go.transform.position = enemyPos;
            go.transform.localScale = _view._enemy.transform.localScale;
            go.transform.parent = _view._enemy.transform.parent;
            go.SetActive(true);
            enemyList_.Add(go);
        }
    }
    
    
}
