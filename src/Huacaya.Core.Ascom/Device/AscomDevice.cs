//-----------------------------------------------------------------------
// <license>
// Copyright © Moe Yassine <nerdybeardo@gmail.com>
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </license>
//-----------------------------------------------------------------------

using ASCOM.DriverAccess;
using Huacaya.Core.Contracts.Device;
using Huacaya.Core.Data.Model.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
namespace Huacaya.Core.Device
{
    public abstract class AscomDevice<T> : IDevice where T : AscomDriver
    {
        protected T driver;

        public AscomDevice(T driver, uint id)
        {
            this.driver = driver;
            this.Id = id;
        }

        public uint Id { get; protected set; }

        public bool Connected => driver.Connected;

        public string Description => driver.Description;

        public string DriverInfo => driver.DriverInfo;

        public string DriverVersion => driver.DriverVersion;

        public string Name => driver.Name;

        public IEnumerable<string> SupportedActions => 
            driver.SupportedActions.ToArray().Select(t => t.ToString()).ToList();

        public string Action(string actionName, string parameters) => 
            driver.Action(actionName, parameters);

        public void CommandBlind(string command, bool raw) => 
            driver.CommandBlind(command, raw);

        public bool CommandBool(string command, bool raw) =>
            driver.CommandBool(command, raw);

        public string CommandString(string command, bool raw) =>
            driver.CommandString(command, raw);

        public virtual void Connect() => driver.Connected = true;

        public virtual void Disconnect() => driver.Connected = false;
    }
}
