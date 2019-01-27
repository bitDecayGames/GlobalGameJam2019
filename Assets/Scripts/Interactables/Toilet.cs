using Interactables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Toilet : PressAndHoldInteractable, IObjective
{


    private bool _isFull;
    public bool IsFull
    {
        get { return _isFull; }
    }

    private Collider2D body;
    private SpriteRenderer sprite;
    private Blinker blinker;
    public Sprite offSprite;
    private Sprite onSprite;


    void Start()
    {
        body = GetComponentInChildren<Collider2D>();
        if (!body) throw new Exception("Collider2D is missing on this object");

        sprite = GetComponentInChildren<SpriteRenderer>();
        if (!sprite) throw new Exception("Sprite is missing on this object");

        onSprite = sprite.sprite;
        Poop();
        GameStateManager.getLocalReference().Register(this);
    }

    public void Poop()
    {
        if (!_isFull)
        {
            sprite.sprite = onSprite;
            _isFull = true;
        }
    }

    public void Flush()
    {
        if (_isFull)
        {
            sprite.sprite = offSprite;
            _isFull = false;
        }
    }

    public override void Trigger()
    {
        if (_isFull) Flush();
        else Poop();
        OnTrigger();
    }

    protected override void OnInteract()
    {
        base.OnInteract();
        blinker = gameObject.AddComponent<Blinker>();
    }

    protected override void OnDisconnect()
    {
        base.OnDisconnect();
        if (blinker) Destroy(blinker);
    }

    protected override void OnTrigger()
    {
        base.OnTrigger();
        if (blinker) Destroy(blinker);
    }

    public bool isComplete()
    {
        return !IsFull;
    }

}
