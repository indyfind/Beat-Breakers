using UnityEngine;
using System.Collections;



public class BlockMover: MonoBehaviour{

	public float delay;
	private float startime;
	private Vector3 start;
	private Vector3 finish;
	public string side;
	private float f;
	private bool started;
	
	void Start(){
		started = false;
	}
	
	void Update(){
	
	f = (Time.time - startime) / 2f;
	if (started == true)
	{
		transform.position = Vector3.Lerp(start, finish, f);
		
		if (transform.position ==finish)
		{
                startime = Time.time;
			transform.position = start;
			
		}
	}
	}
	
	public void BattleStart()
	{
		start = this.transform.position;
		if(side == "left"){
		finish = new Vector3(-3.1f, -0.28f,0f);
		
		}
		else{
			finish = new Vector3(3.1f, 0.28f, 0f);
		}
		startime = Time.time + delay;
		started = true;
		f = (Time.time - startime) * 2;
}


}
