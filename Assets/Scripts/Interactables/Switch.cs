using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Interactables
{
    public class Switch : PressAndHoldInteractable, IObjective
    {
        private bool _isOn;
        public bool IsOn
        {
            get { return _isOn; }
        }

        private Rigidbody2D body;
        private SpriteRenderer sprite;
        private Blinker blinker;
        public Sprite offSprite;
        private Sprite onSprite;


        void Start()
        {
            body = GetComponentInChildren<Rigidbody2D>();
            if (!body) throw new Exception("RigidBody2D is missing on this object");

            sprite = GetComponentInChildren<SpriteRenderer>();
            if (!sprite) throw new Exception("Sprite is missing on this object");

            onSprite = sprite.sprite;
            this.On();
            GameStateManager.getLocalReference().Register(this);
        }

        public void On()
        {
            if (!_isOn)
            {
                sprite.sprite = onSprite;
                body.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
                _isOn = true;
            }
        }

        public void Off()
        {
            if (_isOn)
            {
                sprite.sprite = offSprite;
                body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                _isOn = false;
            }
        }
        
        public override void Trigger()
        {
            if (_isOn) Off();
            else On();
            OnTrigger();
        }

        protected override void OnInteract()
        {
            blinker = gameObject.AddComponent<Blinker>();
        }

        protected override void OnDisconnect()
        {
            if (blinker) Destroy(blinker);
        }

        protected override void OnTrigger()
        {
            base.OnTrigger();
            if (blinker) Destroy(blinker);
        }

        public bool isComplete()
        {
            return !IsOn;
        }

    }
}