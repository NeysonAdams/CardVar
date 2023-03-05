using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PopupStyle
{
    SIMPLE,
    WIN,
    LOSE
}

/// <summary>
/// This Class Controol Popup
/// </summary>

public class PopupController
{
    PopUpView view;
    AnimationController animator;
    bool can_hide = true; // use this if you don't want to hide poup after 1.5 sec

    public PopupController(
        PopUpView _view,
        AnimationController _animator)
    {
        view = _view;
        animator = _animator;
        view.SetModel = new Model();
    }
    /// <summary>
    /// Open pop for a short time (1.5 sec), and hide it
    /// </summary>
    /// <param name="_upLine"> Upper Text label Text </param>
    /// <param name="_downLine"> Down Text label Text </param>
    /// <param name="_dellay"> Delay before popup will be shown</param>
    /// <param name="_afterPopupAction"> Delegete after popup will be hidden </param>
    public void ShowPopup(
        string _upLine, 
        string _downLine, 
        float _dellay = 0, 
        Action _afterPopupAction = null)
    {
        view.Init(_upLine, _downLine);
        
        view.Show((action) =>
        {
            animator.Animate(view, _dellay, () =>
            {
                if (can_hide)
                {
                    view.Hide((_action) => { animator.Animate(view, 1.5f); });
                    if (_afterPopupAction != null)
                        _afterPopupAction.Invoke();
                }
            });
        });
    }
    /// <summary>
    /// Show poup for Raund Status information (Win, Lose or Draw)
    /// </summary>
    /// <param name="_status"> Satus of Round (Win, Lose or Draw)</param>
    public void ShowPopup(RoundStatus _status)
    {
        string caption = _status.ToString();
        view.SetStatus(_status);
        this.ShowPopup(caption, "", 0.2f);
    }
    /// <summary>
    /// Show Poup whitch infrm about Game Result (Win Or Lose)
    /// </summary>
    /// <param name="_stat_style"></param>
    public void ShowPopup(PopupStyle _stat_style)
    {
        view.SetStyle(_stat_style);
        can_hide = false;
        this.ShowPopup("", " ", 0.2f);
    }


}
