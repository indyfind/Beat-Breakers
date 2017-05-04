using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerKeyboardActions : PlayerActionSet
{
	public PlayerAction Up, Down, Left, Right;
	public PlayerAction ShortAtk, LongAtk, BasicAttack;
	public PlayerAction Select;
	// Use this for initialization
	public PlayerKeyboardActions()
	{
		Up = CreatePlayerAction("p1Up");
		Down = CreatePlayerAction("p1Down");
		Left = CreatePlayerAction("p1Left");
		Right = CreatePlayerAction("p1Right");
		ShortAtk = CreatePlayerAction("p1ShortAtk");
		LongAtk = CreatePlayerAction("p1LongAtk");
		BasicAttack = CreatePlayerAction("p1BasicAttack");
		Select = CreatePlayerAction("Select");

	}


}
