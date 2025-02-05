﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using VissmaFlow.Core.Infrastructure.Attributes;
using VissmaFlow.Core.Infrastructure.DataAccess;

namespace VissmaFlow.Core.Models.Communication
{
    public partial class CommSettings:EntityCommon
    {
        #region Имя порта
        [ObservableProperty]
        [Required]
        [NotifyDataErrorInfo]
        private string _portName = string.Empty;
        #endregion

        #region Скорость
        [NotifyDataErrorInfo]
        [ObservableProperty]
        [Range(1200, 115200)]
        private int _baudrate = 115200;
        #endregion

        #region Паритет
        [ObservableProperty]
        private Parity _parity = Parity.None;
        #endregion

        #region Стоповые биты
        [ObservableProperty]        
        private StopBits _stopBitsNum = StopBits.One;
        #endregion

        #region Тип связи
        [ObservableProperty]
        private CommInterface _interface;
        #endregion

        #region Порт
        [ObservableProperty]
        private int _portNumber = 502;
        #endregion

        #region IP
        [ObservableProperty]
        [IsIpAddress]
        [NotifyDataErrorInfo]
        private string _ip = "192.168.1.177"; 
        #endregion
    }
}
