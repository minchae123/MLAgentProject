using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class SnakeAgent : Agent
{
	[SerializeField] private float moveSpeed = 0.1f;
	[SerializeField] Vector2 moveDir = Vector2.right;

	[SerializeField] Transform segmentprefab;
	[SerializeField] private int spawnSegCountStart = 4;
	private List<Transform> segments = new List<Transform>();

	private IEnumerator Start()
	{
		SetUp();

		while (true)
		{
			//transform.position = (Vector2)transform.position + moveDir;
			MovementSegment();

			yield return StartCoroutine("WaitForSeconds", moveSpeed);
		}
	}

	private void MovementSegment()
	{
		for (int i = segments.Count - 1; i > 0; --i)
		{
			segments[i].position = segments[i-1].position;
		}

		transform.position = (Vector2)transform.position + moveDir;
	}

	private void SetUp()
	{
		segments.Add(transform);

		for (int i = 0; i < spawnSegCountStart; i++)
		{
			AddSegment();
		}
	}

	private void AddSegment()
	{
		Transform seg = Instantiate(segmentprefab);
		seg.position = segments[segments.Count - 1].position;
		segments.Add(seg);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.W)) moveDir = Vector2.up;
		else if (Input.GetKeyDown(KeyCode.S)) moveDir = Vector2.down;
		else if (Input.GetKeyDown(KeyCode.A)) moveDir = Vector2.left;
		else if (Input.GetKeyDown(KeyCode.D)) moveDir = Vector2.right;
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

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void OnEpisodeBegin()
	{
		base.OnEpisodeBegin();
	}

	public override void CollectObservations(VectorSensor sensor)
	{
		base.CollectObservations(sensor);
	}

	public override void Heuristic(in ActionBuffers actionsOut)
	{
		base.Heuristic(actionsOut);
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Good"))
		{

		}
		else if (collision.CompareTag("Bad"))
		{

		}
	}
}
