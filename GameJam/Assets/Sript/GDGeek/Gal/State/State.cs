using UnityEngine;
using System.Collections;
/*
namespace GDGeek.Gal{
	public class State : MonoBehaviour {
		public string _nextState = "";
		public GDGeek.Gal.TaskFactory _taskFactory = null;

		private StateWithEventMap state_ = null;

		private GDGeek.StateWithEventMap create(FSM fsm){
			StateWithEventMap state = null;
			if (_taskFactory != null) {
				state = TaskState.Create (delegate {
					return _taskFactory.createTask ();
				}, fsm, _nextState);
			} else {
				state = new GDGeek.StateWithEventMap ();	
			}
			return state;
		}

		public GDGeek.StateWithEventMap getState(FSM fsm) {
			if(state_ == null){
				state_ = create(fsm);
			}
			return state_;
		}
	}
}*/
