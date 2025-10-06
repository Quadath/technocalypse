using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitView : MonoBehaviour
{
    public Unit UnitCore { get; set; }
    private Rigidbody rb;
    [SerializeField] private float rotationLerp = 3f;

    void Awake() => rb = GetComponent<Rigidbody>();

    public void Bind(Unit unit)
    {
        UnitCore = unit;
		UnitCore.OnMessage += OnDebugMessage;
		UnitCore.AddOnDeathListener((Unit) => OnUnitDeath());
    }
	
	private void OnDestroy()
 	{
		UnitCore.OnMessage -= OnDebugMessage;
	}
    private void FixedUpdate()
    {
        DebugDraw.DrawCube(UnitCore.NextPathPointPosition, 1, new Color(1, 1, 0, 0.5f));
        if (UnitCore == null) return;

        // рух через Rigidbody.MovePosition (фізичний рух)
        Vector3 move = UnitCore.TargetDirection * UnitCore.Speed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + move);

        // обертання в Update/FixedUpdate для плавності
        if (UnitCore.TargetDirection.sqrMagnitude > 0.05f)
        {
            Quaternion targetRot = Quaternion.LookRotation(UnitCore.TargetDirection);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRot, rotationLerp * Time.fixedDeltaTime));
        }
    }

    // Візуалізація пострілу (наприклад)
    public void OnShootVisual(Vector3 target)
    {
        // тут можна Instantiate(projectilePrefab) або LineRenderer, particle, etc.
    }
    
    private void OnUnitDeath()
    {
	    // анімація смерті, ефект або знищення об’єкта
		DebugUtil.Log(gameObject, $"{UnitCore.Name} died.");
	    Destroy(gameObject);
    }

	public void OnDebugMessage(string msg)
	{
		DebugUtil.Log(gameObject, msg);
	}
	
	void OnDrawGizmos()
	{
		UnityEditor.Handles.Label(transform.position + Vector3.up * 2f,
			$"{name}\nID: {gameObject.GetInstanceID()}");
	}
}