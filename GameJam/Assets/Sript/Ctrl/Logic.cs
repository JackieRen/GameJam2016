using UnityEngine;
using System.Collections;
using GDGeek;

public class Logic : MonoBehaviour 
{
    
    // public MissionCtrl _ctrl = null;
    
    // public void missionItem(GameObject go){
    //     this.fsmPostObj ("receive", go); 	
    // }
    
    // protected override State openState()
    // {
    //     State state = TaskState.Create (delegate {
    //         return _ctrl.openTask ();
    //     }, this.fsm_, "main");
    //     return state;
    // }
    
    // protected override State closeState()
    // {
    //     State state = TaskState.Create (delegate {
    //         return _ctrl.closeTask ();
    //     }, this.fsm_, "sleep");
    //     return state;
    // }
    
    // private State managerState()
    // {
    //     StateWithEventMap state = new StateWithEventMap ();
    //     state.addEvent ("closeMission", "close");
    //     return state;
    // }
    
    // private State refreshListWebState(){
    //     StateWithEventMap state = TaskState.Create (delegate {
    //         YD.Model.GameData gd = YD.Model.GameData.GetInstance ();
    //         YD.Common.Relink window = YD.Common.Root.GetInstanse()._window;
    //         YD.Common.Relink.WebTask link = window.relinkTask<YD.Model.MissionBagInfo>(YD.Common.Root.GetInstanse().url_+"task/load",
    //                                         delegate(YD.Model.MissionBagInfo info) {
    //             gd.load (info);
    //         }
    //         );
    //         link.pack.addField ("uuid",gd.user.data.uuid);
    //         link.pack.addField("password", gd.user.data.password);
    //         TaskManager.PushBack(link, delegate {
    //             _ctrl.refreshMissionList();
    //         });
    //         return link;
    //     }, fsm_, "list");
    //     return state;
    // }
    
    // private State mainState()
    // {
    //     State state = TaskState.Create (delegate {
    //         return new Task();
    //     }, fsm_, delegate {
    //         return "refreshList.web";
    //     });
    //     return state;
    // }
    
    // private State listState(){
    //     StateWithEventMap state = new StateWithEventMap ();
    //     state.addAction ("receive", delegate(FSMEvent evt) {
    //         YD.Mission.View.MissionItem item = ((GameObject)(evt.obj)).GetComponent<YD.Mission.View.MissionItem> ();
    //         _ctrl.receive(item);
    //         return "receive.web";
    //     });
    //     return state;
    // }
    
    // private State receiveWebState(){
    //     StateWithEventMap state = TaskState.Create (delegate {
    //         YD.Model.GameData gd = YD.Model.GameData.GetInstance ();
    //         YD.Common.Relink window = YD.Common.Root.GetInstanse()._window;
    //         YD.Common.Relink.WebTask link = window.relinkTask<YD.Model.AwardBagInfo>(YD.Common.Root.GetInstanse().url_+"task/get?",
    //                                         delegate(YD.Model.AwardBagInfo info) {
    //             gd.loadMission (info);
    //             YD.Lobby.View.LobbyUI.GetInstance().refreshPLayerLvUp();
    //         }
    //         );
    //         link.pack.addField ("uuid",gd.user.data.uuid);
    //         link.pack.addField("password", gd.user.data.password);
    //         link.pack.addField("taskId", _ctrl.curMissionData.id);
    //         TaskManager.PushBack(link, delegate {
    //             _ctrl.refreshMissionList();
    //         });
    //         return link;
    //     }, fsm_, "list");
    //     return state;
    // }
    
    // private State showState(){
    //     StateWithEventMap state = new StateWithEventMap ();
    //     return state;
    // }
    
    // public void Start()
    // {
    //     base.Start ();
    //     fsm_.addState("manager", managerState(), "weakup");
    //     fsm_.addState("main", mainState(),"weakup");
        
    //     fsm_.addState("show", showState(),"manager");
    //     fsm_.addState("refreshList.web", refreshListWebState(),"manager");
    //     fsm_.addState("list", listState(),"manager");
    //     fsm_.addState("receive.web", receiveWebState(),"manager");
    // }

}                  



