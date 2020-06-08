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

using Huacaya.Core.Data.Model.Device;
using Huacaya.Core.Data.Model.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Huacaya.Core.Contracts.Device
{
    public interface ITelescope : IDevice
    {
        AlignmentModes AlignmentMode { get; }

        double ApertureArea { get; }

        double ApertureDiameter { get; }

        bool AtHome { get; }

        bool AtPark { get; }

        double Altitude { get; }

        double Azimuth { get; }

        TelescopeCapability Capabilities { get; }

        double DeclinationRate { get; set; }

        bool DoesRefration { get; set; }

        EquatorialCoordinateType EquatorialSystem { get; }

        double FocalLength { get; }

        double GuideRateDeclination { get; set; }

        double GuideRateRightAscension { get; set; }

        bool IsPulseGuiding { get; }

        double RightAscension { get; }

        double RightAscensionRate { get; set; }

        PierSide SideOfPier { get; set; }

        double SideRealTime { get; }

        ISite Site { get; }

        bool Slewing { get; }

        short SlewSettleTime { get; set; } 

        double TargetDeclination { get; set; }

        double TargetRightAscension { get; set; }

        bool Tracking { get; set; }

        DriveRate TrackingRate { get; set; }

        IEnumerable<DriveRate> SupportedTrackignRates { get; }

        DateTime UtcTime { get; set; }

        void FindHome();

        bool CanMoveAxis(Axis axis);

        void MoveAxis(Axis axis, double rate);

        void Park();

        void UnPark();

        void PulseGuide(Direction direction, int durationInMilliseconds);

        void SetPark();

        void SlewToAltAz(AltAzCoordinate coordinate);

        void SlewToAltAzAsync(AltAzCoordinate coordinate);

        void SlewToTarget();

        void SlewToTargetAsync();

        void AbortSlew();

        void SyncToAltAz(AltAzCoordinate coordinate);

        void SyncToCoordinates(EquatorialCoordinate coordinate);

        void SyncToTarget();

        bool HasCapabilities(TelescopeCapability capabilities);

        PierSide DestinationSideOfPier(double rightAscension, double declination);

        IEnumerable<AxisRate> AxisRates(Axis axis);
    }
}
