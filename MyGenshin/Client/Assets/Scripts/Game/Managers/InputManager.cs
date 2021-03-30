using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public  class InputManager:MonoSingleton<InputManager>
    {
        public bool IsInput = true;

        public void Update()
        {

            if (Input.GetKey(KeyCode.LeftAlt))
            {
                IsInput = false;
            }
            else
            {
                IsInput = true;
            }
        }



        public float GetAxis(string parm)
        {
            if (IsInput)
            {
                return Input.GetAxis(parm);
            }
            return 0;
        }


        public bool GetButton(string parm)
        {
            if (IsInput)
            {
                return Input.GetButton(parm);
            }
            return false;
        }
        public bool GetButtonUp(string parm)
        {
            if (IsInput)
            {
                return Input.GetButtonUp(parm);
            }
            return false;
        }
        public bool GetButtonDown(string parm)
        {

            if (IsInput)
            {
                return Input.GetButtonDown(parm);
            }
            return false;
        }
    }
}
