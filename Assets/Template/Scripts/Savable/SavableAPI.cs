using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Template.Scripts.Savable
{
    public class SavableAPI : ISavableContainer, IDisposable
    {
        private readonly SavableThread _savable;
        public ulong SavedTimes { get; private set; }
        public DataSource DataSource => _data;
        private DataSource _data;

        public SavableAPI(SavableContext context, IFileInterface file)
        {
            SavedTimes = 0;
            _savable = new SavableThread(context, file, this);
            _data = new DataSource();
            _data = _savable.ReadFile(ref _data);
            _savable.StartAutoSave();
            InitValsIfNeed();
        }

        private void InitValsIfNeed()
        {
            if (_data.LongIds == null) _data.LongIds = new ushort[0];
            if (_data.IntIds == null) _data.IntIds = new ushort[0];
            if (_data.UIntIds == null) _data.UIntIds = new ushort[0];
            if (_data.LongIds == null) _data.LongIds = new ushort[0];
            if (_data.ULongIds == null) _data.ULongIds = new ushort[0];
            if (_data.FloatIds == null) _data.FloatIds = new ushort[0];
            if (_data.DoubleIds == null) _data.DoubleIds = new ushort[0];
            if (_data.StringIds == null) _data.StringIds = new ushort[0];
        }
        
        public void Dispose()
        {
            _savable.Dispose();
        }

        #region Public API

        public ushort[] GetAllIdsOfString()
        {
            return _data.StringIds;
        }
        
        public void SaveInt(ushort id, int value)
        {
            SavableAPIUtils.Update<int>(ref _data.CountInts, ref _data.IntIds, ref _data.Ints,
                id, value);
            SavedTimes++;
        }
        
        public void DeleteInt(ushort id)
        {
            SavableAPIUtils.Delete<int>(ref _data.IntIds, ref _data.Ints, ref _data.CountInts, id);
            SavedTimes++;
        }
        
        public int GetInt(ushort id, int defaultValue = 0)
        {
            return SavableAPIUtils.GetValue<int>(ref _data.CountInts, ref _data.IntIds,
                ref _data.Ints, id, defaultValue);
        }

        public void SaveUInt(ushort id, uint value)
        {
            SavableAPIUtils.Update<uint>(ref _data.CountUInts, ref _data.UIntIds,
                ref _data.UInts, id, value);
            SavedTimes++;

        }
        
        public void DeleteUInt(ushort id)
        {
            SavableAPIUtils.Delete<uint>(ref _data.UIntIds, ref _data.UInts, ref _data.CountUInts, id);
            SavedTimes++;
        }
        
        public uint GetUInt(ushort id, uint defaultValue = 0)
        {
            return SavableAPIUtils.GetValue<uint>(ref _data.CountUInts, ref _data.UIntIds,
                ref _data.UInts, id, defaultValue);
        }
        


        public void SaveLong(ushort id, long value)
        {
            SavableAPIUtils.Update<long>(ref _data.CountLong, ref _data.LongIds,
                ref _data.Longs, id, value);
            SavedTimes++;

        }
        public void DeleteLong(ushort id)
        {
            SavableAPIUtils.Delete<long>(ref _data.LongIds, ref _data.Longs, ref _data.CountLong, id);
            SavedTimes++;
        }
        
        public long GetLong(ushort id, long defaultValue = 0)
        {
            return SavableAPIUtils.GetValue<long>(ref _data.CountLong, ref _data.LongIds,
                ref _data.Longs, id, defaultValue);
        }

        public void SaveULong(ushort id, ulong value)
        {
            SavableAPIUtils.Update<ulong>(ref _data.CountULong, ref _data.ULongIds,
                ref _data.ULongs, id, value);
            SavedTimes++;

        }
        public void DeleteULong(ushort id)
        {
            SavableAPIUtils.Delete<ulong>(ref _data.ULongIds, ref _data.ULongs, ref _data.CountULong, id);
            SavedTimes++;
        }
        
        public ulong GetULong(ushort id, ulong defaultValue = 0)
        {
            return SavableAPIUtils.GetValue<ulong>(ref _data.CountULong, ref _data.ULongIds,
                ref _data.ULongs, id, defaultValue);
        }
        
        public void SaveFloat(ushort id, float value)
        {
            SavableAPIUtils.Update<float>(ref _data.CountFloat, ref _data.FloatIds,
                ref _data.Floats, id, value);
            SavedTimes++;

        }
        public void DeleteFloat(ushort id)
        {
            SavableAPIUtils.Delete<float>(ref _data.FloatIds, ref _data.Floats, ref _data.CountFloat, id);
            SavedTimes++;
        }
        
        public double GetFloat(ushort id, float defaultValue = 0)
        {
            return SavableAPIUtils.GetValue<float>(ref _data.CountFloat, ref _data.FloatIds,
                ref _data.Floats, id, defaultValue);
        }
        
        public void SaveDouble(ushort id, double value)
        {
            SavableAPIUtils.Update<double>(ref _data.CountDouble, ref _data.DoubleIds,
                ref _data.Doubles, id, value);
            SavedTimes++;
        }
        
        public void DeleteDouble(ushort id)
        {
            SavableAPIUtils.Delete<double>(ref _data.DoubleIds, ref _data.Doubles, ref _data.CountDouble, id);
            SavedTimes++;
        }
        
        public double GetDouble(ushort id, double defaultValue = 0)
        {
            return SavableAPIUtils.GetValue<double>(ref _data.CountDouble, ref _data.DoubleIds,
                ref _data.Doubles, id, defaultValue);
        }
        public void SaveString(ushort id, string value)
        {
            SavableAPIUtils.Update<string>(ref _data.CountString, ref _data.StringIds,
                ref _data.Strings, id, value);
            SavedTimes++;

        }
        public void DeleteString(ushort id)
        {
            SavableAPIUtils.Delete<string>(ref _data.StringIds, ref _data.Strings, ref _data.CountString, id);
            SavedTimes++;
        }
        public string GetString(ushort id, string defaultValue = "")
        {
            return SavableAPIUtils.GetValue<string>(ref _data.CountString, ref _data.StringIds,
                ref _data.Strings, id, defaultValue);
        }
        
        #endregion


    }
    
    [Serializable]
    public struct DataSource
    {
        public ushort CountInts;
        public ushort CountUInts;
        public ushort CountLong;
        public ushort CountULong;
        public ushort CountFloat;
        public ushort CountDouble;
        public ushort CountString;

        public ushort[] IntIds;
        public int[] Ints;
        
        public ushort[] UIntIds;
        public uint[] UInts;

        public ushort[] LongIds;
        public long[] Longs;
        
        public ushort[] ULongIds;
        public ulong[] ULongs;

        public ushort[] FloatIds;
        public float[] Floats;

        public ushort[] DoubleIds;
        public double[] Doubles;

        public ushort[] StringIds;
        public string[] Strings;
    }
}
