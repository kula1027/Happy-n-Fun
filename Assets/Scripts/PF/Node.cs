using UnityEngine;
using System.Collections;

public class Node {
	public Vector2 pos;
	public bool[,] walkable;
	public bool open;

	public Node parent;

	public int F;
	public int G;
	public int H;

	public Node(){
		pos = Vector2.zero;
		walkable = new bool[3, 3];
		for (int loop = 0; loop < 3; loop++) {
			for(int loop2 = 0; loop2 < 3; loop2++){
				walkable[loop, loop2] = true;
			}
		}

		open = true;

		parent = null;

		F = 0;
		G = 0;
		H = 0;
	}

	public void setPos(int x, int y){
		pos = new Vector2 (x, y);
	}
}
