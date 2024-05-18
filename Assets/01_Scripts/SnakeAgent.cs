using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;
using UnityEngine;

public class SnakeAgent : Agent
{
	private Snake snake;

	public ItemSpawner spawner;

	public override void Initialize()
	{
		snake = GetComponent<Snake>();
		spawner = transform.parent.GetChild(2).GetComponent<ItemSpawner>();
	}

	public override void OnEpisodeBegin()
	{
		transform.localPosition = Vector3.zero;
		snake.ResetSnake();
		spawner.ResetSpawn();
	}

	public override void CollectObservations(VectorSensor sensor)
	{

	}

	public override void OnActionReceived(ActionBuffers actions)
	{
		var DiscreateActions = actions.DiscreteActions;

		Vector2 dir = snake.moveDir;

		switch (DiscreateActions[0])
		{
			case 1: dir = Vector2.up; break;
			case 2: dir = Vector2.down; break;
			case 3: dir = Vector2.right; break;
			case 4: dir = Vector2.left; break;
		}

		snake.ChangeDir(dir);

		AddReward(-0.01f);
	}

	public override void Heuristic(in ActionBuffers actionsOut)
	{
		var DiscreateActions = actionsOut.DiscreteActions;

		if (Input.GetKey(KeyCode.W))
		{
			DiscreateActions[0] = 1;
		}
		if (Input.GetKey(KeyCode.S))
		{
			DiscreateActions[0] = 2;
		}
		if (Input.GetKey(KeyCode.A))
		{
			DiscreateActions[0] = 4;
		}
		if (Input.GetKey(KeyCode.D))
		{
			DiscreateActions[0] = 3;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Good"))
		{
			AddReward(1f);
			snake.GrowUp();
			Destroy(collision.gameObject);
			EndEpisode();
		}
		else if (collision.CompareTag("Bad"))
		{
			AddReward(-1f);
			Destroy(collision.gameObject);
			EndEpisode();
		}
		else if(collision.CompareTag("Body") || collision.CompareTag("Wall"))
		{
			AddReward(-1f);
			EndEpisode();
		}
	}
}
