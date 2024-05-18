using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
	[SerializeField] private GameObject goodItem;
	[SerializeField] private GameObject badItem;

	[SerializeField] private Transform initPos;

	private List<GameObject> items = new List<GameObject>();

	private void Start()
	{
		StartCoroutine(SpawnCoroutine());
	}

	private IEnumerator SpawnCoroutine()
	{
		while (true)
		{
			SpawnItem();
			yield return new WaitForSeconds(5f);
		}
	}

	public void ResetSpawn()
	{
		StopAllCoroutines();

		if (items.Count > 0)
		{
			foreach (GameObject i in items)
			{
				Destroy(i);
			}
			items.Clear();

		}
		StartCoroutine(SpawnCoroutine());
	}


	public void SpawnItem()
	{
		int rx = Random.Range(0, 19);
		int ry = Random.Range(0, 11);
		Vector3 pos = new Vector3(initPos.position.x + rx, initPos.position.y - ry);

		int r = Random.Range(0, 2);

		if (r == 0)
		{
			GameObject o =  Instantiate(goodItem, pos, Quaternion.identity);
			items.Add(o);
		}
		else
		{
			GameObject o = Instantiate(badItem, pos, Quaternion.identity);
			items.Add(o);
		}

	}
}
