

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obj{
	public int objcode;
	public Sprite[] objimage;
	public GameObject ingameobject;
	public Obj(){
		objimage = new Sprite[3];
		ingameobject = new GameObject ("Object");
		ingameobject.transform.parent = GameObject.Find ("Objects").transform;

		BoxCollider2D bCol = ingameobject.AddComponent<BoxCollider2D> ();
		bCol.size = new Vector2 (0.02f, 0.02f);
		objcode = 0;
		objimage [0] = Resources.Load<Sprite> ("yellow");
		objimage [1] = Resources.Load<Sprite> ("red");
		objimage [2] = Resources.Load<Sprite> ("purple");
	}

}

/*public class ObjMap{
	public Sprite[] objimage;
	public int[,] objtype;
	public ObjMap(){
		objimage = new Sprite[3];
		objtype = new int[Config.MAPSIZE, Config.MAPSIZE];
		objimage [0] = Resources.Load<Sprite> ("yellow");
		objimage [1] = Resources.Load<Sprite> ("red");
		objimage [2] = Resources.Load<Sprite> ("purple");
	}
}*/

public class Tile{

	public int color;
	public int height;
	public bool validity;
	public GameObject ingameobject;
	public Tile(){
		color = 9;
		height = 0;
		validity = true;
		ingameobject = new GameObject ("Tile");
		ingameobject.transform.parent = GameObject.Find ("Tiles").transform;
		ingameobject.transform.position = new Vector3 (0, 0, 0);
//		Debug.Log ("abc");
	}
}

public class Map{
	public Sprite[] tileimage;
	public Tile[,] TileTable;
	public Map(){
		tileimage = new Sprite[20];
		tileimage[0] = Resources.Load<Sprite> ("water");
		tileimage[1] = Resources.Load<Sprite> ("grass");
		tileimage[2] = Resources.Load<Sprite> ("forest");
		tileimage[3] = Resources.Load<Sprite> ("swamp");
		tileimage[4] = Resources.Load<Sprite> ("desert");
		tileimage[5] = Resources.Load<Sprite> ("waste");
		tileimage[6] = Resources.Load<Sprite> ("snow");
		tileimage[7] = Resources.Load<Sprite> ("volcano");
		tileimage[8] = Resources.Load<Sprite> ("dirt");
		tileimage[9] = Resources.Load<Sprite> ("river");
//		tileimage[10] = Resources.Load<Sprite> ("");
		tileimage[11] = Resources.Load<Sprite> ("deep");
		tileimage[12] = Resources.Load<Sprite> ("shallow");
		tileimage[13] = Resources.Load<Sprite> ("ice");
		tileimage[14] = Resources.Load<Sprite> ("lava");
		tileimage[15] = Resources.Load<Sprite> ("volcanicstone");
		TileTable = new Tile[Config.MAPSIZE, Config.MAPSIZE];
		for (int i=0; i<Config.MAPSIZE; i++) {
			for (int j=0; j<Config.MAPSIZE; j++) {
				TileTable[i, j] = new Tile();
				TileTable[i,j].ingameobject.transform.position = new Vector3(i*4, j*4, 0);
			}
		}
	}

	public void AddBiome(int biome, int range){
		List<int> XQ = new List<int>();
		List<int> YQ = new List<int>();
		
		int StartX = 0;
		int StartY = 0;
		do{
			StartX = Random.Range((Config.MAPSIZE/20)*Config.SPARESIZE,Config.MAPSIZE - (Config.MAPSIZE/20) * Config.SPARESIZE);
			StartY = Random.Range((Config.MAPSIZE/20)*Config.SPARESIZE,Config.MAPSIZE - (Config.MAPSIZE/20) * Config.SPARESIZE);
		} while (TileTable[StartX,StartY].color != 9 || TileTable[StartX + 1,StartY].color != 9 || TileTable[StartX,StartY + 1].color != 9 || TileTable[StartX - 1,StartY].color != 9 || TileTable[StartX,StartY - 1].color != 9);
		int NowX = 0;
		int NowY = 0;
		int RandomNum = 0;
		XQ.Add(StartX);
		YQ.Add(StartY);
		
		for (int i = 0; i < range; i++){
			
			RandomNum = Random.Range(0,XQ.Count);
			
			NowX = XQ[RandomNum];
			NowY = YQ[RandomNum];
			
			TileTable[NowX,NowY].color=biome;
			TileTable[NowX,NowY].height++;
			

			if (TileTable[NowX - 1,NowY].validity && NowX - 1 >= Config.SPARESIZE){
				XQ.Add(NowX - 1);	//Left
				YQ.Add(NowY);
				TileTable[NowX - 1,NowY].validity=false;
			}
			if (TileTable[NowX,NowY - 1].validity && NowY - 1 >= Config.SPARESIZE){
				XQ.Add(NowX);		//Up
				YQ.Add(NowY - 1);
				TileTable[NowX,NowY - 1].validity=false;
			}
			if (TileTable[NowX + 1,NowY].validity && NowX + 1 <= Config.MAPSIZE - Config.SPARESIZE){
				XQ.Add(NowX + 1);	//Right
				YQ.Add(NowY);
				TileTable[NowX + 1,NowY].validity=false;
			}
			if (TileTable[NowX,NowY + 1].validity && NowY + 1 <= Config.MAPSIZE - Config.SPARESIZE){
				XQ.Add(NowX);		//Down
				YQ.Add(NowY + 1);
				TileTable[NowX,NowY + 1].validity=false;
			}
			
		}
		
		if (biome == 7){
			List<int> XQ2 = new List<int>();
			List<int> YQ2 = new List<int>();
			
			XQ2.Add(StartX);
			YQ2.Add(StartY);
			
			for (int i = 0; i < 10; i++){
				RandomNum = Random.Range(0,XQ2.Count);
				
				NowX = XQ2[RandomNum];
				NowY = YQ2[RandomNum];
				
				TileTable[NowX,NowY].color=14;
				
				XQ2.Add(NowX - 1);	//Left
				YQ2.Add(NowY);
				
				XQ2.Add(NowX);		//Up
				YQ2.Add(NowY - 1);
				
				XQ2.Add(NowX + 1);	//Right
				YQ2.Add(NowY);
				
				XQ2.Add(NowX);		//Down
				YQ2.Add(NowY + 1);
				
			}
		}
	}

	public void ImageMatch(){
		SpriteRenderer spr;
		for (int i=0; i<Config.MAPSIZE; i++) {
			for (int j=0; j<Config.MAPSIZE; j++) {
				TileTable [i, j].ingameobject.transform.localScale = new Vector3 (1, 1, 0);
				spr = TileTable [i, j].ingameobject.AddComponent<SpriteRenderer> ();
				spr.sortingLayerName = "Tiles";
				switch(TileTable[i, j].color){
				case 0:
					spr.sprite = tileimage[0];
					break;
				case 1:
					spr.sprite = tileimage[1];
					spr.sortingOrder=5;
					break;
				case 2:
					spr.sprite = tileimage[2];
					spr.sortingOrder=6;
					break;
				case 3:
					spr.sprite = tileimage[3];
					spr.sortingOrder=2;
					break;
				case 4:
					spr.sprite = tileimage[4];
					spr.sortingOrder=3;
					break;
				case 5:
					spr.sprite = tileimage[5];
					spr.sortingOrder=7;
					break;
				case 6:
					spr.sprite = tileimage[6];
					spr.sortingOrder=25;
					break;
				case 7:
					spr.sprite = tileimage[7];
					spr.sortingOrder=9;
					break;
				case 8:
					spr.sprite = tileimage[8];
					break;
				case 9:
					spr.sprite = tileimage[9];
					spr.sortingOrder=20;
					break;
				case 10:
					spr.sprite = tileimage[10];
					break;
				case 11:
					spr.sprite = tileimage[11];
					break;
				case 12:
					spr.sprite = tileimage[12];
					break;
				case 13:
					spr.sprite = tileimage[13];
					spr.sortingOrder=21;
					break;
				case 14:
					spr.sprite = tileimage[14];
					break;
				case 15:
					spr.sprite = tileimage[15];
					spr.sortingOrder=30;
					break;
				}
			}
		}
	}

	public void OceanPainting(){
		Queue<int> XQ = new Queue<int>();
		Queue<int> YQ = new Queue<int>();
		
		int StartX = Config.SPARESIZE;
		int StartY = Config.SPARESIZE;
		int NowX = 0;
		int NowY = 0;
		
		XQ.Enqueue(StartX);
		YQ.Enqueue(StartY);
		TileTable[StartX,StartY].validity=false;	//다시 볼필요없는 타일임을 표시함.
		
//		int counter = 0;
		while (XQ.Count!=0){
			NowX = XQ.Dequeue();
			NowY = YQ.Dequeue();
			
			TileTable[NowX,NowY].color=11;
			
			
			if (NowX + 1 <= Config.MAPSIZE - Config.SPARESIZE && TileTable[NowX + 1,NowY].validity){
				XQ.Enqueue(NowX + 1);	//Right
				YQ.Enqueue(NowY);
				TileTable[NowX + 1,NowY].validity=false;
			}
			if (NowX - 1 >= Config.SPARESIZE && TileTable[NowX - 1,NowY].validity){
				XQ.Enqueue(NowX - 1);	//Left
				YQ.Enqueue(NowY);
				TileTable[NowX - 1,NowY].validity=false;
			}
			if (NowY + 1 <= Config.MAPSIZE - Config.SPARESIZE && TileTable[NowX,NowY + 1].validity){
				XQ.Enqueue(NowX);		//Down
				YQ.Enqueue(NowY + 1);
				TileTable[NowX,NowY + 1].validity=false;
			}
			if (NowY - 1 >= Config.SPARESIZE && TileTable[NowX,NowY - 1].validity){
				XQ.Enqueue(NowX);		//Up
				YQ.Enqueue(NowY - 1);
				TileTable[NowX,NowY - 1].validity=false;
			}
			
		}
		
	}

	public void BeachPainting(){
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 9){
					if (TileTable[i + 1,j].color == 11 || TileTable[i - 1,j].color == 11 || TileTable[i,j + 1].color == 11 || TileTable[i,j - 1].color == 11){
						TileTable[i,j].color=12;
					}
				}
			}
		}
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 9){
					if (TileTable[i + 1,j].color == 12 || TileTable[i - 1,j].color == 12 || TileTable[i,j + 1].color == 12 || TileTable[i,j - 1].color == 12){
						TileTable[i,j].color=10;
					}
				}
			}
		}
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 10){
					TileTable[i,j].color=12;
				}
			}
		}
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 9){
					if (TileTable[i + 1,j].color == 12 || TileTable[i - 1,j].color == 12 || TileTable[i,j + 1].color == 12 || TileTable[i,j - 1].color == 12){
						TileTable[i,j].color=10;
					}
				}
			}
		}
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 10){
					TileTable[i,j].color=12;
				}
			}
		}
		for (int k = 0; k < 10; k++){
			for (int i = Config.SPARESIZE; i < Config.MAPSIZE - Config.SPARESIZE; i++){
				for (int j = Config.SPARESIZE; j < Config.MAPSIZE - Config.SPARESIZE; j++){
					if (TileTable[i,j].color == 12 && Random.Range(0,4) < 2){
						if (TileTable[i + 1,j].color == 11){
							TileTable[i + 1,j].color=10;
						}
						if (TileTable[i - 1,j].color == 11){
							TileTable[i - 1,j].color=10;
						}
						if (TileTable[i,j + 1].color== 11){
							TileTable[i,j + 1].color=10;
						}
						if (TileTable[i,j - 1].color == 11){
							TileTable[i,j - 1].color=10;
						}
					}
				}
			}
			for (int i = Config.SPARESIZE; i < Config.MAPSIZE - Config.SPARESIZE; i++){
				for (int j = Config.SPARESIZE; j < Config.MAPSIZE - Config.SPARESIZE; j++){
					if (TileTable[i,j].color == 10){
						TileTable[i,j].color=12;
					}
				}
			}
		}
		for (int k = 0; k < 10; k++){
			for (int i = Config.SPARESIZE; i < Config.MAPSIZE - Config.SPARESIZE; i++){
				for (int j = Config.SPARESIZE; j < Config.MAPSIZE - Config.SPARESIZE; j++){
					if (TileTable[i,j].color == 12 && Random.Range(0,4) < 1){	
						if (TileTable[i + 1,j].color == 11){
							TileTable[i + 1,j].color=10;
						}
						if (TileTable[i - 1,j].color == 11){
							TileTable[i - 1,j].color=10;
						}
						if (TileTable[i,j + 1].color == 11){
							TileTable[i,j + 1].color=10;
						}
						if (TileTable[i,j - 1].color == 11){
							TileTable[i,j - 1].color=10;
						}
					}
				}
			}
			for (int i = Config.SPARESIZE; i < Config.MAPSIZE - Config.SPARESIZE; i++){
				for (int j = Config.SPARESIZE; j < Config.MAPSIZE - Config.SPARESIZE; j++){
					if (TileTable[i,j].color == 10){
						TileTable[i,j].color=12;
					}
				}
			}
		}
	}

	public void IcePaintig(){
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 9){
					if (TileTable[i + 1,j].color == 6 || TileTable[i - 1,j].color == 6 || TileTable[i,j + 1].color == 6 || TileTable[i,j - 1].color == 6){
						TileTable[i,j].color=13;
					}
				}
			}
		}
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 12){
					if (TileTable[i + 1,j].color == 6 || TileTable[i - 1,j].color == 6 || TileTable[i,j + 1].color == 6 || TileTable[i,j - 1].color == 6){
						TileTable[i,j].color=13;
					}
				}
			}
		}
		
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 9){
					if (TileTable[i + 1,j].color == 13 || TileTable[i - 1,j].color == 13 || TileTable[i,j + 1].color == 13 || TileTable[i,j - 1].color == 13){
						TileTable[i,j].color=10;
					}
				}
			}
		}
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 10){
					TileTable[i,j].color=13;
				}
			}
		}
		
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 12 && Random.Range(0,4) < 2){	
					if (TileTable[i + 1,j].color == 13 || TileTable[i - 1,j].color == 13 || TileTable[i,j + 1].color == 13 || TileTable[i,j - 1].color == 13){
						TileTable[i,j].color=10;
					}
				}
			}
		}
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 10){
					TileTable[i,j].color=13;
				}
			}
		}
		
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 12 && Random.Range(0,4) < 2){
					if (TileTable[i + 1,j].color == 13 || TileTable[i - 1,j].color == 13 || TileTable[i,j + 1].color == 13 || TileTable[i,j - 1].color == 13){
						TileTable[i,j].color=10;
					}
				}
			}
		}
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 10){
					TileTable[i,j].color=13;
				}
			}
		}
		
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 12 && Random.Range(0,4) < 2){
					if (TileTable[i + 1,j].color == 13 || TileTable[i - 1,j].color == 13 || TileTable[i,j + 1].color == 13 || TileTable[i,j - 1].color == 13){
						TileTable[i,j].color=10;
					}
				}
			}
		}
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 10){
					TileTable[i,j].color=13;
				}
			}
		}
	}

	public void VolcanicPainting(){
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 9){
					if (TileTable[i + 1,j].color == 7 || TileTable[i - 1,j].color == 7 || TileTable[i,j + 1].color == 7 || TileTable[i,j - 1].color == 7){
						TileTable[i,j].color=15;
					}
				}
			}
		}
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 12){
					if (TileTable[i + 1,j].color == 7 || TileTable[i - 1,j].color == 7 || TileTable[i,j + 1].color == 7 || TileTable[i,j - 1].color == 7){
						TileTable[i,j].color=15;
					}
				}
			}
		}
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 7){
					if (TileTable[i + 1,j].color == 15 || TileTable[i - 1,j].color == 15 || TileTable[i,j + 1].color == 15 || TileTable[i,j - 1].color == 15){
						TileTable[i,j].color=10;
					}
				}
			}
		}
		for (int i = Config.SPARESIZE; i<Config.MAPSIZE - Config.SPARESIZE; i++){
			for (int j = Config.SPARESIZE; j<Config.MAPSIZE - Config.SPARESIZE; j++){
				if (TileTable[i,j].color == 10){
					TileTable[i,j].color=15;
				}
			}
		}
	}
}

public class Map_Generate : MonoBehaviour {

	void Awake () {

		Map g_map = new Map();


		g_map.AddBiome(6, (Config.MAPSIZE*Config.MAPSIZE)/200);
//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(6, (Config.MAPSIZE*Config.MAPSIZE)/200);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(7, (Config.MAPSIZE*Config.MAPSIZE)/200);
		//		g_map.DeleteBiomeGlitch();

		g_map.AddBiome(7, (Config.MAPSIZE*Config.MAPSIZE)/200);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(1, (Config.MAPSIZE*Config.MAPSIZE)/20);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(1, (Config.MAPSIZE*Config.MAPSIZE)/7);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(1, (Config.MAPSIZE*Config.MAPSIZE)/9);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(1, (Config.MAPSIZE*Config.MAPSIZE)/9);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(2, (Config.MAPSIZE*Config.MAPSIZE)/40);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(2, (Config.MAPSIZE*Config.MAPSIZE)/20);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(3, (Config.MAPSIZE*Config.MAPSIZE)/40);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(4, (Config.MAPSIZE*Config.MAPSIZE)/40);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(4, (Config.MAPSIZE*Config.MAPSIZE)/40);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(5, (Config.MAPSIZE*Config.MAPSIZE)/40);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(5, (Config.MAPSIZE*Config.MAPSIZE)/20);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(1, (Config.MAPSIZE*Config.MAPSIZE)/20);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(4, (Config.MAPSIZE*Config.MAPSIZE)/20);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(4, (Config.MAPSIZE*Config.MAPSIZE)/200);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(4, (Config.MAPSIZE*Config.MAPSIZE)/200);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(4, (Config.MAPSIZE*Config.MAPSIZE)/200);
		//		g_map.DeleteBiomeGlitch();
		
		g_map.AddBiome(5, (Config.MAPSIZE*Config.MAPSIZE)/40);
		//		g_map.DeleteBiomeGlitch();

		g_map.OceanPainting ();
		g_map.BeachPainting ();
		g_map.IcePaintig ();
		g_map.VolcanicPainting ();

		g_map.ImageMatch();

		SpriteRenderer spr;

		for(int i=Config.SPARESIZE;i<Config.MAPSIZE-Config.SPARESIZE;i++){
			for(int j=Config.SPARESIZE;j<Config.MAPSIZE-Config.SPARESIZE;j++){
				if(g_map.TileTable[i,j].color==9){
					GameObject dirt = new GameObject();
					dirt.transform.position=new Vector3 (i*4,j*4,0);
					dirt.transform.parent = GameObject.Find("Tiles").transform;
					spr=dirt.AddComponent<SpriteRenderer> ();
					spr.sprite=g_map.tileimage[8];
					spr.sortingLayerName = "Tiles";
					spr.sortingOrder=10;
				}
			}
		}



		List<Obj> objlist = new List<Obj> ();


		Obj temp = new Obj();
		for (int i=0; i<Config.MAPSIZE; i++) {
			for (int j=0; j<Config.MAPSIZE; j++) {
				switch(g_map.TileTable[i,j].color){
				case 1:
					if(Random.Range(0,4)<1){
						temp = new Obj();
						temp.objcode=1;
						temp.ingameobject.transform.position=new Vector3(4*i,4*j,0);
						temp.ingameobject.transform.localScale = new Vector3 (50, 50, 0);
						spr = temp.ingameobject.AddComponent<SpriteRenderer> ();
						spr.sortingLayerName = "Objects";
						spr.sprite=temp.objimage[1];
						//spr.sortingOrder=1;
						objlist.Add(temp);
					}
					break;
				default:
					break;
				}
			}
		}
	}
}
