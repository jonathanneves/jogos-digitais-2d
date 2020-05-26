using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script {
    public class ButtonFX : MonoBehaviour
    {
        private AudioSource myFx;
        public AudioClip hoverFx;
        public AudioClip clickFx;

        void Awake(){
            myFx = this.GetComponent<AudioSource>();
        }

        public void HoverSound(){
            myFx.PlayOneShot(hoverFx);
        }

        public void ClickSound() {
            myFx.PlayOneShot(clickFx);
        }
    }
}