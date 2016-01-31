using UnityEngine;
using System.Collections;
using GDGeek;

public class Ctrl : MonoBehaviour {
    public View _view = null;
    public GameData _data = null;
    public Tree[] _treeList = null;
    public Animator _beginAnimator = null;
    public Animator _endAnimator = null;
    public Animator _black = null;
    public int nextWaveNum_ = 0;

    private FSM fsm_ = new FSM();

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
            TaskWait tw = new TaskWait(2f);
            TaskManager.PushBack(tw, delegate(){
                _view._endPanel.SetActive(true);
                for(int i = 0; i < _data._enemyList.Length; ++i){
                    _data._enemyList[i]._isMove = false;
                    _data._followerList[i]._isMove = false;
                }
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
        nextWaveNum_ = 0;
        _view._player.transform.position = Vector3.zero;
        for(int i = 0; i < _data._enemyList.Length; ++i){
            _data._enemyList[i].gameObject.SetActive(false);
            _data._followerList[i].gameObject.SetActive(false);
            _data._awardList[i].transform.parent.gameObject.SetActive(false);
            _data._rockList[i].SetActive(false);
        }
        int awardNo = Random.Range(0, 4);
        int spriteNo = Random.Range(0, 9);
        _data._awardList[awardNo]._image.sprite = _data._awardSpriteList[spriteNo];
        _data._awardList[awardNo]._awardSprite.sprite = _data._awardSpriteList[spriteNo];
        _data._awardList[awardNo].transform.parent.gameObject.SetActive(true);
        for(int i = 0; i < _treeList.Length; ++i){
            _treeList[i]._treeLifeNum = 20;
            _treeList[i].gameObject.SetActive(false);
            _treeList[i]._deathTree.gameObject.SetActive(false);
        }
        _treeList[0].gameObject.SetActive(true);
    }
    
    public void PlayNext()
    {
        char[] awardList = _data._showAwardPos[nextWaveNum_].ToCharArray();
        char[] awardSpriteList = _data._showAwardSprite[nextWaveNum_].ToCharArray();
        char[] enemyList = _data._showEnemyPos[nextWaveNum_].ToCharArray();
        for(int i = 0; i < awardSpriteList.Length; ++i){
            int num1 = int.Parse(awardList[i].ToString());
            int num2 = int.Parse(awardSpriteList[i].ToString());
            if(num1 != 0 && num2 != 0){
                _data._awardList[num1 - 1]._image.sprite = _data._awardSpriteList[num2 - 1];
                _data._awardList[num1 - 1]._awardSprite.sprite = _data._awardSpriteList[num2 - 1];
                _data._awardList[num1 - 1].transform.parent.gameObject.SetActive(true);
            }
        }
        for(int i = 0; i < enemyList.Length; ++i){
            int num1 = int.Parse(enemyList[i].ToString());
            int num2 = int.Parse(awardSpriteList[i].ToString());
            if(num1 != 0 && num2 != 0){
                _data._enemyList[num1 - 1]._talkBoxSprite.sprite = _data._awardSpriteList[num2 - 1];
                _data._enemyList[num1 - 1].gameObject.SetActive(true);
                _data._enemyList[num1 - 1]._isMove = true;
            }
        }
    }
    
    
}
