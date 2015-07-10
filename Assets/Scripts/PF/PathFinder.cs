using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//배열 번호 - size / 2 == pos
public class PathFinder : MonoBehaviour {
	private Map_Path map_path;
	private Node[,] nodePath;
	private int detSize;
	private int middle;

	public void Initiate(int _detSize){
		detSize = _detSize;//detSize는 홀수가 이롭다.
		middle = detSize / 2;
		nodePath = new Node[detSize, detSize];

		for(int loop = 0; loop < detSize; loop++){
			for(int loop2 = 0; loop2 < detSize; loop2++){
				nodePath[loop, loop2] = new Node();
			}
		}
	}

	private List<Node> openList;
	private int curx;
	private int cury;
	public List<Vector2> FindPath(Vector3 start, Vector3 end){
		map_path = Map_Path.Instance;
		start = new Vector3 (Mathf.RoundToInt (start.x), Mathf.RoundToInt (start.y)); 
		end = new Vector3 (Mathf.RoundToInt (end.x), Mathf.RoundToInt (end.y));

		PrepareMap_path (new Vector2(start.x, start.y));

		curx = middle;
		cury = middle;

		openList = new List<Node> ();

		int count = detSize * detSize;
		while (count > 0) {
			count--;
			if( curx < 1 || cury < 1 || curx > detSize - 2 || cury > detSize - 2){
				continue;
			}

			for(int loop = -1; loop < 2; loop++){
				for(int loop2 = -1; loop2 < 2; loop2++){
					if(loop == 0 && loop2 == 0)continue;
					NodePathSet(curx + loop, loop, cury + loop2, loop2, end);
				}
			}

			if(openList.Count != 0){
				Node smallest = openList[0];
				int smallNum = 0;
				for(int loop = 0; loop < openList.Count; loop++){
					if(smallest.F > openList[loop].F){
						smallest = openList[loop];
						smallNum = loop;
					}
				}
				openList.RemoveAt(smallNum);
				nodePath[curx, cury].open = false;

				Vector2 toMove = new Vector2();
				toMove = smallest.pos - nodePath[curx, cury].pos;
				curx += (int)toMove.x;
				cury += (int)toMove.y;
			}
			if((int)(nodePath[curx, cury].pos.x) == (int)(end.x) && (int)(nodePath[curx, cury].pos.y) == (int)(end.y)){
				break;
			}
		}

		List<Vector2> destList = new List<Vector2> ();
		
		count = 0;
		Node tempNode;
		tempNode = nodePath [curx, cury];
		if (tempNode.parent == null)
			return destList;
		while (count < 2000) {
			destList.Add(tempNode.pos);
			tempNode = tempNode.parent;
			if(tempNode.parent == null)break;
			count++;
		}
		if (count > 1999)
			Debug.Log ("something is wrong... RIP... count over 2000");

		return destList;
	}

	void NodePathSet(int x, int tox, int y, int toy, Vector2 end){
		if (nodePath [x - tox, y - toy].walkable[tox + 1, toy + 1] && nodePath [x, y].open) {
			if(nodePath[x, y].parent == null){//처음 방문하셧나요
				openList.Add(nodePath[x, y]);//열린 목록에 추가
				nodePath [x, y].parent = nodePath [curx, cury];
				nodePath [x, y].G = nodePath [curx, cury].G + 10;
				nodePath [x, y].H = 10 * (int)(Mathf.Abs(end.x - nodePath [x, y].pos.x) + Mathf.Abs(end.y - nodePath [x, y].pos.y));
				nodePath [x, y].F = nodePath [x, y].G + nodePath [x, y].H;
			}
			else if(nodePath [x, y].G > nodePath [curx, cury].G + 10){
				nodePath [x, y].parent = nodePath [curx, cury];
				nodePath [x, y].G = nodePath [curx, cury].G + 10;
			}
		}
	}
	
	void PrepareMap_path(Vector2 start){
		int from_x = Mathf.RoundToInt(start.x) - middle;
		int from_y = Mathf.RoundToInt(start.y) - middle;

		for (int loop = 0; loop < detSize; loop++) {
			for (int loop2 = 0; loop2 < detSize; loop2++) {
				if(from_x + loop > 0 && from_y + loop2 > 0){
					//Debug.Log (from_x + loop);
					nodePath[loop, loop2] = map_path.node[from_x + loop, from_y + loop2];
					nodePath[loop, loop2].open = true;
					nodePath[loop, loop2].F = 0;
					nodePath[loop, loop2].G = 0;
					nodePath[loop, loop2].H = 0;
					nodePath[loop, loop2].parent = null;
				}
			}
		}
	}

	public void PrepareMap_path_Simple(Vector2 start){
		for (int loop = 0; loop < 7; loop++) {
			for (int loop2 = 0; loop2 < 7; loop2++) {
				int orimapx = loop + Mathf.RoundToInt(start.x);
				int orimapy = loop2 + Mathf.RoundToInt(start.y);
				
				if(orimapx > (int)(detSize / 2) && orimapy > (int)(detSize / 2)){
					nodePath[loop, loop2] = map_path.node[orimapx - (int)(detSize / 2), orimapy - (int)(detSize / 2)];
				}
			}
		}
	}
}
