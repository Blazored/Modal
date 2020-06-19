using System;
using System.Collections.Generic;
using System.Text;

namespace Blazored.Modal
{
    public class ModalAnimation
    {
        public ModalAnimation(ModalAnimationType type, double duration)
        {
            Type = type;
            Duration = duration;
        }

        public ModalAnimationType Type { get; set; }

        public double? Duration { get; set; }
    }

    public enum ModalAnimationType
    {
        None = 0, // Set to 0 so it is definitely the default value
        FadeIn,
        FadeOut,
        FadeInOut
    }
}
