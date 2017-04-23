﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushmanSeeker : MonoBehaviour {

	[SerializeField]
	private float _speed = 1.0f;

	[SerializeField]
	private float _speedRamp = 0.0f;

	private GameObject _target = null;

	private void Awake() {
		_target = null;
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	private void Update () {
		_speed += (_speedRamp * Time.deltaTime);

		MoveToTarget();
	}

	private void FindTarget() {
		float bestDistance = float.MaxValue;
		GameObject newTarget = null;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(GameObject enemy in enemies) {
			//DebugDrawTarget(enemy);
			float distance = (enemy.transform.position - this.transform.position).sqrMagnitude;
			if(distance < bestDistance) {
				newTarget = enemy;
				bestDistance = distance;
			}
		}
		if(newTarget) {
			_target = newTarget;
		}
	}

	private void MoveToTarget() {
		if(!_target) {
			FindTarget();
		}
		if(_target) {
			//DebugDrawTarget(_target);

			Vector3 dirToTarget = (_target.transform.position - this.transform.position).normalized;
			//Debug.DrawRay(this.transform.position, dirToTarget, Color.magenta);
			this.transform.position += (dirToTarget * Time.deltaTime * _speed);
		}
	}

	private void DebugDrawTarget(GameObject go) {
		Debug.DrawRay(go.transform.position, Vector3.up, Color.red);
		Debug.DrawRay(go.transform.position, Vector3.left, Color.green);
	}
}
