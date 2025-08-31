using UnityEngine;
using UnityEngine.UI;

public class PointsDisplay : MonoBehaviour
{
    private int bodovi = 0;
    private int maxBodovi = 99;
    public Text bodoviText;
    public Text congratsText;

    void Start()
    {
        congratsText.gameObject.SetActive(false);
    }

    void Update()
    {
        bodoviText.text = "Points: " + bodovi;
    }

    public void AddPoints(int amount)
    {
        bodovi += amount;

        if (bodovi > maxBodovi)
        {
            congratsText.gameObject.SetActive(true);
            congratsText.text = "CONGRATULATIONS!";
            congratsText.fontSize = 100;
            congratsText.alignment = TextAnchor.MiddleCenter;
            
            AudioManagerGame.PlaySFX(AudioManagerGame.instance.gotovoUpecaneSveRibe);
        }
    }
}
