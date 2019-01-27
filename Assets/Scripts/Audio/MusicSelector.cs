using UnityEngine;

public class MusicSelector : MonoBehaviour
{
    public bool Enabled = true;
    public bool SendParameterAtSceneStart;
    public FmodParameters.FmodParameterEnum Parameter;
    public float ParameterValue;
    public Songs.SongName Song;
    
    private void Awake()
    {
        if (Enabled)
        {
            FMODMusicPlayer.Instance.PlaySongIfNoneAreCurrentlyPlaying(Song);

            if (SendParameterAtSceneStart)
            {
                FMODMusicPlayer.Instance.SetParameter(Parameter.ToString(), 0);
            }
        }
    }
}