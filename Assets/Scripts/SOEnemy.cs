using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class SOEnemy : ScriptableObject
{

    public float life = 100;
    public float speed = 1;
    public float scale = 1;
    public Sprite sprite;

}
