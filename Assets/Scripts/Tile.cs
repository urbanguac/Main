using System;
using UnityEngine.Internal;
using UnityEngine;
using System.Collections;
namespace Tiling
{
	public class Tile : UnityEngine.Object
	{
		public static GameObject hole_tile, filled_tile;
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
			Debug.Log ("Made Hole");
			makeTile ();
			Debug.Log ("Done");
		}
		public void makeTile(){
			Debug.Log ("Making tile");
			Debug.Log (Instantiate (hole_tile, this.getPos (), hole_tile.transform.rotation).name);
		}
	}
	
	public class Walkway : Tile
	{
		public float fillSize = 1f;
		public Walkway(Vector3 position)
		{
			Debug.Log ("Made walkway");
			setPos (position); setSize (fillSize*tileSize);
			makeTile ();
			Debug.Log ("Done");
		}
		public void makeTile(){
			Debug.Log ("Making Tile");
			Debug.Log (Instantiate (filled_tile, this.getPos (), filled_tile.transform.rotation).name);
		}
	}
	
	public class BigHole : Hole
	{
		float holes;
		public BigHole(float size, Vector3 position) : base(position)
		{
			setPos(position);
			setSize (new Vector3(255f,255f,255f));
			holes=size;
		}
		public void makeTile(){
			Hole myHole = new Hole (getPos ());
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

}

