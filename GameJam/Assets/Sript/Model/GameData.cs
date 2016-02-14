using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GameData : MonoBehaviour {

    public int _nextWaveNum = 0;
    public int _enemyDeathNum = 0;
    public WaveInfo[] _waveData = null;
    public Vector3[] _showAwardPos = null;
    public Vector3[] _showEnemyPos = null;
    public Sprite[] _awardSpriteList = null;
    
}
