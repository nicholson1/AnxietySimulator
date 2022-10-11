using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class FogParticleControl : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    private void Start()
    {
        GameManager.AnxietyChanged += AnxietyHasChanged;
    }

    private void OnDestroy()
    {
        GameManager.AnxietyChanged -= AnxietyHasChanged;
    }
    
    private void AnxietyHasChanged(int anxietyLevel)
    {
        //cant change direct PS emission
        var emission = ps.emission;
        emission.rateOverTime =   anxietyLevel- 25 ;

    }
}
