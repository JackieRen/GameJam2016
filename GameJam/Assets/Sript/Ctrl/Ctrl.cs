using UnityEngine;
using System.Collections;
using GDGeek;

public class Ctrl : MonoBehaviour {
    public View _view = null;
    public GameData _data = null;
    public int nextWaveNum_ = 0;

    private bool isPlayState = false;
    private bool isHaveFollower = false;
    private FSM fsm_ = new FSM();

    void Start()
    {
        fsm_.addState("begin", beginState());
        fsm_.addState("play", playState());
        fsm_.addState("end", endState());
        fsm_.init("begin");
    }

    void Update () 
    {
        
    }

    void FixedUpdate()
    {
        
    }
    
    public void fsmPost(string msg)
    {
        fsm_.post(msg);
    }
    
    public void follower(GameObject go)
    {
        
    }
    
    private State beginState()
    {
        StateWithEventMap state = new StateWithEventMap();
        state.onStart += delegate{
            _view._beginPanel.SetActive(true);
            _view._playPanel.SetActive(false);
            _view._endPanel.SetActive(false);
        };
        state.onOver += delegate{
            _view._beginPanel.SetActive(false);
            _view._playPanel.SetActive(true);
        };
        state.addEvent("begin", "play");
        return state;
    }
    
    private State playState()
    {
        // StateWithEventMap state = TaskState.Create(delegate{
        //     return new Task();
        // }, fsm_, "end");
        StateWithEventMap state = new StateWithEventMap();
        state.onStart += delegate{
            PlayPanelInit();
            isPlayState = true;
        };
        state.onOver += delegate{
            isPlayState = false;
        };
        state.addEvent("end", "end");
        return state;
    }
    
    private State endState()
    {
        StateWithEventMap state = new StateWithEventMap();
        state.onStart += delegate{
            _view._endPanel.SetActive(true);
            for(int i = 0; i < _data._enemyList.Length; ++i){
                _data._enemyList[i]._isMove = false;
            }
        };
        state.onOver += delegate{
            _view._endPanel.SetActive(false);
        };
        state.addEvent("again", "begin");
        return state;
    }
    
    private void PlayPanelInit()
    {
        nextWaveNum_ = 0;
        _view._player.transform.position = Vector3.zero;
        for(int i = 0; i < _data._enemyList.Length; ++i){
            _data._enemyList[i].gameObject.SetActive(false);
            _data._awardList[i].transform.parent.gameObject.SetActive(false);
        }
        int awardNo = Random.Range(0, 4);
        int spriteNo = Random.Range(0, 9);
        _data._awardList[awardNo]._image.sprite = _data._awardSpriteList[spriteNo];
        _data._awardList[awardNo]._awardSprite.sprite = _data._awardSpriteList[spriteNo];
        _data._awardList[awardNo].transform.parent.gameObject.SetActive(true);
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
    
    private int[] GetRandomList(int length, int minNum, int maxNum)
    {
        int[] numList = new int[length];
        for(int i = 0; i < length; ++i){
            int randomNumber = Random.Range(minNum, maxNum + 1);
            int num=0;
            for(int j = 0; j < i; ++j) {
                if (numList[j] == randomNumber){
                    num = num + 1;
                }
            }
            if(num == 0){
                numList[i] = randomNumber;
            }else{
                i = i - 1;  
            } 
        }
        return numList;
    }
    
}
