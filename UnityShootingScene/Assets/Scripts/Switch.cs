using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : StateMachineBehaviour
{

    public bool IsPistol;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (IsPistol)
        {
            if (stateInfo.speed > 0)
            {
                if (!animator.GetComponent<Player>().PistolHand.activeSelf &&
                     stateInfo.normalizedTime / stateInfo.length > 0.05)
                {
                    animator.GetComponent<Player>().PistolBack.SetActive(false);
                    animator.GetComponent<Player>().PistolHand.SetActive(true);
                }
            }
            else
            {
                if (!animator.GetComponent<Player>().PistolBack.activeSelf &&
                     stateInfo.normalizedTime / stateInfo.length > 0.95)
                {
                    animator.GetComponent<Player>().PistolBack.SetActive(true);
                    animator.GetComponent<Player>().PistolHand.SetActive(false);
                }
            }
        }
        else
        {
            if (stateInfo.speed > 0)
            {
                if (!animator.GetComponent<Player>().AKHand.activeSelf &&
                     stateInfo.normalizedTime / stateInfo.length > 0.30)
                {
                    animator.GetComponent<Player>().AKBack.SetActive(false);
                    animator.GetComponent<Player>().AKHand.SetActive(true);
                }
            }
            else
            {
                if (!animator.GetComponent<Player>().AKBack.activeSelf &&
                     stateInfo.normalizedTime / stateInfo.length > 0.70)
                {
                    animator.GetComponent<Player>().AKBack.SetActive(true);
                    animator.GetComponent<Player>().AKHand.SetActive(false);
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (IsPistol)
        {
            if (stateInfo.speed > 0)
            {
                animator.GetComponent<Player>().PistolBack.SetActive(false);
                animator.GetComponent<Player>().PistolHand.SetActive(true);
            }
            else
            {
                animator.GetComponent<Player>().PistolBack.SetActive(true);
                animator.GetComponent<Player>().PistolHand.SetActive(false);
            }
        }
        else
        {
            if (stateInfo.speed > 0)
            {
                animator.GetComponent<Player>().AKBack.SetActive(false);
                animator.GetComponent<Player>().AKHand.SetActive(true);
            }
            else
            {
                animator.GetComponent<Player>().AKBack.SetActive(true);
                animator.GetComponent<Player>().AKHand.SetActive(false);
            }
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
