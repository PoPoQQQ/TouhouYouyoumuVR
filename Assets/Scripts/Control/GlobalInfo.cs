using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInfo : MonoBehaviour
{
    public enum Difficulty
    {
        Undefined,
        Easy,
        Normal,
        Hard,
        Lunatic,
        Extra,
        Phantasm
    }

    public static Difficulty currDifficulty = Difficulty.Normal;
    public static int lastScore = -1;

    static public int deathCount;

    public static int easy_hp = 100;
    public static int normal_hp = 20;
    public static int hard_hp = 10;
    public static int lunatic_hp = 5;
    public static int extra_hp = 3;
    public static int phantasm_hp = 1;

    static public void ReportScore(int score)
    {
        lastScore = score;
    }
}
