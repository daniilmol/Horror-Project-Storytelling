using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentManager : MonoBehaviour
{
    [Header("Moments Requiring Prerequisite Moments")]
    [SerializeField] private Moment[] momentPrerequisites;
    public void MomentTriggered(){
        for(int i = 0; i < momentPrerequisites.Length; i++){
            if(Moment.numMoments >= momentPrerequisites[i].GetRequired()){
                momentPrerequisites[i].enabled = true;
            }
        }
    }
}
