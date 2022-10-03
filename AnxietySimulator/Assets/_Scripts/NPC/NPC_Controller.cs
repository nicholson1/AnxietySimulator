using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NPC_Controller : MonoBehaviour
{
    [SerializeField] NavMeshAgent navAgent;
    [SerializeField] private Material[] Materials;
    [SerializeField] private SkinnedMeshRenderer[] Skins;
    [SerializeField] Animator animator;

    [Header("NPC Stats")] 
    public int Talkative;
    public int Friendly;
    public int Paitent;
    public int Swag;

    

    private void Start()
    {
        
        Randomize();

    }

    private void Randomize()
    {
        // disable all models
        // enable random 1
        // assign material to random 1

        foreach (var skin in Skins)
        {
            skin.gameObject.SetActive(false);
        }

        int SkinIndex = Random.Range(0, Skins.Length);

        Skins[SkinIndex].gameObject.SetActive(true);
        Skins[SkinIndex].material = Materials[Random.Range(0, Materials.Length)];

        // boo ya
        // randomize stats
        Friendly = Random.Range(0, 100);
        Paitent = Random.Range(0, 100);
        Swag = Random.Range(0, 100);
        Talkative = Random.Range(0, 100);


    }


    public void SetWalk()
    {
        animator.SetTrigger("Move");
        int randomWalk = Random.Range(0, 100);
        //Debug.Log("Walk Roll: " + randomWalk);

        // if we are very impatient
        // we have a chance to run
        if (Paitent <= 15)
        {
            if (randomWalk > Paitent)
            {
                animator.SetInteger("Type", (int)AnimationTypes.NoPatient);
                navAgent.speed = 2;
                return;
            }
        }
        // if we made it here we are walking
        // now we determine the kind of walk
        
        // if we big friendly, likely that we friendly walk
        if (Friendly >= 75)
        {
            if (randomWalk < Friendly)
            {
                animator.SetInteger("Type", (int)AnimationTypes.Friendly);
                navAgent.speed = 1.5f;
                return;

            }
        }
        else if (Friendly <= 25)
        {
            if (randomWalk > Friendly)
            {
                animator.SetInteger("Type", (int)AnimationTypes.NoFriendly);
                navAgent.speed = 2;
                return;

            }
        }
        
        // if we big Swag
        if (Swag >= 85)
        {
            if (randomWalk < Swag)
            {
                animator.SetInteger("Type", (int)AnimationTypes.Swag);
                navAgent.speed = 1;
                return;

            }
        }
        // if we no Swag
        else if (Swag <= 15)
        {
            if (randomWalk > Swag)
            {
                animator.SetInteger("Type", (int)AnimationTypes.NoSwag);
                navAgent.speed = .75f;
                return;

            }
        }
        
        animator.SetInteger("Type", (int)AnimationTypes.Basic);
        navAgent.speed = 1;

    }

    public void SetIdle()
    {
        animator.SetTrigger("Idle");
        int randomIdle = Random.Range(0, 100);
        
        
        if (Friendly >= 75)
        {
            if (randomIdle < Friendly)
            {
                animator.SetInteger("Type", (int)AnimationTypes.Friendly);
                return;

            }
        }
        else if (Friendly <= 25)
        {
            if (randomIdle > Friendly)
            {
                animator.SetInteger("Type", (int)AnimationTypes.NoFriendly);
                return;

            }
        }
        
        // if we big Swag
        if (Swag >= 75)
        {
            if (randomIdle < Swag)
            {
                animator.SetInteger("Type", (int)AnimationTypes.Swag);
                return;

            }
        }
        // if we no Swag
        else if (Swag <= 25)
        {
            if (randomIdle > Swag)
            {
                animator.SetInteger("Type", (int)AnimationTypes.NoSwag);
                return;

            }
        }
        
        if (Paitent >= 75)
        {
            if (randomIdle < Paitent)
            {
                animator.SetInteger("Type", (int)AnimationTypes.Patient);
                return;

            }
        }
        // if we no patient
        else if (Paitent <= 25)
        {
            if (randomIdle > Paitent)
            {
                animator.SetInteger("Type", (int)AnimationTypes.NoPatient);
                return;

            }
        }
    }

    public bool InteractCheck()
    {
        // if we unfriendly or we are friendly interact?
        int randomInteract = Random.Range(0, 100);
        if (Friendly >= 80)
        {
            if (randomInteract< Friendly)
            {
                
                return true;

            }
        }
        else if (Friendly <= 30)
        {
            if(randomInteract > Friendly)
            {
                
                return true;

            }
        }
        if (Talkative >= 65)
        {
            if (randomInteract< Talkative)
            {
                
                return true;

            }
        }
        
        
        return false;
        
    }

    public void SetWave()
    {
        animator.SetTrigger("Wave");
        int randomWave = Random.Range(0, 100);
        
        
        if (Friendly >= 75)
        {
            if (randomWave < Friendly)
            {
                animator.SetInteger("Type", (int)AnimationTypes.Friendly);
                return;

            }
        }
        else if (Friendly <= 15)
        {
            if (randomWave > Friendly)
            {
                animator.SetInteger("Type", (int)AnimationTypes.NoFriendly);
                return;

            }
        }
        
        // if we big Swag
        if (Swag >= 75)
        {
            if (randomWave < Swag)
            {
                animator.SetInteger("Type", (int)AnimationTypes.Swag);
                return;

            }
        }
        // if we no Swag
        else if (Swag <= 15)
        {
            if (randomWave > Swag)
            {
                animator.SetInteger("Type", (int)AnimationTypes.NoSwag);
                return;

            }
        }
        
        if (Paitent >= 75)
        {
            if (randomWave < Paitent)
            {
                animator.SetInteger("Type", (int)AnimationTypes.Patient);
                return;

            }
        }
        // if we no patient
        else if (Paitent <= 15)
        {
            if (randomWave > Paitent)
            {
                animator.SetInteger("Type", (int)AnimationTypes.NoPatient);
                return;

            }
        }
    }

    private enum AnimationTypes
    {
        Basic,
        Friendly,
        NoFriendly,
        Patient,
        NoPatient,
        Swag,
        NoSwag
        
    }
    
    
}
