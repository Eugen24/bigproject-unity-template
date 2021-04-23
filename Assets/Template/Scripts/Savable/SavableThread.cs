using System;
using System.IO;
using System.Threading;
using UnityEngine;

namespace Template.Scripts.Savable
{
    public class SavableThread : IDisposable
    {
        private readonly IFileInterface _file;
        private readonly SavableContext _context;
        private readonly ISavableContainer _container;
        private ulong _currentSaveTime;
        private bool _isAutoSave;
        public SavableThread(SavableContext context, IFileInterface file, ISavableContainer container)
        {
            _file = file;
            _context = context;
            _container = container;
            _currentSaveTime = 0;
            _isAutoSave = false;
        }
        
        public void Dispose()
        {
            _isAutoSave = false;
        }
        
        public void StartAutoSave()
        {
            if (_isAutoSave == false)
            {
                _isAutoSave = true;
                ThreadPool.QueueUserWorkItem(AutoSave);
            }
            else
            {
                Debug.LogWarning("Thread is already started");
            }
        }

        private void AutoSave(object o)
        {
            while (_isAutoSave)
            {
                var saveTime = _container.SavedTimes;
                if (saveTime > _currentSaveTime)
                {
                    try
                    {
                        WriteFile();
                    }
                    catch (Exception e)
                    {
                        saveTime = 0;
                    }
                    _currentSaveTime = saveTime;
                }

                Thread.Sleep(100);
            }
        }

        public ref DataSource ReadFile(ref DataSource dataSource)
        {
            var file = _file.ReadBytes(_context.SourceFilePath);
            Stream s = new MemoryStream(file);
            BinaryReader br = new BinaryReader(s);

            var ushortReader = new UShortReader(br);

            var intReader = new IntReader(br);
            dataSource.CountInts = ushortReader.Read();
            dataSource.Ints = SavableAPIUtils.Create<int>(dataSource.CountInts, intReader);
            dataSource.IntIds = SavableAPIUtils.Create<ushort>(dataSource.CountInts, ushortReader);

            
            var uintReader = new UIntReader(br);
            dataSource.CountUInts = ushortReader.Read();
            dataSource.UInts = SavableAPIUtils.Create<uint>(dataSource.CountUInts, uintReader);
            dataSource.UIntIds = SavableAPIUtils.Create<ushort>(dataSource.CountUInts, ushortReader);


            var longReader = new LongReader(br);
            dataSource.CountLong = ushortReader.Read();
            dataSource.Longs = SavableAPIUtils.Create<long>(dataSource.CountLong, longReader);
            dataSource.LongIds = SavableAPIUtils.Create<ushort>(dataSource.CountLong, ushortReader);


            var ulongReader = new ULongReader(br);
            dataSource.CountULong = ushortReader.Read();
            dataSource.ULongs = SavableAPIUtils.Create<ulong>(dataSource.CountULong, ulongReader);
            dataSource.ULongIds = SavableAPIUtils.Create<ushort>(dataSource.CountULong, ushortReader);


            var floatReader = new FloatReader(br);
            dataSource.CountFloat = ushortReader.Read();
            dataSource.Floats = SavableAPIUtils.Create<float>(dataSource.CountFloat, floatReader);
            dataSource.FloatIds = SavableAPIUtils.Create<ushort>(dataSource.CountFloat, ushortReader);

            
            var doubleReader = new DoubleReader(br);
            dataSource.CountDouble = ushortReader.Read();
            dataSource.Doubles = SavableAPIUtils.Create<double>(dataSource.CountDouble, doubleReader);
            dataSource.DoubleIds = SavableAPIUtils.Create<ushort>(dataSource.CountDouble, ushortReader);


            var stringReader = new StringReader(br);
            dataSource.CountString = ushortReader.Read();
            dataSource.Strings = SavableAPIUtils.Create<string>(dataSource.CountString, stringReader);
            dataSource.StringIds = SavableAPIUtils.Create<ushort>(dataSource.CountString, ushortReader);
            
            //here can be added new items
            
            
            
            s.Dispose();
            br.Dispose();
            
            return ref dataSource;
        }

        private void WriteFile()
        {
            var dataSource = _container.DataSource;
            var s = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(s);
            writer.Write(dataSource.CountInts);
            for (int i = 0; i < dataSource.CountInts; i++)
                writer.Write(dataSource.Ints[i]);
            for (int i = 0; i < dataSource.CountInts; i++)
                writer.Write(dataSource.IntIds[i]);

            
            writer.Write(dataSource.CountUInts);
            for (int i = 0; i < dataSource.CountUInts; i++)
                writer.Write(dataSource.UInts[i]);
            for (int i = 0; i < dataSource.CountUInts; i++)
                writer.Write(dataSource.UIntIds[i]);

            
            writer.Write(dataSource.CountLong);
            for (int i = 0; i < dataSource.CountLong; i++)
                writer.Write(dataSource.Longs[i]);
            for (int i = 0; i < dataSource.CountLong; i++)
                writer.Write(dataSource.LongIds[i]);
            
            
            writer.Write(dataSource.CountULong);
            for (int i = 0; i < dataSource.CountULong; i++)
                writer.Write(dataSource.ULongs[i]);
            for (int i = 0; i < dataSource.CountULong; i++)
                writer.Write(dataSource.ULongIds[i]);
            
            

            writer.Write(dataSource.CountFloat);
            for (int i = 0; i < dataSource.CountFloat; i++)
                writer.Write(dataSource.Floats[i]);
            for (int i = 0; i < dataSource.CountFloat; i++)
                writer.Write(dataSource.FloatIds[i]);
            
            

            writer.Write(dataSource.CountDouble);
            for (int i = 0; i < dataSource.CountDouble; i++)
                writer.Write(dataSource.Doubles[i]);
            for (int i = 0; i < dataSource.CountDouble; i++)
                writer.Write(dataSource.DoubleIds[i]);
            

            writer.Write(dataSource.CountString);
            for (int i = 0; i < dataSource.CountString; i++)
                writer.Write(dataSource.Strings[i]);
            for (int i = 0; i < dataSource.CountString; i++)
                writer.Write(dataSource.StringIds[i]);

            //here can be added new items

            _file.WriteBytes(_context.SourceFilePath, s.GetBuffer());

            s.Dispose();
            writer.Dispose();

        }

    }

    public interface ISavableContainer
    {
        ulong SavedTimes { get; }
        DataSource DataSource { get; }
    }
}
