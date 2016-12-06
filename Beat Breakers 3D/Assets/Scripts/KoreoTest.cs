using UnityEngine;
using SonicBloom.Koreo;

public class KoreoTest : MonoBehaviour {
	public GameObject cube;
	private int col = 0;
	// Use this for initialization
	void Start () {
		Koreographer.Instance.RegisterForEvents("beat", OnMusicalTrigger);
	}

	void OnMusicalTrigger (KoreographyEvent evt) {
		if (Input.GetButton("Fire1") && Time.timeScale != 0f)
		{

		}
		if (col == 0){
			cube.GetComponent<MeshRenderer>().material.color = Color.yellow;
			col = 1;
		} else {
			cube.GetComponent<MeshRenderer>().material.color = Color.red;
			col = 0;
		}
		//Debug.Log ("HELLO");
	}
}
