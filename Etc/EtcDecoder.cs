using System;
using System.Runtime.CompilerServices;

namespace Etc
{
	public class EtcDecoder
	{
		public void DecompressETC(byte[] input, int width, int height, byte[] output)
		{
			int bcw = (width + 3) / 4;
			int bch = (height + 3) / 4;
			int clen_last = (width + 3) % 4 + 1;
			int d = 0;
			for (int t = 0; t < bch; t++)
			{
				for (int s = 0; s < bcw; s++, d += 8)
				{
					DecodeEtc1Block(input, d);
					int clen = (s < bcw - 1 ? 4 : clen_last) * 4;
					for (int i = 0, y = height - t * 4 - 1; i < 4 && y >= 0; i++, y--)
					{
						Buffer.BlockCopy(m_buf, i * 4 * 4, output, y * 4 * width + s * 4 * 4, clen);
					}
				}
			}
		}

		public void DecompressETC2(byte[] input, int width, int height, byte[] output)
		{
			int bcw = (width + 3) / 4;
			int bch = (height + 3) / 4;
			int clen_last = (width + 3) % 4 + 1;
			int d = 0;
			for (int t = 0; t < bch; t++)
			{
				for (int s = 0; s < bcw; s++, d += 8)
				{
					DecodeEtc2Block(input, d);
					int clen = (s < bcw - 1 ? 4 : clen_last) * 4;
					for (int i = 0, y = height - t * 4 - 1; i < 4 && y >= 0; i++, y--)
					{
						Buffer.BlockCopy(m_buf, i * 4 * 4, output, y * 4 * width + s * 4 * 4, clen);
					}
				}
			}
		}

		public void DecompressETC2A1(byte[] input, int width, int height, byte[] output)
		{
			int bcw = (width + 3) / 4;
			int bch = (height + 3) / 4;
			int clen_last = (width + 3) % 4 + 1;
			int d = 0;
			for (int t = 0; t < bch; t++)
			{
				for (int s = 0; s < bcw; s++, d += 8)
				{
					DecodeEtc2a1Block(input, d);
					int clen = (s < bcw - 1 ? 4 : clen_last) * 4;
					for (int i = 0, y = height - t * 4 - 1; i < 4 && y >= 0; i++, y--)
					{
						Buffer.BlockCopy(m_buf, i * 4 * 4, output, y * 4 * width + s * 4 * 4, clen);
					}
				}
			}
		}

		public void DecompressETC2A8(byte[] input, int width, int height, byte[] output)
		{
			int bcw = (width + 3) / 4;
			int bch = (height + 3) / 4;
			int clen_last = (width + 3) % 4 + 1;
			int d = 0;
			for (int t = 0; t < bch; t++)
			{
				for (int s = 0; s < bcw; s++, d += 16)
				{
					DecodeEtc2Block(input, d + 8);
					DecodeEtc2a8Block(input, d);
					int clen = (s < bcw - 1 ? 4 : clen_last) * 4;
					for (int i = 0, y = height - t * 4 - 1; i < 4 && y >= 0; i++, y--)
					{
						Buffer.BlockCopy(m_buf, i * 4 * 4, output, y * 4 * width + s * 4 * 4, clen);
					}
				}
			}
		}

		public void DecompressEACRUnsigned(byte[] input, int width, int height, byte[] output)
		{
			int bcw = (width + 3) / 4;
			int bch = (height + 3) / 4;
			int clen_last = (width + 3) % 4 + 1;
			int d = 0;
			for (int i = 0; i < 16; i++)
			{
				m_buf8[i * 4 + 0] = 0;
				m_buf8[i * 4 + 1] = 0;
				m_buf8[i * 4 + 2] = 0;
				m_buf8[i * 4 + 3] = 0xFF;
			}
			for (int t = 0; t < bch; t++)
			{
				for (int s = 0; s < bcw; s++, d += 8)
				{
					DecodeEacUnsignedBlock(input, d, 2);
					int clen = (s < bcw - 1 ? 4 : clen_last) * 4;
					for (int i = 0, y = height - t * 4 - 1; i < 4 && y >= 0; i++, y--)
					{
						Buffer.BlockCopy(m_buf8, i * 4 * 4, output, y * 4 * width + s * 4 * 4, clen);
					}
				}
			}
		}

		public void DecompressEACRSigned(byte[] input, int width, int height, byte[] output)
		{
			int bcw = (width + 3) / 4;
			int bch = (height + 3) / 4;
			int clen_last = (width + 3) % 4 + 1;
			int d = 0;
			for (int i = 0; i < 16; i++)
			{
				m_buf8[i * 4 + 0] = 0;
				m_buf8[i * 4 + 1] = 0;
				m_buf8[i * 4 + 2] = 0;
				m_buf8[i * 4 + 3] = 0xFF;
			}
			for (int t = 0; t < bch; t++)
			{
				for (int s = 0; s < bcw; s++, d += 8)
				{
					DecodeEacSignedBlock(input, d, 2);
					int clen = (s < bcw - 1 ? 4 : clen_last) * 4;
					for (int i = 0, y = height - t * 4 - 1; i < 4 && y >= 0; i++, y--)
					{
						Buffer.BlockCopy(m_buf8, i * 4 * 4, output, y * 4 * width + s * 4 * 4, clen);
					}
				}
			}
		}

		public void DecompressEACRGUnsigned(byte[] input, int width, int height, byte[] output)
		{
			int bcw = (width + 3) / 4;
			int bch = (height + 3) / 4;
			int clen_last = (width + 3) % 4 + 1;
			int d = 0;
			for (int i = 0; i < 16; i++)
			{
				m_buf8[i * 4 + 0] = 0;
				m_buf8[i * 4 + 1] = 0;
				m_buf8[i * 4 + 2] = 0;
				m_buf8[i * 4 + 3] = 0xFF;
			}
			for (int t = 0; t < bch; t++)
			{
				for (int s = 0; s < bcw; s++, d += 16)
				{
					DecodeEacUnsignedBlock(input, d + 0, 2);
					DecodeEacUnsignedBlock(input, d + 8, 1);
					int clen = (s < bcw - 1 ? 4 : clen_last) * 4;
					for (int i = 0, y = height - t * 4 - 1; i < 4 && y >= 0; i++, y--)
					{
						Buffer.BlockCopy(m_buf8, i * 4 * 4, output, y * 4 * width + s * 4 * 4, clen);
					}
				}
			}
		}

		public void DecompressEACRGSigned(byte[] input, int width, int height, byte[] output)
		{
			int bcw = (width + 3) / 4;
			int bch = (height + 3) / 4;
			int clen_last = (width + 3) % 4 + 1;
			int d = 0;
			for (int i = 0; i < 16; i++)
			{
				m_buf8[i * 4 + 0] = 0;
				m_buf8[i * 4 + 1] = 0;
				m_buf8[i * 4 + 2] = 0;
				m_buf8[i * 4 + 3] = 0xFF;
			}
			for (int t = 0; t < bch; t++)
			{
				for (int s = 0; s < bcw; s++, d += 16)
				{
					DecodeEacSignedBlock(input, d + 0, 2);
					DecodeEacSignedBlock(input, d + 8, 1);
					int clen = (s < bcw - 1 ? 4 : clen_last) * 4;
					for (int i = 0, y = height - t * 4 - 1; i < 4 && y >= 0; i++, y--)
					{
						Buffer.BlockCopy(m_buf8, i * 4 * 4, output, y * 4 * width + s * 4 * 4, clen);
					}
				}
			}
		}

		private void DecodeEtc1Block(byte[] data, int offset)
		{
			m_code[0] = (byte)(data[offset + 3] >> 5);
			m_code[1] = (byte)(data[offset + 3] >> 2 & 7);
			int ti = data[offset + 3] & 1;
			if ((data[offset + 3] & 2) != 0)
			{
				unchecked
				{
					m_c[0,0] = (byte)(data[offset + 0] & 0xf8);
					m_c[0,1] = (byte)(data[offset + 1] & 0xf8);
					m_c[0,2] = (byte)(data[offset + 2] & 0xf8);
					m_c[1,0] = (byte)(m_c[0, 0] + (data[offset + 0] << 3 & 0x18) - (data[offset + 0] << 3 & 0x20));
					m_c[1,1] = (byte)(m_c[0, 1] + (data[offset + 1] << 3 & 0x18) - (data[offset + 1] << 3 & 0x20));
					m_c[1,2] = (byte)(m_c[0, 2] + (data[offset + 2] << 3 & 0x18) - (data[offset + 2] << 3 & 0x20));
					m_c[0,0] |= (byte)(m_c[0, 0] >> 5);
					m_c[0,1] |= (byte)(m_c[0, 1] >> 5);
					m_c[0,2] |= (byte)(m_c[0, 2] >> 5);
					m_c[1,0] |= (byte)(m_c[1, 0] >> 5);
					m_c[1,1] |= (byte)(m_c[1, 1] >> 5);
					m_c[1,2] |= (byte)(m_c[1, 2] >> 5);
				}
			}
			else
			{
				unchecked
				{
					m_c[0, 0] = (byte)(data[offset + 0] & 0xf0 | data[offset + 0] >> 4);
					m_c[1, 0] = (byte)(data[offset + 0] & 0x0f | data[offset + 0] << 4);
					m_c[0, 1] = (byte)(data[offset + 1] & 0xf0 | data[offset + 1] >> 4);
					m_c[1, 1] = (byte)(data[offset + 1] & 0x0f | data[offset + 1] << 4);
					m_c[0, 2] = (byte)(data[offset + 2] & 0xf0 | data[offset + 2] >> 4);
					m_c[1, 2] = (byte)(data[offset + 2] & 0x0f | data[offset + 2] << 4);
				}
			}

			ushort j = (ushort)(data[offset + 6] << 8 | data[offset + 7]);
			ushort k = (ushort)(data[offset + 4] << 8 | data[offset + 5]);
			for (int i = 0; i < 16; i++, j >>= 1, k >>= 1)
			{
				byte s = Etc1SubblockTable[ti, i];
				int index = k << 1 & 2 | j & 1;
				int m = Etc1ModifierTable[m_code[s], index];
				m_buf[WriteOrderTable[i]] = ApplicateColor(m_c, s, m);
			}
		}

		private void DecodeEtc2Block(byte[] data, int offset)
		{
			ushort j = (ushort)(data[offset + 6] << 8 | data[offset + 7]);
			ushort k = (ushort)(data[offset + 4] << 8 | data[offset + 5]);

			if ((data[offset + 3] & 2) != 0)
			{
				byte r = (byte)(data[offset + 0] & 0xf8);
				short dr = (short)((data[offset + 0] << 3 & 0x18) - (data[offset + 0] << 3 & 0x20));
				byte g = (byte)(data[offset + 1] & 0xf8);
				short dg = (short)((data[offset + 1] << 3 & 0x18) - (data[offset + 1] << 3 & 0x20));
				byte b = (byte)(data[offset + 2] & 0xf8);
				short db = (short)((data[offset + 2] << 3 & 0x18) - (data[offset + 2] << 3 & 0x20));
				if (r + dr < 0 || r + dr > 255)
				{
					// T
					unchecked
					{
						m_c[0, 0] = (byte)(data[offset + 0] << 3 & 0xc0 | data[offset + 0] << 4 & 0x30 | data[offset + 0] >> 1 & 0xc | data[offset + 0] & 3);
						m_c[0, 1] = (byte)(data[offset + 1] & 0xf0 | data[offset + 1] >> 4);
						m_c[0, 2] = (byte)(data[offset + 1] & 0x0f | data[offset + 1] << 4);
						m_c[1, 0] = (byte)(data[offset + 2] & 0xf0 | data[offset + 2] >> 4);
						m_c[1, 1] = (byte)(data[offset + 2] & 0x0f | data[offset + 2] << 4);
						m_c[1, 2] = (byte)(data[offset + 3] & 0xf0 | data[offset + 3] >> 4);
					}
					byte d = Etc2DistanceTable[data[offset + 3] >> 1 & 6 | data[offset + 3] & 1];
					uint[] color_set =
					{
						ApplicateColorRaw(m_c, 0),
						ApplicateColor(m_c, 1, d),
						ApplicateColorRaw(m_c, 1),
						ApplicateColor(m_c, 1, -d)
					};
					for (int i = 0; i < 16; i++, j >>= 1, k >>= 1)
					{
						m_buf[WriteOrderTable[i]] = color_set[k << 1 & 2 | j & 1];
					}
				}
				else if (g + dg < 0 || g + dg > 255)
				{
					// H
					unchecked
					{
						m_c[0, 0] = (byte)(data[offset + 0] << 1 & 0xf0 | data[offset + 0] >> 3 & 0xf);
						m_c[0, 1] = (byte)(data[offset + 0] << 5 & 0xe0 | data[offset + 1] & 0x10);
						m_c[0, 1] |= (byte)(m_c[0, 1] >> 4);
						m_c[0, 2] = (byte)(data[offset + 1] & 8 | data[offset + 1] << 1 & 6 | data[offset + 2] >> 7);
						m_c[0, 2] |= (byte)(m_c[0, 2] << 4);
						m_c[1, 0] = (byte)(data[offset + 2] << 1 & 0xf0 | data[offset + 2] >> 3 & 0xf);
						m_c[1, 1] = (byte)(data[offset + 2] << 5 & 0xe0 | data[offset + 3] >> 3 & 0x10);
						m_c[1, 1] |= (byte)(m_c[1, 1] >> 4);
						m_c[1, 2] = (byte)(data[offset + 3] << 1 & 0xf0 | data[offset + 3] >> 3 & 0xf);
					}
					int di = data[offset + 3] & 4 | data[offset + 3] << 1 & 2;
					if (m_c[0, 0] > m_c[1, 0] || (m_c[0, 0] == m_c[1, 0] && (m_c[0, 1] > m_c[1, 1] || (m_c[0, 1] == m_c[1, 1] && m_c[0, 2] >= m_c[1, 2]))))
					{
						++di;
					}
					byte d = Etc2DistanceTable[di];
					uint[] color_set =
					{
						ApplicateColor(m_c, 0, d),
						ApplicateColor(m_c, 0, -d),
						ApplicateColor(m_c, 1, d),
						ApplicateColor(m_c, 1, -d)
					};
					for (int i = 0; i < 16; i++, j >>= 1, k >>= 1)
					{
						m_buf[WriteOrderTable[i]] = color_set[k << 1 & 2 | j & 1];
					}
				}
				else if (b + db < 0 || b + db > 255)
				{
					// planar
					unchecked
					{
						m_c[0, 0] = (byte)(data[offset + 0] << 1 & 0xfc | data[offset + 0] >> 5 & 3);
						m_c[0, 1] = (byte)(data[offset + 0] << 7 & 0x80 | data[offset + 1] & 0x7e | data[offset + 0] & 1);
						m_c[0, 2] = (byte)(data[offset + 1] << 7 & 0x80 | data[offset + 2] << 2 & 0x60 | data[offset + 2] << 3 & 0x18 | data[offset + 3] >> 5 & 4);
						m_c[0, 2] |= (byte)(m_c[0, 2] >> 6);
						m_c[1, 0] = (byte)(data[offset + 3] << 1 & 0xf8 | data[offset + 3] << 2 & 4 | data[offset + 3] >> 5 & 3);
						m_c[1, 1] = (byte)(data[offset + 4] & 0xfe | data[offset + 4] >> 7);
						m_c[1, 2] = (byte)(data[offset + 4] << 7 & 0x80 | data[offset + 5] >> 1 & 0x7c);
						m_c[1, 2] |= (byte)(m_c[1, 2] >> 6);
						m_c[2, 0] = (byte)(data[offset + 5] << 5 & 0xe0 | data[offset + 6] >> 3 & 0x1c | data[offset + 5] >> 1 & 3);
						m_c[2, 1] = (byte)(data[offset + 6] << 3 & 0xf8 | data[offset + 7] >> 5 & 0x6 | data[offset + 6] >> 4 & 1);
						m_c[2, 2] = (byte)(data[offset + 7] << 2 | data[offset + 7] >> 4 & 3);
					}
					for (int y = 0, i = 0; y < 4; y++)
					{
						for (int x = 0; x < 4; x++, i++)
						{
							int ri = Clamp255((x * (m_c[1, 0] - m_c[0, 0]) + y * (m_c[2, 0] - m_c[0, 0]) + 4 * m_c[0, 0] + 2) >> 2);
							int gi = Clamp255((x * (m_c[1, 1] - m_c[0, 1]) + y * (m_c[2, 1] - m_c[0, 1]) + 4 * m_c[0, 1] + 2) >> 2);
							int bi = Clamp255((x * (m_c[1, 2] - m_c[0, 2]) + y * (m_c[2, 2] - m_c[0, 2]) + 4 * m_c[0, 2] + 2) >> 2);
							m_buf[i] = Color(ri, gi, bi, 255);
						}
					}
				}
				else
				{
					// differential
					byte[] code = { (byte)(data[offset + 3] >> 5), (byte)(data[offset + 3] >> 2 & 7) };
					int ti = data[offset + 3] & 1;
					unchecked
					{
						m_c[0, 0] = (byte)(r | r >> 5);
						m_c[0, 1] = (byte)(g | g >> 5);
						m_c[0, 2] = (byte)(b | b >> 5);
						m_c[1, 0] = (byte)(r + dr);
						m_c[1, 1] = (byte)(g + dg);
						m_c[1, 2] = (byte)(b + db);
						m_c[1, 0] |= (byte)(m_c[1, 0] >> 5);
						m_c[1, 1] |= (byte)(m_c[1, 1] >> 5);
						m_c[1, 2] |= (byte)(m_c[1, 2] >> 5);
					}
					for (int i = 0; i < 16; i++, j >>= 1, k >>= 1)
					{
						byte s = Etc1SubblockTable[ti, i];
						int index = k << 1 & 2 | j & 1;
						int m = Etc1ModifierTable[code[s], index];
						m_buf[WriteOrderTable[i]] = ApplicateColor(m_c, s, m);
					}
				}
			}
			else
			{
				// individual
				byte[] code = { (byte)(data[offset + 3] >> 5), (byte)(data[offset + 3] >> 2 & 7) };
				int ti = data[offset + 3] & 1;
				unchecked
				{
					m_c[0, 0] = (byte)(data[offset + 0] & 0xf0 | data[offset + 0] >> 4);
					m_c[1, 0] = (byte)(data[offset + 0] & 0x0f | data[offset + 0] << 4);
					m_c[0, 1] = (byte)(data[offset + 1] & 0xf0 | data[offset + 1] >> 4);
					m_c[1, 1] = (byte)(data[offset + 1] & 0x0f | data[offset + 1] << 4);
					m_c[0, 2] = (byte)(data[offset + 2] & 0xf0 | data[offset + 2] >> 4);
					m_c[1, 2] = (byte)(data[offset + 2] & 0x0f | data[offset + 2] << 4);
				}
				for (int i = 0; i < 16; i++, j >>= 1, k >>= 1)
				{
					byte s = Etc1SubblockTable[ti, i];
					int index = k << 1 & 2 | j & 1;
					int m = Etc1ModifierTable[code[s], index];
					m_buf[WriteOrderTable[i]] = ApplicateColor(m_c, s, m);
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void DecodeEtc2a1Block(byte[] data, int offset)
		{
			if ((data[offset + 3] & 2) != 0)
			{
				// Opaque
				DecodeEtc2Block(data, offset);
			}
			else
			{
				DecodeEtc2PunchThrowBlock(data, offset);
			}
		}

		private void DecodeEtc2PunchThrowBlock(byte[] data, int offset)
		{
			ushort j = (ushort)(data[offset + 6] << 8 | data[offset + 7]);
			ushort k = (ushort)(data[offset + 4] << 8 | data[offset + 5]);

			byte r = (byte)(data[offset + 0] & 0xf8);
			short dr = (short)((data[offset + 0] << 3 & 0x18) - (data[offset + 0] << 3 & 0x20));
			byte g = (byte)(data[offset + 1] & 0xf8);
			short dg = (short)((data[offset + 1] << 3 & 0x18) - (data[offset + 1] << 3 & 0x20));
			byte b = (byte)(data[offset + 2] & 0xf8);
			short db = (short)((data[offset + 2] << 3 & 0x18) - (data[offset + 2] << 3 & 0x20));
			if (r + dr < 0 || r + dr > 255)
			{
				// T (Etc2Block + mask for color)
				unchecked
				{
					m_c[0, 0] = (byte)(data[offset + 0] << 3 & 0xc0 | data[offset + 0] << 4 & 0x30 | data[offset + 0] >> 1 & 0xc | data[offset + 0] & 3);
					m_c[0, 1] = (byte)(data[offset + 1] & 0xf0 | data[offset + 1] >> 4);
					m_c[0, 2] = (byte)(data[offset + 1] & 0x0f | data[offset + 1] << 4);
					m_c[1, 0] = (byte)(data[offset + 2] & 0xf0 | data[offset + 2] >> 4);
					m_c[1, 1] = (byte)(data[offset + 2] & 0x0f | data[offset + 2] << 4);
					m_c[1, 2] = (byte)(data[offset + 3] & 0xf0 | data[offset + 3] >> 4);
				}
				byte d = Etc2DistanceTable[data[offset + 3] >> 1 & 6 | data[offset + 3] & 1];
				uint[] color_set =
				{
						ApplicateColorRaw(m_c, 0),
						ApplicateColor(m_c, 1, d),
						ApplicateColorRaw(m_c, 1),
						ApplicateColor(m_c, 1, (short)-d)
					};
				for (int i = 0; i < 16; i++, j >>= 1, k >>= 1)
				{
					int index = k << 1 & 2 | j & 1;
					uint mask = PunchthroughMaskTable[index];
					m_buf[WriteOrderTable[i]] = color_set[index] & mask;
				}
			}
			else if (g + dg < 0 || g + dg > 255)
			{
				// H (Etc2Block + mask for color)
				unchecked
				{
					m_c[0, 0] = (byte)(data[offset + 0] << 1 & 0xf0 | data[offset + 0] >> 3 & 0xf);
					m_c[0, 1] = (byte)(data[offset + 0] << 5 & 0xe0 | data[offset + 1] & 0x10);
					m_c[0, 1] |= (byte)(m_c[0, 1] >> 4);
					m_c[0, 2] = (byte)(data[offset + 1] & 8 | data[offset + 1] << 1 & 6 | data[offset + 2] >> 7);
					m_c[0, 2] |= (byte)(m_c[0, 2] << 4);
					m_c[1, 0] = (byte)(data[offset + 2] << 1 & 0xf0 | data[offset + 2] >> 3 & 0xf);
					m_c[1, 1] = (byte)(data[offset + 2] << 5 & 0xe0 | data[offset + 3] >> 3 & 0x10);
					m_c[1, 1] |= (byte)(m_c[1, 1] >> 4);
					m_c[1, 2] = (byte)(data[offset + 3] << 1 & 0xf0 | data[offset + 3] >> 3 & 0xf);
				}
				int di = data[offset + 3] & 4 | data[offset + 3] << 1 & 2;
				if (m_c[0, 0] > m_c[1, 0] || (m_c[0, 0] == m_c[1, 0] && (m_c[0, 1] > m_c[1, 1] || (m_c[0, 1] == m_c[1, 1] && m_c[0, 2] >= m_c[1, 2]))))
				{
					++di;
				}
				byte d = Etc2DistanceTable[di];
				uint[] color_set =
				{
						ApplicateColor(m_c, 0, d),
						ApplicateColor(m_c, 0, (short)-d),
						ApplicateColor(m_c, 1, d),
						ApplicateColor(m_c, 1, (short)-d)
					};
				for (int i = 0; i < 16; i++, j >>= 1, k >>= 1)
				{
					int index = k << 1 & 2 | j & 1;
					uint mask = PunchthroughMaskTable[index];
					m_buf[WriteOrderTable[i]] = color_set[index] & mask;
				}
			}
			else if (b + db < 0 || b + db > 255)
			{
				// planar (same as Etc2Block)
				unchecked
				{
					m_c[0, 0] = (byte)(data[offset + 0] << 1 & 0xfc | data[offset + 0] >> 5 & 3);
					m_c[0, 1] = (byte)(data[offset + 0] << 7 & 0x80 | data[offset + 1] & 0x7e | data[offset + 0] & 1);
					m_c[0, 2] = (byte)(data[offset + 1] << 7 & 0x80 | data[offset + 2] << 2 & 0x60 | data[offset + 2] << 3 & 0x18 | data[offset + 3] >> 5 & 4);
					m_c[0, 2] |= (byte)(m_c[0, 2] >> 6);
					m_c[1, 0] = (byte)(data[offset + 3] << 1 & 0xf8 | data[offset + 3] << 2 & 4 | data[offset + 3] >> 5 & 3);
					m_c[1, 1] = (byte)(data[offset + 4] & 0xfe | data[offset + 4] >> 7);
					m_c[1, 2] = (byte)(data[offset + 4] << 7 & 0x80 | data[offset + 5] >> 1 & 0x7c);
					m_c[1, 2] |= (byte)(m_c[1, 2] >> 6);
					m_c[2, 0] = (byte)(data[offset + 5] << 5 & 0xe0 | data[offset + 6] >> 3 & 0x1c | data[offset + 5] >> 1 & 3);
					m_c[2, 1] = (byte)(data[offset + 6] << 3 & 0xf8 | data[offset + 7] >> 5 & 0x6 | data[offset + 6] >> 4 & 1);
					m_c[2, 2] = (byte)(data[offset + 7] << 2 | data[offset + 7] >> 4 & 3);
				}
				for (int y = 0, i = 0; y < 4; y++)
				{
					for (int x = 0; x < 4; x++, i++)
					{
						int ri = Clamp255((x * (m_c[1, 0] - m_c[0, 0]) + y * (m_c[2, 0] - m_c[0, 0]) + 4 * m_c[0, 0] + 2) >> 2);
						int gi = Clamp255((x * (m_c[1, 1] - m_c[0, 1]) + y * (m_c[2, 1] - m_c[0, 1]) + 4 * m_c[0, 1] + 2) >> 2);
						int bi = Clamp255((x * (m_c[1, 2] - m_c[0, 2]) + y * (m_c[2, 2] - m_c[0, 2]) + 4 * m_c[0, 2] + 2) >> 2);
						m_buf[i] = Color(ri, gi, bi, 255);
					}
				}
			}
			else
			{
				// differential (Etc1Block + mask + specific mod table)

				m_code[0] = (byte)(data[offset + 3] >> 5);
				m_code[1] = (byte)(data[offset + 3] >> 2 & 7);
				int ti = data[offset + 3] & 1;
				unchecked
				{
					m_c[0, 0] = (byte)(data[offset + 0] & 0xf8);
					m_c[0, 1] = (byte)(data[offset + 1] & 0xf8);
					m_c[0, 2] = (byte)(data[offset + 2] & 0xf8);
					m_c[1, 0] = (byte)(m_c[0, 0] + (data[offset + 0] << 3 & 0x18) - (data[offset + 0] << 3 & 0x20));
					m_c[1, 1] = (byte)(m_c[0, 1] + (data[offset + 1] << 3 & 0x18) - (data[offset + 1] << 3 & 0x20));
					m_c[1, 2] = (byte)(m_c[0, 2] + (data[offset + 2] << 3 & 0x18) - (data[offset + 2] << 3 & 0x20));
					m_c[0, 0] |= (byte)(m_c[0, 0] >> 5);
					m_c[0, 1] |= (byte)(m_c[0, 1] >> 5);
					m_c[0, 2] |= (byte)(m_c[0, 2] >> 5);
					m_c[1, 0] |= (byte)(m_c[1, 0] >> 5);
					m_c[1, 1] |= (byte)(m_c[1, 1] >> 5);
					m_c[1, 2] |= (byte)(m_c[1, 2] >> 5);
				}

				for (int i = 0; i < 16; i++, j >>= 1, k >>= 1)
				{
					byte s = Etc1SubblockTable[ti, i];
					int index = k << 1 & 2 | j & 1;
					int m = PunchthroughModifierTable[m_code[s], index];
					uint mask = PunchthroughMaskTable[index];
					m_buf[WriteOrderTable[i]] = ApplicateColor(m_c, s, m) & mask;
				}
			}
		}
		
		private void DecodeEtc2a8Block(byte[] data, int offset)
		{
			int @base = data[offset + 0];
			int data1 = data[offset + 1];
			int mul = data1 >> 4;
			if (mul == 0)
			{
				for (int i = 0; i < 16; i++)
				{
					uint c = m_buf[WriteOrderTableRev[i]];
					c &= 0x00FFFFFF;
					c |= unchecked((uint)(@base << 24));
					m_buf[WriteOrderTableRev[i]] = c;
				}
			}
			else
			{
				int table = data1 & 0xF;
				ulong l = Get6SwapedBytes(data, offset);
				for (int i = 0; i < 16; i++, l >>= 3)
				{
					uint c = m_buf[WriteOrderTableRev[i]];
					c &= 0x00FFFFFF;
					c |= unchecked((uint)(Clamp255(@base + mul * Etc2AlphaModTable[table, l & 7]) << 24));
					m_buf[WriteOrderTableRev[i]] = c;
				}
			}
		}

		private void DecodeEacUnsignedBlock(byte[] data, int offset, int channel)
		{
			int @base = 4 + data[offset + 0] * 8;
			int data1 = data[offset + 1];
			int table = data1 & 0xF;
			int mul = (data1 >> 4) * 8;
			if (mul == 0)
			{
				mul = 1;
			}
			ulong l = Get6SwapedBytes(data, offset);
			DecodeEac11Block(channel, @base, table, mul, l);
		}

		private void DecodeEacSignedBlock(byte[] data, int offset, int channel)
		{
			int @base = 1023 + unchecked((sbyte)data[offset + 0]) * 8;
			int data1 = data[offset + 1];
			int table = data1 & 0xF;
			int mul = (data1 >> 4) * 8;
			if (mul == 0)
			{
				mul = 1;
			}
			ulong l = Get6SwapedBytes(data, offset);
			DecodeEac11Block(channel, @base, table, mul, l);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void DecodeEac11Block(int channel, int @base, int table, int mul, ulong l)
		{
			for (int i = 0; i < 16; i++, l >>= 3)
			{
				int val = @base + mul * Etc2AlphaModTable[table, l & 7];
				val = Clamp(val, 0, 2047);
				m_buf8[WriteOrderTableRev[i] * 4 + channel] = (byte)(val / 8);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint Color(int r, int g, int b, int a)
		{
			return unchecked((uint)(r << 16 | g << 8 | b | a << 24));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int Clamp255(int n)
		{
			return n < 0 ? 0 : n > 255 ? 255 : n;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int Clamp(int n, int min, int max)
		{
			return n < min ? min : n > max ? max : n;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint ApplicateColor(byte[,] c, int o, int m)
		{
			return Color(Clamp255(c[o, 0] + m), Clamp255(c[o, 1] + m), Clamp255(c[o, 2] + m), 255);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint ApplicateColorRaw(byte[,] c, int o)
		{
			return Color(c[o, 0], c[o, 1], c[o, 2], 255);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong Get6SwapedBytes(byte[] data, int offset)
		{
			return data[offset + 7] | (uint)data[offset + 6] << 8 |
					(uint)data[offset + 5] << 16 | (uint)data[offset + 4] << 24 |
					(ulong)data[offset + 3] << 32 | (ulong)data[offset + 2] << 40;
		}

		private static readonly byte[] WriteOrderTable = { 0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15 };
		private static readonly byte[] WriteOrderTableRev = { 15, 11, 7, 3, 14, 10, 6, 2, 13, 9, 5, 1, 12, 8, 4, 0 };
		private static readonly int[,] Etc1ModifierTable =
		{
			{ 2, 8, -2, -8, },
			{ 5, 17, -5, -17, },
			{ 9, 29, -9, -29,},
			{ 13, 42, -13, -42, },
			{ 18, 60, -18, -60, },
			{ 24, 80, -24, -80, },
			{ 33, 106, -33, -106, },
			{ 47, 183, -47, -183, }
		};
		private static readonly int[,] PunchthroughModifierTable =
		{
			{ 0, 8, 0, -8, },
			{ 0, 17, 0, -17, },
			{ 0, 29, 0, -29, },
			{ 0, 42, 0, -42, },
			{ 0, 60, 0, -60, },
			{ 0, 80, 0, -80, },
			{ 0, 106, 0, -106, },
			{ 0, 183, 0, -183, }
		};
		private static readonly byte[,] Etc1SubblockTable =
		{
			{0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1},
			{0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1}
		};
		private static readonly byte[] Etc2DistanceTable = { 3, 6, 11, 16, 23, 32, 41, 64 };
		private static readonly sbyte[,] Etc2AlphaModTable =
		{
			{-3, -6,  -9, -15, 2, 5, 8, 14},
			{-3, -7, -10, -13, 2, 6, 9, 12},
			{-2, -5,  -8, -13, 1, 4, 7, 12},
			{-2, -4,  -6, -13, 1, 3, 5, 12},
			{-3, -6,  -8, -12, 2, 5, 7, 11},
			{-3, -7,  -9, -11, 2, 6, 8, 10},
			{-4, -7,  -8, -11, 3, 6, 7, 10},
			{-3, -5,  -8, -11, 2, 4, 7, 10},
			{-2, -6,  -8, -10, 1, 5, 7,  9},
			{-2, -5,  -8, -10, 1, 4, 7,  9},
			{-2, -4,  -8, -10, 1, 3, 7,  9},
			{-2, -5,  -7, -10, 1, 4, 6,  9},
			{-3, -4,  -7, -10, 2, 3, 6,  9},
			{-1, -2,  -3, -10, 0, 1, 2,  9},
			{-4, -6,  -8,  -9, 3, 5, 7,  8},
			{-3, -5,  -7,  -9, 2, 4, 6,  8}
		};
		private static readonly uint[] PunchthroughMaskTable = { 0xFFFFFFFF, 0xFFFFFFFF, 0x00000000, 0xFFFFFFFF };

		private readonly uint[] m_buf = new uint[16];
		private readonly byte[] m_buf8 = new byte[16 * 4];
		private byte[] m_code = new byte[2];
		byte[,] m_c = new byte[3, 3];
	}
}
