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

using Huacaya.Core.Data.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huacaya.Core.Data.Model.Device
{
    public class SwitchDevice
    {
        public SwitchDevice(int id, bool canWrite, SwitchState state, 
            string description, string name, double minValue, double maxValue)
        {
            this.Id = id;
            this.CanWrite = canWrite;
            this.State = state;
            this.Description = description;
            this.Name = name;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public int Id { get; private set; }

        public bool CanWrite { get; private set; }

        public SwitchState State { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public double MinValue { get; private set; }

        public double MaxValue { get; private set; }
    }
}
