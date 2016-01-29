using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace GDGeek{
	public class WebPlayer : MonoBehaviour {
		public Text _log;
		public void log(string text){
			_log.text = text;
		}
		private static void Register(){
			
			Application.ExternalEval (
				"function JamSendMessage(g, f, p){" +
					"var es = document.getElementsByTagName('embed');" +
					"for(var i = 0; i<es.length; ++i){" +
						"if(es[i].src.indexOf('unity')){es[i].SendMessage(g, f, p);}" +
					"}" +
				"}" 
				);

			/*
			Application.ExternalEval (
				"function post(url, params) {" +
				"var temp = document.createElement('form');" +
				"temp.method = 'post';" +
				"temp.style.display = 'none';" +
				"for (var x in params) {" +
				"var opt = document.createElement('textarea');" +
				"opt.name = x;" +
				"opt.value = params[x];" +
				"temp.appendChild(opt);" +
				"}" +
				"document.body.appendChild(temp);" +
				"temp.submit();" +
				"return temp;" +
				"}" 
				);*/
		}
		public static void Share(string text, string url){
			
			
			Application.ExternalEval (
				"if(oBody && div){oBody.removeChild(div);}"+
				"var oBody = document.getElementsByTagName('BODY').item(0);" +
				"var div= document.createElement('div');" +
				"div.height = '280';" +
				"div.width = '440';" +
				"div.hspace = '0';" +
				"div.vspace = '0';" +
				"div.style.position ='absolute';" +
				"div.style.top ='50%';" +
				"div.style.left ='50%';" +
				"div.style.margin ='-100px 0 0 -100px';" +
				"div.style.width ='200px';" +
				"div.style.height ='90px';" +
				"div.style.display ='';" +
				"div.style.border ='1px solid red';" +
				"div.id = 'jam_window';  " +
				"div.style.background= 'yellow';" +
				"div.marginwidth = '0';" +
				"div.marginheight = '0';" +
				"var share = document.createElement('A');" +
				"share.setAttribute('href','"+url+"');" +
				"share.appendChild(document.createTextNode('"+text+"'));" +
				"div.appendChild(document.createElement('BR'));" +
				"div.appendChild(share);" +
				"div.appendChild(document.createElement('BR'));" +
				"div.appendChild(document.createElement('BR'));" +
				"var close = document.createElement('A');" +
				"close.setAttribute('href','#'); " +
				"close.setAttribute('onclick','oBody.removeChild(div);');   " +
				"close.appendChild(document.createTextNode('关闭'));" +
				"div.appendChild(close);" +
				"oBody.appendChild(div);"
				);
			
			
		}
		void Start () {
			WebPlayer.Register ();
		}
		

	}
}