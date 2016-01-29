using UnityEngine;
using System.Collections;
using GDGeek;
using Pathfinding.Serialization.JsonFx;

namespace YD.Model{
	public class CommonDataInfo : DataInfo {
		[JsonMember]
		public int newMailNum = 0;
		
	}
}


