﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network;
using SkillBridge.Message;
using UnityEngine;
using Managers;

namespace Services
{
    class FriendService : Singleton<FriendService>, IDisposable
    {
        public FriendService()
        {
            MessageDistributer.Instance.Subscribe<FriendAddResponse>(this.OnFriendAddResponse);
            MessageDistributer.Instance.Subscribe<FriendRemoveResponse>(this.OnFriendRemove);
            MessageDistributer.Instance.Subscribe<FriendListResponse>(this.OnFriendList);
        }
        public void Dispose()
        {
            MessageDistributer.Instance.Unsubscribe<FriendAddResponse>(this.OnFriendAddResponse);
            MessageDistributer.Instance.Unsubscribe<FriendRemoveResponse>(this.OnFriendRemove);
            MessageDistributer.Instance.Unsubscribe<FriendListResponse>(this.OnFriendList);
        }
        public void SendFriendAddRequest(int friendID)
        {
            NetMessage message = new NetMessage()
            {
                Request = new NetMessageRequest()
                {
                    messageSendRequest=new MessageSendRequest()
                    {
                        Type=MessageType.Friend,
                        ToId=friendID,
                        messageInfo=new NMessageInfo()
                        {
                            FromInfo =new NMessageCharInfo()
                            {
                                Id=Models.User.Instance.CurrentCharacter.Id,
                                Name= Models.User.Instance.CurrentCharacter.Name,
                                Level= Models.User.Instance.CurrentCharacter.Level,
                                Class= Models.User.Instance.CurrentCharacter.Class
                            }
                        }
                    }
                }
            };
            NetClient.Instance.SendMessage(message);
        }

        internal void SendFriendDeleteRequest(int friendId)
        {
            NetMessage message = new NetMessage()
            {
                Request = new NetMessageRequest()
                {
                    friendRemove = new FriendRemoveRequest()
                    {
                        friendId = friendId
                    }
                }
            };
            NetClient.Instance.SendMessage(message);
        }




        //public void SendFriendAddResponse(bool accept, FriendAddRequest request)
        //{
        //    NetMessage message = new NetMessage()
        //    {
        //        Request = new NetMessageRequest()
        //        {
        //            friendAddResponse = new FriendAddResponse()
        //            {
        //                Result = accept ? Result.Success : Result.Failed,
        //                Errormsg = accept ? "对方同意了你的好友申请" : "对方拒绝了你的请求",
        //                Request = request
        //            }
        //        }
        //    };
        //    NetClient.Instance.SendMessage(message);
        //}

        public void SendFriendRemove(int friendID)
        {
            NetMessage message = new NetMessage()
            {
                Request = new NetMessageRequest()
                {
                    friendRemove = new FriendRemoveRequest()
                    {
                        friendId = friendID
                    }
                }
            };

        }





        private void OnFriendList(object sender, FriendListResponse message)
        {
            //Debug.Log($"OnFriendList Update");
            if (message.Result == Result.Success)
            {
                FriendManager.Instance.FriendInit(message.Friends);
            }
            
        }

        private void OnFriendRemove(object sender, FriendRemoveResponse message)
        {
            FriendManager.Instance.FriendRemove(message.Result);
        }

        private void OnFriendAddResponse(object sender, FriendAddResponse message)
        {
            FriendManager.Instance.OnFriendAdd(message);
        }

        //private void OnFriendAddRequest(object sender, FriendAddRequest message)
        //{
            
        //}
    }
}
