using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class Insatntiate all game objects (CardsView), create Tousing Animation 
/// and initialized GamePlay Controller 
/// </summary>
public class GameBuildController
{
    GameFieldView view;
    List<CardView> cards = new List<CardView>();
    AnimationController animation;
    private PlayrInfoController infoController;
    private PopupController popup;
    private GamePlayController gameplay;

    public GameBuildController(
        GameFieldView _view,
        PlayrInfoController _infoController,
        AnimationController _animation,
        PopupController _popup,
        GamePlayController _gameplay
        )
    {
        animation = _animation;
        view = _view;
        infoController = _infoController;
        popup = _popup;
        gameplay = _gameplay;
    }
    /// <summary>
    /// Initialization class
    /// </summary>
    public void Init()
    {
        view.Init(out cards);
        cards = gameplay.Touse(cards);
        infoController.SetPlayers();

        CardModel model = new CardModel();
        float delay = 0.03f;
        int j = 0;
        for (int i = cards.Count - 1; i >= 0; i--)
         {
            if (i%2  == 0)
            {
                cards[i].MoveTo(GlobalVariables.playerCardHoldPosition - new Vector2(i * 0.001f, i * -0.001f),
                    () => {
                        int index = i;
                        animation.Animate(cards[i], delay * j,
                        ()=> {
                            infoController.AddCardTo(PlayerType.PLAYER, cards[index]);
                            }); 
                    });
            }
            else
            {
                cards[i].MoveTo(GlobalVariables.aiCardHoldPosition - new Vector2(i*0.001f, i*-0.001f),
                    () => {
                        int index = i;
                        animation.Animate(cards[i], delay * j,
                        () => {
                            infoController.AddCardTo(PlayerType.COMPUTER, cards[index]);
                        }); 
                    });
            }
            j++;
        }
        popup.ShowPopup("LETS THE GAME", "BEGIN", 1.5f, 
            ()=>
            {
                gameplay.Init();
            });
    }

}
