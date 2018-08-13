using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField]
    private Enemy _enemy;
    private float _life = 0;
    private bool _doesMove;
    private float _movementSpeed;
    private int direction = -1;

    void Start()
    {
        _life = _enemy.life;
        _doesMove = _enemy.doesMove;
        _movementSpeed = _enemy.movementSpeed;
        StartCoroutine("MoveEnemy");
    }

    IEnumerator MoveEnemy()
    {
        while (_doesMove)
        {
            var nextPos = transform.position + new Vector3(direction * _movementSpeed, 0);
            var nextTile = Map.GetTileAt((int)nextPos.x, (int)nextPos.y);
            if (nextTile == null || nextTile.tag != "Ground")
            {
                direction *= -1;
                RotateWithDirection();
            }
            nextPos = transform.position + new Vector3(direction * _movementSpeed, 0);
            nextTile = Map.GetTileAt((int)nextPos.x, (int)nextPos.y);
            if (nextTile != null && nextTile.tag == "Ground")
            {
                gameObject.GetComponent<Rigidbody>().MovePosition(nextPos);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void RotateWithDirection()
    {
        if (direction == 1)
        {
            transform.Rotate(0, 0, 180);
        }
        else if (direction == -1)
        {
            transform.Rotate(0, 0, 0);
        }
    }

    void Update()
    {
        if (_life <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Shot")
        {
            _life--;
        }
    }
}