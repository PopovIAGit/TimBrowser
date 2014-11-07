using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.Helpers
{
    // Идентификаторы устройств
    public enum DeviceIdNumbers
    {
        BurT = 4000,
        BurM = 4001,
        BurBig = 4002,
        BKD = 5000
    }
    
    // Типы журналов
    public enum LogTypeNumbers
    {
        EventLog = 1,                       // Журнал событий
        CommandLog,                         // Журнал команд
        ParameterLog                        // Журнал изменения параметров
    }


}
