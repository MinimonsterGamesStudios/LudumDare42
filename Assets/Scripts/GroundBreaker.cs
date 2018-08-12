using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Tile))]
public class GroundBreaker : MonoBehaviour
{
    [SerializeField]
    private float _initialOnCollisionBreakTime = 1f;
    [SerializeField]
    private float _initialBreakTime = 1f;
    [SerializeField]
    private float _initialBreakTimeDiff = 1f;
    [SerializeField]
    private float _breakTimeAcceleration = 0.1f;
    [SerializeField]
    private float _minBreakTimeDiff = 0.1f;

    public static float? breakTime = null;
    public static float? breakTimeDiff = null;
    public static float? onCollisionbreakTimeDiff = null;

    private TileModel _tile;
    private Animator _animator;
    private Renderer _renderer;
    private Material _brokenTileMaterial;

    private bool _blockIsBroken = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<Renderer>();
        _tile = GetComponent<Tile>().GetTileModel();
        _brokenTileMaterial = _tile.brokenTileMaterial;
        if (breakTime == null)
        {
            breakTime = _initialBreakTime;
        }
        if (breakTimeDiff == null)
        {
            breakTimeDiff = _initialBreakTimeDiff;
        }
        StartCoroutine("BreakBlock");
    }

    private void Update()
    {
        if (_blockIsBroken && !_renderer.isVisible)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator BreakBlock()
    {
        if (breakTime == null)
        {
            breakTime = _initialBreakTime;
        }
        if (breakTimeDiff == null)
        {
            breakTimeDiff = _initialBreakTimeDiff;
        }
        if (onCollisionbreakTimeDiff == null)
        {
            onCollisionbreakTimeDiff = _initialOnCollisionBreakTime;
        }
        yield return new WaitForSeconds(_initialBreakTime);
        breakTime += breakTimeDiff;
        yield return new WaitForSeconds((float)breakTime + Random.Range(0, 5));
        if (breakTimeDiff - _breakTimeAcceleration > _minBreakTimeDiff)
        {
            breakTimeDiff -= _breakTimeAcceleration;
        }
        if (onCollisionbreakTimeDiff - _breakTimeAcceleration > _minBreakTimeDiff)
        {
            onCollisionbreakTimeDiff -= _breakTimeAcceleration;
        }
        _renderer.material = _brokenTileMaterial;
        _animator.enabled = true;
        yield return new WaitForSeconds(30);
        _blockIsBroken = true;
    }

    IEnumerator BreakBlockOnCollision()
    {
        _renderer.material = _brokenTileMaterial;
        yield return new WaitForSeconds((float)onCollisionbreakTimeDiff);
        _animator.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.tag == "Player")
        {
            StartCoroutine("BreakBlockOnCollision");
        }
    }

    public static void ResetTimeValues()
    {
        breakTime = null;
        breakTimeDiff = null;
    }
}
