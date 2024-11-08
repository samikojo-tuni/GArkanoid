using System;
using System.IO;
using UnityEngine;

namespace GA.GArkanoid.Persistance
{
	public class BinarySaver
	{
		private BinaryReader _reader;
		private BinaryWriter _writer;
		private FileStream _fileStream;

		#region Initializations

		public bool PrepareRead(string filePath)
		{
			if (!File.Exists(filePath))
			{
				return false;
			}

			try
			{
				_fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read);
				_reader = new BinaryReader(_fileStream);
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				return false;
			}

			return true;
		}

		public bool PrepareWrite(string filePath)
		{
			try
			{
				// Returns the directory's path on the disk.
				string directory = Path.GetDirectoryName(filePath);
				if (!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}

				_fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write);
				_writer = new BinaryWriter(_fileStream);
			}
			catch(Exception e)
			{
				Debug.LogException(e);
				return false;
			}

			return true;
		}

		public void FinalizeRead()
		{
			_reader.Close();
			_fileStream.Close();

			_reader = null;
			_fileStream = null;
		}

		public void FinalizeWrite()
		{
			_writer.Flush();

			_writer.Close();
			_fileStream.Close();

			_writer = null;
			_fileStream = null;
		}

		#endregion Initializations

		#region Reading

		public bool ReadBool()
		{
			return _reader.ReadBoolean();
		}

		public int ReadInt32()
		{
			return _reader.ReadInt32();
		}

		public long ReadInt64()
		{
			return _reader.ReadInt64();
		}

		public short ReadInt16()
		{
			return _reader.ReadInt16();
		}

		public byte ReadByte()
		{
			return _reader.ReadByte();
		}

		public float ReadFloat()
		{
			return _reader.ReadSingle();
		}

		public double ReadDouble()
		{
			return _reader.ReadDouble();
		}

		public string ReadString()
		{
			return _reader.ReadString();
		}

		public Vector2 ReadVector2()
		{
			float x = _reader.ReadSingle();
			float y = _reader.ReadSingle();

			return new Vector2(x, y);
		}

		public Vector3 ReadVector3()
		{
			float x = _reader.ReadSingle();
			float y = _reader.ReadSingle();
			float z = _reader.ReadSingle();

			return new Vector3(x, y, z);
		}

		public Quaternion ReadQuaternion()
		{
			float w = _reader.ReadSingle();
			float x = _reader.ReadSingle();
			float y = _reader.ReadSingle();
			float z = _reader.ReadSingle();

			return new Quaternion(x, y, z, w);
		}

		#endregion Reading

		#region Writing

		public void WriteBool(bool value)
		{
			_writer.Write(value);
		}

		public void WriteInt32(int value)
		{
			_writer.Write(value);
		}

		public void WriteInt64(long value)
		{
			_writer.Write(value);
		}

		public void WriteInt16(short value)
		{
			_writer.Write(value);
		}

		public void WriteByte(byte value)
		{
			_writer.Write(value);
		}

		public void WriteFloat(float value)
		{
			_writer.Write(value);
		}

		public void WriteDouble(double value)
		{
			_writer.Write(value);
		}

		public void WriteString(string value)
		{
			_writer.Write(value);
		}

		public void WriteVector2(Vector2 value)
		{
			_writer.Write(value.x);
			_writer.Write(value.y);
		}

		public void WriteVector3(Vector3 value)
		{
			_writer.Write(value.x);
			_writer.Write(value.y);
			_writer.Write(value.z);
		}

		public void WriteQuaternion(Quaternion value)
		{
			_writer.Write(value.w);
			_writer.Write(value.x);
			_writer.Write(value.y);
			_writer.Write(value.z);
		}

		#endregion Writing
	}
}