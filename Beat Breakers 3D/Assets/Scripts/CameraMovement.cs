using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	private GameObject battleMaster;

	// Use this for initialization
	void Start () {
		battleMaster = GameObject.FindGameObjectWithTag ("BattleMaster");
		if (battleMaster.GetComponent<EndBattle> ().round != 1) {
			transform.position = target;
			transform.localEulerAngles = new Vector3 (59.985f, 0f, 0f);
		}
	}
	//private float rotationAmount = 59.985f;
	private Vector3 rotateTo = new Vector3 (0f, .5f, 0f);
	private Vector3 target = new Vector3 (0f, 6.63f, -3.61f);
	private float speed = 6.0f;
	void Update () {
		if (battleMaster.GetComponent<EndBattle> ().round == 1) {
			float step = speed * Time.deltaTime;

			transform.position = Vector3.MoveTowards (transform.position, target, step);

			Vector3 targetDir = rotateTo - transform.position;
			Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0f);
			transform.rotation = Quaternion.LookRotation (newDir);
		}
	}
}
