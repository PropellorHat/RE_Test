using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRagdoll : MonoBehaviour
{
    public Transform[] animatedTransforms;
    public ConfigurableJoint[] conJoints;

    private Quaternion[] initRots = new Quaternion[10];
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < conJoints.Length; i++)
        {
            ConfigurableJointExtensions.SetupAsCharacterJoint(conJoints[i]);
            
            initRots[i] = conJoints[i].transform.localRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < conJoints.Length; i++)
        {
            ConfigurableJointExtensions.SetTargetRotationLocal(conJoints[i], animatedTransforms[i + 1].localRotation, initRots[i]);
        }
    }
}
