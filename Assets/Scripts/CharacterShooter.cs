using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShooter : MonoBehaviour
{

    [SerializeField]
    private GameObject _shotPrefab;

    private bool _canShoot = true;

    void Update()
    {
        if (Input.GetAxisRaw("Fire1") == 1)
        {
            if (_canShoot)
            {
                _canShoot = false;
                var shot = Instantiate(_shotPrefab, gameObject.transform.position, Quaternion.identity);
                shot.transform.up = gameObject.transform.up;
                Destroy(shot, 5);
            }

        }
        else
        {
            _canShoot = true;
        }
    }

}