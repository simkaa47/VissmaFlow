using EasyModbus;
using Microsoft.Extensions.Logging;
using System.Text;
using VissmaFlow.Core.Contracts.Communication;
using VissmaFlow.Core.Models.Communication.Modbus;
using VissmaFlow.Core.Models.Parameters;
using VissmaFlow.Core.Services.Communication.Modbus;
using VissmaFlow.Core.ViewModels;

namespace VissmaFlow.Core.Services.Communication
{
    public class ModbusRtuService : IComminicationService
    {
        private readonly ILogger<ModbusRtuService> _logger;
        private readonly CommunicationVm _communicationVm;
        private ModbusClient _client = new ModbusClient();
        public ModbusReadMemory holdingReadMemory = new ModbusReadMemory();
        public ModbusReadMemory inputReadMemory = new ModbusReadMemory();
        public List<ModbusReadCommand> commands = new List<ModbusReadCommand>();

        public ModbusRtuService(ILogger<ModbusRtuService> logger, CommunicationVm communicationVm)
        {
            _logger = logger;
            _communicationVm = communicationVm;
        }

        

        public bool Connected { set; get; }

        public event Action ScanCompletedEvent = delegate { };

        private void Init(IEnumerable<ParameterBase> parameters)
        {
            InitByType(ModbusRegType.Holding, holdingReadMemory, parameters);
            InitByType(ModbusRegType.Reading, inputReadMemory, parameters);
        }

        private void InitByType(ModbusRegType regType, ModbusReadMemory readMemory, IEnumerable<ParameterBase> parameters)
        {
            var sequences = GetParameterSequences(regType, parameters).OrderBy(s => s.Start).ToList();
            if (sequences.Count == 0) return;
            var min = sequences.First().Start;
            var max = sequences.Select(s => s.End).Max();
            readMemory.Offset = min;
            readMemory.Buffer = new ushort[max - min + 1];
            commands.Clear();
            int i = min;
            do
            {
                var belongeds = GetBelongedSequences(new ParameterSequence(i, i + 99), sequences);
                if (belongeds.Count == 0)
                {
                    i = i + 100;
                    continue;
                }
                else
                {
                    var points = belongeds.SelectMany(b => new int[] { b.Start, b.End }).ToList();
                    var minPoint = Math.Max(points.Min(), i);
                    var maxPoint = Math.Min(points.Max(), i + 99);
                    var count = maxPoint - minPoint + 1;
                    commands.Add(new ModbusReadCommand(minPoint, count, readMemory.Buffer, regType));
                    i = maxPoint + 1;
                }
            } while (i < max);

        }

        private List<ParameterSequence> GetParameterSequences(ModbusRegType regType, IEnumerable<ParameterBase> parameters)
        {
            var list = new List<ParameterSequence>();
            foreach (var par in parameters)
            {
                if (par is Parameter<ushort> parUshort && parUshort.ModbusRegType == regType && parUshort.IsRequired)
                {
                    list.Add(new ParameterSequence(parUshort.ModbRegNum, parUshort.ModbRegNum));
                }

                else if (par is Parameter<short> parShort && parShort.ModbusRegType == regType && parShort.IsRequired)
                    list.Add(new ParameterSequence(parShort.ModbRegNum, parShort.ModbRegNum));
                else if (par is Parameter<bool> parBool && parBool.ModbusRegType == regType && parBool.IsRequired)
                    list.Add(new ParameterSequence(parBool.ModbRegNum, parBool.ModbRegNum));
                else if (par is Parameter<int> parInt && parInt.ModbusRegType == regType && parInt.IsRequired)
                    list.Add(new ParameterSequence(parInt.ModbRegNum, parInt.ModbRegNum + 1));
                else if (par is Parameter<uint> parUint && parUint.ModbusRegType == regType && parUint.IsRequired)
                    list.Add(new ParameterSequence(parUint.ModbRegNum, parUint.ModbRegNum + 1));
                else if (par is Parameter<float> parFloat && parFloat.ModbusRegType == regType && parFloat.IsRequired)
                    list.Add(new ParameterSequence(parFloat.ModbRegNum, parFloat.ModbRegNum + 1));
                else if (par is Parameter<double> parDouble && parDouble.ModbusRegType == regType && parDouble.IsRequired)
                    list.Add(new ParameterSequence(parDouble.ModbRegNum, parDouble.ModbRegNum + 3));
                else if (par is Parameter<string> parstring && parstring.ModbusRegType == regType && parstring.IsRequired)
                {
                    var regs = parstring.StrLength % 2 != 0 ? parstring.StrLength / 2 + 1 : parstring.StrLength / 2;
                    list.Add(new ParameterSequence(parstring.ModbRegNum, parstring.ModbRegNum + regs - 1));
                }

            }
            return list;
        }

        private void GetValuesForParameters(IEnumerable<ParameterBase> parameters)
        {
            foreach (var par in parameters)
            {
                if (par is Parameter<ushort> parUshort && parUshort.IsRequired)
                {
                    var memory = parUshort.ModbusRegType == ModbusRegType.Holding ? holdingReadMemory : inputReadMemory;
                    if (memory is not null && memory.Buffer is not null)
                    {
                        var bytes = BitConverter.GetBytes(memory.Buffer[parUshort.ModbRegNum - memory.Offset]);
                        SetBytesOrder(parUshort, bytes);
                        parUshort.Value = BitConverter.ToUInt16(bytes);
                    }
                        
                }
                else if (par is Parameter<short> parShort && parShort.IsRequired)
                {
                    var memory = parShort.ModbusRegType == ModbusRegType.Holding ? holdingReadMemory : inputReadMemory;
                    if (memory is not null && memory.Buffer is not null)
                    {
                        var bytes = BitConverter.GetBytes(memory.Buffer[parShort.ModbRegNum - memory.Offset]);
                        SetBytesOrder(parShort, bytes);
                        parShort.Value = BitConverter.ToInt16(bytes);
                    }

                }
                else if (par is Parameter<bool> parBool && parBool.IsRequired)
                {
                    var memory = parBool.ModbusRegType == ModbusRegType.Holding ? holdingReadMemory : inputReadMemory;
                    if (memory is not null && memory.Buffer is not null)
                        parBool.Value = (memory.Buffer[parBool.ModbRegNum - memory.Offset] & (ushort)Math.Pow(2, parBool.BitNum)) > 0;
                }
                else if (par is Parameter<int> parInt && parInt.IsRequired)
                {
                    var memory = parInt.ModbusRegType == ModbusRegType.Holding ? holdingReadMemory : inputReadMemory;
                    if (memory is not null && memory.Buffer is not null)
                    {
                        var bytes = BitConverter.GetBytes(memory.Buffer[parInt.ModbRegNum - memory.Offset]);
                        SetBytesOrder(parInt, bytes);
                        parInt.Value = BitConverter.ToInt32(bytes);
                    }

                }
                else if (par is Parameter<uint> parUint && parUint.IsRequired)
                {
                    var memory = parUint.ModbusRegType == ModbusRegType.Holding ? holdingReadMemory : inputReadMemory;
                    if (memory is not null && memory.Buffer is not null)
                    {
                        var bytes = BitConverter.GetBytes(memory.Buffer[parUint.ModbRegNum - memory.Offset]);
                        SetBytesOrder(parUint, bytes);
                        parUint.Value = BitConverter.ToUInt32(bytes);
                    }

                }
                else if (par is Parameter<float> parFloat && parFloat.IsRequired)
                {
                    var memory = parFloat.ModbusRegType == ModbusRegType.Holding ? holdingReadMemory : inputReadMemory;
                    var bytes = memory?.Buffer?.Skip(parFloat.ModbRegNum - memory.Offset).Take(2).SelectMany(s => BitConverter.GetBytes(s)).ToArray();
                    SetBytesOrder(parFloat, bytes);
                    parFloat.Value = BitConverter.ToSingle(bytes);
                }
                else if (par is Parameter<double> parDouble && parDouble.IsRequired)
                {
                    var memory = parDouble.ModbusRegType == ModbusRegType.Holding ? holdingReadMemory : inputReadMemory;
                    var bytes = memory?.Buffer?.Skip(parDouble.ModbRegNum - memory.Offset).Take(4).SelectMany(s => BitConverter.GetBytes(s)).ToArray();
                    SetBytesOrder(parDouble, bytes);
                    parDouble.Value = BitConverter.ToSingle(bytes);
                }
                else if (par is Parameter<string> parstring && parstring.IsRequired)
                {
                    var memory = parstring.ModbusRegType == ModbusRegType.Holding ? holdingReadMemory : inputReadMemory;
                    var regs = parstring.StrLength % 2 != 0 ? parstring.StrLength / 2 + 1 : parstring.StrLength / 2;
                    var bytes = memory?.Buffer?
                        .Skip(parstring.ModbRegNum - memory.Offset)
                        .Take(regs)
                        .SelectMany(s => BitConverter.GetBytes(s))
                        .TakeWhile(b => b > 0)
                        .ToArray();
                    SetBytesOrder(parstring, bytes);
                    if (bytes != null)
                        parstring.Value = Encoding.ASCII.GetString(bytes).Replace("\0", "");
                }


            }
        }

        private void SetBit(ref ushort aByte, int pos, bool value)
        {
            if (value)
            {
                //left-shift 1, then bitwise OR
                aByte = (ushort)(aByte | (1 << pos));
            }
            else
            {
                //left-shift 1, then take complement, then bitwise AND
                aByte = (ushort)(aByte & ~(1 << pos));
            }
        }

        private List<ParameterSequence> GetBelongedSequences(ParameterSequence diapasone, IEnumerable<ParameterSequence> sequences)
        {
            var list = new List<ParameterSequence>();
            list = sequences.Where(s => IsBelongToSequence(diapasone, s)).ToList();
            return list;
        }


        private static bool IsBelongToSequence(ParameterSequence diapasone, ParameterSequence sequence)
        {
            return (sequence.Start >= diapasone.Start && sequence.Start <= diapasone.End) ||
                (sequence.End >= diapasone.Start && sequence.End <= diapasone.End) ||
                (sequence.Start < diapasone.Start && sequence.End > diapasone.End);
        }

        public void ReadAllData(IEnumerable<ParameterBase>? parameters,  int unitId)
        {
            if (parameters is null) return;
            Init(parameters);
            _client.UnitIdentifier = (byte)unitId;
            foreach (var command in commands)
            {
                if (command.RegType == ModbusRegType.Holding)
                {
                    var regs = ReadHoldingRegisters(command.Start, command.Count);
                    regs.CopyTo(0, holdingReadMemory.Buffer, command.Start - holdingReadMemory.Offset, regs.Count);
                }
                else
                {
                    var regs = ReadReadingRegisters(command.Start, command.Count);
                    regs.CopyTo(0, inputReadMemory.Buffer, command.Start - inputReadMemory.Offset, regs.Count);
                }

            }
            GetValuesForParameters(parameters);
            ScanCompletedEvent?.Invoke();
        }

        public void WriteParameter(ParameterBase parameter)
        {
            if (parameter.Owner is null) return;
            _client.UnitIdentifier = (byte)parameter.Owner.UnitId;
            if (parameter is Parameter<short> parShort)
            {
                var bytes = BitConverter.GetBytes(parShort.WriteValue);
                SetBytesOrder(parShort, bytes);
                WriteRegisters(new ushort[] { BitConverter.ToUInt16(bytes) }, parShort.ModbRegNum);
            }
            else if (parameter is Parameter<ushort> parUShort)
            {
                var bytes = BitConverter.GetBytes(parUShort.WriteValue);
                SetBytesOrder(parUShort, bytes);
                WriteRegisters(new ushort[] { BitConverter.ToUInt16(bytes) }, parUShort.ModbRegNum);
            }
            else if (parameter is Parameter<int> parInt)
            {
                var bytes = BitConverter.GetBytes(parInt.WriteValue);
                SetBytesOrder(parInt, bytes);
                WriteRegisters(new ushort[] { BitConverter.ToUInt16(bytes, 0), BitConverter.ToUInt16(bytes, 2), }, parInt.ModbRegNum);
            }
            else if (parameter is Parameter<uint> parUInt)
            { 
                var bytes = BitConverter.GetBytes(parUInt.WriteValue);
                SetBytesOrder(parUInt, bytes);
                WriteRegisters(new ushort[] { BitConverter.ToUInt16(bytes, 0), BitConverter.ToUInt16(bytes, 2), }, parUInt.ModbRegNum);
            }
            else if (parameter is Parameter<float> parFloat)
            {
                var bytes = BitConverter.GetBytes(parFloat.WriteValue);
                SetBytesOrder(parFloat, bytes);
                WriteRegisters(new ushort[] { BitConverter.ToUInt16(bytes, 0), BitConverter.ToUInt16(bytes, 2), }, parFloat.ModbRegNum);
            }
            else if (parameter is Parameter<double> parDouble)
            {
                var bytes = BitConverter.GetBytes(parDouble.WriteValue);
                SetBytesOrder(parDouble, bytes);                
                WriteRegisters(new ushort[] { 
                    BitConverter.ToUInt16(bytes, 0), 
                    BitConverter.ToUInt16(bytes, 2),
                    BitConverter.ToUInt16(bytes, 4),
                    BitConverter.ToUInt16(bytes, 6)
                }, parDouble.ModbRegNum);
            }
            else if (parameter is Parameter<bool> parBool)
            {
                var reg = ReadHoldingRegisters(parBool.ModbRegNum, 1).First();
                SetBit(ref reg, parBool.BitNum, parBool.WriteValue);
                WriteRegisters(new ushort[] { reg }, parBool.ModbRegNum);
            }
            else if (parameter is Parameter<string> parString)
            {

                var bytes = parString.WriteValue is not null ? Encoding.ASCII.GetBytes(parString.WriteValue).
                    Take(Math.Min(parString.WriteValue.Length, parString.StrLength))
                    .Append((byte)0)
                    .ToArray() : new byte[] {0 };
                SetBytesOrder(parString, bytes);
                var regs = new ushort[(bytes.Length + 1) / 2];
                for (int i = 0; i < bytes.Length; i += 2)
                {
                    if (i + 1 == bytes.Length)
                        regs[i / 2] = (ushort)bytes[i];
                    else regs[i / 2] = BitConverter.ToUInt16(bytes, i);
                }
                WriteRegisters(regs, parString.ModbRegNum);
            }
        }

        private List<ushort> ReadHoldingRegisters(int startRegNum, int regCnt)
        {
            try
            {
                if (_client == null) throw new Exception("Modbus client is null");
                if (!_client.Connected)
                    Connect();
                return _client.ReadHoldingRegisters(startRegNum, regCnt).Select(i => (ushort)i).ToList();

            }
            catch (Exception ex)
            {
                Disconnect();
                throw new Exception($"Чтение {regCnt} holding Modbus регистров по адресу {startRegNum} - {ex.Message}");
            }
        }

        private List<ushort> ReadReadingRegisters(int startRegNum, int regCnt)
        {
            try
            {
                if (_client == null) throw new Exception("Modbus client is null");
                if (!_client.Connected)
                    Connect();
                return _client.ReadInputRegisters(startRegNum, regCnt).Select(i => (ushort)i).ToList();

            }
            catch (Exception ex)
            {
                Disconnect();
                throw new Exception($"Чтение {regCnt} input Modbus регистров по адресу  {startRegNum} - {ex.Message}");
            }
        }

        private void WriteRegisters(ushort[] buf, int startRegNum)
        {
            try
            {
                if (_client == null) throw new Exception("Modbus client is null");
                if (!_client.Connected)
                    Connect();
                var regs = buf.Select(i => (int)i).ToArray();
                _client.WriteMultipleRegisters(startRegNum, regs);

            }
            catch (Exception ex)
            {
                Disconnect();
                throw new Exception($"Запись {buf.Length} Modbus регистров по адресу {startRegNum} - {ex.Message}");
            }
        }

        public void WriteCoil(int coilNumber, bool value)
        {
            try
            {
                if (_client == null) throw new Exception("Modbus client is null");
                if (!_client.Connected)
                    Connect();
                _client.WriteSingleCoil(coilNumber, value);
            }
            catch (Exception ex)
            {
                Disconnect();
                throw new Exception($"Запись Modbus coil №{coilNumber} - {ex.Message}");
            }
        }

        public void Connect()
        {
            var sett = _communicationVm.CommSettings;
            if (sett is null) return;
            _client.Baudrate = sett.Baudrate;
            _client.Parity = sett.Parity;
            _client.StopBits = sett.StopBitsNum;
            _client.SerialPort = sett.PortName;

            _logger.LogInformation($"Выполняется подключение к порту {sett.PortName}");
            _client.Connect();
            Connected = _client.Connected;
            if (Connected)
            {
                _logger.LogInformation($"Подключение к порту {sett.PortName} выполнено успешно");
            }
        }

        private void Disconnect()
        {
            if (_client != null && _client.Connected)
            {
                _logger.LogInformation($"Выполняется отключение от {_client.SerialPort}");
                _client.Disconnect();
                _logger.LogInformation($"Отключение от {_client.SerialPort}  выполнено успешно");
                Connected = _client.Connected;
            }
        }

        private void SetBytesOrder(ParameterBase par, byte[]? bytes)
        {
            if (bytes is null) return;
            byte[] tmpArr = new byte[2];
            byte tmp = 0;
            switch (par.ByteOrder)
            {
                case ByteOrder.ABCD:
                    break;
                case ByteOrder.CDAB:                    
                    for (int i = 3; i < bytes.Length; i += 4)
                    {
                        tmpArr[0]= bytes[i-1];
                        tmpArr[1] = bytes[i];
                        bytes[i-1] = bytes[i-3];
                        bytes[i] = bytes[i-2];
                        bytes[i - 3] = tmpArr[0];
                        bytes[i - 2] = tmpArr[1];
                    }
                    break;
                case ByteOrder.BADC:                    
                    
                    for (int i = 1; i < bytes.Length; i += 2)
                    {
                        tmp = bytes[i];
                        bytes[i] = bytes[i - 1];
                        bytes[i - 1] = tmp;
                    }
                    break;
                case ByteOrder.DCBA:
                    var reversed = bytes.Reverse().ToArray();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        bytes[i] = reversed[i];
                    }
                    break;
                default:
                    break;
            }


            
        }


    }
}
