using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace NetSocket
{
    public class MsgStream
    {
        //是否使用默认编码格式
        private static bool is_default_char = false;


        //是否转换大小端
        private bool bool_ = false;
      
        private int _readindex;
        private List<byte[]> list;
        public MsgStream()
        {
            list = new List<byte[]>();
            _readindex = 0;
        }

        /// <summary>
        /// 是否转换大小端
        /// </summary>
        /// <param name="isReverse"></param>
        public void SetReverse(bool isReverse)
        {
            this.bool_ = isReverse;
        }

        //ntoh
        private Int16 ntoh16(Int16 value)
        {
            if (this.bool_ == false)
            {
                return value;
            }
            byte[] p = BitConverter.GetBytes(value);
            Array.Reverse(p);
            return BitConverter.ToInt16(p, 0);
        }

        /// <summary>
        /// 大小端转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private Int32 ntoh32(Int32 value)
        {
            if (this.bool_ == false)
            {
                return value;
            }
            byte[] p = BitConverter.GetBytes(value);
            Array.Reverse(p);
            return BitConverter.ToInt32(p, 0);
        }
        private Single ntoh32(Single value)
        {
            if (this.bool_ == false)
            {
                return value;
            }
            byte[] p = BitConverter.GetBytes(value);
            Array.Reverse(p);
            return BitConverter.ToSingle(p, 0);
        }
        private double ntoh32(double value)
        {
            if (this.bool_ == false)
            {
                return value;
            }
            byte[] p = BitConverter.GetBytes(value);
            Array.Reverse(p);
            return BitConverter.ToDouble(p, 0);
        }
        private long ntoh64(long value)
        {
            if (this.bool_ == false)
            {
                return value;
            }
            byte[] p = BitConverter.GetBytes(value);
            Array.Reverse(p);
            return BitConverter.ToInt64(p, 0);
        }

        private ulong ntohu64(ulong value)
        {
            if (this.bool_ == false)
            {
                return value;
            }
            byte[] p = BitConverter.GetBytes(value);
            Array.Reverse(p);
            return BitConverter.ToUInt64(p, 0);
        }

        public void WriteBytes(bool value)
        {
            byte[] buff = BitConverter.GetBytes(value);
            byte[] bu = new byte[1];
            Array.Copy(buff, 0, bu, 0, 1);
            list.Add(bu);
        }

        public void InsertBytes(short value, int index)
        {
      
            value = ntoh16(value);
            byte[] bu = BitConverter.GetBytes(value);
            list[index] = bu;
        }
        public void WriteBytes(char value)
        {
            byte[] buff = BitConverter.GetBytes(value);
            byte[] bu = new byte[1];
            Array.Copy(buff, 0, bu, 0, 1);
            list.Add(bu);
        }

        public void WriteBytes(byte value)
        {
            byte[] bu = new byte[1];
            bu[0] = value;
            list.Add(bu);
        }
        public void WriteBytes(sbyte value)
        {
            byte[] bu = new byte[1];
            bu[0] =  Convert.ToByte(value);
            list.Add(bu);
        }
        public void WriteBytes(double value)
        {
            value = ntoh32(value);
            byte[] bu = BitConverter.GetBytes(value);
            list.Add(bu);
        }

        public void WriteBytes(float value)
        {
            value = ntoh32(value);
            byte[] bu = BitConverter.GetBytes(value);
            list.Add(bu);
        }

        public void WriteBytes(int value)
        {
            value = ntoh32(value);
            byte[] bu = BitConverter.GetBytes(value);
            list.Add(bu);
        }
        public void WriteBytes(ulong value)
        {
            value = ntohu64(value);
            byte[] bu = BitConverter.GetBytes(value);
            list.Add(bu);
        }
        public void WriteBytes(long value)
        {
            value = ntoh64(value);
            byte[] bu = BitConverter.GetBytes(value);
            list.Add(bu);
        }

        public void WriteBytes(short value)
        {
            value = ntoh16(value);
            byte[] bu = BitConverter.GetBytes(value);
            list.Add(bu);
        }
        public void WriteBytes(uint value)
        {
            value = (uint)ntoh32((int)value);
            byte[] bu = BitConverter.GetBytes(value);
            list.Add(bu);
        }
        public void strWriteBytes(ulong value)
        {
            value = (ulong)ntoh64((Int64)value);
            byte[] bu = BitConverter.GetBytes(value);
            list.Add(bu);
        }
        public void WriteBytes(ushort value)
        {
            value = (ushort)ntoh16((Int16)value);
            byte[] bu = BitConverter.GetBytes(value);
            list.Add(bu);
        }

        public void WriteBytes(string value)
        {
            int length = 0;
            byte[] bu;
            if (MsgStream.is_default_char == true)
            {
                bu = System.Text.Encoding.Default.GetBytes(value);
            }
            else
            {
                //byte[] utf8buf = System.Text.Encoding.UTF8.GetBytes(value);
                //bu = GUconvert.Utf8toGb2312(utf8buf);
                bu = System.Text.Encoding.UTF8.GetBytes(value);
            }
            length = bu.Length + 1;
            if (length > 4095)
            {
                length = 0;
            }
            WriteBytes((short)((length << 4) | 0x03));
            list.Add(bu);
            WriteBlock();
        }

        public void WriteBytesText(string value)
        {

            byte[] bu;
            if (MsgStream.is_default_char == true)
            {
                bu = System.Text.Encoding.Default.GetBytes(value);
            }
            else
            {
                //byte[] utf8buf = System.Text.Encoding.UTF8.GetBytes(value);
                //bu = GUconvert.Utf8toGb2312(utf8buf);
                bu = System.Text.Encoding.UTF8.GetBytes(value);
            }
            //WriteBytes((short)bu.Length);
            list.Add(bu);
        }

        public void WriteBlock()
        {
            byte[] bu = new byte[1];
            bu[0] = 0;
            list.Add(bu);
        }

        public void WriteByte(int value)
        {
            byte[] bu = new byte[1];
            bu[0] = (byte)value;
            list.Add(bu);
        }

        public void WriteBytes(byte[] value)
        {
            list.Add(value);
        }

        public uint GetPosition()
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            for (int i = 0; i < this.list.Count; i++)
            {
                byte[] bu = this.list[i];
                ms.Write(bu, 0, bu.Length);
            }
            return (uint)ms.Position;
        }

        public int GetCanRead(byte[] msg)
        {
            return msg.Length - _readindex;
        }
       

        //读取流
        public void Read(byte[] msg, ref bool type)
        {
            int bytesize = 1;
            byte[] messtype_buf = new byte[bytesize + 1];
            Array.Copy(msg, _readindex, messtype_buf, 0, bytesize);
            type = (bool)BitConverter.ToBoolean(messtype_buf, 0);
            _readindex += bytesize;
        }

        public void Read(byte[] msg, ref char type)
        {
            int bytesize = 1;
            byte[] messtype_buf = new byte[2];
            Array.Copy(msg, _readindex, messtype_buf, 0, bytesize);
            type = (char)BitConverter.ToChar(messtype_buf, 0);
            _readindex += bytesize;
        }

        public void Read(byte[] msg, ref byte type)
        {
            int bytesize = 1;
            byte[] messtype_buf = new byte[2];
            Array.Copy(msg, _readindex, messtype_buf, 0, bytesize);
            type = messtype_buf[0];
            _readindex += bytesize;
        }

        public void Read(byte[] msg, int length, ref byte[] type)
        {
            byte[] messtype_buf = new byte[length];
            Array.Copy(msg, _readindex, messtype_buf, 0, length);
            type = messtype_buf;
            _readindex += length;
        }

        public void Read(byte[] msg,int offset,int length, ref byte[] data)
        {
            byte[] messtype_buf = new byte[length];
            Array.Copy(msg, offset, messtype_buf, 0, length);
            data = messtype_buf;
            _readindex += length;
        }

        public void Read(byte[] msg, ref sbyte type)
        {
            byte tmp = msg[_readindex + 1];
            if (tmp < 128)
            {
                type = (sbyte) tmp;
            }
            else
            {
                type = (sbyte) (tmp - 256);
            }
         
            int bytesize = 1;
            //byte[] messtype_buf = new byte[2];
            //Array.Copy(msg, _readindex, messtype_buf, 0, bytesize);
            //type = Convert.ToSByte(messtype_buf[0]);
            _readindex += bytesize;
        }

        public void Read(byte[] msg, ref double type)
        {
            int bytesize = 8;
            byte[] messtype_buf = new byte[bytesize];
            Array.Copy(msg, _readindex, messtype_buf, 0, bytesize);
            type = BitConverter.ToDouble(messtype_buf, 0);

            type = ntoh32(type);
            _readindex += bytesize;
        }

        public void Read(byte[] msg, ref Int16 type)
        {
            int bytesize = 2;
            byte[] messtype_buf = new byte[bytesize];
            Array.Copy(msg, _readindex, messtype_buf, 0, bytesize);
            type = (Int16)BitConverter.ToInt16(messtype_buf, 0);

            type = ntoh16(type);
            _readindex += bytesize;
        }

        public void Read(byte[] msg, int startindex, ref Int16 type)
        {
            int bytesize = 2;
            byte[] messtype_buf = new byte[bytesize];
            Array.Copy(msg, _readindex + startindex, messtype_buf, 0, bytesize);
            type =BitConverter.ToInt16(messtype_buf, 0);

            type = ntoh16(type);
            _readindex += bytesize;
        }

        public void ReadInt(byte[] msg, ref Int32 type)
        {
            int bytesize = 4;
            byte[] messtype_buf = new byte[bytesize];
            Array.Copy(msg, _readindex, messtype_buf, 0, bytesize);
            type =BitConverter.ToInt32(messtype_buf, 0);

            type = ntoh32(type);
            _readindex += bytesize;

        }

        public void Read(byte[] msg, ref long type)
        {
            int bytesize = 8;
            byte[] messtype_buf = new byte[bytesize];
            Array.Copy(msg, _readindex, messtype_buf, 0, bytesize);
            type = (long)BitConverter.ToInt64(messtype_buf, 0);

            type = ntoh64(type);
            _readindex += bytesize;

        }


        public void Read(byte[] msg, ref Single type)
        {
            int bytesize = 4;
            byte[] messtype_buf = new byte[bytesize];
            Array.Copy(msg, _readindex, messtype_buf, 0, bytesize);
            type = BitConverter.ToSingle(messtype_buf, 0);
            type = ntoh32(type);
            _readindex += bytesize;

        }

        public void Read(byte[] msg, ref String type)
        {
            int bytesize = (int)msg.Length;
            byte[] messtype_buf = new byte[bytesize];
            Array.Copy(msg, _readindex, messtype_buf, 0, bytesize);
            if (is_default_char == true)
            {
                type = System.Text.Encoding.Default.GetString(messtype_buf);}
            else
            {
                type = System.Text.Encoding.UTF8.GetString(messtype_buf);
            }

            _readindex += bytesize;
        }

        public void ReadStr(byte[] msg, ref string type)
        {
            int count = _readindex;

            while (count <= msg.Length)
            {
                if (Convert.ToInt32(msg.GetValue(count)) == 0)
                {
                    byte[] messtype_buf = new byte[count - _readindex];
                    Array.Copy(msg, _readindex, messtype_buf, 0, count - _readindex);
                    _readindex = count + 1;
                    type=Encoding.UTF8.GetString(messtype_buf);
                    return;
                }
                count++;
            }
        }

        public void Read(byte[] msg, uint bytesize,ref String type)
        {
            uint realLen = bytesize;
            for (uint i = 0; i < bytesize; i++)
			{
			    if (msg[_readindex + i] == 0)
			    {
			        realLen = i;
			        break;
			    }
			}
            if (is_default_char == true)
            {
                type = Encoding.Default.GetString(msg, _readindex, (int)realLen);
            }
            else
            {
                type = Encoding.UTF8.GetString(msg, _readindex, (int)realLen);               
            }
            _readindex += (int)bytesize;
        }

        public void Read(byte[] msg, ref UInt16 type)
        {
            int bytesize = 2;
            byte[] messtype_buf = new byte[bytesize];
            Array.Copy(msg, _readindex, messtype_buf, 0, bytesize);
            type = (UInt16)BitConverter.ToUInt16(messtype_buf, 0);
            type = (UInt16)ntoh16((Int16)type);
            _readindex += bytesize;


        }
        //读取int
        /*
        public int ReadInt(byte[] msg)
        {
            int bytesize = 4;
            byte[] messtype_buf = new byte[bytesize];
            Array.Copy(msg, _readindex, messtype_buf, 0, bytesize);
            Array.Reverse(messtype_buf);
            int value = BitConverter.ToInt32(messtype_buf, 0);
            return value;

        }
        */
        public void Read(byte[] msg, ref UInt32 type)
        {
            int bytesize = 4;
            byte[] messtype_buf = new byte[bytesize];
            Array.Copy(msg, _readindex, messtype_buf, 0, bytesize);
            type = (UInt32)BitConverter.ToUInt32(messtype_buf, 0);

            type = (UInt32)ntoh32((Int32)type);
            _readindex += bytesize;

        }
        public void Read(byte[] msg, ref UInt64 type)
        {
            int bytesize = 8;
            byte[] messtype_buf = new byte[bytesize];
            Array.Copy(msg, _readindex, messtype_buf, 0, bytesize);
            type = (UInt64)BitConverter.ToUInt64(messtype_buf, 0);

            type = (UInt64)ntoh64((Int64)type);
            _readindex += bytesize;

        }

        public int bytesAvailable(int length)
        {
            return length - _readindex;
        }

        public void setReadIndex(int index)
        {
            _readindex = index;
        }

        public bool IsAvailable(int length)
        {
            if (_readindex >= length || _readindex == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public byte[] OutByte()
        {

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            for (int i = 0; i < this.list.Count; i++)
            {
                byte[] bu = this.list[i];
                ms.Write(bu, 0, bu.Length);
            }
            list.Clear();
            byte[] tmpbuf;
            if (ms.Length > 0)
            {
                tmpbuf = ms.ToArray();
            }
            else
            {
            
                tmpbuf=new byte[0];
            }
            ms.Close();
            return tmpbuf;
        }

        public void Dispose()
        {
            list.Clear();
        }

        public uint position
        {
            get { return (uint)_readindex; }
            set { _readindex = (int) value; }
        }
    }
}
