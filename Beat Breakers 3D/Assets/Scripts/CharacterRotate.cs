using UnityEngine;
using InControl;
using System.Collections;

public class CharacterRotate : MonoBehaviour {
    private InputDevice device;
    public GameObject wheel;

    public GameObject EvaModel;
    public GameObject NazModel;

    private Quaternion targetRotation;
    private float rotation = 0f;
    private float t = 0f;
    private float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;

        //start idle animations
        EvaModel.GetComponent<Animator>().SetBool("gameStart", true);
        NazModel.GetComponent<Animator>().SetBool("gameStart", true);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //rotate wheel
        //wheel.transform.rotation = Quaternion.Lerp(wheel.transform.rotation, targetRotation, Time.deltaTime * 2f);
        //t = (Time.time - startTime) / 5f;
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
        if (InputManager.ActiveDevice.LeftStickLeft.WasPressed)
        {
            //wheel.transform.Rotate(new Vector3(0f, -45f, 0f));
            rotation = (rotation - 45f);
            if (rotation < 0f)
            {
                rotation += 360f;
                wheel.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            targetRotation = Quaternion.Euler(new Vector3(0f, rotation, 0f));
        }
        else if (InputManager.ActiveDevice.LeftStickRight.WasPressed)
        {
            //wheel.transform.Rotate(new Vector3(0f, 45f, 0f
            rotation = (rotation + 45f);
            targetRotation = Quaternion.Euler(new Vector3(0f, rotation, 0f));
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
