using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using TimBrowser.DataCore.Download.Model;
using System.ComponentModel;

namespace TimBrowser.DataCore.DownloadBLE
{
    // FuncOne
    public partial class DownloadInformationModule
    {
        public static int Result;    
        int[] _CrcTable = new int[256];
        public static  int INIT_CRC = 0xFFFF;
        public static  int GOOD_CRC = 0x0000;
        public static  int GENER_CRC = 0xA001;

        public void GenerateCrcTable()
        {
            int Crc=0;
            int j=0;

            for (int i=0; i < 256; i++)
            {
                Crc = i;
                for (j=0; j < 8; j++)
                {
                    if ((Crc & 1)==1) Crc =  ((Crc >> 1) ^ (GENER_CRC));
                    else Crc =  (Crc >> 1);
                }
                _CrcTable[i] = Crc;
            }
        }

        public int CalcFrameCrc(byte[] pArray, short Len)
        {
            int Count=0;
            int Crc=0;
            Crc = INIT_CRC;
            do {Crc = ((Crc >> 8) ^ _CrcTable[(Crc ^ (int)pArray[Count]) & 0x00FF]); Count++; }
            while (Count<Len);// -6-
            Result = Crc;
            return Crc;
        }

        private FuncOneData FuncOneDownload()
        {
            byte funcCodeTransmit = 1;
            byte[] bufsend = new byte[20 + 9];

            GenerateCrcTable();
            string result = null;

            //---------------------------------------------------
            /*String StrKey_String="";
            String strIndex="";
            String strHwInfo = null;
            try
            {
                ManagementObjectSearcher searcher1 = new ManagementObjectSearcher("select * from " + StrKey_String);
                foreach (ManagementObject share in searcher1.Get())
                {
                    strHwInfo += share[strIndex];
                }
            }
            catch (Exception ex)
            {
                // show some error message
            }*/
            //string strMAC = getManagementInfo("Win32_NetworkAdapterConfiguration", "MacAddress");
            //string strProcessorId = getManagementInfo("Win32_Processor", "ProcessorId");
            //---------------------------------------------------
            /*String serial1 = "";
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard");
                ManagementObjectCollection moc = mos.Get();

                foreach (ManagementObject mo in moc)
                {
                    serial1 = mo["SerialNumber"].ToString();
                }
            }
            catch (Exception)
            {
            }*/
            //---------------------------------------------------

            /*string wmiClass = "Win32_NetworkAdapterConfiguration";
            string wmiProperty = "MACAddress";
            string wmiMustBeTrue = "IPEnabled";

            string result3 = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc2 = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc2)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result3 == "")
                    {
                        try
                        {
                            result3 = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }*/
            //---------------------------------------------------
            /*
            string query = "SELECT * FROM Win32_BaseBoard";

            ManagementObjectSearcher searcher =

            new ManagementObjectSearcher(query);

            foreach (ManagementObject info in searcher.Get()) { 

                result = info.GetPropertyValue("SerialNumber").ToString();
                if (result.StartsWith("To be filled")) {
                    result = info.Path.Server.ToString();
                }
            }

            //TODO формируем пакет на запись серийного номера материнки
            
            bufsend[0] = 127;
            bufsend[1] = 0x10;
            bufsend[2] = (byte)255;
            bufsend[3] = 65;
            bufsend[4] = 0;
            bufsend[5] = 10;
            bufsend[6] = 20;
            int len = result.Length;
            for (int i = 0; i < 16; i++)
            {
                bufsend[7 + i] = 0;
            }

            for (int i = 0; i < len; i++)
            {
                char[] ch =  new char[1];
                ch = result.ToCharArray(i, 1);
                bufsend[7 + i] = Convert.ToByte(ch[0]);
            }

            //расчитываем CRC как в Modbus
            short Crc = (short)CalcFrameCrc(bufsend, (short)(20 + 7));
            bufsend[7 + 20 + 0] = (byte)(Crc & 0xFF);
            bufsend[7 + 20 + 1] = (byte)(Crc >> 8);

            for (int i = 0; i < 29; i++)
            {
                TransmitByte(bufsend[i]);                                                     // Передаем код функции
                System.Threading.Thread.Sleep(30);
            }
            
            System.Threading.Thread.Sleep(5000);
            */
            Povtor1: ;
            System.Threading.Thread.Sleep(200);
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
