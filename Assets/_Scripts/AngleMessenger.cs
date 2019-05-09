﻿using UnityEngine;

public class AngleMessenger : MonoBehaviour
{
    [SerializeField]
    private CDJointOrientationSetter cdJointOrientationSetter;

    [SerializeField]
    private JointOrientationSetter jointOrientationSetter;

    private CDJoint[] cdJoints;
    private Joint[] joints;

    public bool isRealtimePlayer = true;

    private void Awake()
    {
        cdJoints = cdJointOrientationSetter.joints;
        joints = jointOrientationSetter.joints;
    }

    private void Update()
    {
        if (isRealtimePlayer)
            SendAngle();
    }

    void SendAngle()
    {
        SendAngleToNeck();
        SendAngleToRightArm();
        SendAngleToLeftArm();
    }

    void SendAngleToRightArm()
    {
        if (CollisionManager.rightArmMove)
        {
            for (int i = 0; i < 3; i++)
                joints[i].angle = cdJoints[i].angle;
        }
    }

    void SendAngleToLeftArm()
    {
        if (CollisionManager.leftArmMove)
        {
            for (int i = 3; i < 6; i++)
                joints[i].angle = cdJoints[i].angle;
        }
    }

    void SendAngleToNeck()
    {
        if (CollisionManager.neckMove)
        {
            for (int i = 6; i < 8; i++)
                joints[i].angle = cdJoints[i].angle;
        }
    }
}