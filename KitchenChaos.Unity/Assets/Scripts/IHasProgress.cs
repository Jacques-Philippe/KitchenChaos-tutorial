using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenChaosTutorial
{

    public interface IHasProgress
    {
        /// <summary>
        /// Event fired for progress changed
        /// </summary>
        public event EventHandler<ProgressChangedEventArgs> OnProgressChanged;
        public class ProgressChangedEventArgs : EventArgs
        {
            /// <summary>
            /// The value of the new progress, should be between 0 and 1
            /// </summary>
            public float normalizedProgress;
        }
    }

}