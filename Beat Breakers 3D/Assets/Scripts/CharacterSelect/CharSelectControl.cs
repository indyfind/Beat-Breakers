using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CharSelectControl: MonoBehaviour {
    private InputDevice device;
    public GameObject wheel;

    public GameObject EvaModel;
    public GameObject NazModel;

    private Quaternion targetRotation;
    private float rotation = 0f;
    //private float t = 0f;
    private float startTime;
    private float speed = 4f;

    //InControl input device
    private InputDevice device1;
    private InputDevice device2;
    private GameObject inputMaster;

    private bool p1picked = false;
    private bool p2picked = false;

    public string p1char = "";
    public string p2char = "";

    public GameObject p1coin;
    public GameObject p2coin;

    private GameObject charPicks;

    public Text charText;
    private GameObject menuSong;
    public GameObject soundPlayer;

    // Use this for initialization
    void Start () {

        charPicks = GameObject.FindGameObjectWithTag("CharPicks");
        menuSong = GameObject.FindGameObjectWithTag("MenuSong");

        //assign controllers
        inputMaster = GameObject.FindGameObjectWithTag("InputMaster");
        device1 = inputMaster.GetComponent<InputMaster>().player1Controller;
        device2 = inputMaster.GetComponent<InputMaster>().player2Controller;

        //startTime = Time.time;

        //start idle animations
        EvaModel.GetComponent<Animator>().SetBool("gameStart", true);
        NazModel.GetComponent<Animator>().SetBool("gameStart", true);

        soundPlayer.GetComponent<SoundPlayer>().PlaySound("ChooseYourCharacter");
    }

    // Update is called once per frame
    void Update()
    {
        //display char name
        float temp = rotation;
        while (temp < 0f)
        {
            temp += 360f;
        }
        if (temp % 360f == 0f)
        {
            charText.text = "Eva";
        } else if (temp % 360f == 45f)
        {
            charText.text = "Naz";
        } else if (temp % 360f == 90f)
        {
            charText.text = "?";
        }
        else if (temp % 360f == 135f)
        {
            charText.text = "?";
        }
        else if (temp % 360f == 180f)
        {
            charText.text = "?";
        }
        else if (temp % 360f == 225f)
        {
            charText.text = "?";
        }
        else if (temp % 360f == 270f)
        {
            charText.text = "?";
        }
        else if (temp % 360f == 315f)
        {
            charText.text = "?";
        }

        //rotate wheel
        wheel.transform.rotation = Quaternion.Lerp(wheel.transform.rotation, targetRotation, Time.deltaTime * speed);

        //t = (Time.time - startTime) / 5f;
        /*
        t = Time.deltaTime * 10f;

        if (wheel.transform.rotation != targetRotation)
        {
            wheel.transform.eulerAngles = new Vector3(0f, Mathf.SmoothStep(wheel.transform.eulerAngles.y, rotation, t), 0f);
            //wheel.transform.rotation = Quaternion.Euler(new Vector3(0f, Mathf.SmoothStep(wheel.transform.rotation.eulerAngles.y, 360f, t), 0f
        } else
        { 
            if (rotation == 360f)
            {
                rotation = 0f;
                wheel.transform.eulerAngles = new Vector3(0f, rotation, 0f);
            }
        }
        Debug.Log("rotation" + rotation);
        Debug.Log("y" + wheel.transform.eulerAngles.y);
        */
        if (!p1picked)
        {
            if (device1.LeftStickLeft.WasPressed || device1.DPadLeft.WasPressed)
            {
                //wheel.transform.Rotate(new Vector3(0f, -45f, 0f));
                rotation = (rotation - 45f);
                /*
                if (rotation < 0f)
                {
                    rotation += 360f;
                    wheel.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }
                */
                targetRotation = Quaternion.Euler(new Vector3(0f, rotation, 0f));
            }
            else if (device1.LeftStickRight.WasPressed || device1.DPadRight.WasPressed)
            {
                //wheel.transform.Rotate(new Vector3(0f, 45f, 0f
                rotation = (rotation + 45f);
                targetRotation = Quaternion.Euler(new Vector3(0f, rotation, 0f));
            }
            if (device1.Action1.WasPressed)
            {
                if (rotation%360f == 0f)
                {
                    p1char = "Eva";
                    p1picked = true;
                    p1coin.SetActive(false);
                    p2coin.SetActive(true);
                    soundPlayer.GetComponent<SoundPlayer>().PlaySound("Eva", true);
                }
                else if (rotation % 360f == 45f)
                {
                    p1char = "Naz";
                    p1picked = true;
                    p1coin.SetActive(false);
                    p2coin.SetActive(true);
                    soundPlayer.GetComponent<SoundPlayer>().PlaySound("Naz", true);
				} else {
					soundPlayer.GetComponent<SoundPlayer>().PlaySound("No");
				}
            }
        } else if (!p2picked)
        {
            if (device2.LeftStickLeft.WasPressed || device2.DPadLeft.WasPressed)
            {
                //wheel.transform.Rotate(new Vector3(0f, -45f, 0f));
                rotation = (rotation - 45f);
                /*
                if (rotation < 0f)
                {
                    rotation += 360f;
                    wheel.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                }
                */
                targetRotation = Quaternion.Euler(new Vector3(0f, rotation, 0f));
            }
            else if (device2.LeftStickRight.WasPressed || device2.DPadRight.WasPressed)
            {
                //wheel.transform.Rotate(new Vector3(0f, 45f, 0f
                rotation = (rotation + 45f);
                targetRotation = Quaternion.Euler(new Vector3(0f, rotation, 0f));
            }
            if (device2.Action1.WasPressed)
            {
                if (rotation % 360f == 0f)
                {
                    p2char = "Eva";
                    p2picked = true;
                    soundPlayer.GetComponent<SoundPlayer>().PlaySound("Eva", true);
                } else if (rotation % 360f == 45f)
                {
                    p2char = "Naz";
                    p2picked = true;
                    soundPlayer.GetComponent<SoundPlayer>().PlaySound("Naz", true);
				} else {
					soundPlayer.GetComponent<SoundPlayer>().PlaySound("No", true);
				}
            }
        } else if (p1picked && p2picked)
        {
            charPicks.GetComponent<CharPicks>().p1char = p1char;
            charPicks.GetComponent<CharPicks>().p2char = p2char;
            Destroy(menuSong);
            SceneManager.LoadScene(1);
        }
	}

    /*
    IEnumerator Rotate(float rotationAmount)
    {
        Quaternion finalRotation = Quaternion.Euler(0, rotationAmount, 0) * startingRotation;

        while (this.transform.rotation != finalRotation)
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, finalRotation, Time.deltaTime * speed);
            yield return 0;
        }
    }
    */
}
