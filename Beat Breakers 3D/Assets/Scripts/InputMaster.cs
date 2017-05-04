using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class InputMaster : MonoBehaviour
{

	public InputDevice player1Controller;
	public InputDevice player2Controller;
	public PlayerKeyboardActions player1KeyboardActions;
	public PlayerKeyboardActions player2KeyboardActions;
	private InputDevice device;
	public AudioSource clicksound;
	public float playernum;
	public GameObject menu1, menu2, menu3;
	public GameObject controllerText;
	public GameObject TitleScreen;
	public bool player1keyboard, player2keyboard;
	// Use this for initialization
	void Start()
	{
		player1KeyboardActions = new PlayerKeyboardActions();
		player2KeyboardActions = new PlayerKeyboardActions();
		player1keyboard = false;
		player2keyboard = false;


		DontDestroyOnLoad(this.gameObject);
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);

		}
		else
		{
			//menu1.SetActive(false);
			//menu2.SetActive(false);
			//menu3.SetActive(false);
			TitleScreen.GetComponent<Image>().color = Color.gray;
			controllerText.GetComponent<Text>().text = "Player 1 \r\n Press Any Button";
			StartCoroutine(assigncontrollers());
		}

	}

	// Update is called once per frame
	void Update()
	{

	}
	public void SetPlayerController(InputDevice device, float player)
	{
		if (player == 1)
		{
			player1Controller = device;
		}
		else if (player == 2)
		{
			player2Controller = device;
		}

	}

	public bool notassignedtoplayer(InputDevice newdevice, float player)
	{
		if (player == 1 && newdevice != player2Controller)
		{
			return true;
		}
		if (player == 2 && newdevice != player1Controller)
		{
			return true;
		}
		return false;

	}

	IEnumerator assigncontrollers()
	{
		bool player1HasNoController = true;
		bool player2HasNoController = true;
		InputDevice tempdevice;
		tempdevice = InputManager.ActiveDevice;

		playernum = 1;
		while (player1HasNoController)
		{
			tempdevice = InputManager.ActiveDevice;
			if (this.GetComponent<InputMaster>().notassignedtoplayer(tempdevice, playernum))
			{
				if (Input.anyKey)
				{
					player1KeyboardActions.Down.AddDefaultBinding(Key.S);
					player1KeyboardActions.Left.AddDefaultBinding(Key.A);
					player1KeyboardActions.Right.AddDefaultBinding(Key.D);
					player1KeyboardActions.Up.AddDefaultBinding(Key.W);
					player1KeyboardActions.LongAtk.AddDefaultBinding(Key.Q);
					player1KeyboardActions.ShortAtk.AddDefaultBinding(Key.E);
					player1KeyboardActions.BasicAttack.AddDefaultBinding(Key.C);
					player1KeyboardActions.Select.AddDefaultBinding(Key.Space);

					clicksound.Play();
					yield return new WaitForSeconds(.3f);
					controllerText.GetComponent<Text>().text = "";
					controllerText.GetComponent<Text>().color = Color.white;
					yield return new WaitForSeconds(.05f);
					controllerText.GetComponent<Text>().text = "Player 2 \r\n Press Any Button";
					playernum = 2;
					player1HasNoController = false;
					player1keyboard = true;
				}
				if (tempdevice.AnyButton)
				{
					this.GetComponent<InputMaster>().SetPlayerController(tempdevice, playernum);
					controllerText.GetComponent<Text>().color = Color.green;
					clicksound.Play();
					yield return new WaitForSeconds(.3f);
					controllerText.GetComponent<Text>().text = "";
					controllerText.GetComponent<Text>().color = Color.white;
					yield return new WaitForSeconds(.05f);
					controllerText.GetComponent<Text>().text = "Player 2 \r\n Press Any Button or key";
					playernum = 2;
					player1HasNoController = false;
				}
			}
			yield return new WaitForSeconds(.05f);
		}

		while (player2HasNoController)
		{
			tempdevice = InputManager.ActiveDevice;
			if (this.GetComponent<InputMaster>().notassignedtoplayer(tempdevice, playernum))
			{
				if (Input.anyKey)
				{
					player2KeyboardActions.Down.AddDefaultBinding(Key.DownArrow);
					player2KeyboardActions.Left.AddDefaultBinding(Key.LeftArrow);
					player2KeyboardActions.Right.AddDefaultBinding(Key.RightArrow);
					player2KeyboardActions.Up.AddDefaultBinding(Key.UpArrow);
					player2KeyboardActions.LongAtk.AddDefaultBinding(Key.M);
					player2KeyboardActions.ShortAtk.AddDefaultBinding(Key.Comma);
					player2KeyboardActions.BasicAttack.AddDefaultBinding(Key.Period);
					player2KeyboardActions.Select.AddDefaultBinding(Key.Space);
					clicksound.Play();
					player2HasNoController = false;
					yield return new WaitForSeconds(.3f);
					//menu1.SetActive(true);
					//menu2.SetActive(true);
					//menu3.SetActive(true);

					controllerText.GetComponent<Text>().text = "";
					player2keyboard = true;
				}
				if (tempdevice.AnyButton)
				{
					this.GetComponent<InputMaster>().SetPlayerController(tempdevice, playernum);
					controllerText.GetComponent<Text>().color = Color.green;
					clicksound.Play();
					player2HasNoController = false;
					yield return new WaitForSeconds(.3f);
					// menu1.SetActive(true);
					// menu2.SetActive(true);
					// menu3.SetActive(true);
					TitleScreen.GetComponent<Image>().color = Color.white;
					controllerText.GetComponent<Text>().text = "";
				}
			}
			yield return new WaitForSeconds(.05f);
		}
	}
	public PlayerKeyboardActions getP1Actions()
	{

		return player1KeyboardActions;
	}

	public PlayerKeyboardActions getP2Actions()
	{

		return player2KeyboardActions;
	}
}