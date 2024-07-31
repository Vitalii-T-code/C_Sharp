using System.Collections.Generic;
using UnityEngine;

namespace BLINK
{
    public class AnimationDemo : MonoBehaviour
    {
        public enum AnimationType
        {
            Trigger,
            Bool
        }

        [System.Serializable]
        public class AnimationEntry
        {
            public string animationName;
            public AnimationType type;
        }

        public List<AnimationEntry> entries = new List<AnimationEntry>();
        public List<Animator> animators = new List<Animator>();
        public int entryIndex;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                NextAnimation();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ReplayAnimation();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetAnimationByKey(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetAnimationByKey(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetAnimationByKey(2);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SetAnimationByKey(3);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SetAnimationByKey(4);
            }
        }

        private void SetAnimationByKey(int index)
        {
            if (index >= 0 && index < entries.Count)
            {
                entryIndex = index;
                PlayAnimation();
            }
        }

        public void NextAnimation()
        {
            entryIndex++;
            if (entries.Count - 1 < entryIndex) entryIndex = 0;
            PlayAnimation();
        }

        public void PreviousAnimation()
        {
            entryIndex--;
            if (entryIndex < 0) entryIndex = entries.Count - 1;
            PlayAnimation();
        }

        public void ReplayAnimation()
        {
            PlayAnimation();
        }

        private void ResetAllBool()
        {
            foreach (var entry in entries)
            {
                if (entry.type != AnimationType.Bool) continue;
                foreach (var animator in animators)
                {
                    animator.SetBool(entry.animationName, false);
                }
            }
        }

        private void PlayAnimation()
        {
            ResetAllBool();

            if (entries[entryIndex].type == AnimationType.Bool)
            {
                foreach (var animator in animators)
                {
                    animator.SetBool(entries[entryIndex].animationName, true);
                }
            }
            else
            {
                foreach (var animator in animators)
                {
                    animator.SetTrigger(entries[entryIndex].animationName);
                }
            }
        }
    }
}
