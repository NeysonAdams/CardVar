using System;
using UnityEngine;
using UnityEngine.UI;

public class PopUpView : View
{

    [Header ("Game Objects")]
    [SerializeField] GameObject background;
    [SerializeField] GameObject upline;

    [Header("PopUP Sprites")]
    [SerializeField] Sprite win;
    [SerializeField] Sprite lose;
    [SerializeField] Sprite draw;
    [SerializeField] Sprite winStyle;
    [SerializeField] Sprite loseStyle;
    [SerializeField] Sprite simpleStyle;

    [Header("PopUP Labels")]
    [SerializeField] Text upLabel;
    [SerializeField] Text dowLabel;

    [SerializeField] Model model;


    public Model SetModel
    {
        set { model = value; }
    }
    public override Model GetModel => model;

    public void Init(string _upText, string _downtext)
    {
        upLabel.text = _upText;
        dowLabel.text = _downtext;

        background.SetActive(!string.IsNullOrEmpty(dowLabel.text));
        upline.SetActive(!string.IsNullOrEmpty(upLabel.text));
    }

    public override View Show(Action<Action> _animator)
    {
        transform.localScale = Vector2.zero;
        model.scale = Vector2.one;
        _animator.Invoke(null);
        return this;
    }

    public override View Hide(Action<Action> _animator)
    {
        model.scale = Vector2.zero;
        _animator.Invoke(null);
        return this;
    }

    public void SetStatus(RoundStatus _status)
    {
        Image upl = upline.GetComponent<Image>();
        switch (_status)
        {
            case RoundStatus.WIN:
                upl.sprite = win;
                break;
            case RoundStatus.LOSE:
                upl.sprite = lose;
                break;
            case RoundStatus.DRAW:
                upl.sprite = draw;
                break;
        }


    }

    public void SetStyle(PopupStyle _style)
    {
        Image bimg = background.GetComponent<Image>();
        switch (_style)
        {
            case PopupStyle.WIN:
                bimg.sprite = winStyle;
                break;
            case PopupStyle.LOSE:
                bimg.sprite = loseStyle;
                break;
            case PopupStyle.SIMPLE:
                bimg.sprite = simpleStyle;
                break;
        }
    }
}
