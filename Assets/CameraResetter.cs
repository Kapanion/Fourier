using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResetter : SingletonBase<CameraResetter>
{
    new public Camera camera;

    public void Reset()
    {
        CameraFollowTrail.Instance.StopFollowing();
        camera.orthographicSize = 5;
        camera.transform.position = new Vector3(0, 0, camera.transform.position.z);
    }
}
