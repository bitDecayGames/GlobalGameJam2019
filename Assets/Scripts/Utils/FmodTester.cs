using UnityEngine;

public class FmodTester : MonoBehaviour
{
    public FmodParameters.FmodParameterEnum Parameter;
    public float ParameterValue;
    public bool SendParameterToActiveSong;
    

    private void Update()
    {
        if (SendParameterToActiveSong)
        {
            SendParameterToActiveSong = false;
            FMODMusicPlayer.Instance.SetParameter(Parameter.ToString(), ParameterValue);
        }
    }
}