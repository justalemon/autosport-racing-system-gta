using GTA;
using GTA.Native;
using System;

using static ARS.MiscUtils;
using static ARS.NativeMemory;
using static ARS.Logger;
using System.Collections.Generic;

namespace ARS
{
    public static class VehicleExtensions
    {
        //offsets
        public static ulong SteerOffset = 0x0;
        public static ulong ThrottleOffset = 0x0;
        public static ulong BrakeOffset = 0x0;
        public static ulong Wheelsptr = 0x0;
        public static ulong NumWheelsOffset = 0x0;
        public static ulong SteerAngleOffset = 0x0;

        static public unsafe float GetGravity(Vehicle handle)
        {

            if (!CanWeUse(handle)) return 0f;
            ulong addrOffset = 0x0BA0;


            IntPtr addr = (IntPtr)FindPattern("\x74\x0A\xF3\x0F\x11\xB3\x1C\x09\x00\x00\xEB\x25", "xxxxxx????xx");

            if (addr != null)
            {
                var address = (ulong)handle.MemoryAddress;
                return *((float*)(address + addrOffset));
            }
            return 0;
        }


        static public unsafe void SetNitroOn(Vehicle v)
        {
            var vehHandle = v.MemoryAddress;
            IntPtr address = (IntPtr)FindPattern("\x38\x8B\x00\x00\x00\x00\x74\x04\xB1\x01\xEB\x58", "xx????xxxxxx");
            //\x38\x8B\x00\x00\x00\x00\x74\x04\xB1\x01\xEB\x58
            //\x8A\x83\x00\x00\x00\x00\x84\xC0\x75\x09\x40\x84\xFF
            if (address != null)
            {


                var g_vehNitroEnabledOffset = *(int*)(address + 2);
                bool d = *((bool*)(vehHandle + g_vehNitroEnabledOffset));

                Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "veh_xs_vehicle_mods");

                Function.Call((Hash)0xC8E9B6B71B8E660D, v, 1);//Enable nitro
                Function.Call((Hash)0x30B5831ECD9C35C5, v, 1f); //Set mult
                Function.Call((Hash)0x6E31900C2247D4ED, v, 1); //??
                Function.Call((Hash)0x6DEE944E1EE90CFB, v, 1); //??
                Function.Call((Hash)0x6411EB7E837170B5, v, 1f); //??

                Function.Call((Hash)0x2970EAA18FD5E42F, v, 1); //??



                // *((bool*)(vehHandle + g_vehNitroEnabledOffset)) = true;
            }
        }

        static public unsafe void SetSteerInput(Vehicle handle, float value)
        {

            if (!CanWeUse(handle)) return;

            if (SteerOffset == 0x0)
            {
                IntPtr addr = (IntPtr)FindPattern("\x74\x0A\xF3\x0F\x11\xB3\x1C\x09\x00\x00\xEB\x25", "xxxxxx????xx");
                if (addr != null)
                {
                    SteerOffset = *(uint*)(addr + 6);
                    Log(LogImportance.Info, "[MEMORY] Learned the steer offset: " + SteerOffset);
                }
            }
            else
            {
                var address = (ulong)handle.MemoryAddress;
                *((float*)(address + SteerOffset)) = value;
            }

        }

        static public unsafe void SetSteerAngle(Vehicle handle, float value)
        {

            if (!CanWeUse(handle)) return;

            if (SteerAngleOffset == 0x0)
            {
                IntPtr addr = (IntPtr)FindPattern("\x74\x0A\xF3\x0F\x11\xB3\x1C\x09\x00\x00\xEB\x25", "xxxxxx????xx");
                if (addr != null)
                {
                    SteerAngleOffset = *(uint*)(addr + 6) + 8;
                    Log(LogImportance.Info, "[MEMORY] Learned the steer offset: " + SteerAngleOffset);
                }
            }
            else
            {
                var address = (ulong)handle.MemoryAddress;
                *((float*)(address + SteerAngleOffset)) = value;
            }

        }

        static public unsafe float GetSteerInput(Vehicle handle)
        {

            if (!CanWeUse(handle)) return 0f;

            if (SteerOffset == 0x0)
            {
                IntPtr addr = (IntPtr)FindPattern("\x74\x0A\xF3\x0F\x11\xB3\x1C\x09\x00\x00\xEB\x25", "xxxxxx????xx");

                if (addr != null)
                {
                    SteerOffset = *(uint*)(addr + 6);
                    Log(LogImportance.Info, "[MEMORY] Learned the steer offset:" + SteerOffset);
                }
            }
            else
            {
                var address = (ulong)handle.MemoryAddress;
                return *((float*)(address + SteerOffset));
            }

            return 0;
        }

        static public unsafe void SetThrottle(Vehicle handle, float value)
        {
            if (ThrottleOffset == 0x0)
            {
                IntPtr addr = (IntPtr)FindPattern("\x74\x0A\xF3\x0F\x11\xB3\x1C\x09\x00\x00\xEB\x25", "xxxxxx????xx");

                if (addr != null)
                {


                    ThrottleOffset = *(uint*)(addr + 6) + 0x10;
                    Log(LogImportance.Info, "[MEMORY] Learned the throttle offset: " + ThrottleOffset);

                }
            }
            else
            {
                var address = (ulong)handle.MemoryAddress;

                *((float*)(address + ThrottleOffset)) = value;
            }


        }

        static public unsafe void SetBrakes(Vehicle handle, float value)
        {
            if (!CanWeUse(handle)) return;
            if (BrakeOffset == 0x0)
            {
                IntPtr addr = (IntPtr)FindPattern("\x74\x0A\xF3\x0F\x11\xB3\x1C\x09\x00\x00\xEB\x25", "xxxxxx????xx");

                if (addr != null)
                {

                    BrakeOffset = *(uint*)(addr + 6) + 0x14;
                    Log(LogImportance.Info, "[MEMORY] Learned the brakeOffset offset:" + BrakeOffset);

                }
            }
            else if (1 == 1)
            {
                var address = (ulong)handle.MemoryAddress;

                *((float*)(address + BrakeOffset)) = value;
            }

        }

        static public unsafe ulong GetWheelsPtr(Vehicle handle)
        {
            GameVersion gameVersion = Game.Version;
            var address = (ulong)handle.MemoryAddress;
            if (Wheelsptr == 0x0)
            {
                IntPtr addr = (IntPtr)FindPattern("\x3B\xB7\x48\x0B\x00\x00\x7D\x0D", "xx????xx");

                if (addr != null)
                {
                    Wheelsptr = *(uint*)(addr + 2) - 8;
                }
            }
            return *((ulong*)(address + Wheelsptr));
        }

        static public unsafe int GetNumWheels(Vehicle handle)
        {

            if (NumWheelsOffset == 0x0)
            {
                IntPtr addr = (IntPtr)FindPattern("\x3B\xB7\x48\x0B\x00\x00\x7D\x0D", "xx????xx");

                if (addr != null)
                {
                    NumWheelsOffset = *(uint*)(addr + 2);
                }
            }
            GameVersion gameVersion = Game.Version;
            var address = (ulong)handle.MemoryAddress;
            return *((int*)(address + NumWheelsOffset));
        }

        static public unsafe List<ulong> GetWheelPtrs(Vehicle handle)
        {
            var wheelPtr = GetWheelsPtr(handle);
            var numWheels = GetNumWheels(handle);
            List<ulong> wheelPtrs = new List<ulong>();
            for (int i = 0; i < numWheels; i++)
            {
                var wheelAddr = *((ulong*)(wheelPtr + 0x008 * (ulong)i));
                wheelPtrs.Add(wheelAddr);
            }
            return wheelPtrs;
        }


        static public unsafe List<float> GetWheelsPower(Vehicle handle)
        {
            List<ulong> wheelPtrs = GetWheelPtrs(handle);
            ulong offset = 0x1D4;
            List<float> angle = new List<float>();
            foreach (var wheel in wheelPtrs)
            {
                float pos = (float)Math.Round(*((float*)(wheel + offset)), 5);
                angle.Add(pos);
            }
            return angle;
        }

        static public unsafe List<float> GetWheelInternalDownforceMod(Vehicle handle)
        {
            List<ulong> wheelPtrs = GetWheelPtrs(handle);
            ulong offset = 0x220;
            List<float> angle = new List<float>();
            foreach (var wheel in wheelPtrs)
            {
                float pos = (float)Math.Round(*((float*)(wheel + offset)), 5);
                angle.Add(pos);
            }
            return angle;
        }

        static public unsafe List<float> GetMoreShit(Vehicle handle)
        {
            List<ulong> wheelPtrs = GetWheelPtrs(handle);
            ulong offset = 0x20A;
            List<float> angle = new List<float>();
            foreach (var wheel in wheelPtrs)
            {
                float pos = (float)Math.Round(*((float*)(wheel + offset)), 5);
                angle.Add(pos);
            }
            return angle;
        }

        static public unsafe List<float> GetWheelsGrip(Vehicle handle)
        {
            List<ulong> wheelPtrs = GetWheelPtrs(handle);
            ulong offset = 0x198;
            List<float> angle = new List<float>();
            foreach (var wheel in wheelPtrs)
            {
                float pos = (float)Math.Round(*((float*)(wheel + offset)), 2);
                angle.Add(pos);
            }
            return angle;
        }

        static public unsafe List<float> GetWheelsWetgrip(Vehicle handle)
        {
            List<ulong> wheelPtrs = GetWheelPtrs(handle);
            ulong offset = 0x19C;
            List<float> angle = new List<float>();
            foreach (var wheel in wheelPtrs)
            {
                float pos = (float)Math.Round(*((float*)(wheel + offset)), 2);
                angle.Add(pos);
            }
            return angle;
        }

        static public unsafe float GetWheelsMaxWheelspin(Vehicle handle)
        {
            List<ulong> wheelPtrs = GetWheelPtrs(handle);
            ulong offset = 0x174;
            float w = 0f;
            foreach (var wheel in wheelPtrs)
            {
                float pos = (float)Math.Round(*((float*)(wheel + offset)), 2);
                if (Math.Abs(pos) > Math.Abs(w)) w = pos;
            }
            return w;
        }

        static public unsafe float GetWheelsAvgWheelspin(Vehicle handle)
        {
            List<ulong> wheelPtrs = GetWheelPtrs(handle);
            ulong offset = 0x174;
            float w = 0f;
            foreach (var wheel in wheelPtrs)
            {
                float pos = (float)Math.Round(*((float*)(wheel + offset)), 2);
                w += pos;
            }
            return w / wheelPtrs.Count;
        }

        static public unsafe List<float> GetWheelSkidmark(Vehicle handle)
        {
            List<ulong> wheelPtrs = GetWheelPtrs(handle);
            ulong offset = 0x1B8;
            if (Game.Version <= GameVersion.VER_1_0_1290_1_STEAM) offset = 0x1B8;
            List<float> angle = new List<float>();
            foreach (var wheel in wheelPtrs)
            {
                float pos = *((float*)(wheel + offset));
                angle.Add(pos);
            }
            return angle;
        }

        static public unsafe List<float> GetWheelSlippage(Vehicle handle)
        {
            List<ulong> wheelPtrs = GetWheelPtrs(handle);
            ulong offset = 0x1A8;
            List<float> angle = new List<float>();
            foreach (var wheel in wheelPtrs)
            {
                float pos = *((float*)(wheel + offset));
                angle.Add(pos);
            }
            return angle;
        }

        public static unsafe ulong GetHandlingPtr(Vehicle v)
        {
            if (!CanWeUse(v)) return (ulong)0;

            var address = (ulong)v.MemoryAddress;
            ulong offset = 0x918;
            if (Game.Version > GameVersion.VER_1_0_1868_0_STEAM) offset = 0x938;
            return *((ulong*)(address + offset));
        }

        public static unsafe float GetTRCurveLat(Vehicle v)
        {

            if (!CanWeUse(v)) return 0f;
            ulong handlingAddress = GetHandlingPtr(v);
            ulong tractionCurveMaxOffset = 0x0098;
            if (handlingAddress < 1) return 0f;
            float result = *(float*)(handlingAddress + tractionCurveMaxOffset);
            return result;
        }
        public static unsafe float GetTRCurveMax(Vehicle v)
        {

            if (!CanWeUse(v)) return 0f;
            ulong handlingAddress = GetHandlingPtr(v);
            ulong tractionCurveMaxOffset = 0x088;
            if (handlingAddress < 1) return 0f;
            float result = *(float*)(handlingAddress + tractionCurveMaxOffset);
            return result;
        }
        public static unsafe float GetSteerLock(Vehicle v)
        {

            if (!CanWeUse(v)) return 0f;
            ulong handlingAddress = GetHandlingPtr(v);
            ulong steerlock = 0x0080;
            if (handlingAddress < 1) return 0f;
            float result = *(float*)(handlingAddress + steerlock);
            return result;
        }
        public static unsafe float GetDownforce(Vehicle v)
        {

            if (!CanWeUse(v)) return 0f;
            ulong handlingAddress = GetHandlingPtr(v);
            ulong downfOffset = 0x0014;
            if (handlingAddress < 1) return 0f;
            float result = *(float*)(handlingAddress + downfOffset);
            return result;
        }

        public static unsafe int GetModelFlags(Vehicle v)
        {

            if (!CanWeUse(v)) return 0;
            ulong handlingAddress = GetHandlingPtr(v);
            ulong modelflags = 0x124;
            if (handlingAddress < 1) return 0;
            int result = *(int*)(handlingAddress + modelflags);
            return result;
        }
        public static unsafe int GetHandlingFlags(Vehicle v)
        {

            if (!CanWeUse(v)) return 0;
            ulong handlingAddress = GetHandlingPtr(v);
            ulong modelflags = 0x128;
            if (handlingAddress < 1) return 0;
            int result = *(int*)(handlingAddress + modelflags);
            return result;
        }
        public static unsafe void SetDefMultiplier(Vehicle v, float mult)
        {
            if (!CanWeUse(v)) return;

            ulong handlingAddress = GetHandlingPtr(v);
            ulong tractionCurveMaxOffset = 0x00D0;
            *(float*)(handlingAddress + tractionCurveMaxOffset) = mult;
        }

        public static unsafe void SetGearRatio(Vehicle v, uint gear, float ratio)
        {
            if (!CanWeUse(v)) return;

            if (gear > 7) return;

            if (!v.Exists()) return;

            *(float*)(v.MemoryAddress + 0x838 + gear * sizeof(float)) = ratio;
        }
    }
}
