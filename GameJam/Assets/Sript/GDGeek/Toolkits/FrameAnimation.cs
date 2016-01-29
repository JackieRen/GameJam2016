using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace GDGeek{
	public class FrameTween : Tween {

		
		public int to = 0;
		public int from = 0;
		public delegate void Receiver (int i);
		public Receiver receiver = null;

		private int current_ = 0;
		
		override protected void OnUpdate (float factor, bool isFinished)
		{	
			if (isFinished) {
				if(current_ != to){
					current_ = to;
					receiver (current_);	
				}
			} else {

				int val = Mathf.FloorToInt((float)(from) * (1f - factor) + (float)(to) * factor);
				if(current_ != val){
					current_ = val;
					receiver (current_);	
				}
			}

			
		}
		
		/// <summary>
		/// Start the tweening operation.
		/// </summary>
		
		static public FrameTween Begin (GameObject go, float duration, int from, int to, Receiver receiver)
		{
			FrameTween comp = Tween.Begin<FrameTween>(go, duration);
			comp.to = to;
			comp.from = from;
			comp.current_ = from;
			receiver (comp.current_);
			comp.receiver = receiver;
			if (duration <= 0f)
			{
				comp.Sample(1f, true);
				comp.enabled = false;
			}
			return comp;
		}
		
	}
}