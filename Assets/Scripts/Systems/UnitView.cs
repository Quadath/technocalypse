using UnityEngine;
using Game.Core;
using Game.Shared.Core;

[RequireComponent(typeof(Rigidbody))]
public class UnitView : MonoBehaviour
{
    [SerializeField] private float rotationLerp = 3f;
    
    private Rigidbody _rb;
    private ParticleSystem _shootEffect;
    private Unit _test;
    
    public IUnit UnitCore { get; private set; }
    
    private static GameObject _deathExplosionPrefab;

    void Awake()
    {
        _test = new Unit("penis", 0, null, null, 2, 100, null);
	    _rb = GetComponent<Rigidbody>();
	    if (!_shootEffect)
	    {
		    _shootEffect = GetComponentInChildren<ParticleSystem>();
	    }
    }

    public void Bind(IUnit unit)
    {
        UnitCore = unit;
        UnitCore.callback = (IUnit u) => { DebugUtil.Log(gameObject, "Unit", "GC COLLECTED ME"); };
		// UnitCore.OnMessage += OnDebugMessage;
		UnitCore.AddOnDeathListener(OnUnitDeath);
    }
	
	private void OnDestroy()
 	{
		// UnitCore.OnMessage -= OnDebugMessage;
        UnitCore = null;
	}
    private void FixedUpdate()
    {
        DebugDraw.DrawCube(UnitCore.NextPathPointPosition, 1, new Color(1, 1, 0, 0.5f));
        if (UnitCore == null) return;

        // рух через Rigidbody.MovePosition (фізичний рух)
        var move = UnitCore.TargetDirection * (UnitCore.Speed * Time.fixedDeltaTime);
        _rb.MovePosition(transform.position + move);

        // обертання в Update/FixedUpdate для плавності
        if (!(UnitCore.TargetDirection.sqrMagnitude > 0.05f)) return;
        var targetRot = Quaternion.LookRotation(UnitCore.TargetDirection);
        _rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRot, rotationLerp * Time.fixedDeltaTime));
    }

    // Візуалізація пострілу (наприклад)
    public void OnShootVisual(AttackBehaviour b)
    {
	    if (_shootEffect)
		    _shootEffect.Play();
    }
    
    private void OnUnitDeath(ITargetable target)
    {
        target.RemoveOnDeathListener(OnUnitDeath);
		if (!_deathExplosionPrefab)
		{
			_deathExplosionPrefab = Resources.Load<GameObject>("PixelExplosion");
		}
		if (_deathExplosionPrefab)
			Instantiate(_deathExplosionPrefab, transform.position, Quaternion.identity);
	    Destroy(gameObject);
    }

	// private void OnDebugMessage(string source, string msg)
	// {
	// 	DebugUtil.Log(gameObject, source, msg);
	// }
	
	void OnDrawGizmos()
	{
		UnityEditor.Handles.Label(transform.position + Vector3.up * 2f,
			$"{name}\nID: {gameObject.GetInstanceID()}");
	}
}