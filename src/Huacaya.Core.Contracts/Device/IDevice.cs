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

using Huacaya.Core.Data.Model.Request;
using Huacaya.Core.Data.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huacaya.Core.Contracts.Device
{
    public interface IDevice
    {
        uint Id { get; }

        bool Connected { get; }

        string Description { get; }

        string DriverInfo { get; }

        string DriverVersion { get; }

        string Name { get; }

        IEnumerable<string> SupportedActions { get; }

        string Action(string actionName, string parameters);

        void CommandBlind(string command, bool raw);

        bool CommandBool(string command, bool raw);

        string CommandString(string command, bool raw);

        void Connect();

        void Disconnect();
    }
}
