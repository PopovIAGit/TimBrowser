using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.Helpers
{
    /// <summary>
    /// Типы журналов
    /// </summary>
    public enum LogTypes
    {
        None = 0,
        EventLog = 1,                       // Журнал событий
        CommandLog,                         // Журнал команд
        ParameterLog,                       // Журнал изменения параметров
        SimIDLog                            // Журнал SimID
    }

    /// <summary>
    /// Идентификаторы устройств
    /// </summary>
    public enum DeviceIds
    {
        None = 0,
        BurT = 4000,
        BurM = 4001,
        BURT2 = 4005,
        BKD = 5000,
        
    }

    /// <summary>
    /// Источник команды в журнале команд
    /// </summary>
    public enum CommandSource
    {
        None = 0
    }

}
