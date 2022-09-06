using Modbus.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace TimBrowser.ApiImplementation.Communication
{
    public class StreamModbusAdapter : IStreamResource
    {
        public StreamModbusAdapter(Stream stream)
        {
            _stream = stream;
        }

        public const int READ_REQUEST_DELAY_MS = 150;
        public const int WRITE_REQUEST_DELAY_MS = 150;
        private const int INFINITE_TIMEOUT_VALUE = 0;

        private Stream _stream;
        private int _requestCounter = 0;
        public Action<string> StreamErrorAction;

        public void DiscardInBuffer()
        {
            try
            {
                _stream.Flush();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InfiniteTimeout
        {
            get { return INFINITE_TIMEOUT_VALUE; }
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            int d = 0;

            System.Threading.Thread.Sleep(READ_REQUEST_DELAY_MS/*+150*/);

            try
            {
                d = _stream.Read(buffer, offset, count);
            }
            catch (System.IO.IOException ioex)
            {
                if (StreamErrorAction != null)
                    StreamErrorAction(ioex.Message);

                throw new Exception(ioex.Message);
            }
            catch (Exception ex)
            {
                if (StreamErrorAction != null)
                    StreamErrorAction(ex.Message);

                throw new Exception(ex.Message);
            }

            return d;
        }

        public int ReadTimeout
        {
            get { return _stream.ReadTimeout; }
            set { _stream.ReadTimeout = value; }
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            System.Threading.Thread.Sleep(WRITE_REQUEST_DELAY_MS);

            try
            {                
                _stream.Write(buffer, offset, count);
            }
            catch (System.IO.IOException ioex)
            {
                if (StreamErrorAction != null)
                    StreamErrorAction(ioex.Message);

                throw new Exception(ioex.Message);
            }
            catch (Exception ex)
            {
                if (StreamErrorAction != null)
                    StreamErrorAction(ex.Message);

                throw new Exception(ex.Message);
            }
            
        }

        public int WriteTimeout
        {
            get { return _stream.WriteTimeout; }
            set { _stream.WriteTimeout = value; }
        }

        public void Dispose()
        {
            _stream.Close();
            _stream.Dispose();
        }

    }
}