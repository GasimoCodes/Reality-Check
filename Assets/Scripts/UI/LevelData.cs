using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Level", menuName = "Gasimo/Level")]
public class LevelData : ScriptableObject
{

    public string LevelName;
    public string SceneName;
    public Sprite thumb;


}
