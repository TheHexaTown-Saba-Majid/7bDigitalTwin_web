using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarRating : MonoBehaviour
{
    public Button[] starButtons; // Array of star buttons
    public TextMeshProUGUI ratingText; // Text element to display the rating

    private int ratedApp; // Current rating

    void Start()
    {
        // Initialize the star buttons
        for (int i = 0; i < starButtons.Length; i++)
        {
            int rating = i + 1;
            starButtons[i].onClick.AddListener(() => RateApplication(rating));
        }
        UpdateRatingText();
    }

    public void RateApplication(int rate)
    {
        ratedApp = rate;

        // Update the star buttons based on the rating
        for (int i = 0; i < starButtons.Length; i++)
        {
            bool isActive = i < rate;
            foreach (Transform t in starButtons[i].transform)
            {
                t.gameObject.SetActive(isActive);
            }
        }

        // Update the rating text
        UpdateRatingText();
    }

    private void UpdateRatingText()
    {
        ratingText.text = "Rating: " + ratedApp + "/" + starButtons.Length + " Stars";
    }
}
