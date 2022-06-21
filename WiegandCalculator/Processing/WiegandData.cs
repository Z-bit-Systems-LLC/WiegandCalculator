using System.Collections;

namespace WiegandCalculator.Processing;

public class WiegandData
{
    public WiegandData(string cardBitData)
    {
        CardBitData = cardBitData;
        Size = cardBitData.Length;
    }
    
    public string CardBitData { get; }
    
    public int Size { get; }
    
    public ulong ExtractValueFromCardData(int start, int length)
    {
        var bitArray = new BitArray(length);
        byte bitIndex = (byte)(length - 1);
        for (var index = start; index < CardBitData.Length && index < start + length; index++)
        {
            bitArray[bitIndex--] = CardBitData[index] == '1';
        }
        
        var array = new byte[8];
        bitArray.CopyTo(array, 0);
        return BitConverter.ToUInt64(array, 0);
    }

    public byte[] ExtractOsdpByteData()
    {
        const byte bitLength = 8;
        var array = new List<byte>();
        int start = 0;

        while (start < Size)
        {
            var bitArray = new BitArray(bitLength);
            byte bitIndex = bitLength - 1;
            
            for (var index = start; index < CardBitData.Length && index < start + bitLength; index++)
            {
                bitArray[bitIndex--] = CardBitData[index] == '1';
            }
            
            byte[] bytes = new byte[1];
            bitArray.CopyTo(bytes, 0);
            array.Add(bytes[0]);
            start += bitLength;
        }

        return array.ToArray();
    }
}
