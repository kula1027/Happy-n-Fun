using UnityEngine;
using System.Collections;

public class Map_Path : MonoBehaviour {
	public Node[,] node;
	int size;

	private static Map_Path instance;
	public static Map_Path Instance { get { return instance; } private set { } }

	void Awake(){
		instance = this;

		size = Config.MAPSIZE * 4;
		node = new Node[size, size];
		for (int loop = 0; loop < size; loop++) {
			for(int loop2 = 0; loop2 < size; loop2++){
				node[loop, loop2] = new Node();
				node[loop, loop2].pos = new Vector2(loop, loop2);
			}
		}

		//배열 번호 - size / 2 == pos

		for (int loop = 1; loop < size - 1; loop++) {
			for(int loop2 = 1; loop2 < size - 1; loop2++){

				for(int loop3 = -1; loop3 < 2; loop3++){
					for(int loop4 = -1; loop4 < 2; loop4++){
						if(loop3 == 0 && loop4 == 0)continue;
						if(Physics2D.Raycast(node[loop, loop2].pos, new Vector2(loop3, loop4), new Vector2(loop3, loop4).magnitude + 0.1f)){
							node[loop, loop2].walkable[loop3 + 1, loop4 + 1] = false;
						}
					}
				}
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;

		for (int loop = 1; loop < size - 1; loop++) {
			for(int loop2 = 1; loop2 < size - 1; loop2++){

				for(int loop3 = -1; loop3 < 2; loop3++){
					for(int loop4 = -1; loop4 < 2; loop4++){
						if(node[loop, loop2].walkable[loop3 + 1, loop4 + 1]){
							Gizmos.DrawLine(node[loop, loop2].pos, node[loop + loop3, loop2 + loop4].pos);
						}
					}
				}
			}
		}
	}
}