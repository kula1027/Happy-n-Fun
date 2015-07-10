using UnityEngine;
using System.Collections;

public class AI_RunAway : AI {
	GameObject runFrom;
	private Map_Path map_path;
	
	void Start () {
		awake = true;
		runFrom = GameObject.Find ("Player");
		map_path = Map_Path.Instance;

		gameObject.AddComponent<CircleCollider2D> ();

		speed = 1f;

		StartCoroutine (State_Idle ());
	}

	void Update () {
//		if (Vector2.Distance (runFrom.transform.position, transform.position) < 5) {
//			StopCoroutine(State_Idle());
//			GetComponent<Rigidbody2D> ().velocity = (transform.position - runFrom.transform.position).normalized;
//		} else if(isIdle){
//			StartCoroutine (State_Idle ());
//			isIdle = false;
//		}
	}
}
