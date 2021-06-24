using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextBoxUI
{
    public class CloseTextBox : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Invoke("FadeOut", 3);
        }
        void OnEnable()
        {
            Invoke("FadeOut", 3);
        }

        // Update is called once per frame
        public void FadeOut()
        {
            gameObject.SetActive(false);
        }
    }
}