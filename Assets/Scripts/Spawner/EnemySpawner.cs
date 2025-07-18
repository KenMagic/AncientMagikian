using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	//public GameObject orcPrefab;
	public GameObject skeletonArcherPrefab;
	public Transform castle;

	void Start()
	{
		//InvokeRepeating(nameof(SpawnOrc), 2f, 30f);
		InvokeRepeating(nameof(SpawnSkeletonArcher), 2f, 30f);
	}

	//void SpawnOrc()
	//{
	//	GameObject orc = Instantiate(orcPrefab, transform.position, Quaternion.identity);
	//	orc.GetComponent<Orc>().target = castle;
	//}

	void SpawnSkeletonArcher()
	{
		GameObject skeletonArcher = Instantiate(skeletonArcherPrefab, transform.position, Quaternion.identity);
	}
}