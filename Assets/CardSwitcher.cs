using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TrainCardSwitcher : MonoBehaviour
{
    public RectTransform[] cards; // List of all cards
    public Button[] dots;         // Array of dots
    public RectTransform maskArea; // The visible area for the train

    private float cardWidth; // Width of a single card
    private float animationDuration = 0.5f; // Duration of the slide animation
    private int currentCardIndex = 0; // Tracks which card is in the center
    private float hoverScale = 1.1f; // Scale when hovered
    private float normalScale = 1f; // Normal scale
    private float hoverDuration = 0.3f; // Animation duration for scaling

    public AudioSource audioSource; // Reference to the Audio Source
    public AudioClip clickSound;    // The click sound effect

    void Start()
    {
        // Calculate the width of a card based on the mask area
        cardWidth = maskArea.rect.width;

        // Assign button listeners
        for (int i = 0; i < dots.Length; i++)
        {
            int index = i; // Local copy for closure
            dots[i].onClick.AddListener(() => OnDotClick(index));
        }

        // Align cards at start
        AlignCards();

        // Highlight the first dot to indicate the first card is active
        HighlightDot(0);

        // Add hover listeners to cards and dots
        foreach (var card in cards)
        {
            AddHoverEffect(card);
        }
        foreach (var dot in dots)
        {
            AddHoverEffect(dot.GetComponent<RectTransform>());
        }
    }

    void OnDotClick(int targetIndex)
    {
        PlayClickSound();
        SlideToCard(targetIndex);
    }

    void SlideToCard(int targetIndex)
    {
        if (targetIndex == currentCardIndex) return; // No need to slide if already selected

        // Calculate the slide distance based on the target index
        float slideDistance = (targetIndex - currentCardIndex) * -cardWidth;

        // Animate each card to slide by the calculated distance
        foreach (var card in cards)
        {
            card.DOAnchorPosX(card.anchoredPosition.x + slideDistance, animationDuration).SetEase(Ease.InOutCubic);
        }

        // Update the current index
        currentCardIndex = targetIndex;

        // Update the highlighted dot
        HighlightDot(targetIndex);
    }

    void AlignCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            // Position cards side by side
            cards[i].anchoredPosition = new Vector2(i * cardWidth, 0);
        }
    }

    void HighlightDot(int dotIndex)
    {
        foreach (var dot in dots)
        {
            dot.GetComponent<Image>().color = Color.white; // Reset all dots to white
        }

        dots[dotIndex].GetComponent<Image>().color = Color.blue; // Set the active dot to blue
    }

    void AddHoverEffect(RectTransform target)
    {
        EventTrigger trigger = target.gameObject.AddComponent<EventTrigger>();

        // Pointer Enter
        EventTrigger.Entry entryEnter = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        entryEnter.callback.AddListener((eventData) => ScaleUp(target));
        trigger.triggers.Add(entryEnter);

        // Pointer Exit
        EventTrigger.Entry entryExit = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        entryExit.callback.AddListener((eventData) => ScaleDown(target));
        trigger.triggers.Add(entryExit);
    }

    void ScaleUp(RectTransform target)
    {
        target.DOScale(hoverScale, hoverDuration).SetEase(Ease.OutCubic);
    }

    void ScaleDown(RectTransform target)
    {
        target.DOScale(normalScale, hoverDuration).SetEase(Ease.OutCubic);
    }

    void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
