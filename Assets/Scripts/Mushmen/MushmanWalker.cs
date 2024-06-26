﻿using Assets.Scripts;
using UnityEngine;

public class MushmanWalker : MushmanBase {

	[SerializeField]
	private float _speed = 1.0f;

  [SerializeField]
  private float _attackSpeed = 0.5f;
  private float _attackTimer = 0.0f;

  private bool _isAttacking = false;

  private GameObject _target = null;

	private Rigidbody2D _rigidBody = null;

  private void Awake() {
    _target = null;
    _rigidBody = this.GetComponent<Rigidbody2D>();
  }

  private void Update() {
    if(_isAttacking) {
      _attackTimer -= Time.deltaTime;
      if(_attackTimer <= 0.0f) {
        FindAndAttackEnemy();
      }
    }
  }

  private void FindAndAttackEnemy() {
    // Find all Enemies within its radius.
    bool foundEnemy = false;
    Collider2D[] colliders = null;
    ContactFilter2D filter = new ContactFilter2D();
    filter.NoFilter();
    Physics2D.OverlapCollider(this.GetComponent<Collider2D>(), filter, colliders);
    if(colliders != null && colliders.Length > 0) {
      foreach(Collider2D collider in colliders) {
  			if(collider.tag == Constants.Tags.Enemy) {
          foundEnemy = true;
  				Enemy enemy = collider.GetComponent<Enemy>();
  				MushmanBase mushman = this.GetComponent<MushmanBase>();
  				mushman.AttackTarget(enemy);
          break;
        }
      }
    }

    if(!foundEnemy) {
      _isAttacking = false;
    }
  }

  override public void AttackTarget(Enemy enemy) {
    _attackTimer = _attackSpeed;
    base.AttackTarget(enemy);
  }

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == Constants.Tags.Enemy) {
			Enemy enemy = other.gameObject.GetComponent<Enemy>();
			AttackTarget(enemy);
		}
	}

  private void FixedUpdate() {
		MoveToTarget();
	}

	private void FindTarget() {
		float bestDistance = float.MaxValue;
		GameObject newTarget = null;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(Constants.Tags.Enemy);
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
			Vector3 dirToTarget = (_target.transform.position - this.transform.position).normalized;

			_rigidBody.velocity = (dirToTarget * Time.fixedDeltaTime * _speed);
		}
	}
}
