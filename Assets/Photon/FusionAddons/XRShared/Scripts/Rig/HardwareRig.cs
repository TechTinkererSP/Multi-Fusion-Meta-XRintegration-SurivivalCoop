using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Fusion.XR.Shared.Rig
{
    public enum RigPart
    {
        None,
        Headset,
        LeftController,
        RightController,
        Undefined
    }

    // Include all rig parameters in a structure
    public struct RigState
    {
        public Vector3 playAreaPosition;
        public Quaternion playAreaRotation;
        public Vector3 leftHandPosition;
        public Quaternion leftHandRotation;
        public Vector3 rightHandPosition;
        public Quaternion rightHandRotation;
        public Vector3 headsetPosition;
        public Quaternion headsetRotation;
        public HandCommand leftHandCommand;
        public HandCommand rightHandCommand;
    }

    /**
     * 
     * Hardware rig gives access to the various rig parts: head, left hand, right hand, and the play area, represented by the hardware rig itself
     *  
     * Can be moved, either instantanesously, or with a camera fade
     * 
     **/

    public class HardwareRig : MonoBehaviour
    {
        public HardwareHand leftHand;
        public HardwareHand rightHand;
        public HardwareHeadset headset;
        //public HardwareRig existingHardwareRig; // Reference to the existing HardwareRig object

        public UserSpawner2 userSpawner; // Reference to UserSpawner2

        // // Method to reposition the HardwareRig at the next spawn point
        // public void RepositionAtNextSpawnPoint()
        // {
        //     if (userSpawner != null)
        //     {
        //         Transform spawnPoint = userSpawner.GetNextSpawnPoint(); // Access the spawn point
        //         transform.position = spawnPoint.position;
        //         transform.rotation = spawnPoint.rotation;

        //         Debug.Log($"HardwareRig repositioned to spawn point at {spawnPoint.position}");
        //     }
        //     else
        //     {
        //         Debug.LogError("UserSpawner2 reference is not assigned!");
        //     }
        // }

        //  private void Start()
        //  {
        //      RepositionAtNextSpawnPoint();
        //  }


        private void Start()
        {
            if (userSpawner != null)
            {
                Transform spawnPoint = userSpawner.GetNextSpawnPoint();
                if (spawnPoint != null)
                {
                    transform.position = spawnPoint.position;
                    transform.rotation = spawnPoint.rotation;
                }
                else
                {
                    Debug.LogError("No valid spawn point found!");
                }
            }
            else
            {
                Debug.LogError("UserSpawner2 reference is not assigned!");
            }
        }



        // [Header("Spawn Points")]
        // [Tooltip("List of predefined spawn points for the rig.")]
        // public List<Transform> spawnPoints = new List<Transform>();

        // [SerializeField] private UserSpawner userSpawner;

        // public void SomeFunction()
        // {
        //     UserSpawner2 userSpawner2 = FindObjectOfType<UserSpawner2>();
        //     Transform spawnPoint = userSpawner2.GetNextSpawnPoint();
        //     //Transform spawnPoint = UserSpawner2.FindAnyObjectByType<UserSpawner2>().GetNextSpawnPoint();
        //     //Transform spawnPoint = userSpawner.GetNextSpawnPoint();
        //     //Debug.Log($"Spawn Point: {spawnPoint.position}");
        //     return;
        // }


        [Serializable]
        public class TeleportEvent : UnityEvent<Vector3, Vector3> { }
        public TeleportEvent onTeleport = new TeleportEvent();

        RigState _rigState = default;

        public virtual RigState RigState
        {


            get
            {
                // _rigState.playAreaPosition = FindAnyObjectByType<UserSpawner2>().GetNextSpawnPoint().position;
                // _rigState.playAreaRotation = FindAnyObjectByType<UserSpawner2>().GetNextSpawnPoint().transform.rotation;
                // _rigState.leftHandPosition = leftHand.transform.position;
                // _rigState.leftHandRotation = leftHand.transform.rotation;
                // _rigState.rightHandPosition = rightHand.transform.position;
                // _rigState.rightHandRotation = rightHand.transform.rotation;
                // _rigState.headsetPosition = headset.transform.position;
                // _rigState.headsetRotation = headset.transform.rotation;
                // _rigState.leftHandCommand = leftHand.handCommand;
                // _rigState.rightHandCommand = rightHand.handCommand;

                _rigState.playAreaPosition = transform.position;
                _rigState.playAreaRotation = transform.rotation;
                _rigState.leftHandPosition = leftHand.transform.position;
                _rigState.leftHandRotation = leftHand.transform.rotation;
                _rigState.rightHandPosition = rightHand.transform.position;
                _rigState.rightHandRotation = rightHand.transform.rotation;
                _rigState.headsetPosition = headset.transform.position;
                _rigState.headsetRotation = headset.transform.rotation;
                _rigState.leftHandCommand = leftHand.handCommand;
                _rigState.rightHandCommand = rightHand.handCommand;
                return _rigState;
            }
        }

        #region Locomotion
        // Update the hardware rig rotation. 
        public virtual void Rotate(float angle)
        {
            transform.RotateAround(headset.transform.position, transform.up, angle);
        }

        // Update the hardware rig position. 
        public virtual void Teleport(Vector3 position)
        {
            Vector3 headsetOffet = headset.transform.position - transform.position;
            headsetOffet.y = 0;
            Vector3 previousPosition = transform.position;
            transform.position = position - headsetOffet;
            if (onTeleport != null) onTeleport.Invoke(previousPosition, transform.position);
        }

        // Teleport the rig with a fader
        public virtual IEnumerator FadedTeleport(Vector3 position)
        {
            if (headset.fader) yield return headset.fader.FadeIn();
            Teleport(position);
            if (headset.fader) yield return headset.fader.WaitBlinkDuration();
            if (headset.fader) yield return headset.fader.FadeOut();
        }

        // Rotate the rig with a fader
        public virtual IEnumerator FadedRotate(float angle)
        {
            if (headset.fader) yield return headset.fader.FadeIn();
            Rotate(angle);
            if (headset.fader) yield return headset.fader.WaitBlinkDuration();
            if (headset.fader) yield return headset.fader.FadeOut();
        }


        /// <summary>
        /// Teleports the rig to a specific spawn point by index.
        /// </summary>
        /// <param name="index">Index of the spawn point in the list.</param>
        // public virtual void TeleportToSpawnPoint(int index)
        // {
        //     if (index < 0 || index >= spawnPoints.Count)
        //     {
        //         Debug.LogWarning($"Invalid spawn point index: {index}");
        //         return;
        //     }

        //     Transform spawnPoint = spawnPoints[index];
        //     Teleport(spawnPoint.position);
        //     transform.rotation = spawnPoint.rotation; // Align the rig's rotation to the spawn point's rotation
        // }



        #endregion







    }
}
