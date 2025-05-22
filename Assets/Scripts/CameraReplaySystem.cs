using System;
using System.Collections.Generic;
using UnityEngine;

namespace CameraReplaySystem
{
    [Serializable]
    public class CameraRecord
    {
        public Vector3 position;
        public Quaternion rotation;
        public float timestamp;
    }

    [Serializable]
    public class CarRecord
    {
        public Vector3 position;
        public Quaternion rotation;
        public float timestamp;
    }

    [Serializable]
    public class DrivingRecording
    {
        public List<CameraRecord> cameraFrames = new List<CameraRecord>();
        public List<CarRecord> carFrames = new List<CarRecord>();
    }
}
