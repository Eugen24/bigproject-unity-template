using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Template.Scripts.Savable
{
    public static class SavableAPIUtils
    {
        public static T[] Create<T>(ushort count, IByteReader<T> reader)
        {
            var array = new T[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = reader.Read();
            }

            return array;
        }
        
        public static void Update<T>(ref ushort count, ref ushort[] ids, ref T[] array, ushort id, T value)
        {
            var index = -1;

            for (int i = 0; i < count; i++)
            {
                if (ids[i] == id)
                {
                    index = i;
                    break;
                }
            }

            if (index < 0)
            {
                var newIds = new ushort[count + 1];
                var newArray = new T[count + 1];
                for (int i = 0; i < count; i++)
                {
                    newIds[i] = ids[i];
                    newArray[i] = array[i];
                }

                count++;
                newIds[count - 1] = id;
                newArray[count - 1] = value;
                ids = newIds;
                array = newArray;
            }
            else
            {
                array[index] = value;
            }
        }
        
        public static void Delete<T>(ref ushort[] ids, ref T[] array, ref ushort countArray, ushort id)
        {
            int index = -1;
            var s = ids;
            ushort count = 0;

            
            for (int i = 0; i < s.Length; i++)
            {
                count++;
                if (id == s[i])
                {
                    index = i;
                }
            }
            
            if (index == -1)
                return;
            //probably not the best performance solution, but it works...

            List<ushort> tmpIds = new List<ushort>(ids);
            List<T> tmpValues = new List<T>(array);
            
            tmpIds.RemoveAt(index);
            tmpValues.RemoveAt(index);
            count--;

            countArray = count;
            ids = tmpIds.ToArray();
            array = tmpValues.ToArray();
        }

        public static T GetValue<T>(ref ushort count, ref ushort[] ids, ref T[] array, ushort id, T defaultValue)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                if (ids[i] == id)
                {
                    return array[i];
                }
            }
            Update<T>(ref count, ref ids, ref array, id, defaultValue);
            return defaultValue;
        }
        
    }

    public class IntReader : IByteReader<int>
    {
        private readonly BinaryReader _reader;

        public IntReader(BinaryReader reader)
        {
            _reader = reader;
        }
        public int Read()
        {
            return _reader.ReadInt32();
        }
                
        public bool IsAbleToRead()
        {
            return _reader.BaseStream.Position + sizeof(int) < _reader.BaseStream.Length;
        }
    }

    public class UShortReader : IByteReader<ushort>
    {
        private readonly BinaryReader _reader;

        public UShortReader(BinaryReader reader)
        {
            _reader = reader;
        }
        public ushort Read()
        {
            if (IsAbleToRead())
                return _reader.ReadUInt16();
            return 0;
        }
                
        public bool IsAbleToRead()
        {
            return _reader.BaseStream.Position + sizeof(ushort) < _reader.BaseStream.Length;
        }
    }


    public class UIntReader : IByteReader<uint>
    {
        private readonly BinaryReader _reader;

        public UIntReader(BinaryReader reader)
        {
            _reader = reader;
        }
        public uint Read()
        {
            if (IsAbleToRead())
                return _reader.ReadUInt32();
            return 0;
        }
                
        public bool IsAbleToRead()
        {
            return _reader.BaseStream.Position + sizeof(uint) < _reader.BaseStream.Length;
        }
    }
    
    public class LongReader : IByteReader<long>
    {
        private readonly BinaryReader _reader;

        public LongReader(BinaryReader reader)
        {
            _reader = reader;
        }
        public long Read()
        {
            if (IsAbleToRead())
                return _reader.ReadInt64();
            return 0;
        }
                
        public bool IsAbleToRead()
        {
            return _reader.BaseStream.Position + sizeof(long) < _reader.BaseStream.Length;
        }
    }
    
    public class ULongReader : IByteReader<ulong>
    {
        private readonly BinaryReader _reader;

        public ULongReader(BinaryReader reader)
        {
            _reader = reader;
        }
        public ulong Read()
        {
            if (IsAbleToRead())
                return _reader.ReadUInt64();
            return 0;
        }
                
        public bool IsAbleToRead()
        {
            
            return _reader.BaseStream.Position + sizeof(ulong) < _reader.BaseStream.Length;
        }
    }
    
    public class FloatReader : IByteReader<float>
    {
        private readonly BinaryReader _reader;

        public FloatReader(BinaryReader reader)
        {
            _reader = reader;
        }
        public float Read()
        {
            if (IsAbleToRead())
                return _reader.ReadSingle();
            return 0;
        }
                
        public bool IsAbleToRead()
        {
            return _reader.BaseStream.Position + sizeof(float) < _reader.BaseStream.Length;
        }
    }
    
    public class DoubleReader : IByteReader<double>
    {
        private readonly BinaryReader _reader;

        public DoubleReader(BinaryReader reader)
        {
            _reader = reader;
        }
        public double Read()
        {
            if (IsAbleToRead())
                return _reader.ReadDouble();
            return 0;
        }
        
        public bool IsAbleToRead()
        {
            return _reader.BaseStream.Position + sizeof(double) < _reader.BaseStream.Length;
        }
    }
    
    public class StringReader : IByteReader<string>
    {
        private readonly BinaryReader _reader;
        private readonly BinaryWriter _writer;
        
        public StringReader(BinaryReader reader)
        {
            _reader = reader;
        }
        public string Read()
        {
            if (IsAbleToRead())
                return _reader.ReadString();
            return string.Empty;
        }
        

        public bool IsAbleToRead()
        {
            return _reader.BaseStream.Position < _reader.BaseStream.Length;
        }
    }

    
    public interface IByteReader<T>
    {
        T Read();
        bool IsAbleToRead();
    }
}