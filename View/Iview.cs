using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Supper class for views whitch well be animated
/// </summary>
public class View : MonoBehaviour
{
    public virtual View MoveTo(Vector2 _point, Action _animator = null, float _delay = 0)
    {
        return this;
    }

    public virtual View Show (Action<Action> _animator = null)
    {
        return this;
    }

    public virtual View Hide(Action<Action> _animator = null)
    {
        return this;
    }

    public virtual Model GetModel
    {
        get
        {
            return null;
        }
    }
}
