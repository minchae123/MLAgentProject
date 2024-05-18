using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 0.1f;
	public Vector2 moveDir = Vector2.right;

	[SerializeField] Transform segmentprefab;
	[SerializeField] private int spawnSegCountStart = 3;
	private List<Transform> segments = new List<Transform>();

	private IEnumerator Start()
	{
		segments.Add(transform);
		SetUp();

		while (true)
		{
			MovementSegment();

			yield return StartCoroutine("WaitForSeconds", moveSpeed);
		}
	}

	public void ChangeDir(Vector3 dir)
	{
		moveDir = dir;
	}

	public void ResetSnake()
	{
		/*if (segments.Count > 1)
		{
			foreach (var t in segments)
			{
				if(t == transform)
					continue;

				print(t);
				Destroy(t.gameObject);
				segments.Remove(t);
			}
		}*/
		SetUp();
	}

	private void MovementSegment()
	{
		/*for (int i = segments.Count - 1; i > 0; --i)
		{
			segments[i].position = segments[i - 1].position;
		}

		transform.position = (Vector2)transform.position + moveDir;*/

		Vector3 previousLocalPosition = transform.localPosition;

		transform.localPosition += (Vector3)moveDir * moveSpeed;

		Vector3 moveVector = transform.localPosition - previousLocalPosition;

		for (int i = 1; i < segments.Count; i++)
		{
		}
	}

	private void SetUp()
	{
		for (int i = 0; i < spawnSegCountStart; i++)
		{
			AddSegment();
		}
	}

	public void GrowUp()
	{
		spawnSegCountStart++;
	}

	private void AddSegment()
	{
		Transform seg = Instantiate(segmentprefab, transform);
		seg.position = segments[segments.Count - 1].position;
		segments.Add(seg);
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
