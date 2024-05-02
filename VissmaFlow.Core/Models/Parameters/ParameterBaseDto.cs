using VissmaFlow.Core.Models.Communication.Modbus;

namespace VissmaFlow.Core.Models.Parameters
{
    public class ParameterBaseDto
    {
        public string? Description { get; set; }
        public string? Notification { get; set; }
        public ByteOrder Order { get; set; }
        public int RegNum { get; set; }
        public int Length { get; set; }
        public DataTypeDto Type { get; set; }
        public ModbusRegType RegType { get; set; }

    }   

    public enum DataTypeDto
    {
        String, Float32, Int32, Uint32, Int16, Uint16
    };

}
