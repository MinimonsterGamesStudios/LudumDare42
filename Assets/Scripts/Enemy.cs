using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "DataModels/New Enemy")]
public class Enemy : ScriptableObject
{
    public float life;
    public bool doesMove;
    public float movementSpeed;
}
