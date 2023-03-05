using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerType
{
    PLAYER,
    COMPUTER
}
public class PlayrInfoController
{
    private PlayerView playerView, aiPlayerView;
    public PlayrInfoController(
        PlayerView _playerView,
        PlayerView _aiPlayerView)
    {
        playerView = _playerView;
        aiPlayerView = _aiPlayerView;
    }

    public void SetPlayers()
    {
        PlayerModel player = new PlayerModel();
        player.name = "Player";
        player.cardCount = 0;

        PlayerModel ai = new PlayerModel();
        ai.name = "Android";
        ai.cardCount = 0;

        playerView.SetModel(player);
        aiPlayerView.SetModel(ai);
    }
    public PlayerModel GetModel(PlayerType _type)
    {
        return (_type == PlayerType.PLAYER) ? playerView.GetModel : aiPlayerView.GetModel;
    }
    public List<CardView> GetPlayerCards(PlayerType _type)
    {
        return (_type == PlayerType.PLAYER) ? playerView.GetCards : aiPlayerView.GetCards;
    }

    public void AddCardTo(PlayerType _type, CardView _card)
    {
        switch (_type)
        {
            case PlayerType.PLAYER:
                playerView.AddCardToPlayer(_card);
                break;
            case PlayerType.COMPUTER:
                aiPlayerView.AddCardToPlayer(_card);
                break;
        }
    }
    
    public CardView PopCardFrom(PlayerType _type)
    {
        if (_type == PlayerType.PLAYER)
            return playerView.PopCard();
        return aiPlayerView.PopCard();
    }

}
