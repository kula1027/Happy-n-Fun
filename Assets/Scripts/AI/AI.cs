using UnityEngine;
using System.Collections;

public class AI: MonoBehaviour {
	public bool awake;
	protected int detSize;
	protected float speed;
	
	void Awake(){
		oriPos = transform.position;
		isIdle = true;
	}

	protected Vector2 oriPos;
	private Vector2 nxtDest;
	protected bool isIdle;
	protected IEnumerator State_Idle(){
		while(true){
			nxtDest = new Vector2 (Random.Range (-3, 3), Random.Range (-3, 3));
			nxtDest = Helper.V3toV2(transform.position) - nxtDest;
			Move (nxtDest);
			yield return new WaitForSeconds (Random.Range(0, 4));
			gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			yield return new WaitForSeconds (Random.Range(0, 4));
		}
	}

	protected void State_Aggressive(){

	}

	protected void Move(Vector3 to){
		Vector2 v = new Vector2 (to.x - transform.position.x, to.y - transform.position.y);
		GetComponent<Rigidbody2D>().velocity = v.normalized * speed;
	}
}
