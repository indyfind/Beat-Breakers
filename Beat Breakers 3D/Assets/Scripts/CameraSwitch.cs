using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {
	public Camera mainCamera;
	public Camera player1Camera;
	public Camera player2Camera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowMainCamera() {
		player1Camera.enabled = false;
		player2Camera.enabled = false;
		mainCamera.enabled = true;
	}

	public void ShowPlayer1Camera() {
		player2Camera.enabled = false;
		//mainCamera.enabled = false;
		player1Camera.enabled = true;
	}

	public void ShowPlayer2Camera() {
		player1Camera.enabled = false;
		//mainCamera.enabled = false;
		player2Camera.enabled = true;
	}
}
