using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitView : MonoBehaviour
{
    public Unit UnitCore { get; set; }
    private Rigidbody rb;
    [SerializeField] private float rotationLerp = 3f;
    private static GameObject deathExplosionPrefab;
    private ParticleSystem shootEffect;

    void Awake()
    {
	    rb = GetComponent<Rigidbody>();
	    if (shootEffect == null)
	    {
		    shootEffect = GetComponentInChildren<ParticleSystem>();
	    }
    }

    public void Bind(Unit unit)
    {
        UnitCore = unit;
		UnitCore.OnMessage += OnDebugMessage;
		UnitCore.AddOnDeathListener(OnUnitDeath);
    }
	
	private void OnDestroy()
 	{
		UnitCore.OnMessage -= OnDebugMessage;
		UnitCore.RemoveOnDeathListener(OnUnitDeath);
	}
    private void FixedUpdate()
    {
        DebugDraw.DrawCube(UnitCore.NextPathPointPosition, 1, new Color(1, 1, 0, 0.5f));
        if (UnitCore == null) return;

        // рух через Rigidbody.MovePosition (фізичний рух)
        var move = UnitCore.TargetDirection * (UnitCore.Speed * Time.fixedDeltaTime);
        rb.MovePosition(transform.position + move);

        // обертання в Update/FixedUpdate для плавності
        if (UnitCore.TargetDirection.sqrMagnitude > 0.05f)
        {
            Quaternion targetRot = Quaternion.LookRotation(UnitCore.TargetDirection);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRot, rotationLerp * Time.fixedDeltaTime));
        }
    }

    // Візуалізація пострілу (наприклад)
    public void OnShootVisual(AttackBehaviour b)
    {
	    Debug.Log("Effect");
	    if (shootEffect != null)
		    shootEffect.Play();
    }
    
    private void OnUnitDeath(Unit unit)
    {
	    // анімація смерті, ефект або знищення об’єкта
		// DebugUtil.Log(gameObject, $"{UnitCore.Name} died.");
		if (deathExplosionPrefab == null)
		{
			deathExplosionPrefab = Resources.Load<GameObject>("PixelExplosion");
		}
		if (deathExplosionPrefab != null)
			Instantiate(deathExplosionPrefab, transform.position, Quaternion.identity);
	    Destroy(gameObject);
    }

	public void OnDebugMessage(string source, string msg)
	{
		DebugUtil.Log(gameObject, source, msg);
	}
	
	void OnDrawGizmos()
	{
		UnityEditor.Handles.Label(transform.position + Vector3.up * 2f,
			$"{name}\nID: {gameObject.GetInstanceID()}");
	}
}