using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : View
{
    [SerializeField] private Image image;
    [SerializeField] private CardModel model;

    private Sprite face;
    private Sprite back;

    private void Start()
    {
        back = image.sprite;
    }
    #region Getters / Setters
    public override Model GetModel
    {
        get
        {
            return model;
        }
    }

    public Sprite Setface
    {
        set
        {
            face = value;
        }
    }

    public void SetModel(Model _model)
    {
        this.model = _model as CardModel;
        transform.position = model.position;
        transform.rotation = model.rotation;
        transform.localScale = model.scale;

    }
    #endregion

    #region Card Action
    /// <summary>
    /// Flip card from Face to back
    /// </summary>
    /// <param name="_animator"> Animation controller delegate</param>
    /// <returns></returns>
    public override View Hide(Action<Action> _animator)
    {
        model.rotation = new Quaternion(0, 180, 0, 1);
        if (_animator != null)
            _animator.Invoke(() => { SwapFace(false); });
        return this;
    }
    /// <summary>
    /// Move cad to chousen position
    /// </summary>
    /// <param name="_point"> Chousen position</param>
    /// <param name="_animator">Animation controller delegate</param>
    /// <returns></returns>
    public View MoveTo(Vector2 _point,
        Action _animator = null)
    {
        this.model.position = _point;
        if(_animator!=null)
            _animator.Invoke();
        return this;
    }
    /// <summary>
    /// Flip card from back to face
    /// </summary>
    /// <param name="_animator">Animation controller delegate</param>
    /// <returns></returns>
    public override View Show(Action<Action> _animator = null)
    {
        transform.rotation = new Quaternion(0, 180, 0, 1);
        model.rotation = Quaternion.identity;
        if (_animator != null)
            _animator.Invoke(()=> { SwapFace(true); });
        return this;
    }
    /*
     * Swap card from back to face and oposite
     * */
    private void SwapFace(bool _flip)
    {
        Sprite sprite = _flip ? face : back;
        image.sprite = sprite;
    }

    #endregion
}
