using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace DeanCode
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class NotificationOnExit : UdonSharpBehaviour
    {
        [Header("Settings")]
        [SerializeField] private NotificationManager manager;
        [SerializeField] private string message;
        [SerializeField] private NotificationType type;
        [SerializeField] private float duration = 5.0f;
        [SerializeField] private AudioClip sound;

        [SerializeField] private float cooldown = 1.0f;
        private float lastTriggerTime = -100f;

        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (!player.isLocal) return;

            // Cooldown check
            if (Time.time - lastTriggerTime < cooldown) return;
            lastTriggerTime = Time.time;

            SendCustomEventDelayedFrames(nameof(TriggerNotification), 1);
        }

        public void TriggerNotification()
        {
            if (manager != null)
            {
                manager._SendNotification(message, type, sound, null, duration);
            }
        }
    }
}
