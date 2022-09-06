using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.DownloadM.Model;

namespace TimBrowser.DataCore.DownloadM
{
    public partial class DownloadInformationModule
    {

        private FuncTwoData FuncTwoDownload(FuncOneData funcOneData_)
        {
            byte funcCodeTransmit = 2;
            ushort[] Data = new ushort[20];

            Povtor2: ;

            //TransmitByte(funcCodeTransmit);                                             // Передаем код функции
            Data[0] = (ushort)funcCodeTransmit;
            TransmitByte(_communication.DeviceAddress, 65123, Data);
            System.Threading.Thread.Sleep(30);
            Data[0] = 0;
            Data = ReceiveBytes(_communication.DeviceAddress, 65123, 18);

            byte funcCodeReceive = (byte)Data[0]; // ReceiveByte();                                       // Считываем ответный код функции
            byte funcDataSize = (byte)Data[1];    // Считанный размер ответного пакета

            if (funcCodeReceive != funcCodeTransmit)                                    // Проверяем правильность считанного кода функции
            {
                System.Threading.Thread.Sleep(200);
                goto Povtor2;
            }

            if (funcDataSize <= 0)                                                      // Валидация считанного размера ответного пакета
            {
                System.Threading.Thread.Sleep(200);
                goto Povtor2;
            }

            int answerPacketLogsLenght = (int)funcDataSize;                             // Расчитываем размер ответного пакета для валидации
            int logsNumber = funcOneData_.LogsNumber;
            int numberOfLogAddrNumFields = (int)FuncTwoIndicies.LogAddrTypeFieldNum;    // Число полей в отетной пакете по каждому типу журнала

            if (answerPacketLogsLenght != (logsNumber * numberOfLogAddrNumFields))      // Валидация принятого пакета данных
            {
                System.Threading.Thread.Sleep(200);
                goto Povtor2;
            }

            List<FuncTwoItem> funcTwoList = new List<FuncTwoItem>();

            for (int i = 0; i < funcDataSize; i += numberOfLogAddrNumFields)            // У каждого типа журнала извлекаем данные
            {
                // Извлекаем текущий адрес журнала
                int logCurrentAddress = (int)((Data[(int)FuncTwoIndicies.LogTypeAddrMsb+2 + i] << 8) |
                    Data[(int)FuncTwoIndicies.LogTypeAddrLsb+2 + i]);

                // Извлекаем число сделанных записей журнала
                int logRecordsNumber = (int)((Data[(int)FuncTwoIndicies.LogTypeNumMsb+2 + i] << 8) |
                    Data[(int)FuncTwoIndicies.LogTypeNumLsb+2 + i]);

                FuncTwoItem funcTwoSubData = new FuncTwoItem(logCurrentAddress, logRecordsNumber);

                // Добавляем полученные данные в список
                funcTwoList.Add(funcTwoSubData);
            }

            return new FuncTwoData(funcTwoList);      //??? что за лист                                  // Возвращаем объект второй функции
        }

    }
}
