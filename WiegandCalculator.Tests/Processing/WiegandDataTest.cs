using NUnit.Framework;
using WiegandCalculator.Processing;

namespace WiegandCalculator.Tests.Processing;

public class WiegandDataTest
{
    [Test]
    public void InitializeWithCardData()
    {
        // Arrange
        string cardBitData = "01011010000000010110001010";
        
        // Act
        var wiegandData = new WiegandData(cardBitData);
        
        // Assert
        Assert.AreEqual(cardBitData, wiegandData.CardBitData);
        Assert.AreEqual(26, wiegandData.Size);
    }

    [Test]
    public void GetCardNumber()
    {
        // Arrange
        string cardBitData = "01011010000000010110001010";
        var wiegandData = new WiegandData(cardBitData);
        
        // Act
        ulong cardNumber = wiegandData.ExtractValueFromCardData(9, 16);
        
        // Assert
        Assert.AreEqual(709, cardNumber);
    }
    
    [Test]
    public void ExtractOsdpData()
    {
        // Arrange
        string cardBitData = "01011010000000010110001010";
        var wiegandData = new WiegandData(cardBitData);
        
        // Act
        var osdpByteData = wiegandData.ExtractOsdpByteData();
        
        // Assert
        Assert.AreEqual(new byte[]{0x5A, 0x01, 0x62, 0x80}, osdpByteData);
    }
    
    //5A-01-62-80
}