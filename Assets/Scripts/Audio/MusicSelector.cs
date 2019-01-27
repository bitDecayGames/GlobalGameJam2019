using UnityEngine;

public class MusicSelector : MonoBehaviour
{
    public bool Enabled = true;
    public Songs.SongName Song;
    
    private void Awake()
    {
        if (Enabled)
        {
            FMODMusicPlayer.Instance.PlaySongIfNoneAreCurrentlyPlaying(Song);    
        }
    }
}