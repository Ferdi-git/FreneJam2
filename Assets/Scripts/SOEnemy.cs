using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class SOEnemy : ScriptableObject
{

    public float life = 100;
    public float speed = 1;
    public float scale = 1;
    public float brightnessCloseness = 2;
    public int points = 10;
    public Sprite sprite;

}
