using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class StateMachineBehaviourDestroyOnExit : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Esto destruye el gameObject cuando salga del estado con este script metido
        Destroy(animator.gameObject);
    }
}
