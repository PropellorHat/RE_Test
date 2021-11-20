using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimation : MonoBehaviour
{
    private NavMeshAgent playerAgent;
    private PlayerController playerCont;
    private ShootScript shootScript;
    private Animator anim;

    private bool doRecoil;
    private float recoilTimer;
    public AnimationCurve recoilCurve;
    public float recoilDuration;
    public float recoilMaxRotation;
    public Transform rightLowerArm;

    // Start is called before the first frame update
    void Awake()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        playerCont = GetComponent<PlayerController>();
        shootScript = GetComponent<ShootScript>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", playerAgent.velocity.magnitude);


        if(playerCont.isShooting)
        {
            anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 1, Time.deltaTime * 10f));
        }
        else
        {
            anim.SetLayerWeight(1, Mathf.Lerp(anim.GetLayerWeight(1), 0, Time.deltaTime * 10f));
        }

        anim.SetBool("Reloading", shootScript.isReloading);

        if (shootScript.hasFired == true)
        {
            doRecoil = true;
            recoilTimer = Time.time;
        }
    }

    private void OnAnimatorIK()
    {
        if (playerCont.isShooting)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.5f);
            //anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0.5f);


            anim.SetLookAtWeight(1);
        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            //anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);

            anim.SetLookAtWeight(0);
        }

        anim.SetIKPosition(AvatarIKGoal.RightHand, playerCont.shootPos);
        //anim.SetIKPosition(AvatarIKGoal.LeftHand, playerCont.shootPos);

        anim.SetLookAtPosition(playerCont.shootPos);
    }

    private void LateUpdate()
    {
        if(doRecoil)
        {
            if(recoilTimer < 0)
            {
                return;
            }
            float curveTime = (Time.time - recoilTimer) / recoilDuration;
            if(curveTime > 1)
            {
                doRecoil = false;
                recoilTimer = -1;
            }
            else
            {
                rightLowerArm.Rotate(new Vector3(0.7f, 0f, -0.7f), recoilCurve.Evaluate(curveTime) * recoilMaxRotation, Space.Self);
            }
        }
    }
}
