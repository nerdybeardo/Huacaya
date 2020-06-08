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

using ASCOM.Utilities;
using ASCOM.Utilities.Interfaces;
using Huacaya.Core.Contracts.Device;
using Huacaya.Core.Contracts.Repository;
using Huacaya.Core.Data.Model.Device;
using Huacaya.Core.Device;
using Huacaya.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Huacaya.Core.Repositories
{
    public abstract class AscomRepository<T> : IDeviceRepository<T>
        where T : IDevice
    {
        private string dictionaryKey;

        private string deviceKey;

        private string deviceType;

        public AscomRepository(string deviceType) =>
            (this.dictionaryKey, this.deviceKey, this.deviceType) =
            ($"{deviceType}_DICTIONARY", $"{deviceType}_DEVICES", deviceType);

        public IEnumerable<DeviceListItem> GetDevices()
        {
            var devices = CacheUtils.GetFromCache<List<DeviceListItem>>(this.dictionaryKey);

            if (devices == null)
            {
                devices = this.GetDictionary();
                CacheUtils.SaveToCache<List<DeviceListItem>>(this.dictionaryKey, devices);
            }

            return devices;
        }

        public T GetById(uint id)
        {
            var deviceList = GetDevices();
            if(!deviceList.Any(d=>d.Id == id))
            {
                throw new Exception($"Could not find device with id {id}");
            }

            if(!IsLoaded(id))
            {
                Load(id);
            }

            return this.GetLoadedDevices().Single(d=>d.Id == id);
        }

        public bool IsLoaded(uint id) => this.GetLoadedDevices().Any(d => d.Id == id);

        public void Load(uint id)
        {
            // This is already loaded
            if (IsLoaded(id))
            {
                return;
            }

            var deviceList = GetDevices();
            if (!deviceList.Any(d=>d.Id == id))
            {
                throw new Exception($"Could not find device with id {id}");
            }

            var devices = this.GetLoadedDevices();
            var device = (AscomDeviceListItem)deviceList.Single(d => d.Id == id);

            devices.Add(this.GetDevice(device.ProgId, id));

            CacheUtils.SaveToCache(this.deviceKey, devices);
        }

        public void Unload(uint id)
        {
            // it's not loaded
            if (!IsLoaded(id))
            {
                return;
            }

            var devices = this.GetLoadedDevices();

            // No devices are loaded
            if (devices.Any(d=>d.Id == id))
            {
                var device = devices.First(d => d.Id == id);
                // Make sure we disconnect safely if already connected
                if (device.Connected)
                {
                    device.Disconnect();
                }

                devices.Remove(device);
                CacheUtils.SaveToCache(this.deviceKey, devices);
            }
        }

        public void UnloadAll()
        {
            var devices = GetDevices();
            foreach (var device in devices)
            {
                this.Unload(device.Id);
            }
        }

        protected abstract T GetDevice(string key, uint id);

        protected List<DeviceListItem> GetDictionary()
        {
            var profile = new Profile();

            // TODO: don't ignore remote
            var registeredDevices = profile.RegisteredDevices(this.deviceType)
                                    .ToList<IKeyValuePair>()
                                    .Where(d => !d.Key.Contains("Remote"))
                                    .Select(d => new AscomDeviceListItem
                                    {
                                        Id = this.GenerateId(d.Key),
                                        ProgId = d.Key,
                                        Description = d.Value
                                    })
                                    .ToList<DeviceListItem>();

            return registeredDevices;
        }

        private uint GenerateId(string key)
        {
            byte[] result;
            var sha = new SHA1CryptoServiceProvider();
            result = sha.ComputeHash(Encoding.UTF8.GetBytes(key));

            return BitConverter.ToUInt32(result, 0);
        }

        private List<T> GetLoadedDevices()
        {
            var devices = CacheUtils.GetFromCache<List<T>>(this.deviceKey);

            // No devices are loaded just save an empty set to the cache
            if (devices == null)
            {
                devices = new List<T>();
                CacheUtils.SaveToCache(this.deviceKey, devices);
            }

            return devices;
        }
    }
}
