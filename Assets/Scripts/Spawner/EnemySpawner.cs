using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public GameObject orcPrefab;
	public Transform castle;

	void Start()
	{
		InvokeRepeating(nameof(SpawnOrc), 2f, 30f);
	}

	void SpawnOrc()
	{
		GameObject orc = Instantiate(orcPrefab, transform.position, Quaternion.identity);
		orc.GetComponent<Orc>().target = castle;
	}
}