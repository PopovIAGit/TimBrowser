using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimBrowser.DataCore.Download.Model;

namespace TimBrowser.DataCore.DownloadBLE
{
    public partial class DownloadInformationModule
    {

        private FuncTwoData FuncTwoDownload(FuncOneData funcOneData_)
        {
            byte funcCodeTransmit = 2;

            Povtor2: ;

            TransmitByte(funcCodeTransmit);                                             // Передаем код функции
            System.Threading.Thread.Sleep(30);
            byte funcCodeReceive = ReceiveByte();                                       // Считываем ответный код функции

            if (funcCodeReceive != funcCodeTransmit)                                    // Проверяем правильность считанного кода функции
            {
                //ThrowDownloadException(DownloadErrorCode.FuncTwoError);
                //SendAlgorythmMessage(DataManagerRes.AlgFuncTwoTransmitFuncCode, true);
                //return null;
                for (int i = 0; i < 20; i++) ReceiveByte();
                System.Threading.Thread.Sleep(200);
                goto Povtor2;
            }

            byte funcDataSize = ReceiveByte();                                          // Считанный размер ответного пакета

            if (funcDataSize <= 0)                                                      // Валидация считанного размера ответного пакета
            {
                //ThrowDownloadException(DownloadErrorCode.FuncTwoError);
                //SendAlgorythmMessage(DataManagerRes.AlgFuncTwoFuncDataSize, true);
                //return null;
                for (int i = 0; i < 20; i++) ReceiveByte();
                System.Threading.Thread.Sleep(200);
                goto Povtor2;
            }

            byte[] funcData = ReceiveBytes((int)funcDataSize);                          // Считываем пакет данных

            int answerPacketLogsLenght = (int)funcDataSize;                             // Расчитываем размер ответного пакета для валидации
            int logsNumber = funcOneData_.LogsNumber;
            int numberOfLogAddrNumFields = (int)FuncTwoIndicies.LogAddrTypeFieldNum;    // Число полей в отетной пакете по каждому типу журнала

            if (answerPacketLogsLenght != (logsNumber * numberOfLogAddrNumFields))      // Валидация принятого пакета данных
            {
                //ThrowDownloadException(DownloadErrorCode.FuncTwoError);
                //SendAlgorythmMessage(DataManagerRes.AlgFuncTwoAnswerPacket, true);
                //return null;
                for (int i = 0; i < 20; i++) ReceiveByte();
                System.Threading.Thread.Sleep(200);
                goto Povtor2;
            }

            List<FuncTwoItem> funcTwoList = new List<FuncTwoItem>();

            for (int i = 0; i < funcDataSize; i += numberOfLogAddrNumFields)            // У каждого типа журнала извлекаем данные
            {
                // Извлекаем текущий адрес журнала
                int logCurrentAddress = (int)((funcData[(int)FuncTwoIndicies.LogTypeAddrMsb + i] << 8) |
                    funcData[(int)FuncTwoIndicies.LogTypeAddrLsb + i]);

                // Извлекаем число сделанных записей журнала
                int logRecordsNumber = (int)((funcData[(int)FuncTwoIndicies.LogTypeNumMsb + i] << 8) |
                    funcData[(int)FuncTwoIndicies.LogTypeNumLsb + i]);

                FuncTwoItem funcTwoSubData = new FuncTwoItem(logCurrentAddress, logRecordsNumber);

                // Добавляем полученные данные в список
                funcTwoList.Add(funcTwoSubData);
            }

            return new FuncTwoData(funcTwoList);                                        // Возвращаем объект второй функции
        }

    }
}
