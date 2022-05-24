using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTrail : SingletonBase<CameraFollowTrail>
{
    new public Camera camera;
    public TrailRenderer trail;
    public LineRenderer wave;
    public bool following = false;
    public Transform VectorsParent => FourierDrawer.Instance.vectorParent;

    protected override void DoAwake()
    {
        if (camera == null) camera = Camera.main;
    }

    public void StartFollowing()
    {
        following = true;
    }

    public void StopFollowing()
    {
        camera.transform.position = new Vector3(0, 0, camera.transform.position.z);
        following = false;
    }

    Vector3 GetTrailPos()
    {
        int lastIndex = FourierDrawer.Instance.currentVectorAmount - 1;

        return VectorsParent.GetChild(lastIndex).position;

        //return FourierDrawer.Instance.trailMode switch
        //{
        //    FourierDrawer.TrailMode.Trail => trail.GetPosition(trail.positionCount - 1),
        //    FourierDrawer.TrailMode.Wave => wave.transform.position + wave.GetPosition(wave.positionCount - 1),
        //    _ => VectorsParent.GetChild(VectorsParent.childCount - 1).position
        //};
    }

    private void Update()
    {
        if (!following) return;

        Vector3 trailEndPos = GetTrailPos();

        camera.transform.position = new Vector3(trailEndPos.x, trailEndPos.y, camera.transform.position.z);
    }
}
