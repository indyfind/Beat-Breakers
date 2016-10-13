using UnityEngine;
using System.Collections;

public class OnPlayerHUD : MonoBehaviour {
	public string player;
	public Vector3 screenPosition;
	public Font font;
	GUIStyle style = new GUIStyle();
	Texture2D texture = new Texture2D(128, 128);
	// Use this for initialization
	void Start () {
		style.font = font; 
		style.alignment = TextAnchor.MiddleCenter;
		for (int y = 0; y < texture.height; ++y)
		{
			for (int x = 0; x < texture.width; ++x) {
				texture.SetPixel(x, y, Color.clear);
			}
		}
		texture.Apply();
		style.normal.background = texture;
		if (player == "P1") {
			style.normal.textColor = Color.yellow;
		} else {
			style.normal.textColor = Color.magenta;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {

		screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		screenPosition.y = Screen.height - screenPosition.y;
		GUI.Box(new Rect(screenPosition.x-10, screenPosition.y-20, 20, 20), player, style);
	}
}
