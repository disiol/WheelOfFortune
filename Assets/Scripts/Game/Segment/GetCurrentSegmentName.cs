using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Segment
{
    public class GetCurrentSegmentName : MonoBehaviour
    {
        public string segmentName;

   
        private void OnTriggerEnter2D(Collider2D col)
        {

            segmentName = col.name;
            Debug.Log("GetCurrentSegmentName segmentName " + segmentName);

        }
    }
}