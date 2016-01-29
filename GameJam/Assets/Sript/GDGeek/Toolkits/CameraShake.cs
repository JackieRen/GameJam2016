using UnityEngine;
using System.Collections;
using System;


namespace GDGeek{
	public class CameraShake : MonoBehaviour {
		[Serializable]
		public struct Parameter{
			public Vector2 amplitude;
			public Tween.Method methon;
			public int times;
			public float time;
			public bool damp;
		}
		private Vector2 damp(bool d, Vector2 from, Vector2 to, float radio){
			if (d) {
				return (from * (1.0f - radio) + to * radio);
			}
			return to;
		}
		private Task move (Tween.Method method, Vector2 to, float time)
		{
			TweenTask tt = new TweenTask (delegate{

				Tween tween = TweenCamera.Begin(this.gameObject, time, to);
				tween.method = method;
				return tween;
			});
			return tt;
		}

		private Task shake(bool d, Tween.Method method, int times, float time, Vector2 original, Vector2 border1, Vector2 border2){
			TaskList tl = new TaskList ();
			int index = 0;
			float step = time / (float)(times);

			while (times > 0) {

				float radio = times*step/time;
				switch(index){
				case 0:
					index = 1;
					times -= 1;
					tl.push (move(method, damp(d, original, border1, radio), step));
					break;
				case 1:
					if(times >=2){
						index -= 2;
						index = -1;
						tl.push (move(method, damp(d, original, border2, radio), 2 * step));
					}else{
						times-= 1;
						index = 0;
						tl.push (move(method, original, 2 * step));
					}
					break;
				case -1:
					if(times >= 2){
						times -= 2;
						index = 1;
						tl.push (move(method, damp(d, original, border1, radio), 2 * step));
					}else{
						times -= 1;
						index = 0;
						tl.push (move(method, original, step));
					}
					break;
					
				}
			}
			return tl;
		}
		private bool _used = false;
		public bool used {
			get{
				return _used;
			}
			set{
				_used = value;
			}

		}
		public void doShake(Parameter p){
			if (used == false) {
				TaskManager.Run (shake(p));			
			}
		}
		public Task shake (Parameter p)
		{
			Task task = shake (p.damp, p.methon, p.times * 2, p.time, Vector2.zero, p.amplitude, -p.amplitude);
			TaskManager.PushFront (task, delegate {
				this.used = true;
						});
			TaskManager.PushBack (task, delegate {
								this.used = false;
						});

			return task;
		}

		// Use this for initialization
		void Start () {
			TaskList tl = new TaskList ();
			tl.push (new TaskWait (0.5f));
			Parameter p = new Parameter();

			tl.push (this.shake (p));
			TaskManager.Run (tl);
		}
		
	
	}
}