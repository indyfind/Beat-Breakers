using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // required when using UI elements in scripts
using UnityEngine.EventSystems;// Required when using Event data.

public class MainMenu : MonoBehaviour {
	public GameObject menu1, menu2, menu3;
	int position = 1;
	// Use this for initialization
	void Start () {
		EventSystem.current.SetSelectedGameObject(menu1, new BaseEventData(EventSystem.current));

	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			if (position == 1)
			{
				EventSystem.current.SetSelectedGameObject(menu3, new BaseEventData(EventSystem.current));


				position = 3;

			}
			else if (position == 2)
			{
				EventSystem.current.SetSelectedGameObject(menu1, new BaseEventData(EventSystem.current));
				position = 1;


			}
			else if (position == 3)
			{
				EventSystem.current.SetSelectedGameObject(menu2, new BaseEventData(EventSystem.current));
				position = 2;


			}
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			if (position == 1)
			{
				EventSystem.current.SetSelectedGameObject(menu2, new BaseEventData(EventSystem.current));
				position = 2;



			}
			else if (position == 2)
			{
				EventSystem.current.SetSelectedGameObject(menu3, new BaseEventData(EventSystem.current));
				position = 3;


			}
			else if (position == 3)
			{
				EventSystem.current.SetSelectedGameObject(menu1, new BaseEventData(EventSystem.current));
				position = 1;


			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (position == 1)
			{
				menu1.GetComponent<LoadOnClick>();

			}
			else if (position == 2)
			{
				menu2.GetComponent<LoadOnClick>();
			}
			else if (position == 3)
			{
				menu2.GetComponent<LoadOnClick>();
			}
		}


	}



}