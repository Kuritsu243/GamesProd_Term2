using System;
using System.Collections;
using Cards;
using Player.Inventory;
using UnityEngine;

namespace Player.Buff
{
    public class PlayerBuff : MonoBehaviour
    {
        [Header("Damage Buff Settings")] 
        [SerializeField] private float damageBuffModifier;
        [SerializeField] private float damageBuffDuration;
        [Header("Jump Buff Settings")] 
        [SerializeField] private float jumpBuffModifier;
        [SerializeField] private float jumpBuffDuration;
        [Header("Dash Buff Settings")] 
        [SerializeField] private float dashBuffDuration;
        [SerializeField] private float dashCount;
        [SerializeField] private bool durationBased;
        [Header("Speed Buff Settings")] 
        [SerializeField] private float speedBuffModifier;
        [SerializeField] private float speedBuffDuration;

        private PlayerInventory _playerInventory;
        private float _buffRemaining;
        
        public float CurrentBuffDuration { get; set; }
        public float BuffRemaining => _buffRemaining;
        public float DamageBuffModifier => damageBuffModifier;

        public float JumpBuffModifier => jumpBuffModifier;
        public float SpeedBuffModifier => speedBuffModifier;
        public bool IsDamageBuffActive { get; set; }
        public bool IsJumpBuffActive { get; set; }
        public bool IsSpeedBuffActive { get; set; }
        public bool IsDashBuffActive { get; set; }

        public void CallStartBuff(cardObject activeCard)
        {
            if (IsBuffActive()) return;
            if (!_playerInventory.IsInBuffMode) return;
            switch (activeCard.buffType)
            {
                case cardObject.BuffType.Dash:
                    StartDashBuff();
                    break;
                case cardObject.BuffType.Damage:
                    StartDamageBuff();
                    break;
                case cardObject.BuffType.JumpHeight:
                    StartJumpBuff();
                    break;
                case cardObject.BuffType.Speed:
                    StartSpeedBuff();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Start()
        {
            _playerInventory = GetComponent<PlayerInventory>();
        }

        private bool IsBuffActive()
        {
            return IsDamageBuffActive || IsDashBuffActive || IsSpeedBuffActive || IsJumpBuffActive;
        }
        

        private void StartDamageBuff()
        {
            IsDamageBuffActive = true;
            _playerInventory.CanvasScript.ShowBuffDurationUI();
            StartCoroutine(BuffDuration(damageBuffDuration));
            if (!_playerInventory.IsInBuffMode) return;
            _playerInventory.ChangeActiveCardMode();
        }
        
        
        private void StartJumpBuff()
        {
            IsJumpBuffActive = true;
            _playerInventory.CanvasScript.ShowBuffDurationUI();
            StartCoroutine(BuffDuration(jumpBuffDuration));
            if (!_playerInventory.IsInBuffMode) return;
            _playerInventory.ChangeActiveCardMode();
        }

        private void StartDashBuff()
        {
            IsDashBuffActive = true;
            _playerInventory.CanvasScript.ShowBuffDurationUI();
            StartCoroutine(BuffDuration(dashBuffDuration));
            if (!_playerInventory.IsInBuffMode) return;
            _playerInventory.ChangeActiveCardMode();
        }

        private void StartSpeedBuff()
        {
            IsSpeedBuffActive = true;
            _playerInventory.CanvasScript.ShowBuffDurationUI();
            StartCoroutine(BuffDuration(speedBuffDuration));
            if (!_playerInventory.IsInBuffMode) return;
            _playerInventory.ChangeActiveCardMode();
            
        }



        private IEnumerator BuffDuration(float buffLength)
        {
            CurrentBuffDuration = buffLength;
            _buffRemaining = buffLength;
            while (_buffRemaining > 0.01f)
            {
                _buffRemaining -= Time.deltaTime;
                yield return null;
            }

            if (IsDamageBuffActive)
                IsDamageBuffActive = false;
            else if (IsJumpBuffActive)
                IsJumpBuffActive = false;
            else if (IsSpeedBuffActive)
                IsSpeedBuffActive = false;
            else if (IsDashBuffActive)
                IsDashBuffActive = false;
            
            _playerInventory.CanvasScript.HideBuffDurationUI();
        }

    }
}
