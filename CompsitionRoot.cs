using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rot Class for initialazing all classes (Controllers, Viewewrs) , and global variables
/// whitch will be used in the game
/// </summary>

public class CompsitionRoot : MonoBehaviour
{
    private GameFieldView gameFieldView;
    private PlayerView playerView, aiPlayerView;
    private PopUpView popupView;

    private GameBuildController gameBuilder;
    private AnimationController animationController;
    private PopupController poupController;
    private PlayrInfoController infoController;
    private GamePlayController gameplay;
    private GameRouleController gameRoule;

    private void Awake()
    {
        playerView = GameObject.Find("PlayerInfoView").GetComponent<PlayerView>();
        aiPlayerView = GameObject.Find("ComputerInfoView").GetComponent<PlayerView>();
        gameFieldView = GameObject.Find("CardContainer").GetComponent<GameFieldView>();
        popupView = GameObject.Find("PopUpView").GetComponent<PopUpView>();

        
        animationController = GameObject.Find("Animation").GetComponent<AnimationController>();
        infoController = new PlayrInfoController(playerView, aiPlayerView);
        poupController = new PopupController(popupView, animationController);
        gameRoule = new GameRouleController(poupController, infoController);
        gameplay = new GamePlayController(gameFieldView, infoController, animationController, gameRoule);

        gameBuilder = new GameBuildController(
            gameFieldView,
            infoController,
            animationController,
            poupController,
            gameplay);

    }

    public Vector2 SwitchToRectTransform(RectTransform _from)
    {
        Vector2 result = new Vector2(0, 0);
        Vector2 r = GameObject.Find("CardContainer").GetComponent<RectTransform>().rect.size;
        Vector2 screenPos = r / 2;
        var max = _from.anchorMax;
        if(max == Vector2.zero)
        {
            result = new Vector2(
                0 - (screenPos.x - _from.anchoredPosition.x),
                0- (screenPos.y - _from.anchoredPosition.y));
        }
        if (max == Vector2.one)
        {
            result = new Vector2(
                0 + (screenPos.x + _from.anchoredPosition.x),
                0 + (screenPos.y + _from.anchoredPosition.y));
        }

        return result;
    }


    // Start is called before the first frame update
    void Start()
    {
        GlobalVariables.playerCardHoldPosition = SwitchToRectTransform(
            GameObject.Find("PlayerCardHolder").GetComponent<RectTransform>());

        GlobalVariables.aiCardHoldPosition = SwitchToRectTransform(
            GameObject.Find("AICardHolder").GetComponent<RectTransform>());
        GlobalVariables.playerCardFlippPosition = 
            GameObject.Find("PlayerCardFlip").GetComponent<RectTransform>().anchoredPosition;
        GlobalVariables.aiCardFlippPosition = 
            GameObject.Find("AiCardFlip").GetComponent<RectTransform>().anchoredPosition;


        gameBuilder.Init();

    }

}
