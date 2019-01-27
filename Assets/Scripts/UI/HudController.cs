using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudController : MonoBehaviour {

	

	public TextMeshProUGUI LightOffStatusLabel;
    public TextMeshProUGUI CopTimerText;

    public void SetLabel(int currentCompletion, int winThreshold)
    {
        LightOffStatusLabel.text = currentCompletion.ToString() +" / "+ winThreshold.ToString();
    }

    public void SetTimer(float seconds)
    {
        CopTimerText.text = seconds.ToString();
    }
}
