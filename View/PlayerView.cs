using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Playr Info panel that show ass Info about player name, and how meny card do the player have
/// also contains list of cards for the each of the player
/// </summary>
public class PlayerView : MonoBehaviour
{
    [SerializeField] Text PlayerName;
    [SerializeField] Text CardCount;
    PlayerModel model;
    List<CardView> playerCards;

    #region Getters/ Setters
    public List<CardView> GetCards => playerCards;
    public PlayerModel GetModel => model;

    public void SetModel(PlayerModel _model)
    {
        model = _model;
        PlayerName.text = "Name : " + model.name;
        CardCount.text = "Card Count: " + model.cardCount.ToString();
        playerCards = new List<CardView>();

    }
    #endregion
    #region Helpers
    /// <summary>
    /// Add the card to player List and increase the amount of playe's cards
    /// </summary>
    /// <param name="_card">Thius card will br added into the player's card list</param>
    public void AddCardToPlayer(CardView _card)
    {
        playerCards.Add(_card);
        model.cardCount++;
        CardCount.text = "Card Count: " + model.cardCount;
    }
    /// <summary>
    /// Take the first card from the player' card list, remove that from list 
    /// and return this card 
    /// </summary>
    /// <returns></returns>
    public CardView PopCard()
    {
        model.cardCount--;
        CardCount.text = "Card Count: " + model.cardCount;
        CardView card = playerCards.PopAt(0);
        card.transform.SetAsFirstSibling();
        return card;
    }
    #endregion
}
