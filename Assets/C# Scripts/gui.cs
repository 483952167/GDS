using UnityEngine;
using System.Collections;

public class gui : MonoBehaviour {
	public float barDisplay ;//current progress
	public float barDisplay_1;
	public Vector2 pos  ;
	public Vector2 size ;
	public Texture2D emptyTex;
	public Texture2D fullTex;
	public Texture2D fullTex_mana;
	private int[] left_array;


	Color redcolor = Color.red;
	Color greencolor = Color.green;
	Color graycolor = Color.grey;
	Color bluecolor = Color.blue;
	GUIStyle style = new GUIStyle();
	GUIStyle style_mana = new GUIStyle();




	
	
	
	void OnGUI() {
		fullTex_mana.Apply ();
		fullTex.Apply();
		float width_x = (Screen.width / 6);
		style.normal.background = fullTex;
		style_mana.normal.background = fullTex_mana;
		
		//draw the background:
		GUI.BeginGroup (new Rect (pos.x, 10, width_x+60, size.y));
		GUI.Box (new Rect (60, 0, width_x, size.y), emptyTex);
		GUI.TextArea (new Rect (0,0,60, size.y), "Player1");
		//draw the filled-in part:
		GUI.BeginGroup (new Rect (60, 0, width_x*barDisplay, size.y));
		GUI.Box (new Rect (0, 0, width_x, size.y), new GUIContent(""), style);
		GUI.EndGroup ();
		GUI.EndGroup ();
		
		
		GUI.BeginGroup (new Rect (pos.x, 40, width_x+60, size.y));
		GUI.Box (new Rect (60, 0, width_x, size.y), emptyTex);
		GUI.TextArea (new Rect (0,0,60, size.y), "Player2");
		//draw the filled-in part:
		GUI.BeginGroup (new Rect (60, 0, width_x*barDisplay, size.y));
		GUI.Box (new Rect (0, 0, width_x, size.y), new GUIContent(""), style);
		GUI.EndGroup ();
		GUI.EndGroup ();


		GUI.BeginGroup (new Rect (pos.x, 70, width_x+60, size.y));
		GUI.Box (new Rect (60, 0, width_x, size.y), emptyTex);
		GUI.TextArea (new Rect (0,0,60, size.y), "Player3");
		//draw the filled-in part:
		GUI.BeginGroup (new Rect (60, 0, width_x*barDisplay, size.y));
		GUI.Box (new Rect (0, 0, width_x, size.y), new GUIContent(""), style);
		GUI.EndGroup ();
		GUI.EndGroup ();

		GUI.BeginGroup (new Rect (pos.x, 100, width_x+60, size.y));
		GUI.Box (new Rect (60, 0, width_x, size.y), emptyTex);
		GUI.TextArea (new Rect (0,0,60, size.y), "Enemy");
		GUI.BeginGroup (new Rect (60, 0, width_x*barDisplay, size.y));
		GUI.Box (new Rect (0, 0, width_x, size.y), new GUIContent(""), style);
		GUI.EndGroup ();
		GUI.EndGroup ();

		GUI.BeginGroup (new Rect (pos.x, 130, width_x+60, size.y));
		GUI.TextArea (new Rect (0,0,60, size.y), "Mano");
		GUI.EndGroup ();



		
		//draw the background:
		GUI.BeginGroup (new Rect (pos.x, 160, width_x+60, size.y));
		GUI.Box (new Rect (60, 0, width_x, size.y), emptyTex);
		GUI.TextArea (new Rect (0,0,60, size.y), "Player1");
		//draw the filled-in part:
		GUI.BeginGroup (new Rect (60, 0, width_x*barDisplay, size.y));
		GUI.Box (new Rect (0, 0, width_x, size.y), new GUIContent(""), style_mana);
		GUI.EndGroup ();
		GUI.EndGroup ();


		GUI.BeginGroup (new Rect (pos.x, 190, width_x+60, size.y));
		GUI.Box (new Rect (60, 0, width_x, size.y), emptyTex);
		GUI.TextArea (new Rect (0,0,60, size.y), "Player2");
		GUI.BeginGroup (new Rect (60, 0, width_x*barDisplay, size.y));
		GUI.Box (new Rect (0, 0, width_x, size.y), new GUIContent(""), style_mana);
		GUI.EndGroup ();
		GUI.EndGroup ();

		GUI.BeginGroup (new Rect (pos.x, 220, width_x+60, size.y));
		GUI.Box (new Rect (60, 0, width_x, size.y), emptyTex);
		GUI.TextArea (new Rect (0,0,60, size.y), "Player3");
		GUI.BeginGroup (new Rect (60, 0, width_x*barDisplay, size.y));
		GUI.Box (new Rect (0, 0, width_x, size.y), new GUIContent(""), style_mana);
		GUI.EndGroup ();
		GUI.EndGroup ();

		GUI.BeginGroup (new Rect (pos.x, 250, width_x+60, size.y));
		GUI.Box (new Rect (60, 0, width_x, size.y), emptyTex);
		GUI.TextArea (new Rect (0,0,60, size.y), "Enemy");
		GUI.BeginGroup (new Rect (60, 0, width_x*barDisplay, size.y));
		GUI.Box (new Rect (0, 0, width_x, size.y), new GUIContent(""), style_mana);
		GUI.EndGroup ();
		GUI.EndGroup ();
		
	}
	
	// Use this for initialization
	void Start () {
		pos = new Vector2(20,40);
		size = new Vector2(80,20);
		fullTex = new Texture2D(1, 1);
		fullTex.SetPixel(1, 1, greencolor);
		fullTex_mana = new Texture2D(1, 1);
		fullTex_mana.SetPixel(1, 1, graycolor);
		barDisplay = 1;
		int[] left_array = {10, 40, 70,100,130,160,190,220};
		
		
		
		
		
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		//barDisplay = barDisplay - Time.time*0.01f;
		if (barDisplay > 0.4)
		{
			barDisplay = barDisplay - Time.time*0.01f;
			fullTex_mana.SetPixel(1, 1, graycolor);
			fullTex.SetPixel(1, 1, greencolor);
		}
		
		if (barDisplay <= 0.4)

		{
			fullTex_mana.SetPixel(1, 1, bluecolor);
			fullTex.SetPixel(1, 1, redcolor);
		}
	}
}
