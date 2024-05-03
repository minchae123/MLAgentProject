using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
	[SerializeField] private GameObject goodItem;
	[SerializeField] private GameObject badItem;

	[SerializeField] private Transform initPos;

	private void Start()
	{
		SpawnItem();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.O))
		{
			SpawnItem();
		}
	}

	public void SpawnItem()
	{
		int rx = Random.Range(0, 19);
		int ry = Random.Range(0, 11);
		Vector3 pos = new Vector3(initPos.position.x + rx, initPos.position.y - ry);

		int r = Random.Range(0, 2);

		if(r == 0)
		{
			Instantiate(goodItem, pos, Quaternion.identity);
		}
		else
		{
			Instantiate(badItem, pos, Quaternion.identity);
		}
	}
}
