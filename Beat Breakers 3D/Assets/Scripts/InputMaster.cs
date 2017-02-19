using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class InputMaster : MonoBehaviour
{

    public InputDevice player1Controller;
    public InputDevice player2Controller;
    private InputDevice device;
    public AudioSource clicksound;
    public float playernum;
    public GameObject menu1, menu2, menu3;
    public GameObject controllerText;
    public GameObject TitleScreen;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);

        }
        else
        {
            menu1.SetActive(false);
            menu2.SetActive(false);
            menu3.SetActive(false);
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
            if (tempdevice.AnyButton && this.GetComponent<InputMaster>().notassignedtoplayer(tempdevice, playernum))
            {
                this.GetComponent<InputMaster>().SetPlayerController(tempdevice, playernum);
                controllerText.GetComponent<Text>().color = Color.green;
                clicksound.Play();
                yield return new WaitForSeconds(.3f);
                controllerText.GetComponent<Text>().text = "";
                controllerText.GetComponent<Text>().color = Color.white;
                yield return new WaitForSeconds(.05f);
                controllerText.GetComponent<Text>().text = "Player 2 \r\n Press Any Button";
                playernum = 2;
                player1HasNoController = false;
            }
            yield return new WaitForSeconds(.05f);
        }

        while (player2HasNoController)
        {
            tempdevice = InputManager.ActiveDevice;
            if (tempdevice.AnyButton && this.GetComponent<InputMaster>().notassignedtoplayer(tempdevice, playernum))
            {
                this.GetComponent<InputMaster>().SetPlayerController(tempdevice, playernum);
                controllerText.GetComponent<Text>().color = Color.green;
                clicksound.Play();
                player2HasNoController = false;
                yield return new WaitForSeconds(.3f);
                menu1.SetActive(true);
                menu2.SetActive(true);
                menu3.SetActive(true);
                TitleScreen.GetComponent<Image>().color = Color.white;
                controllerText.GetComponent<Text>().text = "";

            }
            yield return new WaitForSeconds(.05f);
        }
    }
}
