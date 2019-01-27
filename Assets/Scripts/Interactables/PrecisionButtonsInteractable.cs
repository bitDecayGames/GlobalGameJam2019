using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using GameInput;
using Interactables;
using Interactables.Progress;
using UnityEngine;

public class PrecisionButtonsInteractable : AbstractInteractable
{
    public GameObject ProgressBarPrefab;
    private GameObject _progressBarGameObject;
    private ProgressBarController _progressBarController;
    public Sprite AButton;
    public Sprite XButton;
    public Sprite YButton;
    public Sprite BButton;

    protected bool _hasSkippedFirstFrame;
    private const int SuccessesRequired = 4;

    private enum _controllerButtons
    {
        A,
        X,
        Y,
        B
    }

    private Queue<_controllerButtons> _buttonsToPress;
    private _controllerButtons _currentQuicktimeButton;
    private GameObject _currentQuicktimeButtonSpriteGameObject;
    private SpriteRenderer _currentQuicktimeButtonSpriteRenderer;
    protected bool _isUnlocked;

    private static float DefaultCooldown = 1f;
    private float _cooldown;

    private void OnGUI()
    {
        if (!_isInteracting)
        {
            return;
        }
        
        StringBuilder currentQueue = new StringBuilder();

        foreach (_controllerButtons button in _buttonsToPress)
        {
            currentQueue.Append(string.Format("{0} ", button));
        }
        
        GUI.Label(new Rect(0, 0, 1000, 1000), string.Format("Queue size: {0}", _buttonsToPress.Count));   
        GUI.Label(new Rect(0, 20, 1000, 1000), string.Format("Current dequeued button: {0}", _currentQuicktimeButton));   
        GUI.Label(new Rect(0, 40, 1000, 1000), string.Format("Remaining queue contents: {0}", currentQueue));   
        GUI.Label(new Rect(0, 60, 1000, 1000), string.Format("Is interacting: {0}", _isInteracting.ToString()));   
        GUI.Label(new Rect(0, 80, 1000, 1000), string.Format("Is unlocked: {0}", _isUnlocked.ToString()));   
    }

    private void Awake()
    {
        _currentQuicktimeButtonSpriteGameObject = new GameObject();
        _currentQuicktimeButtonSpriteRenderer = _currentQuicktimeButtonSpriteGameObject.AddComponent<SpriteRenderer>();
        _currentQuicktimeButtonSpriteRenderer.sortingOrder = 10000;
        _currentQuicktimeButtonSpriteRenderer.enabled = false;

        _progressBarGameObject = Instantiate(ProgressBarPrefab);
        _progressBarController = _progressBarGameObject.GetComponent<ProgressBarController>();
        if (_progressBarController == null) throw new Exception("Could not find progress bar controller");
        _progressBarController.SetSuccessesRequired(SuccessesRequired);
    }
    
    protected void Update() {
        
        if (_cooldown > 0)
        {
            _cooldown -= Time.deltaTime;
        }

        if (!_isUnlocked && _isInteracting)
        {
            _currentQuicktimeButtonSpriteRenderer.enabled = true;
        }
        else if (!_isUnlocked && !_isInteracting)
        {
            _currentQuicktimeButtonSpriteRenderer.enabled = false;
            _progressBarController.Disable();
        }
        
        if (_isInteracting)
        {
            // Give the controller a one frame buffer to avoid the initial interact button from failing the door
            if (!_hasSkippedFirstFrame)
            {
                _hasSkippedFirstFrame = true;
                return;
            }
            
            if (IsCorrectButtonPressed())
            {
                _progressBarController.IncrementSuccesses();
                if (_buttonsToPress.Count > 0)
                {
                    FMODSoundEffectsPlayer.Instance.PlaySoundEffect(SFX.ButtonSuccess);
                    SetupNextQuicktimeButton();
                }
                else
                {
                    Trigger();
                    Disconnect();
                }
            }
            else if (IsIncorrectButtonPressed())
            {
                FMODSoundEffectsPlayer.Instance.PlaySoundEffect(SFX.ButtonFailure);
                Disconnect();
            }
        }
    }

    private void SetupQuicktimeQueue()
    {
        _buttonsToPress = new Queue<_controllerButtons>();
        System.Random random = new System.Random();
        int previousButtonIndex = -1;
        for (int i = 0; i < SuccessesRequired; i++)
        {
            int buttonIndex;
            do
            {
                buttonIndex = random.Next(0, 4);
            } while (buttonIndex == previousButtonIndex);

            previousButtonIndex = buttonIndex;
            _buttonsToPress.Enqueue(ConvertNumberToEnumValue(buttonIndex));
        }
        
        if (_buttonsToPress.Count > 0)
        {
            SetupNextQuicktimeButton();
        }
        else
        {
            throw new Exception("The queue is empty before anything started");
        }
    }

    private void SetupNextQuicktimeButton()
    {
        _currentQuicktimeButton = _buttonsToPress.Dequeue();
        UpdateDisplayedSprite();
    }

    private _controllerButtons ConvertNumberToEnumValue(int number)
    {
        switch (number)
        {
            case 0:
                return _controllerButtons.A;
            case 1:
                return _controllerButtons.X;
            case 2:
                return _controllerButtons.Y;
            case 3:
                return _controllerButtons.B;
            default:
                throw new Exception("Unable to resolve provided value to an enum: " + number);
        }
    }

    public override void Interact(InputController interactee) {
        if (!_isInteracting && !_isUnlocked) {
            _isInteracting = true;
            _interactee = interactee;
            
            _currentQuicktimeButtonSpriteGameObject.transform.position = _interactee.transform.position + Vector3.up * .25f;
            _progressBarController.Activate(_interactee.transform.position);
            SetupQuicktimeQueue();
        }
    }

    public override void Disconnect() {
        if (_isInteracting) {
            _isInteracting = false;
            _interactee = null;
            _hasSkippedFirstFrame = false;
            _progressBarController.Reset();

            _cooldown = DefaultCooldown;
            
            SetupQuicktimeQueue();
        }
    }

    public override void Trigger()
    {
        _isUnlocked = true;
        _progressBarController.Complete();
        _currentQuicktimeButtonSpriteRenderer.enabled = false;
    }

    protected bool IsCorrectButtonPressed() {
        
        switch (_currentQuicktimeButton)
        {
            case _controllerButtons.A:
                return _interactee.ControllerMapper.APressed();
            case _controllerButtons.X:
                return _interactee.ControllerMapper.XPressed();
            case _controllerButtons.Y:
                return _interactee.ControllerMapper.YPressed();
            case _controllerButtons.B:
                return _interactee.ControllerMapper.BPressed();
            default:
                throw new Exception("Unable to resolve privded enum: " + _currentQuicktimeButton);
        }
    }
    
    protected bool IsIncorrectButtonPressed() {
        switch (_currentQuicktimeButton)
        {
            case _controllerButtons.A:
                return _interactee.ControllerMapper.XPressed() || _interactee.ControllerMapper.YPressed() ||
                        _interactee.ControllerMapper.BPressed();
            case _controllerButtons.X:
                return _interactee.ControllerMapper.APressed() || _interactee.ControllerMapper.YPressed() ||
                        _interactee.ControllerMapper.BPressed();
            case _controllerButtons.Y:
                return _interactee.ControllerMapper.XPressed() || _interactee.ControllerMapper.APressed() ||
                        _interactee.ControllerMapper.BPressed();
            case _controllerButtons.B:
                return _interactee.ControllerMapper.XPressed() || _interactee.ControllerMapper.YPressed() ||
                        _interactee.ControllerMapper.APressed();
            default:
                throw new Exception("Unable to resolve privded enum: " + _currentQuicktimeButton);
        }
    }

    private void UpdateDisplayedSprite()
    {
        switch (_currentQuicktimeButton)
        {
            case _controllerButtons.A:
                _currentQuicktimeButtonSpriteRenderer.sprite = AButton;
                break;
            case _controllerButtons.X:
                _currentQuicktimeButtonSpriteRenderer.sprite = XButton;
                break;
            case _controllerButtons.Y:
                _currentQuicktimeButtonSpriteRenderer.sprite = YButton;
                break;
            case _controllerButtons.B:
                _currentQuicktimeButtonSpriteRenderer.sprite = BButton;
                break;
            default:
                throw new Exception("Unable to resolve privded enum: " + _currentQuicktimeButton);
        }
    }
}