using GTA;
using GTA.Math;
using GTA.Native;
using System;

namespace ARS
{
    public static class MathExtensions
    {
        static Random rnd = new Random();

        public static float LeftOrRight(Vector3 pos, Vector3 refPoint, Vector3 refDir)
        {
            Vector3 right = Vector3.Cross(refDir, Vector3.WorldUp);
            Vector3 rPos = pos - refPoint;
            return Vector3.Dot(right, rPos);
        }

        public static Vector3 RotateDir(Vector3 d, float angle, Vector3 axis)
        {
            return Quaternion.RotationAxis(axis, (float)(Math.PI / 180f) * angle) * -d;
        }

        public static Vector3 GetOffset(Entity reference, Entity ent)
        {
            Vector3 pos = ent.Position;
            return Function.Call<Vector3>(Hash.GET_OFFSET_FROM_ENTITY_GIVEN_WORLD_COORDS, reference, pos.X, pos.Y, pos.Z);
        }

        public static Vector3 GetOffset(Entity reference, Vector3 pos)
        {
            return Function.Call<Vector3>(Hash.GET_OFFSET_FROM_ENTITY_GIVEN_WORLD_COORDS, reference, pos.X, pos.Y, pos.Z);
        }

        public static float EngineTopSpeed(Vehicle v)
        {
            return Function.Call<float>(Hash._0x53AF99BAA671CA47, v) / 0.75f;
        }

        public static float Map(float x, float in_min, float in_max, float out_min, float out_max, bool clamp = false)
        {
            float r = (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
            if (clamp) r = Clamp(r, out_min, out_max);
            return r;
        }

        public static float MapGamma(float value, float in_min, float in_max, float min, float max, float gamma, bool clamp = false)
        {
            if (value > in_max) value = in_max;
            value /= in_max; // scale to 1.0;
            value = (float)Math.Pow(value, gamma); //original 0.4f

            float r = Map(value, 0.0f, 1.0f, in_min, in_max, clamp);
            r = Map(r, in_min, in_max, min, max);
            return r;
        }

        public static float Clamp(float val, float min, float max)
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static float Rad2Deg(float rad)
        {
            return (rad * (180.0f / (float)Math.PI)); //3.14159265358979323846264338327950288f));
        }

        public static float Deg2Rad(float angle)
        {
            return (float)(Math.PI * angle / 180.0f);
        }

        public static float LerpDelta(float current, float from, float to, float delta)
        {
            float r = from;
            float percent = (float)Math.Round((current * 100f) / to, 2);
            r = percent * Map(percent, 0f, 50f, 0f, 1f, true);
            return (float)Math.Round(r, 1);
        }

        public static float GetRatioForAngle(float degrees, float gamma)
        {
            if (degrees > 40) degrees = 40;
            degrees /= 40; // scale to 1.0;

            float input = (float)Math.Pow(degrees, gamma);
            return input;
        }

        public static Vector3 QuaternionRotate(Vector3 v, float a, Vector3 axis)
        {
            Vector3 r = Quaternion.RotationAxis(axis, (float)(Math.PI / 180f) * a) * -v; // Quaternion.RotationAxis takes radian angles
            return r;
        }

        public static float GetSteerOffsetToReachDeviation(float distToDev, float maxDist, float maxSteer)
        {
            float result = 0f;

            result = Map(distToDev, -maxDist, maxDist, -maxSteer, maxSteer);
            result = Clamp(result, -maxSteer, maxSteer);

            return result;
        }

        public static Vector3 Bezier3(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 p = uuu * p0;
            p += 3 * uu * t * p1;
            p += 3 * u * tt * p2;
            p += ttt * p3;

            return p;
        }

        public static Vector3 Bezier2(Vector3 Start, Vector3 End, float t)
        {
            return (((1 - t) * (1 - t)) * Start) + (2 * t * (1 - t) * Vector3.Zero) + ((t * t) * End);
        }

        public static Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
        {
            Vector3 P = x * Vector3.Normalize(B - A) + A;
            return P;
        }

        public static Vector3 GetXfromPosInDirection(Vector3 mypos, Vector3 dir, Vector3 pos)
        {
            Vector3 newDir = mypos - pos;
            Vector3 offset = Vector3.Cross(dir, newDir);

            return offset;
        }

        public static float MStoMPH(float ms)
        {
            return (float)Math.Round(ms * 2.236936f, 3);
        }

        public static float MPHtoMS(float mph)
        {
            return (float)Math.Round(mph * 0.44704f, 3);
        }

        public static TimeSpan ParseToTimeSpan(int gameTime)
        {
            TimeSpan t = new TimeSpan();
            t = TimeSpan.FromMilliseconds(gameTime);
            return t; //TimeSpan.FromMilliseconds(gameTime).ToString("m':'ss'.'f");
        }

        public static float GetPercent(float current, float max)
        {
            return (current / max) * 100;
        }

        public static int GetRandomInt(int min, int max)
        {
            return rnd.Next(min, max);
        }

        public static bool IsBitSet(int number, int bit)
        {
            return (number & bit) != 0;
        }
    }
}
