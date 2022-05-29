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

    public enum UIElementPosition
    {
        Left,
        Right,
    }

    public RectTransform target;
    public RectTransform arrow;
    public UIElementState startingState;
    public UIElementPosition position = UIElementPosition.Left;

    [Space] public float transitionTime;
    public AnimationCurve curve;

    private UIElementState currentState;
    
    private Vector3 normalPos;
    private Vector3 hiddenPos;
    private float normalRot;
    private float hiddenRot;

    private bool transitionActive;

    private void Start()
    {
        UpdatePositions();
        currentState = startingState;
        
        
        if (currentState == UIElementState.Hidden)
        {
            target.anchoredPosition = hiddenPos;
            arrow.rotation = Quaternion.Euler(0, 0, hiddenRot);
        }
        else
        {
            target.anchoredPosition = normalPos;
            arrow.rotation = Quaternion.Euler(0, 0, normalRot);
        }
    }

    private void UpdatePositions()
    {        
        normalPos = target.anchoredPosition;
        hiddenPos = normalPos;
        hiddenPos.x = (position == UIElementPosition.Left ? -1 : 1) * target.rect.width / 2;
        
        normalRot = position == UIElementPosition.Left ? 180 : 0;
        hiddenRot = 180 - normalRot;
    }

    public void StartTransition()
    {
        if (transitionActive) return;
        switch (currentState)
        {
            case UIElementState.Normal:
                StartCoroutine(Transition(normalPos, hiddenPos, normalRot, hiddenRot, transitionTime));
                currentState = UIElementState.Hidden;
                break;
            case UIElementState.Hidden:
                StartCoroutine(Transition(hiddenPos, normalPos, hiddenRot, normalRot, transitionTime));
                currentState = UIElementState.Normal;
                break;
        }
    }
    
    private IEnumerator Transition(Vector3 startPos, Vector3 endPos, float startRot, float endRot, float time)
    {
        transitionActive = true;
        for (float i = 0; ; i += Time.deltaTime )
        {
            var t = curve.Evaluate(i / time);
            target.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            arrow.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(startRot, endRot, t));
            if (i >= transitionTime) break;
            yield return null;
        }
        transitionActive = false;
    }
}
