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

using ASCOM.DeviceInterface;
using ASCOM.DriverAccess;
using Huacaya.Core.Contracts.Device;
using Huacaya.Core.Data.Model.Device;
using Huacaya.Core.Data.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using PierSide = Huacaya.Core.Data.Model.Enums.PierSide;
using AlignmentModes = Huacaya.Core.Data.Model.Enums.AlignmentModes;
using EquatorialCoordinateType = Huacaya.Core.Data.Model.Enums.EquatorialCoordinateType;

namespace Huacaya.Core.Device
{
    public sealed class AscomTelescope : AscomDevice<Telescope>, ITelescope
    {
        public AscomTelescope(Telescope telescope, uint id) :
            base(telescope, id)
        {
            this.Site = new AscomSite(telescope);
            this.SupportedTrackignRates = this.GetTrackingRates();
        }

        public AlignmentModes AlignmentMode => (AlignmentModes)this.driver.AlignmentMode;

        public double ApertureArea => this.driver.ApertureArea;

        public double ApertureDiameter => this.driver.ApertureDiameter;

        public bool AtHome => this.driver.AtHome;

        public bool AtPark => this.driver.AtPark;

        public double Altitude => this.driver.Altitude;

        public double Azimuth => this.driver.Azimuth;

        public TelescopeCapability Capabilities { get; private set; }

        public double DeclinationRate
        {
            get => this.driver.DeclinationRate;
            set => this.driver.DeclinationRate = value;
        }

        public bool DoesRefration
        {
            get => this.driver.DoesRefraction;
            set => this.driver.DoesRefraction = value;
        }

        public EquatorialCoordinateType EquatorialSystem => (EquatorialCoordinateType)this.driver.EquatorialSystem;

        public double FocalLength => this.driver.FocalLength;

        public double GuideRateDeclination
        {
            get => this.driver.GuideRateDeclination;
            set => this.driver.GuideRateDeclination = value;
        }

        public double GuideRateRightAscension
        {
            get => this.driver.GuideRateRightAscension;
            set => this.driver.GuideRateRightAscension = value;
        }

        public bool IsPulseGuiding => this.driver.IsPulseGuiding;

        public double RightAscension => this.driver.RightAscension;

        public double RightAscensionRate
        {
            get => this.driver.RightAscensionRate;
            set => this.driver.RightAscensionRate = value;
        }
        public PierSide SideOfPier
        {
            get => (PierSide)this.driver.SideOfPier;
            set => this.driver.SideOfPier = (ASCOM.DeviceInterface.PierSide)value;
        }

        public double SideRealTime => this.driver.SiderealTime;

        public ISite Site { get; private set; }

        public bool Slewing => this.driver.Slewing;

        public short SlewSettleTime
        {
            get => this.driver.SlewSettleTime;
            set => this.driver.SlewSettleTime = value;
        }

        public double TargetDeclination
        {
            get => this.driver.TargetDeclination;
            set => this.driver.TargetDeclination = value;
        }

        public double TargetRightAscension
        {
            get => this.driver.TargetRightAscension;
            set => this.driver.TargetRightAscension = value;
        }

        public bool Tracking
        {
            get => this.driver.Tracking;
            set => this.driver.Tracking = value;
        }

        public DriveRate TrackingRate { 
            get => (DriveRate)this.driver.TrackingRate; 
            set => this.driver.TrackingRate = (DriveRates)value; }

        public IEnumerable<DriveRate> SupportedTrackignRates { get; private set; }

        public DateTime UtcTime { get => this.driver.UTCDate; set => this.driver.UTCDate = value; }

        public void AbortSlew() => this.driver.AbortSlew();
        public void FindHome() => this.driver.FindHome();

        public void Park() => this.driver.Park();
        public void SetPark() => this.driver.SetPark();

        public void SlewToTarget() => this.driver.SlewToTarget();

        public void SlewToTargetAsync() => this.driver.SlewToTargetAsync();

        public void SyncToTarget() => this.driver.SyncToTarget();

        public void UnPark() => this.driver.Unpark();

        public bool CanMoveAxis(Axis axis) => this.driver.CanMoveAxis((TelescopeAxes)axis);

        public bool HasCapabilities(TelescopeCapability capabilities) => 
            (this.Capabilities & capabilities) == capabilities;

        public PierSide DestinationSideOfPier(double rightAscension, double declination) =>
            (PierSide)this.driver.DestinationSideOfPier(rightAscension, declination);

        public void MoveAxis(Axis axis, double rate) =>
            this.driver.MoveAxis((TelescopeAxes)axis, rate);

        public void PulseGuide(Direction direction, int durationInMilliseconds) =>
            this.driver.PulseGuide((GuideDirections)direction, durationInMilliseconds);

        public void SlewToAltAz(AltAzCoordinate coordinate) =>
            this.driver.SlewToAltAz(coordinate.Azimuth, coordinate.Altitude);

        public void SlewToAltAzAsync(AltAzCoordinate coordinate) =>
            this.driver.SlewToAltAzAsync(coordinate.Azimuth, coordinate.Altitude);

        public void SyncToAltAz(AltAzCoordinate coordinate) =>
             this.driver.SyncToAltAz(coordinate.Azimuth, coordinate.Altitude);

        public void SyncToCoordinates(EquatorialCoordinate coordinate) =>
            this.driver.SyncToCoordinates(coordinate.RightAscension, coordinate.Declination);

        public override void Connect()
        {
            base.Connect();

            this.Capabilities = TelescopeCapability.None;
            this.SetCapability(TelescopeCapability.FindHome, this.driver.CanFindHome);
            this.SetCapability(TelescopeCapability.Park, this.driver.CanPark);
            this.SetCapability(TelescopeCapability.PulseGuide, this.driver.CanPulseGuide);
            this.SetCapability(TelescopeCapability.SetDeclinationRate, this.driver.CanSetDeclinationRate);
            this.SetCapability(TelescopeCapability.SetPark, this.driver.CanSetPark);
            this.SetCapability(TelescopeCapability.SetPierSide, this.driver.CanSetPierSide);
            this.SetCapability(TelescopeCapability.SetRightAscensionRate, this.driver.CanSetRightAscensionRate);
            this.SetCapability(TelescopeCapability.SetTracking, this.driver.CanSetTracking);
            this.SetCapability(TelescopeCapability.Slew, this.driver.CanSlew);
            this.SetCapability(TelescopeCapability.SlewAltAzAsync, this.driver.CanSlewAltAzAsync);
            this.SetCapability(TelescopeCapability.SlewAltSz, this.driver.CanSlewAsync);
            this.SetCapability(TelescopeCapability.Sync, this.driver.CanSync);
            this.SetCapability(TelescopeCapability.SyncAltAz, this.driver.CanSyncAltAz);
        }

        public IEnumerable<AxisRate> AxisRates(Axis axis)
        {
            var rates = this.driver.AxisRates((TelescopeAxes)axis);

            foreach (IRate rate in rates)
            {
                yield return new AxisRate { Maximum = rate.Maximum, Minimum = rate.Minimum };
            }
        }

        private void SetCapability(TelescopeCapability capability, bool hasCapability)
        {
            if (hasCapability)
            {
                this.Capabilities =  this.Capabilities != TelescopeCapability.None ? this.Capabilities | capability : capability;
            }
        }

        private IEnumerable<DriveRate> GetTrackingRates()
        {
            foreach (var rate in this.driver.TrackingRates)
            {
                yield return (DriveRate)rate;
            }
        }

    }
}
