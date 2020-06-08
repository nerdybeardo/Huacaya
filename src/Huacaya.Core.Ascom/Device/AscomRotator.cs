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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huacaya.Core.Device
{
    public sealed class AscomRotator : AscomDevice<Rotator>, IRotator
    {
        public AscomRotator(Rotator driver, uint id) :
            base(driver, id)
        {

        }

        public bool CanReverse => this.driver.CanReverse;

        public bool IsMoving => this.driver.IsMoving;

        public double Position => this.driver.Position;

        public bool Reverse { get => this.driver.Reverse; set => this.driver.Reverse = value; }

        public double StepSize => this.driver.StepSize;

        public double TargetPosition => this.driver.TargetPosition;

        public void Halt() => this.driver.Halt();

        public void Move(float position) => this.driver.Move(position);

        public void MoveAbsolute(float position) => this.driver.MoveAbsolute(position);
    }
}
