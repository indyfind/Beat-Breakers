using UnityEngine;
using System.Collections;

public class BattleStats : MonoBehaviour
{

    public int PerfectsP1, PerfectsP2, GreatsP1, GreatsP2, GoodP1, GoodP2, MaxComboP1, MaxComboP2, ScoreP1, ScoreP2;
	public int RangedAttackP1, RangedAttackP2, MeleeAttackP1, MeleeAttackP2;
    public string FavAttackP1, FavAttackP2;
    public int winner;

    // Use this for initialization
    void Start()
    {

    }
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
		if (FindObjectsOfType (GetType ()).Length > 1) {
			Destroy (gameObject);
		}
	}

    // Update is called once per frame
    void Update()
    {

    }
    public void ResetStats()
    {
        PerfectsP1 = 0;
        PerfectsP2 = 0;
        GreatsP1 = 0;
        GreatsP2 = 0;
        GoodP1 = 0;
        GoodP2 = 0;
        MaxComboP1 = 0;
        MaxComboP2 = 0;
        ScoreP1 = 0;
        ScoreP2 = 0;
        MeleeAttackP1 = 0;
		MeleeAttackP2 = 0;
		RangedAttackP1 = 0;
		RangedAttackP2 = 0;
        FavAttackP1 = "None";
        FavAttackP2 = "None";
        winner = 0;
    }
}
   
