using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	private GameObject battleMaster;
	private Vector3 targetPosition = new Vector3 (0f, 6.63f, -3.61f);
	private Vector3 targetRotation = new Vector3 (59.985f, 0f, 0f);
	public float smoothTime;
	private Vector3 velocity = Vector3.zero;
	private Vector3 velocity2 = Vector3.zero;

	// Use this for initialization
	void Start () {
		battleMaster = GameObject.FindGameObjectWithTag ("BattleMaster");
		if (battleMaster.GetComponent<EndBattle> ().round != 1) {
			//Debug.Log(true);
			transform.position = targetPosition;
			transform.localEulerAngles = targetRotation;
		}
	}

	void Update () {
		if (battleMaster.GetComponent<EndBattle> ().round == 1) {
			
			//move to target
			transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, smoothTime);

			//rotate to target
			Vector3 newDir = Vector3.SmoothDamp (transform.localEulerAngles, targetRotation, ref velocity2, smoothTime);
			transform.rotation = Quaternion.Euler (newDir);
		}
	}
}
