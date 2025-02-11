﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network;

namespace GameServer.Managers
{

    class  NoSessionException : Exception
    {
        public override string Message =>"不存在或不在线的玩家";
    }

    class SessionManager : Singleton<SessionManager>
    {
        Dictionary<int, NetConnection<NetSession>> Sessions = new Dictionary<int, NetConnection<NetSession>>();

        public void AddSession(int characterId, NetConnection<NetSession> session)
        {
            Sessions[characterId] = session;

        }
        public void RemoveSession(int characterId)
        {
            Sessions.Remove(characterId);
        }

        public bool GetSession(int characterId, out NetConnection<NetSession> session)
        {
            return Sessions.TryGetValue(characterId, out session);
        }


        public void ForeachSession(Action<NetConnection<NetSession>> callback)
        {
            foreach (var kvp in Sessions)
            {
                callback.Invoke(kvp.Value);
            }

        }


    }
}
