using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteContainer : MonoBehaviour
{
    public Sprite[] pLegs, pUnarmedWalk, pCleaverWalk, pCleaverAttack, pThrowingKnivesAtk, pDead;
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
    
    public Sprite[] getPlayerCleaverWalk()
    {
        return pCleaverWalk;
    }
    
    public Sprite[] getPlayerCleaverAttack()
    {
        // Return the P Cleaver Attack sprites
        return pCleaverAttack;
    }

    public Sprite[] getPlayerThrowingKnivesAtk()
    {
        return pThrowingKnivesAtk;
    }

    public Sprite[] getPlayerDead()
    {
        return pDead;
    }

}
