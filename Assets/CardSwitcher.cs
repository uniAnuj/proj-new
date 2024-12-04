using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // Add DOTween namespace

public class CardSwitcher : MonoBehaviour
{
    public RectTransform card1;
    public RectTransform card2;
    public RectTransform card3;

    public Button dot1;
    public Button dot2;
    public Button dot3;

    private Vector2 leftPosition = new Vector2(-500, 0);
    private Vector2 centerPosition = new Vector2(0, 0);
    private Vector2 rightPosition = new Vector2(500, 0);

    private float animationDuration = 0.5f; // Duration of the tween

    void Start()
    {
        dot1.onClick.AddListener(() => SwitchToCard(1));
        dot2.onClick.AddListener(() => SwitchToCard(2));
        dot3.onClick.AddListener(() => SwitchToCard(3));
    }

    void SwitchToCard(int cardNumber)
    {
        switch (cardNumber)
        {
            case 1:
                card1.DOAnchorPos(centerPosition, animationDuration);
                card2.DOAnchorPos(rightPosition, animationDuration);
                card3.DOAnchorPos(leftPosition, animationDuration);
                HighlightDot(dot1);
                break;
            case 2:
                card1.DOAnchorPos(leftPosition, animationDuration);
                card2.DOAnchorPos(centerPosition, animationDuration);
                card3.DOAnchorPos(rightPosition, animationDuration);
                HighlightDot(dot2);
                break;
            case 3:
                card1.DOAnchorPos(rightPosition, animationDuration);
                card2.DOAnchorPos(leftPosition, animationDuration);
                card3.DOAnchorPos(centerPosition, animationDuration);
                HighlightDot(dot3);
                break;
        }
    }

    void HighlightDot(Button activeDot)
    {
        dot1.GetComponent<Image>().color = Color.white;
        dot2.GetComponent<Image>().color = Color.white;
        dot3.GetComponent<Image>().color = Color.white;

        activeDot.GetComponent<Image>().color = Color.blue;
    }
}
