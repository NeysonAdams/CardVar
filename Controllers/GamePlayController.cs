using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This class discribe all GamePlay Scenarios, Like Flipping the cards from both (Player / Computer).
/// and fallowed by roules of the game
/// </summary>
public class GamePlayController
{
    private GameFieldView view;
    private PlayrInfoController infoController;
    private AnimationController animator;
    private GameRouleController roule;

    private CardView playCard , aiCard;
    private List<CardView> roundCards = new List<CardView>();

    public GamePlayController(
        GameFieldView _view,
        PlayrInfoController _infoController,
        AnimationController _animator,
        GameRouleController _roule
        )
    {
        view = _view;
        infoController = _infoController;
        animator = _animator;
        roule = _roule;
    }
    /*
     * Initialization point For GamePlayCntroller
     * */
    public void Init()
    {
        view.flipEvent += delegate { OnFlipClickHandler(); };
        view.musikEvent +=  delegate { OnMusikButtonClickHandler(); };
        view.soundEvent += delegate { OnSoundButtonClickHandler(); };
        view.reloadEvent += delegate { OnReload(); };
    }
    #region Flip Button Click Handler
    /*
     * This Method Will be colled when you will press "Flip" Button
     * */
    private void OnFlipClickHandler()
    {
        if (!GlobalVariables.is_on) return;
        GlobalVariables.is_on = false;

        playCard = infoController.PopCardFrom(PlayerType.PLAYER);
        aiCard = infoController.PopCardFrom(PlayerType.COMPUTER);
        for(int i =0; i< roundCards.Count; i++)
        {
            roundCards[i].transform.SetAsFirstSibling();
        }
        roundCards.Add(playCard);
        roundCards.Add(aiCard);
        
        playCard.MoveTo(GlobalVariables.playerCardFlippPosition)
            .Show((action_) =>
            {
                animator.AnimateWithMiddleAction(playCard, 0, action_);
            });
        aiCard.MoveTo(GlobalVariables.aiCardFlippPosition)
            .Show((action_) =>
            {
                animator.AnimateWithMiddleAction(aiCard, 0, action_,
                    ()=> {
                        CardModel pmod = playCard.GetModel as CardModel;
                        CardModel amod = aiCard.GetModel as CardModel;

                        RaundResult(roule.Compare(pmod.type, amod.type));
                    });
                
                
            });

    }
    /*
     * Discription of the scenapio when Card is on the bord and copered
     * (What we need to if player / ai Win or draw)
     * */
    private void RaundResult(RoundStatus _status)
    {
        if(_status == RoundStatus.DRAW)
        {
            GlobalVariables.is_on = !roule.CheckGameIsFinished();
            return;
        }

        Vector2 pos = (_status == RoundStatus.WIN) ? GlobalVariables.playerCardHoldPosition :
            GlobalVariables.aiCardHoldPosition;
        PlayerType _playeType = (_status == RoundStatus.WIN) ? PlayerType.PLAYER : PlayerType.COMPUTER;
        int i = 0, len = roundCards.Count;
        for (i = len-1; i >=0 ; i--)
        {
            var card = roundCards.PopAt(i);
            card.MoveTo(pos).
                Hide((action_) =>
                {
                    var _card = card;
                    animator.AnimateWithMiddleAction(card, 2 + i * 0.03f, action_,
                        ()=>{
                            
                            infoController.AddCardTo(_playeType, _card);
                            SibilingCards(infoController.GetPlayerCards(_playeType), pos);
                            GlobalVariables.is_on = !roule.CheckGameIsFinished();

                            
                        });
                });

        }

    }
    #endregion

    #region GamePlay Helpers
    /// <summary>
    /// Random sorting for the list of GameCards
    /// </summary>
    /// <param name="_cards"> List whicth we ned to sort</param>
    /// <returns></returns>
    public List<CardView> Touse(List<CardView> _cards)
    {
        System.Random rng = new System.Random();
        int n = _cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = _cards[k];
            _cards[k] = _cards[n];
            _cards[n] = value;
        }

        SibilingCards(_cards, Vector2.zero);

        return _cards;
    }
    /// <summary>
    /// Set List of card by Sibling Index Position
    /// (depth)
    /// </summary>
    /// <param name="_cards"> List that we need to set by Sibling Index Position</param>
    public void SibilingCards(List<CardView> _cards, 
        Vector2 frompos)
    {

        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].transform.SetSiblingIndex(i);
            Vector2 currentPossition = _cards[0].transform.position;
            _cards[i].transform.position = currentPossition+ new Vector2(i * -0.001f, i * 0.001f);
        }
    }
    #endregion

    #region Other Buttons  Click  Handlers
    private void OnMusikButtonClickHandler()
    {

    }

    private void OnSoundButtonClickHandler()
    {

    }

    private void OnReload()
    {
        SceneManager.LoadScene(0);
    }
    #endregion
}
