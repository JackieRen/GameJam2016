using UnityEngine;
using System.Collections;

namespace YD.Common.View{
	public class FrameAnimation : MonoBehaviour {
		public UnityEngine.UI.Image image = null;	
		public Sprite[] spirets = null;
		public float framesPerSecond = 0;//
		public int timesShot = 0;//can play times
		private int curTimes = 0;//current play times
		
		public bool isPlay_ = false;
		private float startTime_ = 0;
		
		public void play(){
			Debug.Log("play");
			image.gameObject.SetActive (true);
			startTime_ = Time.time;
			curTimes = 0;
			isPlay_ = true;
		}
		
		void Awake () {
			if (!isPlay_) {
				isPlay_ = false;
				image.gameObject.SetActive (false);
			}
		}
		
		void Update () {
			if (isPlay_) {
				playAnimation ();
			}
		}

		private void playAnimation(){
			int index = Mathf.FloorToInt ((Time.time - startTime_) * framesPerSecond);
			if(timesShot <= 0){
				if(index >= spirets.Length){
					startTime_ = Time.time;	
					index = 0;
					curTimes++; 
				}else{
					image.sprite = spirets [index];
				}
			}else{
				if(curTimes < timesShot){
					if(index >= spirets.Length){
						startTime_ = Time.time;	
						index = 0;
						curTimes++;
					}else{
						image.sprite = spirets [index];
					}
				}else if(curTimes <= 0){
					if(index >= spirets.Length){
						startTime_ = Time.time;	
						index = 0;
					}else{
						image.sprite = spirets [index];
					}
				}else{
					isPlay_ = false;	
					image.gameObject.SetActive(false);
					image.sprite = spirets[0];
				}
			}
		}
	}
}

