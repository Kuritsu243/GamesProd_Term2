
using System;
using UnityEngine;

namespace Environment.ZipLine
{
    public class ZipLineChild : MonoBehaviour
    {
        [SerializeField] private bool isStart;
        [SerializeField] private bool isEnd;
        [SerializeField] private Transform zipLineDraw;
        
        public bool IsStart
        {
            get => isStart;
            set => isStart = value;
        }

        public Transform ZipLineDraw
        {
            get => zipLineDraw;
            set => zipLineDraw = value;
        }

        public bool IsEnd
        {
            get => isEnd;
            set => isStart = value;
        }

        private void Start()
        {
            if (!isEnd) return;
            GetComponent<Collider>().enabled = false;
            
        }
    }
}
