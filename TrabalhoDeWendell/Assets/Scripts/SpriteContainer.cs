using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteContainer : MonoBehaviour
{
    public Sprite[] pLegs, pUnarmedWalk, pCleaverWalk, pCleaverAttack, pThrowingKnivesAtk;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite[] getPlayerLegs()
    {
        return pLegs;
    }

    public Sprite[] getPlayerUnarmedwalk()
    {
        return pUnarmedWalk;
    }

    public Sprite[] getWeapon(string weapon)
    {
        switch (weapon)
        {
            case "Cleaver":
                return pCleaverAttack;
                break;
            case "ThrowingKnives":
                return pThrowingKnivesAtk;
                break;
        }
    }

    public Sprite[] getWeaponWalk(string weapon)
    {
        switch (weapon)
        {
            case "Cleaver":
                return pCleaverWalk;
                break;
        }
    }
}
