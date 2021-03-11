using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


/*This class controls the HUD used in Concept 8. It changes the colour from
 * red to green, text from unsafe to safe and the icon. 
 - Created: 10/3/2021*/

public class HUDController : MonoBehaviour
{

    public Color newColor;
    public Sprite newSprite;
    public string newText;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            print("Lubbu");
            Image img = GameObject.Find("Panel").GetComponent<Image>();
            img.color = newColor;

            Image currSprite = GameObject.Find("Image").GetComponent<Image>();
            currSprite.sprite = newSprite;

            Text currText = GameObject.Find("Text").GetComponent<Text>();
            currText.fontSize = 14;
            currText.horizontalOverflow = HorizontalWrapMode.Wrap;
            currText.verticalOverflow = VerticalWrapMode.Overflow;
            currText.text = newText;
            currText.text = "SAFE TO\nCROSS\n";
            //currText.text = "CAREFUL\nA VEHICLE IS\nAPPROACHING!";
        }
    }
}
