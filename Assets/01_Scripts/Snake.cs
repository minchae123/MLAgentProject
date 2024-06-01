using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 0.1f;
	public Vector2 moveDir = Vector2.right;

	[SerializeField] Transform segmentprefab;
	[SerializeField] private int spawnSegCountStart = 4;
	[SerializeField] private Transform parent;

	private List<Transform> segments = new List<Transform>();

	private bool isStart = false;


	private IEnumerator Start()
	{
		SetUp();

		while (true)
		{
			MovementSegment();

			yield return StartCoroutine("WaitForSeconds", moveSpeed);
		}
	}

	private void MovementSegment()
	{
		transform.localPosition = (Vector2)transform.localPosition + moveDir;
	}


	private void SetUp()
	{
		
	}

	public void GrowUp(int c)
	{
		spawnSegCountStart += c;
	}

	private void AddSegment()
	{
		Transform seg = Instantiate(segmentprefab, parent);
		seg.localPosition = segments[segments.Count - 1].localPosition;
		segments.Add(seg);
	}


	public void ChangeDir(Vector3 dir)
	{
		moveDir = dir;
	}

	private IEnumerator WaitForSeconds(float time)
	{
		float cur = 0;
		float percent = 0;

		while (percent < 1)
		{
			cur += Time.deltaTime;
			percent = cur / time;

			yield return null;
		}
	}
}
