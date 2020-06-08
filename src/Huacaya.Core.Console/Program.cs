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

using Huacaya.Core.Contracts.Device;
using Huacaya.Core.Contracts.Repository;
using Huacaya.Core.Data.Model.Device;
using Huacaya.Core.Device;
using Huacaya.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huacaya.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Filterwheel");
            Console.WriteLine("2. Environmental Sensor");
            Console.WriteLine("3. Focuser");
            Console.WriteLine("4. Rotator");
            Console.WriteLine("5. Safety Mointor");
            Console.WriteLine("6. Telescope");

            Console.Write("Choose a device type: ");
            var option = int.Parse(Console.ReadLine());
            Console.Clear();
            switch (option)
            {
                case 1:
                    FilterWheel();
                    break;
                case 2:
                    EnvironmentalSensor();
                    break;
                case 3:
                    Focuser();
                    break;
                case 4:
                    Rotator();
                    break;
                case 5:
                    SafetyMonitor();
                    break;
                case 6:
                    Telescope();
                    break;
                default:
                    break;
            }
        }

        static void FilterWheel()
        {
            var device = Repository<FilterWheelRepository, IFilterWheel>();
            Console.ReadLine();
        }

        static void EnvironmentalSensor()
        {
            var device = Repository<EnvironmentalSensorRepository, IEnvironmentalSensor>();
            Console.ReadLine();
        }

        static void Rotator()
        {
            var device = Repository<RotatorRepository, IRotator>();
            Console.ReadLine();
        }

        static void Focuser()
        {
            var device = Repository<FocuserRepository, IFocuser>();
            Console.ReadLine();
        }

        static void SafetyMonitor()
        {
            var device = Repository<SafetyMonitorRepository, ISafetyMonitor>();
            Console.ReadLine();
        }

        static void Telescope()
        {
            var device = Repository<TelescopeRepository, ITelescope>();
            Console.ReadLine();
        }

        static T1 Repository<T, T1>() 
            where T : IDeviceRepository<T1>, new()
            where T1 : IDevice
        {
            var repository = new T();
            var devices = repository.GetDevices();

            int i = 1;
            foreach (var device in devices)
            {
                Console.WriteLine($"{i}.) {device.Id} {device.Description}");
                i++;
            }

            Console.Write("Choose a device: ");
            string choice = Console.ReadLine();
            int index;

            if(!int.TryParse(choice, out index) || index < 1 || index >= i )
            {
                return repository.GetById(devices.Where(t => t.Description.ToLower().Contains("simulator")).First().Id);
            }

            var deviceId = devices.ToArray()[index - 1].Id;

            return repository.GetById(deviceId);
        }
    }
}
