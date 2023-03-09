using System;
using System.Collections;
using System.Collections.Generic;
using Player.Input;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Environment.ZipLine
{
    public class ZipLineController : MonoBehaviour
    {
        [SerializeField] private float timeToTake;
    
        private ZipLineChild[] _zipLineChildren;
        private Vector3 _startPoint;
        private Vector3 _endPoint;
        private GameObject _player;
        private float _elapsedTime;
        public bool IsZiplining { get; set; }
        
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _zipLineChildren = GetComponentsInChildren<ZipLineChild>();
            foreach (var childObj in _zipLineChildren)
            {
                switch (childObj.IsStart)
                {
                    case true when !childObj.IsEnd:
                        _startPoint = childObj.gameObject.transform.position;
                        break;
                    case false when childObj.IsEnd:
                        _endPoint = childObj.gameObject.transform.position;
                        break;
                    case true when childObj.IsEnd:
                        Debug.LogError("This has glitched");
                        break;
                    case false when !childObj.IsEnd:
                        Debug.LogError("This has glitched");
                        break;
                }
            }
        }

        public void StartZipline()
        {
            if (IsZiplining) return;
            _elapsedTime = 0;
            StartCoroutine(RidingZipline());
        }

        private IEnumerator RidingZipline()
        {
            _elapsedTime = 0f;

            while (_elapsedTime < timeToTake)
            {
                var t = _elapsedTime / timeToTake;
                t = t * t * (3f - 2f * t);

                _player.transform.position = Vector3.Lerp(_startPoint, _endPoint, t);
                _elapsedTime += Time.deltaTime;
                yield return null;
            }

            _player.GetComponent<PlayerMovement>().IsZipLining = false;
            IsZiplining = false;


        }

        
    }
}
