﻿/* Copyright (c) Citrix Systems, Inc. 
 * All rights reserved. 
 * 
 * Redistribution and use in source and binary forms, 
 * with or without modification, are permitted provided 
 * that the following conditions are met: 
 * 
 * *   Redistributions of source code must retain the above 
 *     copyright notice, this list of conditions and the 
 *     following disclaimer. 
 * *   Redistributions in binary form must reproduce the above 
 *     copyright notice, this list of conditions and the 
 *     following disclaimer in the documentation and/or other 
 *     materials provided with the distribution. 
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND 
 * CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE 
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF 
 * SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;

using XenAdmin.Core;
using XenAPI;


namespace XenAdmin.Actions
{
    public class DisableAdAction : PureAsyncAction
    {
        public static readonly string KEY_USER = "user";
        public static readonly string KEY_PASSWORD = "pass";
        private Dictionary<string, string> creds;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DisableAdAction(Pool pool, Dictionary<string, string> creds)
            : base(pool.Connection, string.Format(Messages.DISABLING_AD_ON, Helpers.GetName(pool).Ellipsise(50)), Messages.DISABLING_AD, false)
        {
            if (pool == null)
                throw new ArgumentNullException("pool");

            this.creds = creds;
            this.Pool = pool;
        }

        protected override void Run()
        {
            
            if (!creds.ContainsKey(KEY_USER))
            {
                log.DebugFormat("Disabling AD on pool '{0}' without disabling machine account in AD.", Helpers.GetName(Pool).Ellipsise(50));
            }
            else
            {
                log.DebugFormat("Disabling AD on pool '{0}' and disabling machine account in AD", Helpers.GetName(Pool).Ellipsise(50));
            }
            XenAPI.Pool.disable_external_auth(Session, Pool.opaque_ref, creds);

           
            Description = Messages.COMPLETED;
        }
    }
}