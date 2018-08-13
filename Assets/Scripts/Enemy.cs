using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "DataModels/New Enemy")]
public class Enemy : ScriptableObject
{
    public float life;
    public bool doesMove;
    public float movementSpeed;
    public AudioClip hitAudio;
}
