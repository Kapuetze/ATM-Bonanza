using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Standard", menuName = "Game/Difficulty", order = 1)]
public class Difficulty : ScriptableObject
{
    public int maxRequestedAmount = 100;
    public int minRequestedAmount = 5;
    public float patienceMultiplier = 5f;
}
