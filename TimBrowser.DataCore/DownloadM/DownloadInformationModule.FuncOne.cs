using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.DownloadM.Model;
using System.ComponentModel;

namespace TimBrowser.DataCore.DownloadM
{
    // FuncOne
    public partial class DownloadInformationModule
    {
        private FuncOneData FuncOneDownload()
        {
            byte funcCodeTransmit = 1;
            ushort[] Data = new ushort[20];

            Povtor1: ;

            //TransmitByte(funcCodeTransmit);                                                     // Передаем код функции
            Data[0] = (ushort)funcCodeTransmit;
            TransmitByte(_communication.DeviceAddress, 65123, Data);
            System.Threading.Thread.Sleep(50);
            Data[0] = 0;
            Data = ReceiveBytes(_communication.DeviceAddress, 65123, 14);

            byte funcCodeReceive = (byte)Data[0];
            byte funcDataSize    = (byte)Data[1];

            //byte funcCodeReceive = (byte)ReceiveByte(_communication.DeviceAddress, 65123, 1);                                                // Считываем ответный код функции

            if (funcCodeReceive != funcCodeTransmit)                                            // Проверяем правильность принятого кода функции
            {
                System.Threading.Thread.Sleep(200);
                goto Povtor1;
            }

            //byte funcDataSize = 0;// ReceiveByte();                                                  // Считываем размер ответного пакета

            if (funcDataSize <= 0)                                                              // Валидация считанного размера ответного пакета
            {
                System.Threading.Thread.Sleep(200);
                goto Povtor1;
            }

            int logsNumber = (int)Data[(int)FuncOneIndicies.LogsNumber+2];                    // Извлекаем количество журналов устройства
            //int answerPacketLogsLenght = funcDataSize - ((int)FuncOneIndicies.LogsNumber +2 + 2);  // Расчитываем принятую длинну пакета для валидации

            //byte[] funcData = ReceiveBytes((int)funcDataSize);                                  // Считываем пакет
           // ushort[] funcData = ReceiveBytes(_communication.DeviceAddress, 0, funcDataSize);

            //int logsNumber = (int)funcData[(int)FuncOneIndicies.LogsNumber];                    // Извлекаем количество журналов устройства

            //int answerPacketLogsLenght = funcDataSize - ((int)FuncOneIndicies.LogsNumber + 1);  // Расчитываем принятую длинну пакета для валидации

            //if (answerPacketLogsLenght != logsNumber)                                           // Проверяем правильного считанного пакета
            //{
                //ThrowDownloadException(DownloadErrorCode.FuncOneError);
                //SendAlgorythmMessage(DataManagerRes.AlgFuncOneAnswerPacket, true);
                //return null;
            //    goto Povtor1;
            //}

            int firmwareVersion = (int)((Data[(int)FuncOneIndicies.FirmwareVersionMsb+2] << 8) |            // Извлекаем идентификатор устройства
                Data[(int)FuncOneIndicies.FirmwareVersionLsb+2]);

            int idOfDevice = (int)((Data[(int)FuncOneIndicies.DeviceIdMsb+2] << 8) |            // Извлекаем идентификатор устройства
                Data[(int)FuncOneIndicies.DeviceIdLsb+2]);

            int parametersNumber = (int)((Data[(int)FuncOneIndicies.ParamsNumMsb+2] << 8) |     // Извлекаем количество параметров блока
                Data[(int)FuncOneIndicies.ParamsNumLsb+2]);

            byte[] logsTypes = new byte[logsNumber];

            for (int logsNumberIndex = 0; logsNumberIndex < logsNumber; logsNumberIndex++)      // Извлекаем типы журналов устройства
            {
                logsTypes[logsNumberIndex] = (byte)(Data[(int)FuncOneIndicies.LogsTypeStartIndex + logsNumberIndex +2]);
            }

            _maxIndex = logsNumber + 1;

            return new FuncOneData(idOfDevice, parametersNumber,                                // Возвращаем объект первой функции
                logsNumber, logsTypes, firmwareVersion);
        }
    }
}
