using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Download.Model;
using System.ComponentModel;

namespace TimBrowser.DataCore.Download
{
    // FuncOne
    public partial class DownloadInformationModule
    {
        private FuncOneData FuncOneDownload()
        {
            byte funcCodeTransmit = 1;

            Povtor1: ;

            TransmitByte(funcCodeTransmit);                                                     // Передаем код функции
            System.Threading.Thread.Sleep(30);
            byte funcCodeReceive = ReceiveByte();                                               // Считываем ответный код функции

            if (funcCodeReceive != funcCodeTransmit)                                            // Проверяем правильность принятого кода функции
            {
                //ThrowDownloadException(DownloadErrorCode.FuncOneError);
                //SendAlgorythmMessage(DataManagerRes.AlgFuncOneTransmitFuncCode, true);
                //return null;
                System.Threading.Thread.Sleep(200);
                goto Povtor1;
            }

            byte funcDataSize = ReceiveByte();                                                  // Считываем размер ответного пакета

            if (funcDataSize <= 0)                                                              // Валидация считанного размера ответного пакета
            {
                //ThrowDownloadException(DownloadErrorCode.FuncOneError);
                //SendAlgorythmMessage(DataManagerRes.AlgFuncOneFuncDataSize, true);
                //return null;
                System.Threading.Thread.Sleep(200);
                goto Povtor1;
            }

            byte[] funcData = ReceiveBytes((int)funcDataSize);                                  // Считываем пакет

            int logsNumber = (int)funcData[(int)FuncOneIndicies.LogsNumber];                    // Извлекаем количество журналов устройства

            int answerPacketLogsLenght = funcDataSize - ((int)FuncOneIndicies.LogsNumber + 1);  // Расчитываем принятую длинну пакета для валидации

            if (answerPacketLogsLenght != logsNumber)                                           // Проверяем правильного считанного пакета
            {
                //ThrowDownloadException(DownloadErrorCode.FuncOneError);
                //SendAlgorythmMessage(DataManagerRes.AlgFuncOneAnswerPacket, true);
                //return null;
                goto Povtor1;
            }

            int firmwareVersion = (int)((funcData[(int)FuncOneIndicies.FirmwareVersionMsb] << 8) |            // Извлекаем версию 
                funcData[(int)FuncOneIndicies.FirmwareVersionLsb]);

            int idOfDevice = (int)((funcData[(int)FuncOneIndicies.DeviceIdMsb] << 8) |            // Извлекаем идентификатор устройства
                funcData[(int)FuncOneIndicies.DeviceIdLsb]);

            int parametersNumber = (int)((funcData[(int)FuncOneIndicies.ParamsNumMsb] << 8) |     // Извлекаем количество параметров блока
                funcData[(int)FuncOneIndicies.ParamsNumLsb]);

            byte[] logsTypes = new byte[logsNumber];

            for (int logsNumberIndex = 0; logsNumberIndex < logsNumber; logsNumberIndex++)      // Извлекаем типы журналов устройства
            {
                logsTypes[logsNumberIndex] = (funcData[(int)FuncOneIndicies.LogsTypeStartIndex + logsNumberIndex]);
            }

            _maxIndex = logsNumber + 1;

            return new FuncOneData(idOfDevice, parametersNumber,                                // Возвращаем объект первой функции
                logsNumber, logsTypes, firmwareVersion);
        }
    }
}
