﻿using UnityEngine;

using JointIndex = KinectWrapper.NuiSkeletonPositionIndex;

namespace Real_VATA
{
    class MathUtil
    {
        public static float Dot(Vector3 start, Vector3 end)
        {
            start.Normalize(); end.Normalize();
            float theta = Mathf.Acos(Vector3.Dot(start, end));

            return theta * Mathf.Rad2Deg;
        }

        public static Vector3 GetVectorBetween(JointIndex joint1, JointIndex joint2, KinectManager manager)//순서 중요 | joint1->joint2
        {
            uint playerID = manager != null ? manager.GetPlayer1ID() : 0;

            Vector3 pos1 = manager.GetJointPosition(playerID, (int)joint1);
            Vector3 pos2 = manager.GetJointPosition(playerID, (int)joint2);

            return pos2 - pos1;
        }

        public static Vector3[] GetJointCoordinate(KinectManager manager, JointIndex joint)
        {
            Matrix4x4 coordinate = manager.GetJointOrientationMatrix((int)joint);

            return new Vector3[]
            {
            new Vector3(coordinate.GetColumn(0).x, coordinate.GetColumn(0).y, coordinate.GetColumn(0).z),
            new Vector3(coordinate.GetColumn(1).x, coordinate.GetColumn(1).y, coordinate.GetColumn(1).z),
            new Vector3(coordinate.GetColumn(2).x, coordinate.GetColumn(2).y, coordinate.GetColumn(2).z)
            };
        }

        public static float LimitJointAngle(JointName jointName, float angle) // angle 제한하는 함수
        {
            switch (jointName)
            {
                case JointName.shoulder_l1:
                case JointName.shoulder_r1: angle = Mathf.Clamp(angle, -90f, 90f); break;
                case JointName.shoulder_l2:
                case JointName.elbow_l: angle = Mathf.Clamp(angle, -90f, 0f); break;
                case JointName.shoulder_r2:
                case JointName.elbow_r: angle = Mathf.Clamp(angle, 0f, 90f); break;
                default: break;
            }

            return angle;
        }
    }
}

/*--------------------------------------not use------------------------------------
public static Vector3[] GetHipCenterCoordinate(KinectManager manager)
{
    Matrix4x4 hipCenter = manager.GetJointOrientationMatrix((int)JointIndex.HipCenter);//모든 조인트의 기준점

    return new Vector3[]
    {
        new Vector3(hipCenter.GetColumn(0).x, hipCenter.GetColumn(0).y, hipCenter.GetColumn(0).z),
        new Vector3(hipCenter.GetColumn(1).x, hipCenter.GetColumn(1).y, hipCenter.GetColumn(1).z),
        new Vector3(hipCenter.GetColumn(2).x, hipCenter.GetColumn(2).y, hipCenter.GetColumn(2).z)
    };
}

public static Vector3[] GetHeadCoordinate(KinectManager manager)
{
    Matrix4x4 head = manager.GetJointOrientationMatrix((int)JointIndex.Head);

    return new Vector3[]
    {
        new Vector3(head.GetColumn(0).x, head.GetColumn(0).y, head.GetColumn(0).z),
        new Vector3(head.GetColumn(1).x, head.GetColumn(1).y, head.GetColumn(1).z),
        new Vector3(head.GetColumn(2).x, head.GetColumn(2).y, head.GetColumn(2).z)
    };
}
--------------------------------------not use------------------------------------*/