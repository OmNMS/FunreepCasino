using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace  Accountscript
{
    public class ChangePasswordScreenData : MonoBehaviour
    {
        
    }

    [Serializable]
    public class ChangePasswordForm
    {
        public string user_id;
        public string password;
        public string device;

        public ChangePasswordForm(string user_id, string password, string device)
        {
            this.user_id = user_id;
            this.password = password;
            this.device = device;
        }
    }

    [Serializable]
    public class ChangePasswordResponce
    {
        public string message;
        public string status;
    }
}
