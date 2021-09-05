using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Tileset", menuName = "ScriptableObjects/Tileset", order = 1)]
public class Tileset : ScriptableObject
{
    public Sprite[] nearbyBombs;
    public Sprite hidden;
    public Sprite flagged;
    public Sprite bombIncorrect;
    public Sprite bombDetonated;
    public Sprite bombRevealed;
    public Font font;
}
