using UnityEngine;
using System.Collections;
using GDGeek;

public class Ctrl : MonoBehaviour {
    public View _view = null;

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
        if (isPlayState){ 
            if(isHaveFollower){
                
            }
        }
    }
    
    
    public void fsmPost(string msg)
    {
        fsm_.post(msg);
    }
    
    private State beginState()
    {
        StateWithEventMap state = new StateWithEventMap();
        state.onStart += delegate{
            _view._beginPanel.SetActive(true);
            _view._playPanel.SetActive(false);
            _view._endPanel.SetActive(false);
            PlayPanelInit();
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
        };
        state.onOver += delegate{
            _view._endPanel.SetActive(false);
        };
        state.addEvent("again", "begin");
        return state;
    }
    
    private void PlayPanelInit()
    {
        
    }
    
}
