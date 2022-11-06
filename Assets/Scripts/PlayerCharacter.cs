using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public string playerName;
    public int playerLevel;
    public float playerExp;

    private float requiredExp()
    {
        float f = playerLevel * 1.5f;
        return f;
    }

    public int playerHealth;
    public int playerStrength;

    public void GainXP(float expGain)
    {
        //animate later
        playerExp += expGain;
        if (playerExp >= requiredExp())
        {
            float overflow = playerExp - requiredExp();
            playerLevel++;
            playerExp = overflow;
        }
    }
}
