using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Tile))]
public class GroundBreaker : MonoBehaviour
{
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
        yield return new WaitForSeconds(_initialBreakTime);
        breakTime += breakTimeDiff;
        yield return new WaitForSeconds((float)breakTime);
        if (breakTimeDiff - _breakTimeAcceleration > _minBreakTimeDiff)
        {
            breakTimeDiff -= _breakTimeAcceleration;
        }
        _renderer.material = _brokenTileMaterial;
        _animator.enabled = true;
        yield return new WaitForSeconds(30);
        _blockIsBroken = true;
    }


    public static void ResetTimeValues()
    {
        breakTime = null;
        breakTimeDiff = null;
    }
}
