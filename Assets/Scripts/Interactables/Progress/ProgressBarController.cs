using System.Collections;
using UnityEngine;

namespace Interactables.Progress
{
    public class ProgressBarController : MonoBehaviour
    {
        public Sprite ProgerssBarSprite;
        public Sprite ProgerssBarSpriteSuccess;

        public float _successesRequired;
        private float _successes;
        private bool _complete;
        
        private GameObject _progressBarGameObject;
        private SpriteRenderer _progressBarSpriteRenderer;
        
        private void Start()
        {
            _progressBarGameObject = new GameObject();
            _progressBarGameObject.name = "Unused Progress Bar";
            _progressBarSpriteRenderer = _progressBarGameObject.AddComponent<SpriteRenderer>();
            _progressBarSpriteRenderer.sprite = ProgerssBarSprite;
            _progressBarSpriteRenderer.sortingOrder = 10000;
            _progressBarSpriteRenderer.enabled = false;
        }

        private void Update()
        {
            if (!_complete)
            {
                _progressBarSpriteRenderer.transform.localScale =
                    new Vector3(_successes / _successesRequired,
                        _progressBarSpriteRenderer.transform.localScale.y);
            }
        }

        private void OnGUI()
        {
            if (_progressBarSpriteRenderer.enabled)
            {
                GUI.Label(new Rect(0, 160, 1000, 1000), string.Format("Successes: {0}, Required: {1}", _successes, _successesRequired));
            }
        }

        public void Reset()
        {
            _successes = 0;
            _progressBarGameObject.name = "Inactive Progress Bar";
        }
        
        public void Complete()
        {     
            _complete = true;
            Reset();
            FMODSoundEffectsPlayer.Instance.PlaySoundEffect(SFX.ButtonComplete);
            _progressBarSpriteRenderer.transform.localScale = new Vector3(1, _progressBarSpriteRenderer.transform.localScale.y);
            _progressBarSpriteRenderer.sprite = ProgerssBarSpriteSuccess;
            StartCoroutine(DisableSuccessProgressBar());
        }

        private IEnumerator DisableSuccessProgressBar()
        {
            yield return new WaitForSeconds(1f);
            _progressBarSpriteRenderer.enabled = false;
            _progressBarSpriteRenderer.sprite = ProgerssBarSprite;
            _progressBarSpriteRenderer.transform.localScale = new Vector3(0, _progressBarSpriteRenderer.transform.localScale.y);
        }

        public void Activate(Vector3 position)
        {
            _complete = false;
            _progressBarGameObject.name = "Active Progress Bar";
            _progressBarGameObject.transform.position = position + new Vector3(-ProgerssBarSprite.bounds.extents.x, .5f, 0);
            _progressBarSpriteRenderer.enabled = true;
        }
        
        public void Disable()
        {
            _progressBarSpriteRenderer.enabled = false;
        }

        public void IncrementSuccesses()
        {
            _successes++;
        }
        
        public void SetSuccessesRequired(float successesRequired)
        {
            _successesRequired = successesRequired;
        }
    }
}