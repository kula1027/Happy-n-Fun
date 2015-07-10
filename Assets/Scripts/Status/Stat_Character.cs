using UnityEngine;
using System.Collections;

public class Stat_Character : MonoBehaviour {
	int hp;
	int hunger;
	int speed;

	void Start () {
		StatSet (Config.CHARACTER);
	}

	void StatSet(int ch_num){
		switch (ch_num) {
		case 0:
			hp = 100;
			hunger = 100;
			break;

		}
	}
}
