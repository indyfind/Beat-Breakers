using UnityEngine;
using System.Collections;

public class BattleStats : MonoBehaviour
{

    public int PerfectsP1, PerfectsP2, GreatsP1, GreatsP2, GoodP1, GoodP2, MaxComboP1, MaxComboP2, ScoreP1, ScoreP2;
    public int PopLockP1, PopLockP2, SixStepP1, SixStepP2, HeadSlideP1, HeadSlideP2;
    public string FavAttackP1, FavAttackP2;
    public int winner;

    // Use this for initialization
    void Start()
    {

    }
    void Awake()
    {

        DontDestroyOnLoad(transform.gameObject);
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
        PopLockP1 = 0;
        PopLockP2 = 0;
        SixStepP1 = 0;
        SixStepP2 = 0;
        HeadSlideP1 = 0;
        HeadSlideP2 = 0;
        FavAttackP1 = "None";
        FavAttackP2 = "None";
        winner = 0;
    }
}
   
