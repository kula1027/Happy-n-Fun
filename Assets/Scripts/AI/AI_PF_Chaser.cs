using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_PF_Chaser : AI {
	public GameObject followee;

	private PathFinder pf;
	private Vector2 dest; //destination coordinate
	private Vector2 destTemp; //distance to the destination during PF
	private float distDest; //distance to the destination
	private List<Vector2> list; //list holding the path

	private bool pfFlag;//refresh path depend on the flag
	private float refreshRate;

	void Start(){
		detSize = 21;
		pf = gameObject.AddComponent<PathFinder> ();
		pf.Initiate (detSize);
		awake = true;
		list = new List<Vector2> ();
		destTemp = transform.position;
		distDest = 0;

		speed = 2f;

		pfFlag = true;
		refreshRate = 1;
	}
	void Update () {
		if (awake) {
			if (pfFlag) {
				StartCoroutine(RefreshPath());
				pfFlag = false;
			}
			Chase();
		}
	}

	bool Chase(){
		distDest = Vector2.Distance (transform.position, Helper.V3toV2(followee.transform.position));

		if (Vector2.Distance (transform.position, destTemp) < 0.2f) {
			if(distDest < 1.5f || distDest > detSize){//too far or close enough
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				return false;
			}

			if(list.Count > 0){
				destTemp = list[list.Count - 1];
				list.RemoveAt(list.Count - 1);
			}
		} else {
			Move(destTemp);
		}

		return true;
	}

	IEnumerator RefreshPath(){
		list = pf.FindPath(transform.position, followee.transform.position);
		yield return new WaitForSeconds(refreshRate);
		pfFlag = true;
	}
}
