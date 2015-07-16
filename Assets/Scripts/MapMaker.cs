using UnityEngine;
using System.Collections;
using Tiling;
public class MapMaker : MonoBehaviour {

	//handles tiling
	public class Tile
	{
		//public GameObject hole_tile, filled_tile;
		public Vector3 tileSize= new Vector3(2f,0.3f,2f);
		Vector3 size; Vector3 pos;
		public Vector3 getSize() {
			return size;
		}
		public Vector3 getPos() {
			return pos;
		}
		public void setSize(Vector3 newSize) {
			size = newSize; 
			//hole_tile.transform.localScale = size; 
			//filled_tile.transform.localScale = size;
		}
		public void setPos(Vector3 newPos) {
			pos = newPos;
		}
		public void makeTile () 
		{
			Debug.Log ("performed empty! check inheritance");
		}
	}
	
	public class Hole : Tile
	{
		public float holeSize = 1f;
		public Hole(Vector3 position){
			setPos(position);
			setSize(holeSize*tileSize);
	//		Debug.Log ("Made Hole");
			makeTile ();
	//		Debug.Log ("Done");
		}
		public Hole(Vector3 position, float hsize){
			holeSize = hsize; setPos (position); setSize(new Vector3(hsize*tileSize.x,tileSize.y,hsize*tileSize.z)); makeTile ();
		}
		public void makeTile(){
	//		Debug.Log ("Making tile");
			Instantiate (hole_tile, this.getPos (), hole_tile.transform.rotation);
		}
	}
	
	public class Walkway : Tile
	{
		public float fillSize = 1f;
		public Walkway(Vector3 position)
		{
	//		Debug.Log ("Made walkway");
			setPos (position); setSize (fillSize*tileSize);
			makeTile ();
	//		Debug.Log ("Done");
		}
		public void makeTile(){
	//		Debug.Log ("Making Tile");
			Instantiate (filled_tile, this.getPos (), filled_tile.transform.rotation);
		}
	}
	
	public class BigHole : Hole
	{
		float holes;
		public BigHole(float size, Vector3 position) : base(position)
		{
			Debug.Log ("MAKING BIG HOLE");
			setPos(position);
			setSize (new Vector3(255f,255f,255f));
			holes=size;
			makeTile ();
		}
		public void makeTile(){
//			Hole myHole = new Hole (getPos ());
			Debug.Log ("BigHole at "+this.getPos ().x+","+this.getPos().z);
			if ((((int)Mathf.Sqrt (holes)) * 1f) == Mathf.Sqrt (holes)) new Hole(this.getPos (), Mathf.Sqrt(this.holes));
			else {
				new Hole(this.getPos (), ((int)Mathf.Sqrt(this.holes))*1f);
			}
		}
	}
	
	public class Chasm : Hole
	{
		bool growing; float holes;
		public Chasm(float s, Vector3 p, bool g):base(p)
		{
			setPos(p); setSize (new Vector3(255f,255f,255f)); holes = s; growing = g;
		}
		public void makeTile(){
			Hole myHole = new Hole (getPos ());
		}
	}
	
	public class Canyon : Hole
	{
		float holes;
		public Canyon(float size, Vector3 position):base(position)
		{
			setPos (position); setSize (new Vector3(255f,255f,255f)); holes = size;
		}
		public void makeTile(){
			Hole myHole = new Hole (getPos ());
		}
	}

	public GameObject tileHole, tileFilled, Player;
	public static GameObject hole_tile, filled_tile;
	//PROBABILITY CONTROLS
	public float chanceHole = 0.2f;
	public float chanceBigHole = 0.02f;
	public float chanceChasm = 0.01f;
	public float chanceCanyon = 0.000001f; //one in a million
	//

	//MASTER SIZE CONTROLS, IN TILES
	public Vector3 sizeTile = new Vector3 (2f, 0.3f, 2f);
	public float sizeHole = 1f;
	public float minSizeBigHole = 2f;
	public float maxSizeBigHole = 4f;
	public float minSizeChasm = 7f;
	public float maxSizeChasm = 15f;
	public float minSizeCanyon = 25f;
	public float maxSizeCanyon = 150f;
	//

	public float rand(float min, float max){
		return (float)((int)(Random.Range ((int)min,(int)max)));
	}

	public bool randBool(){
		if (rand (0, 1) == 0)
			return true;
		else
			return false;
	}

	public Tile chooseTile(Vector3 position) {
		float[] probabilities = {chanceCanyon,chanceChasm,chanceBigHole,chanceHole};
		int prob=0; float size; float chanceIndex;
		foreach (float chance in probabilities) {
			chanceIndex = 1/chance;
			if(rand (1,chanceIndex)==1) break;
			else prob++;
		}
		//Debug.Log ("Choosing!");
		switch (prob) {
		case 0: size = rand(minSizeCanyon, maxSizeCanyon); return new Canyon(size, position); break;
		case 1: size = rand (minSizeChasm, maxSizeChasm); return new Chasm(size,position, randBool()); break;
		case 2: size = rand (minSizeBigHole, maxSizeBigHole); Debug.Log ("BIG HOLE");return new BigHole(size,position); break;
		case 3: return new Hole(position); break;
		case 4: return new Walkway(position); break;
		}
		Debug.Log ("Didnt return from switch??");
		return new Walkway (position);
	}

	void makeMap(int x, int y,int l, int w, float minH, float maxH){
		for(float i = x; i<=l; i+=2)
			for(float j = y; j<=w; j+=2)
				chooseTile (new Vector3 (i, Random.Range(minH,maxH), j));
	}

	void Start () {
		hole_tile = tileHole; filled_tile = tileFilled;
		float[] probabilities = {chanceCanyon,chanceChasm,chanceBigHole,chanceHole};
		//		Tile.filled_tile = filled_tile;
		//		Tile.hole_tile = hole_tile;
		//Debug.Log (Tile.filled_tile.name);
	
		makeMap (0, 0, 200, 200, 0f, 0.5f);
	//	Instantiate (filled_tile, new Vector3(0,0,0),filled_tile.transform.rotation);
	}
	
	void Update () {

	}
}
