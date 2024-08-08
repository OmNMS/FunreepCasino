using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//public class PlayerBalanceData
//{
//    public int balance { get; set; }
//}

#region JokerBonus Data Classes
public class JokerBonusBetsPlacedData
{
    public int status { get; set; }
    public string message { get; set; }
    public CardsData data { get; set; }
}

public class JokerBonusDoubleUpData
{
    public string status { get; set; }
    public string message { get; set; }
    public DoubleUpCardData data { get; set; }
}

public class TakeAmountData
{
    public int status { get; set; }
    public string message { get; set; }
}

public class CardsData
{
    public List<int[]> cards { get; set; }
}

public class DoubleUpCardData
{
    public int[] doubleUp_card { get; set; }
}

#endregion

#region Bingo Data Classes

// public class BingoBetsPlacedData
// {
//     public int status { get; set; }
//     public string message { get; set; }
//     public BingoData data { get; set; }
// }

// public class BingoData
// {
//     public int winAmount { get; set; }
// }

#endregion

