using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimBrowser.DataCore.DownloadM
{
    public enum DownloadErrorCode
    {
        NoErr = 0,
        FuncOneError = 10,
        FuncTwoError = 20,
        FuncThreeError = 30,
        FuncFourError = 40,
        FuncFiveError = 50,
        FuncSixError = 60
    }

    public enum FuncOneIndicies
    {
        FirmwareVersionMsb = 0,             // Старший байт версии ПО
        FirmwareVersionLsb,                 // Младший байт версии ПО
        DeviceIdMsb,                        // Старший байт идентификатора устройства
        DeviceIdLsb,                        // Младший байт идентификатора устройства
        ParamsNumMsb,                       // Старший байт количества параметров 
        ParamsNumLsb,                       // Младший байт количества параметров
        LogsNumber,                         // Количество журналов
        LogsTypeStartIndex                  // Начальный индекс для типов журналов
    }

    public enum FuncTwoIndicies
    {
        LogTypeAddrMsb = 0,                 // Старший байт текущего адреса журнала
        LogTypeAddrLsb,                     // Младший байт текущего адреса журнала
        LogTypeNumMsb,                    // Старший байт количества записей журнала
        LogTypeNumLsb,                    // Младший байт количества записей журнала
        LogAddrTypeFieldNum                 // Количество полей в ответе у каждого типа журнала
    }

    public enum FuncThreeIndicies
    {
        LogCellNum = 0,                      // Количество ячеек в записи
        LogCurrentCellFieldNum               // Начальный индекс количества полей в конкретной ячейке
    }

    public enum FuncFourIndicies
    {
        CellFieldAddressMsb = 0,
        CellFieldAddressLsb
    }

    public enum FuncFiveIndicies
    {
        CellFieldValueMsb = 0,
        CellFieldValueLsb
    }

    public enum FuncSixIndicies
    {
        ParamValueMsb = 0,
        ParamValueLsb,
    }
}
