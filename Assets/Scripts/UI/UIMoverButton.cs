using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMoverButton : MonoBehaviour
{
    public enum UIElementState
    {
        Normal,
        Hidden,
    }

    public RectTransform target;
    public RectTransform arrow;
    public UIElementState startingState;

    [Space] public float transitionTime;
    public AnimationCurve curve;

    private UIElementState currentState;
    
    private Vector3 normalPos;
    private Vector3 hiddenPos;

    private bool transitionActive;

    void Start()
    {
        normalPos = target.position;
        hiddenPos = normalPos;
        hiddenPos.x = -target.rect.width / 2;

        currentState = startingState;
        
        
        if (currentState == UIElementState.Hidden)
        {
            target.position = hiddenPos;
            arrow.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            target.position = normalPos;
            arrow.rotation = Quaternion.Euler(0, 0, 180);
        }
    }

    public void StartTransition()
    {
        if (transitionActive) return;
        switch (currentState)
        {
            case UIElementState.Normal:
                StartCoroutine(Transition(normalPos, hiddenPos, 180, 0, transitionTime));
                currentState = UIElementState.Hidden;
                break;
            case UIElementState.Hidden:
                StartCoroutine(Transition(hiddenPos, normalPos, 0, 180, transitionTime));
                currentState = UIElementState.Normal;
                break;
        }
    }
    
    private IEnumerator Transition(Vector3 startPos, Vector3 endPos, float startRot, float endRot, float time)
    {
        transitionActive = true;
        for (float i = 0; ; i += Time.deltaTime )
        {
            float t = curve.Evaluate(i / time);
            target.position = Vector3.Lerp(startPos, endPos, t);
            arrow.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(startRot, endRot, t));
            if (i >= transitionTime) break;
            yield return null;
        }
        transitionActive = false;
    }
}
