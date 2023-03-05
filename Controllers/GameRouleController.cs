using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoundStatus
{
    NONE,
    WIN, 
    LOSE,
    DRAW
}
/// <summary>
/// Class describe roules of the game
/// </summary>
public class GameRouleController
{
    PopupController popup;
    PlayrInfoController infoController;

    public GameRouleController(PopupController _popup, PlayrInfoController _infoController)
    {
        popup = _popup;
        infoController = _infoController;
    }
    /// <summary>
    /// called every raund,  Compare types of the card, 
    /// and return status of the round 
    /// WIN - if player fliped bigger card
    /// LOSE - if ai flipped bigger card
    /// DRAW - if cards is equal
    /// </summary>
    /// <param name="_playCard"> player card</param>
    /// <param name="_aiCard"> ai card</param>
    /// <returns></returns>
    public RoundStatus Compare(CardType _playCard, CardType _aiCard)
    {
        RoundStatus status = RoundStatus.DRAW;
        if ((int)_playCard > (int)_aiCard)
            status =  RoundStatus.WIN;
        else if ((int)_playCard < (int)_aiCard)
            status = RoundStatus.LOSE;
        popup.ShowPopup(status);
        return status;
    }
    /// <summary>
    /// Check if any of players have no card any more
    /// return bool variable:
    /// True - if one of the player is empty
    /// False - if both of them have any card
    /// </summary>
    /// <returns></returns>
    public bool CheckGameIsFinished()
    {
        bool is_player_Lose = infoController.GetModel(PlayerType.PLAYER).cardCount == 0;
        bool is_ai_Lose = infoController.GetModel(PlayerType.COMPUTER).cardCount == 0;

        if(is_ai_Lose)
        {
            popup.ShowPopup(PopupStyle.WIN);
        }
        if(is_player_Lose)
        {
            popup.ShowPopup(PopupStyle.LOSE);
        }

        return is_ai_Lose || is_player_Lose;
    }
}
