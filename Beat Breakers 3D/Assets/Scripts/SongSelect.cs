using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class SongSelect : MonoBehaviour {

	private InputDevice device; 
	public AudioSource select;
	public PlayerKeyboardActions player1KeyboardActions;
	public PlayerKeyboardActions player2KeyboardActions;
	public GameObject songholder;
	private InputDevice device1;
	private InputDevice device2;
	private GameObject inputMaster;
	public string p1char, p2char;
	public GameObject charPicks;
	public GameObject[] songs;
	public GameObject player1text, player2text;
	public GameObject[] p1Portraits;
	public GameObject[] p2Portraits;
	public GameObject[] highlight;
	private int index;

	// Use this for initialization
	void Start () {
		inputMaster = GameObject.FindGameObjectWithTag("InputMaster");
		SetControllers();
		index = 5;
		charPicks = GameObject.FindGameObjectWithTag("CharPicks");
		p1char = charPicks.GetComponent<CharPicks>().p1char;
		p2char = charPicks.GetComponent<CharPicks>().p2char;

		switch (p1char){
		case"Eva" :
			
			p1Portraits[0].SetActive(true);

			break;
		case "Naz":
			p1Portraits[1].SetActive(true);
			break;
		case "CosmicS":
			p1Portraits[2].SetActive(true);
			break;
		case "Jameleon":
			p1Portraits[3].SetActive(true);
			break;
		default:
			break;
		}
		player1text.GetComponent<Text>().text = p1char;
		switch (p2char){
		case"Eva" :
			p2Portraits[0].SetActive(true);
			break;
		case "Naz":
			p2Portraits[1].SetActive(true);
			break;
		case "CosmicS":
			p2Portraits[2].SetActive(true);
			break;
		case "Jameleon":
			p2Portraits[3].SetActive(true);
			break;
		default:
			break;
		}
		player2text.GetComponent<Text>().text = p2char;
		songs[index].GetComponent<AudioSource>().Play();
	}

	// Update is called once per frame
	void Update () {
		if (device1.LeftStickDown.WasPressed || device2.LeftStickDown.WasPressed || device1.DPadDown.WasPressed  || 
			device2.DPadDown.WasPressed || player1KeyboardActions.Down.WasPressed  || player2KeyboardActions.Down.WasPressed ){

			Debug.Log("pressed down");

			if (index > 0){
				songs[index].GetComponent<AudioSource>().Stop();
				highlight[index].SetActive(false);
				index --;
				Debug.Log(index);
				songholder.transform.Translate(0f, 12.78f,0f);
				Debug.Log(songholder.transform.position);
				highlight[index].SetActive(true);
				songs[index].GetComponent<AudioSource>().Play();

			}


		}
		else if (device1.LeftStickUp.WasPressed || device2.LeftStickUp.WasPressed || device1.DPadUp.WasPressed || 
			device2.DPadUp.WasPressed || player1KeyboardActions.Up.WasPressed || player2KeyboardActions.Up.WasPressed){

			Debug.Log("pressed up");

			if (index < 9){
				songs[index].GetComponent<AudioSource>().Stop();
				Debug.Log(index);
				highlight[index].SetActive(false);
				index ++;
				songholder.transform.Translate(0f, -12.78f,0f);
				Debug.Log(songholder.transform.position);
				highlight[index].SetActive(true);
				songs[index].GetComponent<AudioSource>().Play();
			}

		}
		if (device1.Action1.WasPressed || player1KeyboardActions.Select.WasPressed || device2.Action1.WasPressed || player2KeyboardActions.Select.WasPressed){
			songs[index].GetComponent<AudioSource>().Stop();
			select.Play();

			if(index == 0){
				charPicks.GetComponent<CharPicks>().song = "Spook House Theme";
			}
			else if (index == 1){
				charPicks.GetComponent<CharPicks>().song = "pyramid Planet";
			}
			else if (index == 2){
				charPicks.GetComponent<CharPicks>().song = "Neo Nebula";
			}
			else if (index == 3){
				charPicks.GetComponent<CharPicks>().song = "Milky Way Wishes";
			}
			else if (index == 4){
				charPicks.GetComponent<CharPicks>().song = "MegaMix";
			}
			else if (index == 5){
				charPicks.GetComponent<CharPicks>().song = "Groovin Galaxy";
			}
			else if (index == 6){
				charPicks.GetComponent<CharPicks>().song = "Dark Matter";
			}
			else if (index == 7){
				charPicks.GetComponent<CharPicks>().song = "Celestial Catwalk";
			}
			else if (index == 8){
				charPicks.GetComponent<CharPicks>().song = "BoomBox Theory";
			}
			else if (index == 9){
				charPicks.GetComponent<CharPicks>().song = "Baroque Breakers";
			}
						

			StartCoroutine(LoadBattle());
		}
	
	}


	private void SetControllers(){

		if (inputMaster.GetComponent<InputMaster>().player1keyboard)
		{
			device1 = new InputDevice();
			player1KeyboardActions = inputMaster.GetComponent<InputMaster>().getP1Actions();
		}
		else
		{
			player1KeyboardActions = new PlayerKeyboardActions();
			device1 = inputMaster.GetComponent<InputMaster>().player1Controller;
		}
		if (inputMaster.GetComponent<InputMaster>().player2keyboard)
		{
			device2 = new InputDevice();
			player2KeyboardActions = inputMaster.GetComponent<InputMaster>().getP2Actions();
		}
		else
		{
			device2 = inputMaster.GetComponent<InputMaster>().player2Controller;
			player2KeyboardActions = new PlayerKeyboardActions();
		}



	}

	IEnumerator LoadBattle(){
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(1);
	}
}
