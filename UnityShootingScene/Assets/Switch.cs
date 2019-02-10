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
                if (!animator.GetComponent<Test>().PistolHand.activeSelf &&
                     stateInfo.normalizedTime / stateInfo.length > 0.05)
                {
                    animator.GetComponent<Test>().PistolBack.SetActive(false);
                    animator.GetComponent<Test>().PistolHand.SetActive(true);
                }
            }
            else
            {
                if (!animator.GetComponent<Test>().PistolBack.activeSelf &&
                     stateInfo.normalizedTime / stateInfo.length > 0.95)
                {
                    animator.GetComponent<Test>().PistolBack.SetActive(true);
                    animator.GetComponent<Test>().PistolHand.SetActive(false);
                }
            }
        }
        else
        {
            if (stateInfo.speed > 0)
            {
                if (!animator.GetComponent<Test>().AKHand.activeSelf &&
                     stateInfo.normalizedTime / stateInfo.length > 0.30)
                {
                    animator.GetComponent<Test>().AKBack.SetActive(false);
                    animator.GetComponent<Test>().AKHand.SetActive(true);
                }
            }
            else
            {
                if (!animator.GetComponent<Test>().AKBack.activeSelf &&
                     stateInfo.normalizedTime / stateInfo.length > 0.70)
                {
                    animator.GetComponent<Test>().AKBack.SetActive(true);
                    animator.GetComponent<Test>().AKHand.SetActive(false);
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
                animator.GetComponent<Test>().PistolBack.SetActive(false);
                animator.GetComponent<Test>().PistolHand.SetActive(true);
            }
            else
            {
                animator.GetComponent<Test>().PistolBack.SetActive(true);
                animator.GetComponent<Test>().PistolHand.SetActive(false);
            }
        }
        else
        {
            if (stateInfo.speed > 0)
            {
                animator.GetComponent<Test>().AKBack.SetActive(false);
                animator.GetComponent<Test>().AKHand.SetActive(true);
            }
            else
            {
                animator.GetComponent<Test>().AKBack.SetActive(true);
                animator.GetComponent<Test>().AKHand.SetActive(false);
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
