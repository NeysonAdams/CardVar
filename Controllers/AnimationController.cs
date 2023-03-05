using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationController : MonoBehaviour
{
    [SerializeField]float duration = 0.2f;

    /// <summary>
    /// Push any type of animation for View extendable class 
    /// </summary>
    /// <param name="_view"> View whitch we want to animate </param>
    /// <param name="_delay"> How many second we are wayting before animation begin </param>
    /// <param name="_afteranimation">delegete whitch will caled when animation is finished</param>
    public void Animate(
        View _view,
        float _delay = 0,
        Action _afteranimation = null)
    {
        StartCoroutine(IAnimate(_view.gameObject.GetComponent<RectTransform>(), _view.GetModel, _afteranimation, null, _delay));
    }
    /// <summary>
    /// Metod push animation with middle action inside 
    /// </summary>
    /// <param name="_view"> View whitch we want to animate</param>
    /// <param name="_delay"> How many second we are wayting before animation begin</param>
    /// <param name="_middleAction"> delegete whitch will caled in a middle of animation </param>
    /// <param name="_afteranimation">delegete whitch will caled when animation is finished</param>
    public void AnimateWithMiddleAction(View _view,
        float _delay = 0,
        Action _middleAction = null,
        Action _afteranimation = null)
    {
        StartCoroutine(IAnimate(_view.gameObject.GetComponent<RectTransform>(), _view.GetModel, _afteranimation, _middleAction, _delay));
    }

    /*
     * IAnimate - Main Animation Coroutine
     * Transform obj - transform that we need to move
     * Model model - All data about end position and transform status is keep here
     * Action afteranimation - delegate witch is called after aimation
     * Action middleAction - delegate witch is called in a middle of animation
     * */
    IEnumerator IAnimate(RectTransform obj, 
        Model model,
        Action afteranimation,
        Action middleAction,
        float delay = 0)
    {
        float lerpDuration = 1 / duration;
        yield return new WaitForSeconds(delay);
        if (!obj.gameObject.activeSelf) obj.gameObject.SetActive(true);
        float timeElapsed = 0;
        float time = 0;
        Vector2 startPosition = obj.anchoredPosition;
        Vector2 startScale = obj.localScale;
        Quaternion startRotation = obj.rotation;
        while (timeElapsed < lerpDuration)
        {
            obj.anchoredPosition = Vector2.Lerp(startPosition, model.position, timeElapsed );
            obj.localScale = Vector2.Lerp(startScale, model.scale, timeElapsed );
            obj.rotation = Quaternion.Lerp(startRotation, model.rotation, timeElapsed);
            timeElapsed += Time.deltaTime * lerpDuration;
            time += Time.deltaTime;
            if (time >= duration / 2 && middleAction != null)
                middleAction.Invoke();

            yield return new WaitForEndOfFrame();
        }
        if (afteranimation != null)
            afteranimation.Invoke();
    }
}
