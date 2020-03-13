    using UnityEngine;
    using System.Collections;
     
    public class GyroCamera : MonoBehaviour {
     
        Quaternion initialRotation;
        Quaternion gyroInitialRotation;
        bool gyroEnabled;
     
        void Start () {
            initialRotation = transform.rotation;
            Input.gyro.enabled = true;
            gyroInitialRotation = Input.gyro.attitude;
        }
     
        void Update() {
            if(gyroEnabled){
            #if !UNITY_EDITOR
                Quaternion offsetRotation = ConvertRotation(Quaternion.Inverse(gyroInitialRotation) * Input.gyro.attitude);
                transform.rotation = initialRotation * offsetRotation;
            #else
                //for unity editor contorl
                float speed = 2.0f;
                transform.Rotate(Input.GetAxis("Mouse Y") * speed, Input.GetAxis("Mouse X") * speed, 0);
            #endif
            }
        }
     
        public void AlignGyro() {
            gyroEnabled = false;
            transform.rotation = Quaternion.identity;
        }
     
        public void StartGyro() {
            initialRotation = transform.rotation;
            gyroInitialRotation = Input.gyro.attitude;
            gyroEnabled = true;
        }
     
        private static Quaternion ConvertRotation(Quaternion q)
        {
            return new Quaternion(q.x, q.y, -q.z, -q.w);  
        }
    }
