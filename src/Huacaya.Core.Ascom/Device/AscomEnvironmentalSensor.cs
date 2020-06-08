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
using Huacaya.Core.Data.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huacaya.Core.Device
{
    public sealed class AscomEnvironmentalSensor : AscomDevice<ObservingConditions>, IEnvironmentalSensor
    {
        public AscomEnvironmentalSensor(ObservingConditions driver, uint id) : 
            base(driver, id)
        {
        }

        public double AveragePeriod { get => this.driver.AveragePeriod; set => this.driver.AveragePeriod = value; }

        public double CloudCover => this.driver.CloudCover;

        public double DewPoint => this.driver.DewPoint;

        public double Humidity => this.driver.Humidity;

        public double Pressure => this.driver.Pressure;

        public double RainRate => this.driver.RainRate;

        public double SkyBrightness => this.driver.SkyBrightness;

        public double SkyQuality => this.driver.SkyQuality;

        public double SkyTemperature => this.driver.SkyTemperature;

        public double StarFwhm => this.driver.StarFWHM;

        public double Temperature => this.driver.Temperature;

        public double WindDirection => this.driver.WindDirection;

        public double WindGust => this.driver.WindGust;

        public double WindSpeed => this.driver.WindSpeed;

        public double TimeSinceLastUpdate(EnvironmentalSensorType sensor)
        {
            var sensorName = Enum.GetName(typeof(EnvironmentalSensorType), sensor);
            return this.driver.TimeSinceLastUpdate(sensorName);
        }

        public void Refresh() => this.driver.Refresh();
    }
}
