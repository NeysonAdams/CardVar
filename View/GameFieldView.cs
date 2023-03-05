using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// this Class contain all gamePlay objects like Cards gameplay Buttons, instatntiate them,
/// and set Click delegate on the button
/// </summary>
public class GameFieldView : MonoBehaviour
{
    
    [SerializeField] private GameObject prefab;
    [Header("Buttons")]
    [SerializeField] private Button flipButton;
    [SerializeField] private Button reloadButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button musikButton;
    [Header("Sprites")]
    [SerializeField] private List<Sprite> cardImagesList;
    [SerializeField] private Sprite cardBack;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite musikOff;
    [SerializeField] private Sprite musikOn;

    public event Action flipEvent;
    public event Action soundEvent;
    public event Action musikEvent;
    public event Action reloadEvent;



    public void Init(out List<CardView> _cards)
    {
        flipButton.onClick.AddListener(() => { flipEvent.Invoke(); });
        reloadButton.onClick.AddListener(() => { reloadEvent.Invoke(); });
        soundButton.onClick.AddListener(() => { soundEvent.Invoke(); });
        musikButton.onClick.AddListener(() => { musikEvent.Invoke(); });


        _cards = new List<CardView>();
        for (int i = 0; i < cardImagesList.Count; i++)
        {
            CardModel model = new CardModel();
            model.id = i;
            model.type = (CardType)(i - (Mathf.RoundToInt(i/13))*13);
            var card = Instantiate(prefab.transform, transform);
            var cv = card.GetComponent<CardView>();
            cv.SetModel(model);
            cv.Setface = cardImagesList[i];
            _cards.Add(cv);
        }
    }
    
}
